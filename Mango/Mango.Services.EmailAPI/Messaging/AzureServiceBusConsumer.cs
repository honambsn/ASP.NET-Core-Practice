using Azure.Messaging.ServiceBus;
using Mango.Services.EmailAPI.Models.DTO;
using Mango.Services.EmailAPI.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace Mango.Services.EmailAPI.Messaging
{
    #region old ver
    //public class AzureServiceBusConsumer : IAzureServiceBusConsumer
    //{
    //    private readonly string serviceBusConnectionString;
    //    private readonly string emailCartQueue;
    //    private readonly IConfiguration _configuration;
    //    //private readonly EmailService _emailService;

    //    private readonly IServiceScopeFactory _scopeFactory;


    //    private ServiceBusProcessor _emailCartProcessor;

    //    public AzureServiceBusConsumer(IConfiguration configuration, IServiceScopeFactory scopeFactory)
    //    {
    //        _configuration = configuration;
    //        _scopeFactory = scopeFactory;

    //        serviceBusConnectionString = _configuration.GetValue<string>("ServiceBusConnectionString");

    //        emailCartQueue = _configuration.GetValue<string>("TopicAndQueueNames:EmailShoppingCartQueue");

    //        var client = new ServiceBusClient(serviceBusConnectionString);
    //        _emailCartProcessor = client.CreateProcessor(emailCartQueue);
            

    //    }

    //    public async Task Start()
    //    {
    //        _emailCartProcessor.ProcessMessageAsync += OnEmailCartRequestReceived;
    //        _emailCartProcessor.ProcessErrorAsync += ErrorHandler;

    //        await _emailCartProcessor.StartProcessingAsync();
    //    }

    //    private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
    //    {
    //        // where receive message
    //        //var message = args.Message;
    //        //var body = Encoding.UTF8.GetString(message.Body);

    //        //CartDTOs objMessage = JsonConvert.DeserializeObject<CartDTOs>(body);
    //        //try
    //        //{
    //        //    // TODO - try to log email
    //        //    await _emailService.EmailCartAndLog(objMessage);
    //        //    await args.CompleteMessageAsync(args.Message);
    //        //}
    //        var body = Encoding.UTF8.GetString(args.Message.Body);
    //        var objMessage = JsonConvert.DeserializeObject<CartDTOs>(body);

    //        try
    //        {
    //            using var scope = _scopeFactory.CreateScope();
    //            var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();

    //            await emailService.EmailCartAndLog(objMessage);

    //            await args.CompleteMessageAsync(args.Message);
    //        }
    //        catch (Exception ex)
    //        {
    //            throw new Exception();
    //        }


    //    }

    //    private Task ErrorHandler(ProcessErrorEventArgs args)
    //    {
    //        Console.WriteLine(args.Exception.ToString());
    //        return Task.CompletedTask;
    //    }

    //    public async Task Stop()
    //    {
    //        await _emailCartProcessor.StopProcessingAsync();
    //        await _emailCartProcessor.DisposeAsync();
    //    }
    //}
    #endregion

    public class AzureServiceBusConsumer : BackgroundService
    {
        private readonly ServiceBusProcessor _emailCartProccessor;
        private readonly IServiceScopeFactory _scopeFactory;

        //public AzureServiceBusConsumer(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        //{
        //    _scopeFactory = scopeFactory;

        //    var connectionString = configuration.GetConnectionString("ServiceBusConnectionString");
        //    Console.WriteLine("DEBUG CS:");
        //    Console.WriteLine(((IConfigurationRoot)configuration).GetDebugView());

        //    Console.WriteLine("RAW:");
        //    Console.WriteLine(configuration["ConnectionStrings:ServiceBusConnectionString"]);
        //    Console.WriteLine($"SB ConnectionString = {connectionString}");

        //    if (string.IsNullOrEmpty(connectionString))
        //    {
        //        throw new Exception("ServiceBusConnectionString is NULL. Check appsettings.json");
        //    }

        //    var client = new ServiceBusClient(connectionString);

        //    _emailCartProccessor = client.CreateProcessor(configuration["TopicAndQueueNames:EmailShoppingCartQueue"],
        //        new ServiceBusProcessorOptions
        //        {
        //            MaxConcurrentCalls = 1,
        //            AutoCompleteMessages = false,
        //        });
        //}
        public AzureServiceBusConsumer(IConfiguration configuration, IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            var connectionString = configuration.GetConnectionString("ServiceBusConnectionString");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new InvalidOperationException(
                    "ServiceBusConnectionString is missing. Check appsettings.json or environment variables.");
            }

            var queueName = configuration["TopicAndQueueNames:EmailShoppingCartQueue"];

            if (string.IsNullOrWhiteSpace(queueName))
            {
                throw new InvalidOperationException(
                    "EmailShoppingCartQueue is missing in TopicAndQueueNames configuration.");
            }

            var client = new ServiceBusClient(connectionString);

            _emailCartProccessor = client.CreateProcessor(
                queueName,
                new ServiceBusProcessorOptions
                {
                    MaxConcurrentCalls = 1,
                    AutoCompleteMessages = false
                });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _emailCartProccessor.ProcessMessageAsync += OnEmailCartRequestReceived;
            _emailCartProccessor.ProcessErrorAsync += ErrorHandler;

            await _emailCartProccessor.StartProcessingAsync(stoppingToken);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            Console.WriteLine(args.Exception.ToString());
            return Task.CompletedTask;
        }

        private async Task OnEmailCartRequestReceived(ProcessMessageEventArgs args)
        {
            try
            {
                var body = Encoding.UTF8.GetString(args.Message.Body);
                var objMessage = JsonConvert.DeserializeObject<CartDTOs>(body);

                if (objMessage == null)
                {
                    await args.DeadLetterMessageAsync(args.Message);
                    return;
                }

                using var scope = _scopeFactory.CreateScope();
                //var emailService = scope.ServiceProvider.GetRequiredService<EmailService>();
                var emailService = scope.ServiceProvider.GetRequiredService<IEmailService>();

                await emailService.EmailCartAndLog(objMessage);
                await args.CompleteMessageAsync(args.Message); //-> comment to display msg on servicesbus explorer
                Console.WriteLine(body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                await args.DeadLetterMessageAsync(args.Message);
            }
        }


        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            await _emailCartProccessor.StopProcessingAsync(stoppingToken);
            await _emailCartProccessor.DisposeAsync();

            await base.StopAsync(stoppingToken);
        }
    }

}
