using ApiResponseWithExceptionHandlers.ApplicationLayer.Enums;
using ApiResponseWithExceptionHandlers.ApplicationLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiResponseWithExceptionHandlers.ApiLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly ResultService _service;

        public ResultController(ResultService service)
        {
            _service = service;
        }

        [HttpGet("code")]
        public void GetResult(int code)
        {
            _service.ReturnResult((ExceptionEnum)code); 
        }
    }
}
