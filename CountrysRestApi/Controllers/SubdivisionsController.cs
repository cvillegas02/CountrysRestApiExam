using CountrysRestApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ModelsCountrysRestApi.ModelsDB;
using ModelsCountrysRestApi.Request;
using ModelsCountrysRestApi.Response;

namespace CountrysRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubdivisionsController : ControllerBase
    {

        [Authorize]
        [HttpGet]
        [Route("Code/{codeSubdivision}")]
        public ActionResult<BaseResponse<Subdivision>> GetSubdivisionCode(string codeSubdivision)
        {
            return ServiceDataBase.Instance.GetCodeSubdivision(codeSubdivision);
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public ActionResult<BaseResponse<Subdivision>> PostSubdivision(RequestSubdivision requestSubdivision)
        {
            return ServiceDataBase.Instance.SaveSubdivision(requestSubdivision);
        }


        [Authorize]
        [HttpPut]
        [Route("")]
        public ActionResult<BaseResponse<Subdivision>> PutSubdivision(RequestSubdivision requestSubdivision)
        {
            return ServiceDataBase.Instance.UpdateSubdivision(requestSubdivision);
        }

        [Authorize]
        [HttpDelete]
        [Route("{codeSubdivision}")]
        public ActionResult<BaseResponse<Subdivision>> DeleteSubdivision(string codeSubdivision)
        {
            return ServiceDataBase.Instance.DeleteSubdivision(codeSubdivision);
        }
    }
}
