using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using CleanArchitecture.Application.Common.MediatR.Contracts;

namespace CleanArchitecture.Application.Common.MediatR;

public interface INotificationPublisher
{
    Task Publish(IEnumerable<NotificationHandlerExecutor> handlerExecutors, INotification notification,
        CancellationToken cancellationToken);
}