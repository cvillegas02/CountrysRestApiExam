using CountrysRestApi.DB;
using CountrysRestApi.Utils;
using Microsoft.IdentityModel.Tokens;
using ModelsCountrysRestApi.ModelsDB;
using ModelsCountrysRestApi.Request;
using ModelsCountrysRestApi.Response;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CountrysRestApi.Services
{
    public class ServiceDataBase
    {
        private readonly static ServiceDataBase _instance = new ServiceDataBase();

        private ServiceDataBase()
        {
        }

        public static ServiceDataBase Instance
        {
            get
            {
                return _instance;
            }
        }

        public BaseResponse<ResponseRegister> UserRegistre(RequestRegister pUser)
        {
            var responseRegister = new BaseResponse<ResponseRegister>();

            using (var db = new DataBase())
            {
                if (!db.User.Any(x => x.Email == pUser.Email))
                {
                    var email = pUser.Email;
                    var salt = CryptoUtil.GenerateSalt();
                    var password = pUser.Password;
                    var hashedPassword = CryptoUtil.HashMultiple(password, salt);
                    var user = new User();
                    user.Email = email;
                    user.Salt = salt;
                    user.Password = hashedPassword;
                    user.Role = "Admin";

                    db.User.Add(user);
                    db.SaveChanges();
                    responseRegister.status = true;
                }
                else
                {
                    responseRegister.message = "Email is already in use";
                }
            }

            return responseRegister;

        }

        public BaseResponse<ResponseLogin> UserLogin(RequestLogin requestLogin, string pKey)
        {
            try
            {
                var responseLogin = new BaseResponse<ResponseLogin>();

                using (var db = new DataBase())
                {
                    var existingUser = db.User.SingleOrDefault(x => (x.Email == requestLogin.Email && (x.Role.Equals("USER") || x.Role.Equals("APP") || x.Role.Equals("ADMIN"))));
                    if (existingUser != null)
                    {
                        var isPasswordVerified = CryptoUtil.VerifyPassword(requestLogin.Password, existingUser.Salt, existingUser.Password);
                        if (isPasswordVerified)
                        {
                            var claimList = new List<Claim>();
                            claimList.Add(new Claim(ClaimTypes.Name, existingUser.Email));
                            claimList.Add(new Claim(ClaimTypes.Role, existingUser.Role));
                            claimList.Add(new Claim(ClaimTypes.NameIdentifier, existingUser.Id.ToString()));

                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(pKey));
                            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var expireDate = DateTime.UtcNow.AddDays(1);
                            var timeStamp = DateUtil.ConvertToTimeStamp(expireDate);
                            var token = new JwtSecurityToken(
                                claims: claimList,
                                notBefore: DateTime.UtcNow,
                                expires: expireDate,
                                signingCredentials: creds);
                            responseLogin.status = true;

                            responseLogin.data = new ResponseLogin();
                            responseLogin.data.Token = new JwtSecurityTokenHandler().WriteToken(token);
                            responseLogin.data.ExpireDate = timeStamp;
                            responseLogin.message = "Inicio de sesión correcto";

                        }
                        else
                        {
                            responseLogin.status = false;
                            responseLogin.message = "Password is wrong";
                        }
                    }
                    else
                    {
                        responseLogin.status = false;
                        responseLogin.message = "Email is wrong";
                    }
                }

                responseLogin.code = 0;
                return responseLogin;

            }
            catch (Exception ex)
            {
                var resultExeption = new BaseResponse<ResponseLogin>();
                resultExeption.status = false;
                resultExeption.code = 100;
                resultExeption.technicaldetail = ex.StackTrace;
                resultExeption.message = "Ocurrio un error en el sistema.";
               
                return resultExeption;

            }
        }
    }
}
