using ApiResponseWithExceptionHandlers.ApplicationLayer.Enums;
using ApiResponseWithExceptionHandlers.ApplicationLayer.Exceptions;
using System;

namespace ApiResponseWithExceptionHandlers.ApplicationLayer.Services
{
    public class ResultService
    {
        public void ReturnResult(ExceptionEnum code)
        {
            if (code == ExceptionEnum.NotFound)
                throw new NotFoundException();
            else if (code == ExceptionEnum.Validator)
                throw new ValidatorException();
            else if (code == ExceptionEnum.General)
                throw new ArgumentException();
        }
    }
}
