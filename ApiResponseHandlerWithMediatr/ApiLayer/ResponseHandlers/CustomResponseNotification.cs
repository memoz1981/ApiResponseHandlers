using ApiResponseHandlerWithMediatr.ApplicationLayer.Enums;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApiResponseHandlerWithMediatr.ApiLayer.ResponseHandlers
{
    public class CustomResponseNotification : INotification
    {
        public CustomResponseNotification(ExceptionEnum code)
        {
            Code = code;
        }
        
        public ExceptionEnum Code { get; set; }
    }

    public class CustomResponseNotificationHandler : INotificationHandler<CustomResponseNotification>
    {
        private readonly ApiResponseWriter _responseWriter;

        public CustomResponseNotificationHandler(ApiResponseWriter responseWriter)
        {
            _responseWriter = responseWriter;
        }

        public Task Handle(CustomResponseNotification notification, CancellationToken cancellationToken)
        {
            _responseWriter.Set(notification.Code, String.Empty);

            return Task.CompletedTask;
        }
    }
}
