using Application.Dto.Keys;
using Application.Dto.ModelDto;
using Application.Dto.Request;
using Application.Dto.RequestDto;
using Application.IJob;
using Application.IRespostory.IAnalysis;
using Application.IRespostory.ICommand;
using Application.IService;
using Domain.Entities.command;
using Domain.Entities.EntityAnalysis;
using Infrastructure.Helper;
using Infrastructure.ImplimentService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        private ICommandRespostory _commandRespostory;
        private IRequestSearchHistoryRespostory _requestSearchHistoryRespostory;
        public AutoCrawlJob(ILogger<AutoCrawlJob> log, ICompanyService companyService, IShippentService shippentService, ICommandRespostory commandRespostory, IRequestSearchHistoryRespostory requestSearchHistoryRespostory) 
        { 
            this._log = log;
            this._companyService = companyService;
            this._shippentService = shippentService;
            this._commandRespostory = commandRespostory;
            this._requestSearchHistoryRespostory = requestSearchHistoryRespostory;
        }
        public async Task ExecuteAsync(CancellationToken ct)
        {
            _log.LogInformation("Tải tài nguyên Shipment {time:O}", DateTime.UtcNow);
             await LoadCommand(ct);
        }

        public async Task LoadCommand(CancellationToken ct)
        {
            try
            {
                ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"MyJob start COMPANY at ");

                Random randomInt = new Random();

                ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"Get all COMMAND active");
                List<Command> commands = await _commandRespostory.GetAllCommandActive();
                if(commands == null || commands.Count == 0)
                {
                    ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"No found command active");
                    return;
                }
                foreach (var command in commands)
                {
                    try
                    {
                        if(command.TypeCommand.Equals(TypeCommand.SEARCH_COMPANY_DETAIL))
                        {
                            bool isSuccess = await RunJobSearchCompanyDetail(command, ct);
                        }
                        else
                        {
                            await RunJobSearchCompanyOrShipmment(command, ct);
                        }
                       await _commandRespostory.CloseCommand(command.Id);
                    }
                    catch (Exception ex)
                    {
                        ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Error call request and save: Pause 5s");
                        ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"{ex.Message}");
                        await Task.Delay(5000);
                    }
                }
            }
            catch (Exception ex)
            {
                ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Error no found");
                ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"{ex.Message}");
                throw;
            }
        }
        public async Task<bool> RunJobSearchCompanyDetail(Command command, CancellationToken ct)
        {
            int page = 1;
            while (true)
            {
                ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"Start get company detail in {ConsoleLog.RED_REPLATE}{page}{ConsoleLog.NORMAL_REPLATE}");
                try
                {
                    Command command1 = await _commandRespostory.Get(command.Id);
                    if (command1.IsCompleted || command1.IsDeleted)
                    {
                        throw new Exception("Lệnh này đã được chạy xong! chờ đến lệnh tiếp theo");
                    }
                    // TODO: gọi repo/service thật của bạn ở đây
                    CompanySearchRequestDto requestRoot = new CompanySearchRequestDto();
                    requestRoot.dataSource = "00111100001111011111111111111111111111001111110110011111110111111011111111111111111111111111111101111011001111111111011111111111111000011111111111111111100001110000000000000000000000000000";
                    requestRoot.date = new List<string>();
                    requestRoot.date.Add(command.StartDate.ToString("yyyy-MM-dd"));
                    requestRoot.date.Add(command.EndDate.ToString("yyyy-MM-dd"));
                    requestRoot.order = "desc";
                    requestRoot.page = page;
                    requestRoot.page_size = 20;
                    requestRoot.com_role = command.TypeSearch;
                    requestRoot.result_type = SearchProdDesc.Shippent;
                    requestRoot.com_id = new List<string>();
                    if(string.IsNullOrEmpty(command.ComId))
                    {
                        ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Com Id không tồn tại! Cập nhật Com_id");
                        continue;
                    }
                    requestRoot.com_id.Add(command.ComId);

                    requestRoot.source_type = 1;

                    AnalysisDto analysisDto = null;
                    if (command.TypeCommand.Equals(TypeCommand.SEARCH_COMPANY_DETAIL))
                    {
                        analysisDto = await _shippentService.SaveShipmentOfCompany(requestRoot);
                    }

                    if (analysisDto == null)
                    {
                        ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Error call request and save: {ConsoleLog.RED_REPLATE}{analysisDto.total * page}{ConsoleLog.NORMAL_REPLATE}");
                    }
                    if (page * 20 >= analysisDto.total)
                    {
                        ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"Save data count: {ConsoleLog.RED_REPLATE}{analysisDto.total * page}{ConsoleLog.NORMAL_REPLATE}");
                        ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"MyJob end at {ConsoleLog.YELLOW}{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}{ConsoleLog.NORMAL_REPLATE}");

                        _log.LogInformation("MyJob end at {time:O}", DateTime.UtcNow);
                        return true;
                    }
                    ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"Save data: {ConsoleLog.RED_REPLATE}{analysisDto.count * page}{ConsoleLog.NORMAL_REPLATE}");
                    await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                    {
                        PageNumber = page,
                        CommandId = command.Id,
                        ExDataSearch = $"Save data: {ConsoleLog.RED_REPLATE}{analysisDto.count * page}{ConsoleLog.NORMAL_REPLATE}",
                        IsDeleted = false,
                        IsSuccess = true,
                        StatusCode = StatusNumberKey.Success
                    });
                    page++;
                }
                catch (Exception ex)
                {
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Error call request and save: Pause 5s");
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"{ex.Message}");

                    await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                    {
                        PageNumber = page,
                        CommandId = command.Id,
                        ExDataSearch = ex.Message,
                        IsDeleted = false,
                        IsSuccess = false,
                        StatusCode = StatusNumberKey.Success,
                    });
                    await Task.Delay(5000);
                    return false;
                }
            }
        }
        public async Task<bool> RunJobSearchCompanyOrShipmment(Command command,CancellationToken ct)
        {
            int page = 1;
            while (true)
            {
                ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"Start get company in {ConsoleLog.RED_REPLATE}{page}{ConsoleLog.NORMAL_REPLATE}");
                try
                {
                    Command command1 = await _commandRespostory.Get(command.Id);
                    if (command1.IsCompleted || command1.IsDeleted)
                    {
                        throw new Exception("Lệnh này đã được chạy xong! chờ đến lệnh tiếp theo");
                    }
                    // TODO: gọi repo/service thật của bạn ở đây
                    SearchRequestDto.Root requestRoot = new SearchRequestDto.Root();
                    requestRoot.filter_field = null;
                    requestRoot.dataSource = "00111100001111011111111111111111111111001111110110011111110111111011111111111111111111111111111101111011001111111111011111111111111000011111111111111111100001110000000000000000000000000000";
                    requestRoot.date = new List<string>();
                    requestRoot.date.Add(command.StartDate.ToString("yyyy-MM-dd"));
                    requestRoot.date.Add(command.EndDate.ToString("yyyy-MM-dd"));
                    requestRoot.order = "desc";
                    requestRoot.page = page;
                    requestRoot.page_size = 20;
                    requestRoot.prod_desc = command.SearchKey;
                    requestRoot.result_type = command.TypeSearch;
                    if (command.TypeCommand.Equals(TypeCommand.SEARCH_COMPANY))
                        requestRoot.result_type_need_num = true;
                    requestRoot.source_type = 1;

                    AnalysisDto analysisDto = null;
                    if (command.TypeCommand.Equals(TypeCommand.SEARCH_COMPANY))
                    {
                        analysisDto = await _companyService.SaveAllCompany(requestRoot);
                    }
                    if (command.TypeCommand.Equals(TypeCommand.SEARCH_SHIPMENT))
                    {
                        analysisDto = await _shippentService.SaveAllShipment(requestRoot);
                    }

                    if (analysisDto == null)
                    {
                        ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Error call request and save: {ConsoleLog.RED_REPLATE}{analysisDto.total * page}{ConsoleLog.NORMAL_REPLATE}");
                    }
                    if (page * 20 >= analysisDto.total)
                    {
                        ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"Save data count: {ConsoleLog.RED_REPLATE}{analysisDto.total * page}{ConsoleLog.NORMAL_REPLATE}");
                        ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"MyJob end at {ConsoleLog.YELLOW}{DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")}{ConsoleLog.NORMAL_REPLATE}");

                        _log.LogInformation("MyJob end at {time:O}", DateTime.UtcNow);
                        return true;
                    }
                    ConsoleLog.Log(ConsoleLog.INFO, MethodBase.GetCurrentMethod().Name, $"Save data: {ConsoleLog.RED_REPLATE}{analysisDto.count * page}{ConsoleLog.NORMAL_REPLATE}");
                    await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                    {
                        PageNumber = page,
                        CommandId = command.Id,
                        ExDataSearch = $"Save data: {ConsoleLog.RED_REPLATE}{analysisDto.count * page}{ConsoleLog.NORMAL_REPLATE}",
                        IsDeleted = false,
                        IsSuccess = true,
                        StatusCode = StatusNumberKey.Success
                    });
                    page++;
                }
                catch (Exception ex)
                {
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"Error call request and save: Pause 5s");
                    ConsoleLog.Log(ConsoleLog.ERROR, MethodBase.GetCurrentMethod().Name, $"{ex.Message}");

                    await _requestSearchHistoryRespostory.Create(new RequestSearchHisory
                    {
                        PageNumber = page,
                        CommandId = command.Id,
                        ExDataSearch = ex.Message,
                        IsDeleted = false,
                        IsSuccess = false,
                        StatusCode = StatusNumberKey.Success,
                    });
                    await Task.Delay(5000);
                    return false;
                }
            }
            
        }
    }
}
