using Application.Dto.Keys;
using Application.Dto.ModelDto;
using Application.Dto.Request;
using Application.Dto.RequestDto;
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
using Infrastructure.Helper;
using Infrastructure.ImplimentRespostory;
using Infrastructure.ImplimentRespostory.Info;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Token không có vui lòng kiểm tra lại");
                    throw new RequestErrorException($"SaveAllShipment - Error: Token không có vui lòng kiểm tra lại");
                }
                _requestService.Token = authen.Token;
                _requestService.DataSource = authen.dataSourch;

                SearchResponsiveDto.Root root = await _requestService.GetShipment(searchRequestDto);
                if (root == null || root.data == null)
                {
                    if (root != null && (root.code == 403 || root.code == 402))
                    {
                        await _authenTradesparqRespostory.Deactive(authen.Id);
                        ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Token hết hạn vui lòng kiểm tra lại");
                        _logger.LogError($"GetAllCompany - Error: Token hết hạn vui lòng kiểm tra lại");
                        throw new RequestErrorException($"GetAllCompany - Error: Token hết hạn vui lòng kiểm tra lại");
                    }
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Lỗi gửi request tới Tradesparq");
                    throw new RequestErrorException("SaveAllShipment - Error: Lỗi gửi request tới Tradesparq");
                }

                companyAnalysis.total = root.data.numFound;
                companyAnalysis.count = root.data.docs.Count;

                if (root == null || root.code != StatusNumberKey.Success)
                {
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Status: {root.code} Message: {root.data}");
                    _logger.LogError($"SaveAllShipment - Error: Status: {root.code} Message: {root.data}");
                    throw new RequestErrorException($"SaveAllShipment - Error: Status: {root.code} Message: {root.data}");
                }
                // Sau khi request thành công sẽ reset biến đếm lỗi về 0 
                foreach (var item in root.data.docs)
                {
                    bool exits = await _shippentRespostory.Exit(item.id);
                    if (exits)
                    {
                        ConsoleLog.Log(ConsoleLog.WARNING, MethodBase.GetCurrentMethod().Name, $"Company Uuid: [YELLOW]{item.id}[YELLOW] exits in database");
                        continue;
                    }
                    Shipment company = _mapper.Map<Shipment>(item);
                    await _shippentRespostory.Create(company);
                }
                return companyAnalysis;
            }
            catch (Exception ex)
            {
                ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }

        public async Task<ShipmentAnalysisDto> SaveShipmentOfCompany(CompanySearchRequestDto companySearchRequestDto)
        {
            try
            {
                ShipmentAnalysisDto companyAnalysis = new ShipmentAnalysisDto();
                AuthenTradesparq authen = await _authenTradesparqRespostory.GetTokenActive();
                if (authen == null || string.IsNullOrEmpty(authen.Token))
                {
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Token không có vui lòng kiểm tra lại");
                    throw new RequestErrorException($"SaveShipmentOfCompany - Error: Token không có vui lòng kiểm tra lại");
                }
                _requestService.Token = authen.Token;
                _requestService.DataSource = authen.dataSourch;

                CompanySearchResposiveDto.Root root = await _requestService.GetCompanyDeatil(companySearchRequestDto);
                if (root == null || root.data == null)
                {
                    if (root != null && (root.code == 403 || root.code == 402))
                    {
                        await _authenTradesparqRespostory.Deactive(authen.Id);
                        ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Token hết hạn vui lòng kiểm tra lại");
                        _logger.LogError($"SaveShipmentOfCompany - Error: Token hết hạn vui lòng kiểm tra lại");
                        throw new RequestErrorException($"SaveShipmentOfCompany - Error: Token hết hạn vui lòng kiểm tra lại");
                    }
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Lỗi gửi request tới Tradesparq");
                    throw new RequestErrorException("SaveShipmentOfCompany - Error: Lỗi gửi request tới Tradesparq");
                }

                companyAnalysis.total = root.data.numFound;
                companyAnalysis.count = root.data.docs.Count;

                if (root == null || root.code != StatusNumberKey.Success)
                {
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"SaveShipmentOfCompany: Status: {root.code} Message: {root.data}");
                    _logger.LogError($"SaveShipmentOfCompany - Error: Status: {root.code} Message: {root.data}");
                    throw new RequestErrorException($"SaveShipmentOfCompany - Error: Status: {root.code} Message: {root.data}");
                }
                // Sau khi request thành công sẽ reset biến đếm lỗi về 0 
                foreach (var item in root.data.docs)
                {
                    bool exits = await _shippentRespostory.Exit(item.id);
                    if (exits)
                    {
                        ConsoleLog.Log(ConsoleLog.WARNING, MethodBase.GetCurrentMethod().Name, $"Company Uuid: [YELLOW]{item.id}[YELLOW] exits in database");
                        continue;
                    }
                    Shipment company = _mapper.Map<Shipment>(item);
                    await _shippentRespostory.Create(company);
                }
                return companyAnalysis;
            }
            catch (Exception ex)
            {
                ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, ex.Message);
                throw;
            }
        }
    }
}
