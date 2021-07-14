using AppCountrys.Helpers;
using AppCountrys.Models.ModelsDB;
using AppCountrys.Models.Request;
using AppCountrys.Models.Response;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace AppCountrys.Services
{
    public class ServiceRestApi
    {

        public string Token { get; set; }

        static ServiceRestApi defaultInstance = new ServiceRestApi();

        public static ServiceRestApi DefaultServicio
        {
            get
            {
                return defaultInstance;
            }
            private set
            {
                defaultInstance = value;
            }
        }

        HttpClient client;

        public ServiceRestApi()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.Timeout = TimeSpan.FromMinutes(1);
        }

        public async Task<BaseResponse<ResponseLogin>> ValidaUsuarioContrasena(RequestLogin pUser)
        {
            BaseResponse<ResponseLogin> result = null;
            var uri = new Uri(string.Format(Configuration.URL_LOGIN));
            var json = JsonConvert.SerializeObject(pUser);
            HttpContent httpcontent = new StringContent(json, Encoding.UTF8, "application/json");

            try
                {
                    var response = await client.PostAsync(uri, httpcontent);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        
                        result = JsonConvert.DeserializeObject<BaseResponse<ResponseLogin>>(content);
                        return result;
                    }
                    else
                    {
                        result = new BaseResponse<ResponseLogin>();
                        result.code = -1;
                        result.status = false;
                        result.message = "Ocurrio un error en el sistema.";
                    }
                    
                }
                catch (Exception ex)
                {
                    result = new BaseResponse<ResponseLogin>();
                    result.code = -1;
                    result.status = false;
                    result.message = "Ocurrio un error en el sistema.";
                    result.technicaldetail = ex.InnerException.Message;

                }
            

            return result;
        }

        public async Task<BaseResponse<List<Country>>> GetCountrys()
        {
            BaseResponse<List<Country>> result = null;
            HttpResponseMessage response = null;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", this.Token);

            try
            {
                var uri = new Uri(string.Format(Configuration.URL_COUNTRYS));

                response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    //obtenemos la Información encriptada
                    var contentRes = await response.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<BaseResponse<List<Country>>>(contentRes);

                    return result;

                }
                else
                {
                    return new BaseResponse<List<Country>>() { status = false, code = -1, data = null, message = "Ocurrio un error al consultar." };

                }
            }
            catch (Exception ex)
            {
                return new BaseResponse<List<Country>>() { status = false, code = -1, data = null, message = ex.Message, technicaldetail = ex.InnerException.Message};

               
            }

        }

    }
}
