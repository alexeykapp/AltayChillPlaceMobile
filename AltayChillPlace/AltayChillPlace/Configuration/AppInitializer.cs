using System;
using System.Collections.Generic;
using System.Text;
using AltayChillPlace.Interface;
using AltayChillPlace.Models;
using AltayChillPlace.RestClient;
using AltayChillPlace.Services;
using AltayChillPlace.ViewModels;
using AltayChillPlace.Views;
using Microsoft.Extensions.DependencyInjection;
using Prism.Navigation;

namespace AltayChillPlace.Configuration
{
    public class AppInitializer
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public void Initialize()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var apiClient = ConfiguringHttpClient();
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IDataTransferService, DataTransferService>();
            services.AddTransient<AutorizationVM>();
            services.AddTransient<RegistrationModel>();
            services.AddTransient<RegistrationVM>();
            services.AddTransient<LentaVM>();
            services.AddTransient<HousesVM>();
            services.AddTransient<ApiClient>(provider => apiClient);
            services.AddTransient<AuthClient>(provider => new AuthClient(provider.GetService<ApiClient>()));
            services.AddTransient<RegistrationClient>(provider => new RegistrationClient(provider.GetService<ApiClient>()));
            services.AddTransient<TokenService>();
        }

        private ApiClient ConfiguringHttpClient()
        {
            string baseApiAdress = "http://192.168.3.27:5000/api/";
            var apiClient = new ApiClient(baseApiAdress);
            return apiClient;
        }
    }
}
