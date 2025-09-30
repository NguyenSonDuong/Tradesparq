using Application.Dto.Keys;
using Application.Dto.ModelDto;
using Application.Dto.Request;
using Application.Dto.ResponsiveDto;
using Application.IRespostory;
using Application.IRespostory.IAnalysis;
using Application.IRespostory.IAuthen;
using Application.IRespostory.IInfo;
using Application.IService;
using Application.Respostory;
using AutoMapper;
using CrawlService.Controller;
using CrawlService.Dto.Responsive;
using Domain.Entities;
using Domain.Entities.Authen;
using Domain.Entities.EntityAnalysis;
using Infrastructure.ExceptionInfastructure;
using Infrastructure.ImplimentRespostory;
using Infrastructure.ImplimentRespostory.Info;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplimentService
{
    public class ShippentService : IShippentService
    {
        private IApiBaseController _apiBaseController;

        private IRequestSearchHistoryRespostory _requestSearchHistoryRespostory;
        private IAuthenTradesparqRespostory _authenTradesparqRespostory;
        private IRequestService _requestService;
        private IShippentRespostory _shippentRespostory;
        private ILogger<ICompanyService> _logger;
        private IMapper _mapper;

        public ShippentService(IApiBaseController apiBaseController, IRequestSearchHistoryRespostory requestSearchHistoryRespostory, IAuthenTradesparqRespostory authenTradesparqRespostory, IRequestService requestService, IShippentRespostory shippentRespostory, ILogger<ICompanyService> logger, IMapper mapper)
        {
            _apiBaseController = apiBaseController;
            _requestSearchHistoryRespostory = requestSearchHistoryRespostory;
            _authenTradesparqRespostory = authenTradesparqRespostory;
            _requestService = requestService;
            _shippentRespostory = shippentRespostory;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<ShipmentAnalysisDto> SaveAllShipment(SearchRequestDto.Root searchRequestDto)
        {
            try
            {
                ShipmentAnalysisDto companyAnalysis = new ShipmentAnalysisDto();
                AuthenTradesparq authen = await _authenTradesparqRespostory.GetTokenActive();
                if (authen == null || string.IsNullOrEmpty(authen.Token))
                {
                    _logger.LogError($"SaveAllShipment - Error: Token không có vui lòng kiểm tra lại");
                    throw new RequestErrorException($"SaveAllShipment - Error: Token không có vui lòng kiểm tra lại");
                }
                _requestService.Token = authen.Token;
                _requestService.DataSource = authen.dataSourch;

                SearchResponsiveDto.Root root = await _requestService.GetShipment(searchRequestDto);
                if (root == null || root.data == null)
                {
                    await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                    {
                        Keyword = searchRequestDto.prod_desc,
                        ExDataSearch = null,
                        IsDeleted = false,
                        IsSuccess = false,
                        StatusCode = StatusNumberKey.Success,
                        ResultCount = -1,
                        SearchDate = DateTime.Now,
                        TypeSearch = "Shipment", // 1: Company
                        KeySearch = searchRequestDto.prod_desc,
                    });
                    throw new RequestErrorException("SaveAllShipment - Error: Lỗi gửi request tới Tradesparq");
                }

                companyAnalysis.total = root.data.numFound;
                companyAnalysis.count = root.data.docs.Count;

                bool isSaveHistory = await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                {
                    Keyword = searchRequestDto.prod_desc,
                    ExDataSearch = JsonConvert.SerializeObject(searchRequestDto),
                    IsDeleted = false,
                    IsSuccess = true,
                    StatusCode = StatusNumberKey.Success,
                    ResultCount = root.data.docs.Count,
                    SearchDate = DateTime.Now,
                    TypeSearch = "Shipment", // 1: Company
                    KeySearch = searchRequestDto.prod_desc,
                });
                if (isSaveHistory == false)
                {
                    _logger.LogError($"SaveAllShipment - Error: Lưu lịch sử tìm kiếm không thành công");
                }
                if (root == null || root.code != StatusNumberKey.Success)
                {
                    _logger.LogError($"SaveAllShipment - Error: Status: {root.code} Message: {root.data}");
                    throw new RequestErrorException($"SaveAllShipment - Error: Status: {root.code} Message: {root.data}");
                }
                // Sau khi request thành công sẽ reset biến đếm lỗi về 0 
                foreach (var item in root.data.docs)
                {
                    bool exits = await _shippentRespostory.Exit(item.id);
                    if (exits)
                    {
                        _logger.LogInformation($"SaveAllShipment - Info: Company Uuid: {item.id} exits in database");
                        continue;
                    }
                    Shipment company = _mapper.Map<Shipment>(item);
                    await _shippentRespostory.Create(company);
                    companyAnalysis.shipmentId = company.Id;
                    
                }
                return companyAnalysis;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
