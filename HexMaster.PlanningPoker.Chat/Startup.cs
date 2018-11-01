using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using HexMaster.BuildingBlocks.EventBus;
using HexMaster.BuildingBlocks.EventBus.Abstractions;
using HexMaster.BuildingBlocks.EventBus.Configuration;
using HexMaster.BuildingBlocks.EventBusRabbitMQ;
using HexMaster.BuildingBlocks.EventBusServiceBus;
using HexMaster.Helpers.Configuration;
using HexMaster.PlanningPoker.Chat.Contracts.Repositories;
using HexMaster.PlanningPoker.Chat.Contracts.Services;
using HexMaster.PlanningPoker.Chat.IntegrationEvents;
using HexMaster.PlanningPoker.Chat.Repositories;
using HexMaster.PlanningPoker.Chat.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace HexMaster.PlanningPoker.Chat
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            var settingsSection = Configuration.GetSection("EventBus");
            var eventBusSettings = settingsSection.Get<EventBusSettings>();
            services.Configure<EventBusSettings>(settingsSection);

            var mongoSettingsSection = Configuration.GetSection(MongoDbSettings.SettingName);
            var mongoSettings = mongoSettingsSection.Get<MongoDbSettings>();
            services.Configure<MongoDbSettings>(mongoSettingsSection);

            services.AddScoped<IChatRepository, ChatRepository>();
            services.AddScoped<IPlanningPokerChatEventService, PlanningPokerChatEventService>();

            services.AddTransient<IChatService, ChatService>();

            services.AddMemoryCache();

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            ConfigureEventBus(services, eventBusSettings);
            RegisterEventBus(services, eventBusSettings);

            services.AddMvcCore().AddJsonFormatters();

            var container = new ContainerBuilder();
            container.Populate(services);
            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                //app.UseHsts();
            }

            ConfigureEventBus(app);
            app.UseCors("CorsPolicy");
            //app.UseHttpsRedirection();
            app.UseMvc();
        }

        private void ConfigureEventBus(IServiceCollection services, EventBusSettings settings)
        {
            if (settings.AzureServiceBusEnabled)
            {
                services.AddSingleton<IServiceBusPersisterConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(settings.EventBusConnection);
                    serviceBusConnection.EntityPath = settings.SubscriptionClientName;
                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                });
            }
            else
            {
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();

                    var factory = new ConnectionFactory()
                    {
                        HostName = settings.EventBusConnection
                    };
                    factory.UserName = settings.EventBusUserName;
                    factory.Password = settings.EventBusPassword;
                    var retryCount = settings.EventBusRetryCount;
                    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                });
            }
        }

        private void RegisterEventBus(IServiceCollection services, EventBusSettings settings)
        {
            var subscriptionClientName = settings.SubscriptionClientName;
            if (settings.AzureServiceBusEnabled)
            {
                services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                });

            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                    var retryCount = settings.EventBusRetryCount;
                    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope,
                        eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            //services.AddTransient<OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
            //services.AddTransient<OrderStatusChangedToPaidIntegrationEventHandler>();
        }

        protected virtual void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            //eventBus.Subscribe<OrderStatusChangedToAwaitingValidationIntegrationEvent, OrderStatusChangedToAwaitingValidationIntegrationEventHandler>();
            //eventBus.Subscribe<OrderStatusChangedToPaidIntegrationEvent, OrderStatusChangedToPaidIntegrationEventHandler>();
        }
    }
}
