using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AppCountrys.Services
{
    /// <summary>
    /// Interface para servicios
    /// </summary>
    public interface IRestClient
    {
        Task<T> GetAsync<T>(string url, bool useAuthToken = false);
        Task<T> PostAsync<T>(string url, object payload, bool useAuthToken = false);
        Task<T> PutAsync<T>(string url, object payload, bool useAuthToken = false);
        Task<T> DeleteAsync<T>(string url, bool useAuthToken = false);

        string Token { get; set; }
    }
}
