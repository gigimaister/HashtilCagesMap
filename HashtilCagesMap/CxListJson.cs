using HashtilCagesMap.Entities;
using HashtilCagesMap.HttpReq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HashtilCagesMap
{
    public class CxListJson
    {
        public async static Task<ObservableCollection<TotalCageCx>> FetchCxListPhp(int query_selector)
        {
            string url = $"http://hashtildb.pe.hu/cx_list_for_desktop_table.php?query_selector={query_selector}";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var CxList = await response.Content.ReadAsStringAsync();


                        return JsonConvert.DeserializeObject<ObservableCollection<TotalCageCx>>(CxList);


                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch (Exception e)
                {

                    throw new Exception();
                }
            }
        }
    }
}
