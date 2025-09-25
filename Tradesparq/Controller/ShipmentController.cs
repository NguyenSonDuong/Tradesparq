using CrawlService.Dto.Request;
using CrawlService.Dto.Responsive;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tradesparq.Service;

namespace Tradesparq.Controller
{
    public class ShipmentController
    {
        public static string TOKEN = "0f87f81b-a00f-41ef-9bda-bfafbf91665a";
        public static string keySearch = "Dessicated Coconut";
        public static async Task<SearchResponsiveDto.Root> GetShipments(int page = 1)
        {
            RequestService requestService = new RequestService();
            requestService.SetToken(TOKEN);

            SearchRequestDto.Root requestRoot = new SearchRequestDto.Root();
            requestRoot.dataSource = "00111100001111011111111111111111111111001111110110011111110111111011111111111111111111111111111101111011001111111111011111111111111000011111111111111111100001110000000000000000000000000000";
            requestRoot.date = new List<string>();
            requestRoot.date.Add("2024-09-21");
            requestRoot.date.Add("2025-09-21");
            requestRoot.order = "desc";
            requestRoot.page = page;
            requestRoot.page_size = 20;
            requestRoot.prod_desc = keySearch;
            requestRoot.result_type = "record";
            requestRoot.sort = "date";
            requestRoot.source_type = 1;
            SearchResponsiveDto.Root root1 = await requestService.GetSearch(requestRoot);
            return root1;
        }
    }
}
