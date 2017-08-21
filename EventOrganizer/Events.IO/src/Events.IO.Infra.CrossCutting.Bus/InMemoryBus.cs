using Events.IO.Domain.Core.Bus;
using System;
using Events.IO.Domain.Core.AppEvents;
using Events.IO.Domain.Core.Commands;
using Events.IO.Domain.Core.Notifications;

namespace Events.IO.Infra.CrossCutting.Bus
{
    public sealed class InMemoryBus : IBus
    {
        public static Func<IServiceProvider> IoDContainerAccessor { get; set; }
        private static IServiceProvider Container => IoDContainerAccessor();

        public void RaiseEvent<T>(T theEvent) where T : ApplicationCoreEvent
        {
            Publish(theEvent);
        }

        public void SendCommand<T>(T theCommand) where T : Command
        {
            Publish(theCommand);
        }

        private static void Publish<T>(T message) where T : Message
        {
            if (Container == null) return;

            //Convert the message in DomainNotification or Commands/Application Events
            var obj = Container.GetService(message.MessageType.Equals("DomainNotification")
                ? typeof(IDomainNotificationHandler<T>)
                : typeof(IHandler<T>));

            //Explicit cast to type <T> to know what Handle method has to be called among all the possible Commands and/or Application Events
            ((IHandler<T>)obj).Handle(message);
        }
    }
}
