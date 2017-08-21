using AutoMapper;
using Events.IO.Application.Interfaces;
using Events.IO.Application.Services;
using Microsoft.Extensions.DependencyInjection;
using Events.IO.Domain.Core.AppEvents;
using Events.IO.Domain.Core.Bus;
using Events.IO.Domain.Core.Notifications;
using Events.IO.Domain.Events.ApplicationEvents;
using Events.IO.Domain.Events.Commands;
using Events.IO.Domain.Events.Repository;
using Events.IO.Domain.Interfaces;
using Events.IO.Infra.CrossCutting.Bus;
using Events.IO.Infra.Data.Context;
using Events.IO.Infra.Data.Repository;
using Events.IO.Infra.Data.UoW;

namespace Events.IO.Infra.CrossCutting.IoC
{
    public class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Applications
            services.AddSingleton(Mapper.Configuration);
            services.AddScoped<IMapper>(
                sp => new Mapper(sp.GetRequiredService<IConfigurationProvider>(), sp.GetService)); //AutoMapper specific configuration
            services.AddScoped<IEventAppService, EventAppService>();

            //Domain - Commands
            services.AddScoped<IHandler<EventRegistrationCommand>, CommandEventHandler>();
            services.AddScoped<IHandler<EventUpdateCommand>, CommandEventHandler>();
            services.AddScoped<IHandler<EventDeleteCommand>, CommandEventHandler>();

            //Domain - Events
            services.AddScoped<IDomainNotificationHandler<DomainNotification>, DomainNotificationHandler>();
            services.AddScoped<IHandler<EventRegistrationEvent>, EventHandlerEvent>();
            services.AddScoped<IHandler<EventUpdateEvent>, EventHandlerEvent>();
            services.AddScoped<IHandler<EventDeleteEvent>, EventHandlerEvent>();

            //Infra - Data
            services.AddScoped<IEventRepository, EventRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<ContextEvents>();

            //Infra - Bus
            services.AddScoped<IBus, InMemoryBus>();
        }
    }
}