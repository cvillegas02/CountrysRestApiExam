using CountrysRestApi.Services;
using CountrysRestApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ModelsCountrysRestApi.Request;
using ModelsCountrysRestApi.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CountrysRestApi.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public ActionResult<BaseResponse<ResponseLogin>> Login(RequestLogin requestLogin)
        {
            return Services.ServiceDataBase.Instance.UserLogin(requestLogin, _configuration["SecretKey"]);  
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public ActionResult<BaseResponse<ResponseRegister>> Register(RequestRegister requestRegister)
        {
           return ServiceDataBase.Instance.UserRegistre(requestRegister);
        }


      
    }
}
