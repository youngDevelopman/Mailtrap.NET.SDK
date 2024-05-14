using Mailtrap.NET.SDK.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mailtrap.NET.SDK.DependencyInjection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMailtrapClient(this IServiceCollection services, IConfiguration configuration)
        {
            // Transactional
            var transactional = configuration.GetSection("Mailtrap:Client:Transactional");
            var transactionalHttp = transactional.GetSection("http");
            var transactionalSmtp = transactional.GetSection("smtp");

            var transactionalHttpToken = transactionalHttp.GetSection("token").Value;

            var transactionalSmtpUser = transactionalSmtp.GetSection("user").Value;
            var transactionalSmtpPassword = transactionalSmtp.GetSection("password").Value;

            var transactionalHttpCredentials = new HttpCredentials(transactionalHttpToken);
            var transactionalsmtpCredentials = new SmtpCredentials(transactionalSmtpUser, transactionalSmtpPassword);
            var transactionalStreamConfiguration = new TransactionalStreamConfiguration(transactionalHttpCredentials, transactionalsmtpCredentials);


            // Bulk
            var bulk = configuration.GetSection("Mailtrap:Client:Bulk");
            var bulkHttp = bulk.GetSection("http");
            var bulkSmtp = bulk.GetSection("smtp");

            var bulkHttpToken = bulkHttp.GetSection("token").Value;

            var bulkSmtpUser = bulkSmtp.GetSection("user").Value;
            var bulkSmtpPassword = bulkSmtp.GetSection("password").Value;

            var bulkHttpCredentials = new HttpCredentials(bulkHttpToken);
            var bulksmtpCredentials = new SmtpCredentials(bulkSmtpUser, bulkSmtpPassword);
            var bulkStreamConfiguration = new BulkStreamConfiguration(bulkHttpCredentials, bulksmtpCredentials);

            var client = new MailtrapClient(transactionalStreamConfiguration, bulkStreamConfiguration);
            services.AddScoped<IMailtrapClient, MailtrapClient>(sp => client);

            return services;
        }

        public static IServiceCollection AddMailtrapTestClient(this IServiceCollection services, IConfiguration configuration)
        {
            var testClientConfiguration = configuration.GetSection("Mailtrap:TestClient");
            var httpConfig = testClientConfiguration.GetSection("http");
            var smtpConfig = testClientConfiguration.GetSection("smtp");

            int? inboxId = null;
            var inboxIdValue = httpConfig.GetSection("inboxId").Value;
            if (int.TryParse(inboxIdValue, out int result))
            {
                inboxId = result;
            };

            var httpToken = httpConfig.GetSection("token").Value;

            var smtpUser = smtpConfig.GetSection("user").Value;
            var smtpPassword = smtpConfig.GetSection("password").Value;

            var httpCredentials = new HttpCredentials(httpToken);
            var smtpCredentials = new SmtpCredentials(smtpUser, smtpPassword);
            var config = new TestClientConfiguration(inboxId, httpCredentials, smtpCredentials);
            
            var testClient = new MailtrapTestClient(config, TestSenderOptions.Http);
            services.AddScoped<IMailtrapTestClient, MailtrapTestClient>(sp => testClient);

            return services;
        }
    }
}
