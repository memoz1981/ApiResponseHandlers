using ApiResponseHandlerWithMediatr.ApplicationLayer.Enums;
using ApiResponseHandlerWithMediatr.ApplicationLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiResponseHandlerWithMediatr.ApiLayer.Controllers
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
