using Application.Dto.ModelDto;
using Application.Dto.Request;
using Application.IJob;
using Application.IService;
using Infrastructure.ImplimentService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRequest.Dto.Request;

namespace Infrastructure.ImplimentJob
{
    public class AutoCrawlJob : IAutoCrawlJob
    {
        private readonly ILogger<AutoCrawlJob> _log;
        private ICompanyService _companyService;
        private IShippentService _shippentService;
        public AutoCrawlJob(ILogger<AutoCrawlJob> log, ICompanyService companyService, IShippentService shippentService) 
        { 
            this._log = log;
            this._companyService = companyService;
            this._shippentService = shippentService;
        }
        public async Task ExecuteAsync(CancellationToken ct)
        {
            _log.LogInformation("Tải tài nguyên Shipment {time:O}", DateTime.UtcNow);
            await LoadShipment(ct);
            _log.LogInformation("Tải tài nguyên Company {time:O}", DateTime.UtcNow);
            await LoadCompany(ct);
        }

        public async Task LoadCompany(CancellationToken ct)
        {
            try
            {
                Random randomInt = new Random();
                int page = 1;
                while (true)
                {
                    try
                    {

                        _log.LogInformation("MyJob start COMPANY at {time:O}", DateTime.UtcNow);
                        // TODO: gọi repo/service thật của bạn ở đây
                        SearchRequestDto.Root requestRoot = new SearchRequestDto.Root();
                        requestRoot.filter_field = null;
                        requestRoot.dataSource = "00111100001111011111111111111111111111001111110110011111110111111011111111111111111111111111111101111011001111111111011111111111111000011111111111111111100001110000000000000000000000000000";
                        requestRoot.date = new List<string>();
                        requestRoot.date.Add("2024-09-21");
                        requestRoot.date.Add("2025-09-21");
                        requestRoot.order = "desc";
                        requestRoot.page = page;
                        requestRoot.page_size = 20;
                        requestRoot.prod_desc = "Coconut";
                        requestRoot.result_type = "supplier";
                        requestRoot.result_type_need_num = true;
                        requestRoot.source_type = 1;

                        _log.LogInformation("Lưu dữ liệu {time:O}", DateTime.UtcNow);
                        CompanyAnalysisDto companyAnalysis = await _companyService.SaveAllCompany(requestRoot);
                        if (companyAnalysis == null)
                        {
                            if (page * 20 >= companyAnalysis.total)
                            {
                                _log.LogInformation("Đã lưu hết dữ liệu tổng số: {total} {time:O}", companyAnalysis.total, DateTime.UtcNow);
                                _log.LogInformation("MyJob end at {time:O}", DateTime.UtcNow);
                                break;
                            }
                            _log.LogError("Lưu dữ liệu không thành công {time:O}", DateTime.UtcNow);
                        }
                        else
                        {
                            _log.LogInformation("Lưu dữ liệu thành công tổng số: {total} - Số bản ghi mới: {count} - Thời gian: {time:O}", companyAnalysis.total, companyAnalysis.count, DateTime.UtcNow);
                            _log.LogInformation("MyJob end at {time:O}", DateTime.UtcNow);
                        }
                        page++;
                        await Task.Delay(randomInt.Next(3000, 6000));
                    }
                    catch (Exception ex)
                    {
                        _log.LogError("Bị lỗi chờ 5s sau đó chạy lai...", DateTime.UtcNow);
                        _log.LogError(ex.Message);
                        await Task.Delay(5000);
                    }
                }
            }
            catch (Exception ex)
            {

                _log.LogError($"AutoCrawlJob - Error: {ex.Message}");
                throw;
            }
        }
        public async Task LoadShipment(CancellationToken ct)
        {
            try
            {
                Random randomInt = new Random();
                int page = 1;
                while (true)
                {
                    try
                    {

                        _log.LogInformation("MyJob start SHIPMENT at {time:O}", DateTime.UtcNow);
                        // TODO: gọi repo/service thật của bạn ở đây
                        SearchRequestDto.Root requestRoot = new SearchRequestDto.Root();
                        requestRoot.filter_field = null;
                        requestRoot.dataSource = "00111100001111011111111111111111111111001111110110011111110111111011111111111111111111111111111101111011001111111111011111111111111000011111111111111111100001110000000000000000000000000000";
                        requestRoot.date = new List<string>();
                        requestRoot.date.Add("2024-09-21");
                        requestRoot.date.Add("2025-09-21");
                        requestRoot.order = "desc";
                        requestRoot.page = page;
                        requestRoot.page_size = 20;
                        requestRoot.prod_desc = "Coconut";
                        requestRoot.result_type = "record";
                        requestRoot.source_type = 1;

                        _log.LogInformation("Lưu dữ liệu {time:O}", DateTime.UtcNow);
                        ShipmentAnalysisDto companyAnalysis = await _shippentService.SaveAllShipment(requestRoot);
                        if (companyAnalysis == null)
                        {
                            if (page * 20 >= companyAnalysis.total)
                            {
                                _log.LogInformation("Đã lưu hết dữ liệu tổng số: {total} {time:O}", companyAnalysis.total, DateTime.UtcNow);
                                _log.LogInformation("MyJob end at {time:O}", DateTime.UtcNow);
                                break;
                            }
                            _log.LogInformation("Lưu dữ liệu không thành công {time:O}", DateTime.UtcNow);
                        }
                        else
                        {
                            _log.LogInformation("Lưu dữ liệu thành công tổng số: {total} - Số bản ghi mới: {count} - Thời gian: {time:O}", companyAnalysis.total, companyAnalysis.count, DateTime.UtcNow);
                            _log.LogInformation("MyJob end at {time:O}", DateTime.UtcNow);
                        }
                        page++;
                        await Task.Delay(randomInt.Next(3000, 6000));
                    }
                    catch (Exception ex)
                    {
                        _log.LogError("Bị lỗi chờ 5s sau đó chạy lai...", DateTime.UtcNow);
                        _log.LogError(ex.Message);
                        await Task.Delay(5000);
                    }
                }
            }
            catch (Exception ex)
            {

                _log.LogError($"AutoCrawlJob - Error: {ex.Message}");
                throw;
            }
        }
    }
}
