using HashtilCagesMap.Entities;
using HashtilCagesMap.HttpReq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HashtilCagesMap
{
    public class LoginHttp
    {
        string url = "";
        public static async Task<List<UserLogin>> UserLoginCheck(string username, string password)
        {
            string url = $"http://www.hashtildb.pe.hu/LoginMapTable.php?username={username}&password={password}";


            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
                {
                try
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var userLogins = await response.Content.ReadAsStringAsync();
                        if(JsonConvert.DeserializeObject<List<UserLogin>>(userLogins).Count!= 1)
                        {
                            //
                        }
                        
                        return JsonConvert.DeserializeObject<List<UserLogin>>(userLogins);

                        
                    }
                    else
                    {
                        throw new Exception(response.ReasonPhrase);
                    }
                }
                catch(Exception e)
                {
                   
                    throw new Exception();
                }
                }
           
        }
    }
}
