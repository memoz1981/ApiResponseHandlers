using ApiResponseHandlerWithMediatr.ApplicationLayer.Enums;
using System;
using System.Net;

namespace ApiResponseHandlerWithMediatr.ApiLayer.ResponseHandlers
{
    public class ApiResponseWriter
    {
        private int? StatusCode { get; set; }

        private string ResponseMessage { get; set; }

        public void Set(ExceptionEnum exceptionCode, string message)
        {
            StatusCode = exceptionCode == ExceptionEnum.NotFound ? (int)HttpStatusCode.NotFound
                : exceptionCode == ExceptionEnum.General ? (int)HttpStatusCode.InternalServerError
                : exceptionCode == ExceptionEnum.Validator ? (int)HttpStatusCode.BadRequest
                : null;
            ResponseMessage = message;
        }

        public void Reset()
        {
            StatusCode = null;
            ResponseMessage = string.Empty;
        }

        public (int, string) Get()
        {
            return ((int)StatusCode.Value, ResponseMessage);
        }

        public bool IsSet { get => StatusCode != null; }
    }
}
