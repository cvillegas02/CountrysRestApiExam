using System;
using System.Collections.Generic;
using System.Text;

namespace AppCountrys.Models.Response
{
    /// <summary>
    /// Response Base de los Servicios Rest Api
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseResponse<T>
    {
        public bool status { get; set; }
        public int code { get; set; }
        public string message { get; set; }
        public string technicaldetail { get; set; }

        public T data { get; set; }
    }
}
