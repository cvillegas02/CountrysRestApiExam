using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CountrysRestApi.DB;
using CountrysRestApi.Services;
using CountrysRestApi.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelsCountrysRestApi.ModelsDB;
using ModelsCountrysRestApi.Request;
using ModelsCountrysRestApi.Response;
using Newtonsoft.Json;

namespace CountrysRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountrysController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        [Route("PageNumber/{pageNumber}/PageSize/{pageSize}")]
        public ActionResult<BaseResponse<List<Country>>> GetWithPag(int pageNumber, int pageSize)
        {
            var logsAux = ServiceDataBase.Instance.GetCountrysPageCount(pageNumber, pageSize);

            var metadata = new Metadata
            {
                TotalCount = logsAux.TotalCount,
                PageSize = logsAux.PageSize,
                CurrentPage = logsAux.CurrentPage,
                TotalPages = logsAux.TotalPages,
                HasNextPage = logsAux.HasNextPage,
                HasPreviousPage = logsAux.HasPreviousPage,
                //NextPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString(),
                //PreviousPageUrl = _uriService.GetPostPaginationUri(filters, Url.RouteUrl(nameof(GetPosts))).ToString()
            };

            Response.Headers.Add("X-Pagination", JsonConvert.SerializeObject(metadata));

            if (logsAux != null)
            {
                var resultResponse = new BaseResponse<List<Country>>();
                resultResponse.status = true;
                resultResponse.message = "Se consultaron correctamente las transacciones";
                resultResponse.code = 0;
                resultResponse.data = logsAux;

                return resultResponse;
            }
            else
            {
                var resultResponse = new BaseResponse<List<Country>>();
                resultResponse.status = false;
                resultResponse.message = "Ocurrio un error al consultar las transacciones";
                resultResponse.code = -1;

                return resultResponse;
            }



        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public ActionResult<BaseResponse<List<Country>>> Get()
        {
            return ServiceDataBase.Instance.GetCountrys();
        }

        [Authorize]
        [HttpGet]
        [Route("SearchName/{countryName}")]
        public ActionResult<BaseResponse<List<Country>>> GetCountrysName(string countryName)
        {
            return ServiceDataBase.Instance.GetSearchNameCountrys(countryName);
        }

        [Authorize]
        [HttpGet]
        [Route("SearchAlpha/{countryAlpha}")]
        public ActionResult<BaseResponse<List<Country>>> GetCountrysAlpha(string countryAlpha)
        {
            return ServiceDataBase.Instance.GetSearchAlphaCountrys(countryAlpha);
        }

        [Authorize]
        [HttpGet]
        [Route("Alpha/{countryAlpha}")]
        public ActionResult<BaseResponse<Country>> GetCountryAlpha(string countryAlpha)
        {
            return ServiceDataBase.Instance.GetAlphaCountrys(countryAlpha);
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public ActionResult<BaseResponse<Country>> PostCountry(RequestCountry requestCountry)
        {

            return ServiceDataBase.Instance.SaveCountry(requestCountry);

        }


        [Authorize]
        [HttpPut]
        [Route("")]
        public ActionResult<BaseResponse<Country>> PutCountry(RequestCountry requestCountry)
        {
            return ServiceDataBase.Instance.UpdateCountry(requestCountry);
        }

        [Authorize]
        [HttpDelete]
        [Route("{countryAlpha}")]
        public ActionResult<BaseResponse<Country>> DeleteCountry(string countryAlpha)
        {
            return ServiceDataBase.Instance.DeleteCountry(countryAlpha);
        }

        [Authorize]
        [HttpGet]
        [Route("{countryName}/Subdivisions")]
        public ActionResult<BaseResponse<List<Subdivision>>> GetSubdivisionsCountry(string countryName)
        {
            return ServiceDataBase.Instance.GetSubdivisionsAlphaCountrys(countryName);
        }
        
    }
}
