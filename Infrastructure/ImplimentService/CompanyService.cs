using Application.Dto.Keys;
using Application.Dto.ModelDto;
using Application.Dto.Request;
using Application.Dto.ResponsiveDto;
using Application.IRespostory;
using Application.IRespostory.IAnalysis;
using Application.IRespostory.IAuthen;
using Application.IRespostory.IInfo;
using Application.IService;
using AutoMapper;
using CrawlService.Controller;
using DabacoControl.api;
using DabacoControl.Builder;
using DabacoControl.model;
using Domain.Entities;
using Domain.Entities.Authen;
using Domain.Entities.EntityAnalysis;
using Infrastructure.ExceptionInfastructure;
using Infrastructure.ImplimentService.Base;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRequest.Dto.Request;

namespace Infrastructure.ImplimentService
{
    public class CompanyService : BaseService, ICompanyService
    {


        private IApiBaseController _apiBaseController;

        private ICompanyRespostory _companyRespostory;
        // Respostory cho lưu infor
        private IEmailRespostory _emailRespostory;
        private IPhoneNumberRespostory _phoneRespostory;
        private IFaxRespostory _faxRespostory;
        private IPostalCodeRespostory _postalCodeRespostory;
        private ICityRespostory _cityRespostory;
        private IRequestSearchHistoryRespostory _requestSearchHistoryRespostory;
        private IAuthenTradesparqRespostory _authenTradesparqRespostory;
        private IRequestService _requestService;
        private ILogger<ICompanyService> _logger;
        private IMapper _mapper;

        public CompanyService(IApiBaseController apiBaseController, ICompanyRespostory companyRespostory, IEmailRespostory emailRespostory, IPhoneNumberRespostory phoneRespostory, IFaxRespostory faxRespostory, IPostalCodeRespostory postalCodeRespostory, ICityRespostory cityRespostory, IRequestSearchHistoryRespostory requestSearchHistoryRespostory, IAuthenTradesparqRespostory authenTradesparqRespostory, IRequestService requestService, ILogger<ICompanyService> logger, IMapper mapper)
        {
            _apiBaseController = apiBaseController;
            _companyRespostory = companyRespostory;
            _emailRespostory = emailRespostory;
            _phoneRespostory = phoneRespostory;
            _faxRespostory = faxRespostory;
            _postalCodeRespostory = postalCodeRespostory;
            _cityRespostory = cityRespostory;
            _requestSearchHistoryRespostory = requestSearchHistoryRespostory;
            _authenTradesparqRespostory = authenTradesparqRespostory;
            _requestService = requestService;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<CompanyAnalysisDto> SaveAllCompany(SearchRequestDto.Root searchRequestDto)
        {
            try
            {
                CompanyAnalysisDto companyAnalysis = new CompanyAnalysisDto();
                AuthenTradesparq authen = await _authenTradesparqRespostory.GetTokenActive();
                if (authen == null || string.IsNullOrEmpty(authen.Token))
                {
                    _logger.LogError($"GetAllCompany - Error: Token không có vui lòng kiểm tra lại");
                    throw new RequestErrorException($"GetAllCompany - Error: Token không có vui lòng kiểm tra lại");
                }
                _requestService.Token = authen.Token;
                _requestService.DataSource = authen.dataSourch;

                SearchCompanyResponsivDto.Root root = await _requestService.GetCompany(searchRequestDto);
                if(root == null || root.data == null)
                {
                    await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                    {
                        Keyword = searchRequestDto.prod_desc,
                        ExDataSearch = null,
                        IsDeleted = false,
                        IsSuccess = false,
                        StatusCode = root.code,
                        ResultCount = -1,
                        SearchDate = DateTime.Now,
                        TypeSearch = "Company", // 1: Company
                        KeySearch = searchRequestDto.prod_desc,
                    });
                    throw new RequestErrorException("Lỗi gửi request tới Tradesparq");
                }

                companyAnalysis.total = root.data.numBuckets;
                companyAnalysis.count = root.data.buckets.Count;

                bool isSaveHistory = await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                {
                   Keyword = searchRequestDto.prod_desc,
                   ExDataSearch = null,
                   IsDeleted = false,
                   IsSuccess = true,
                   StatusCode = StatusNumberKey.Success,
                   ResultCount = root.data.buckets.Count,
                   SearchDate = DateTime.Now,
                   TypeSearch = "Company", // 1: Company
                   KeySearch = searchRequestDto.prod_desc,
                });
                if(isSaveHistory == false)
                {
                    _logger.LogError($"GetAllCompany - Error: Lưu lịch sử tìm kiếm không thành công");
                }
                if (root == null || root.code != StatusNumberKey.Success)
                {
                    _logger.LogError($"GetAllCompany - Error: Status: {root.code} Message: {root.data}");
                    throw new RequestErrorException($"GetAllCompany - Error: Status: {root.code} Message: {root.data}");
                }
                // Sau khi request thành công sẽ reset biến đếm lỗi về 0 
                foreach (var item in root.data.buckets)
                {
                    bool exits = await _companyRespostory.Exits(item.info.uuid);
                    if (exits)
                    {
                        _logger.LogInformation($"GetAllCompany - Info: Company Uuid: {item.info.uuid} exits in database");
                        continue;
                    }
                    Company company = _mapper.Map<Company>(item);
                    await _companyRespostory.Create(company);
                    companyAnalysis.companyId = company.Id;
                    if (item.info?.city != null)
                    {
                        int success = await _cityRespostory.CreateAll(company.Id, item.info.city);
                        if(success != item.info.city.Count)
                        {
                            _logger.LogError($"GetAllCompany - Error: Company Uuid: {item.info.uuid} - City: Inserted {success} of {item.info.city.Count}");
                        }
                    }
                    if(item.info?.email != null)
                    {
                        int success = await _emailRespostory.CreateAll(company.Id, item.info.email);
                        if(success != item.info.email.Count)
                        {
                            _logger.LogError($"GetAllCompany - Error: Company Uuid: {item.info.uuid} - Email: Inserted {success} of {item.info.email.Count}");
                        }
                    }
                    if(item.info?.fax != null)
                    {
                        int success = await _faxRespostory.CreateAll(company.Id, item.info.fax);
                        if(success != item.info.fax.Count)
                        {
                            _logger.LogError($"GetAllCompany - Error: Company Uuid: {item.info.uuid} - Fax: Inserted {success} of {item.info.fax.Count}");
                        }
                    }
                    if(item.info?.phone != null)
                    {
                        int success = await _phoneRespostory.CreateAll(company.Id, item.info.phone);
                        if(success != item.info.phone.Count)
                        {
                            _logger.LogError($"GetAllCompany - Error: Company Uuid: {item.info.uuid} - Phone: Inserted {success} of {item.info.phone.Count}");
                        }
                    }
                    if(item.info?.postal_code != null)
                    {
                        int success = await _postalCodeRespostory.CreateAll(company.Id, item.info.postal_code);
                        if(success != item.info.postal_code.Count)
                        {
                            _logger.LogError($"GetAllCompany - Error: Company Uuid: {item.info.uuid} - PostalCode: Inserted {success} of {item.info.postal_code.Count}");
                        }
                    }
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
