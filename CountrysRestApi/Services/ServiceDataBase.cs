using CountrysRestApi.DB;
using CountrysRestApi.Helpers;
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
                if (!db.User.Any(x => x.email == pUser.email))
                {
                    var email = pUser.email;
                    var salt = CryptoUtil.GenerateSalt();
                    var password = pUser.password;
                    var hashedPassword = CryptoUtil.HashMultiple(password, salt);
                    var user = new User();
                    user.email = email;
                    user.salt = salt;
                    user.password = hashedPassword;
                    user.role = "Admin";

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
                    var existingUser = db.User.SingleOrDefault(x => (x.email == requestLogin.email ));
                    if (existingUser != null)
                    {
                        var isPasswordVerified = CryptoUtil.VerifyPassword(requestLogin.password, existingUser.salt, existingUser.password);
                        if (isPasswordVerified)
                        {
                            var claimList = new List<Claim>();
                            claimList.Add(new Claim(ClaimTypes.Name, existingUser.email));
                            claimList.Add(new Claim(ClaimTypes.Role, existingUser.role));
                            claimList.Add(new Claim(ClaimTypes.NameIdentifier, existingUser.id.ToString()));

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
                            responseLogin.data.token = new JwtSecurityTokenHandler().WriteToken(token);
                            responseLogin.data.expire_date = timeStamp;
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

        public PagedList<Country> GetCountrysPageCount(int pPageNumber, int pPageSize)
        {

            int PageNumber = pPageNumber == 0 ? Configuration.DefaultPageNumber : pPageNumber;
            int PageSize = pPageSize == 0 ? Configuration.DefaultPageSize : pPageSize;

            using (var db = new DataBase())
            {
                try
                {

                    var posts = db.Country.AsEnumerable();

                    var pagedPosts = PagedList<Country>.Create(posts, PageNumber, PageSize);

                    return pagedPosts;


                }
                catch (Exception ex)
                {
                    //RegisterError(ex);
                    Console.WriteLine(ex.Message);
                }

                return null;

            }
        }

        public BaseResponse<List<Country>> GetCountrys()
        {
            var resultBD = new BaseResponse<List<Country>>();
            using (var db = new DataBase())
            {
                try
                {
                    var countrys = db.Country.ToList();

                    resultBD.code = 0;
                    resultBD.data = countrys;
                    resultBD.message = "Se cargaron correctamente los paises.";
                    resultBD.status = true;

                    return resultBD;
                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al cargar los paises.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }




                return resultBD;

            }
        }

        public BaseResponse<List<Country>> GetSearchNameCountrys(string pName)
        {
            var resultBD = new BaseResponse<List<Country>>();
            using (var db = new DataBase())
            {
                try
                {
                    var countrys = db.Country.Where(t =>
                        t.name.Contains(pName))
                        .OrderBy(p=>p.name)
                        .ToList();
                    
                    resultBD.code = 0;
                    resultBD.data = countrys;
                    resultBD.message = "La busqueda se realizo correctamente.";
                    resultBD.status = true;

                    return resultBD;
                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al buscar los paises.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }




                return resultBD;

            }
        }

        public BaseResponse<List<Country>> GetSearchAlphaCountrys(string pAlpha)
        {
            var resultBD = new BaseResponse<List<Country>>();
            using (var db = new DataBase())
            {
                try
                {
                    var countrys = db.Country.Where(t =>
                                                    t.alpha.Contains(pAlpha))
                                                    .OrderBy(p=>p.name)
                                                    .ToList();
                    resultBD.code = 0;
                    resultBD.data = countrys;
                    resultBD.message = "La busqueda se realizo correctamente.";
                    resultBD.status = true;

                    return resultBD;
                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al buscar los paises.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Country> GetAlphaCountrys(string pAlpha)
        {
            var resultBD = new BaseResponse<Country>();
            using (var db = new DataBase())
            {
                try
                {
                    var country = db.Country.SingleOrDefault(t =>
                                                    t.alpha.Equals(pAlpha));
                    if (country != null)
                    {
                        resultBD.code = 0;
                        resultBD.data = country;
                        resultBD.message = "Pais cargado correctamente";
                        resultBD.status = true;
                    }
                    else
                    {
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "Pais no encontrado";
                        resultBD.status = false;
                    }

                    return resultBD;
                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al buscar los paises.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Country> SaveCountry(Country pCountry)
        {
            var resultBD = new BaseResponse<Country>();
            using (var db = new DataBase())
            {
                try
                {
                    db.Country.Add(pCountry);
                    var resulSave= db.SaveChanges();

                    if(resulSave==1)
                    {
                        resultBD.status = true;
                        resultBD.code = 0;
                        resultBD.data = pCountry;
                        resultBD.message = "Se guardo correctamente el pais.";
                    }
                    else
                    {
                        resultBD.status = false;
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "Ocurrio un error al guarda el pais";
                        resultBD.technicaldetail = "No se creo la excepcion.";
                    }

                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al gaurdar el pais.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Country> UpdateCountry(Country pCountry)
        {
            var resultBD = new BaseResponse<Country>();
            using (var db = new DataBase())
            {
                try
                {
                    var country = db.Country.SingleOrDefault(p=>p.alpha.Equals(pCountry.alpha));

                    if(country!=null)
                    {
                        country.independent = pCountry.independent;
                        country.name = pCountry.name;
                        country.numeric_code = pCountry.numeric_code;

                        db.Country.Update(country);
                        var resulBD = db.SaveChanges();

                        if (resulBD==1)
                        {
                            resultBD.status = true;
                            resultBD.code = 0;
                            resultBD.data = pCountry;
                            resultBD.message = "Se actualizo correctamente el pais.";

                            return resultBD;
                        }
                        {
                            resultBD.status = false;
                            resultBD.code = -1;
                            resultBD.data = null;
                            resultBD.message = "Ocurrio un error al guardar el pais";
                            resultBD.technicaldetail = "No se creo la excepcion de entity.";

                            return resultBD;
                        }

                    }
                    else
                    {
                        resultBD.status = false;
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "No se encontro el pais";
                        resultBD.technicaldetail = "No se creo la excepcion.";

                        return resultBD;
                    }

                   

                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al guardar el pais.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Country> DeleteCountry(string pAlpha)
        {
            var resultBD = new BaseResponse<Country>();
            using (var db = new DataBase())
            {
                try
                {
                    var country = db.Country.SingleOrDefault(p => p.alpha.Equals(pAlpha));

                    if (country != null)
                    {
                       
                        db.Country.Remove(country);
                        var resulBD = db.SaveChanges();

                        if (resulBD == 1)
                        {
                            resultBD.status = true;
                            resultBD.code = 0;
                            resultBD.data = country;
                            resultBD.message = "Se elimino correctamente el pais.";

                            return resultBD;
                        }
                        {
                            resultBD.status = false;
                            resultBD.code = -1;
                            resultBD.data = null;
                            resultBD.message = "Ocurrio un error al eliminar el pais";
                            resultBD.technicaldetail = "No se creo la excepcion de entity.";

                            return resultBD;
                        }

                    }
                    else
                    {
                        resultBD.status = false;
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "No se encontro el pais";
                        resultBD.technicaldetail = "No se creo la excepcion.";

                        return resultBD;
                    }


                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al eliminar el pais.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<List<Subdivision>> GetSubdivisionsAlphaCountrys(string pAlpha)
        {
            var resultBD = new BaseResponse<List<Subdivision>>();
            using (var db = new DataBase())
            {
                try
                {
                    var subdivisions = db.Subdivision.Where(t =>
                                                    t.alpha.Equals(pAlpha))
                                                    .OrderBy(p => p.name)
                                                    .ToList();
                    resultBD.code = 0;
                    resultBD.data = subdivisions;
                    resultBD.message = "La busqueda se realizo correctamente.";
                    resultBD.status = true;

                    return resultBD;
                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al buscar las subdivisiones.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Subdivision> SaveSubdivision(Subdivision pSubdivision)
        {
            var resultBD = new BaseResponse<Subdivision>();
            using (var db = new DataBase())
            {
                try
                {
                    if (db.Country.ToList().Exists(p => p.alpha.Equals(pSubdivision.alpha)))
                    {

                        db.Subdivision.Add(pSubdivision);
                        var resulSave = db.SaveChanges();

                        if (resulSave == 1)
                        {
                            resultBD.status = true;
                            resultBD.code = 0;
                            resultBD.data = pSubdivision;
                            resultBD.message = "Se guardo correctamente la subdivision.";
                        }
                        else
                        {
                            resultBD.status = false;
                            resultBD.code = -1;
                            resultBD.data = null;
                            resultBD.message = "Ocurrio un error al guarda la subdivision";
                            resultBD.technicaldetail = "No se creo la excepcion.";
                        }
                    }
                    else
                    {
                        resultBD.status = false;
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "No existe el codigo Alpha de ese pais";
                    }

                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al gaurdar la subdivision.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Subdivision> UpdateSubdivision(Subdivision pSubdivision)
        {
            var resultBD = new BaseResponse<Subdivision>();
            using (var db = new DataBase())
            {
                try
                {
                    var subdivision = db.Subdivision.SingleOrDefault(p => p.code.Equals(pSubdivision.code));

                    if (subdivision != null)
                    {
                        subdivision.alpha = pSubdivision.alpha;
                        subdivision.name = pSubdivision.name;

                        db.Subdivision.Update(subdivision);
                        var resulBD = db.SaveChanges();

                        if (resulBD == 1)
                        {
                            resultBD.status = true;
                            resultBD.code = 0;
                            resultBD.data = subdivision;
                            resultBD.message = "Se actualizo correctamente la subdivision.";

                            return resultBD;
                        }
                        {
                            resultBD.status = false;
                            resultBD.code = -1;
                            resultBD.data = null;
                            resultBD.message = "Ocurrio un error al actualizar la subdivision";
                            resultBD.technicaldetail = "No se creo la excepcion de entity.";

                            return resultBD;
                        }

                    }
                    else
                    {
                        resultBD.status = false;
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "No se encontro la subdivision";
                        resultBD.technicaldetail = "No se creo la excepcion.";

                        return resultBD;
                    }

                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al actualizar la subdivision.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Subdivision> GetCodeSubdivision(string pCode)
        {
            var resultBD = new BaseResponse<Subdivision>();
            using (var db = new DataBase())
            {
                try
                {
                    var subdivision = db.Subdivision.SingleOrDefault(t =>
                                                    t.code.Equals(pCode));
                    if (subdivision != null)
                    {
                        resultBD.code = 0;
                        resultBD.data = subdivision;
                        resultBD.message = "Subdivision cargado correctamente";
                        resultBD.status = true;
                    }
                    else
                    {
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "Subdivision no encontrado";
                        resultBD.status = false;
                    }

                    return resultBD;
                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al buscar la Subdivision.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }

        public BaseResponse<Subdivision> DeleteSubdivision(string pCode)
        {
            var resultBD = new BaseResponse<Subdivision>();
            using (var db = new DataBase())
            {
                try
                {
                    var subdivision = db.Subdivision.SingleOrDefault(p => p.code.Equals(pCode));

                    if (subdivision != null)
                    {

                        db.Subdivision.Remove(subdivision);
                        var resulBD = db.SaveChanges();

                        if (resulBD == 1)
                        {
                            resultBD.status = true;
                            resultBD.code = 0;
                            resultBD.data = subdivision;
                            resultBD.message = "Se elimino correctamente la subdivision.";

                            return resultBD;
                        }
                        {
                            resultBD.status = false;
                            resultBD.code = -1;
                            resultBD.data = null;
                            resultBD.message = "Ocurrio un error al eliminar la subdivision";
                            resultBD.technicaldetail = "No se creo la excepcion de entity.";

                            return resultBD;
                        }

                    }
                    else
                    {
                        resultBD.status = false;
                        resultBD.code = -1;
                        resultBD.data = null;
                        resultBD.message = "No se encontro la subdivision";
                        resultBD.technicaldetail = "No se creo la excepcion.";

                        return resultBD;
                    }


                }
                catch (Exception ex)
                {
                    resultBD.status = false;
                    resultBD.code = -1;
                    resultBD.data = null;
                    resultBD.message = "Ocurrio un error al eliminar la subdivision.";
                    resultBD.technicaldetail = ex.InnerException.Message;

                    Console.WriteLine(ex.Message);
                }

                return resultBD;

            }
        }
    }
}

