using ApiResponseHandlerWithMediatr.ApiLayer.ResponseHandlers;
using ApiResponseHandlerWithMediatr.ApplicationLayer.Enums;
using MediatR;

namespace ApiResponseHandlerWithMediatr.ApplicationLayer.Services
{
    public class ResultService
    {
        private readonly IMediator _mediator;

        public ResultService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void ReturnResult(ExceptionEnum code)
        {
            var query = new CustomResponseNotification(code);
            _mediator.Publish(query);
        }
    }
}
