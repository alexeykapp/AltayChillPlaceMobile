using AltayChillPlace.Interface;
using AltayChillPlace.Models;
using AltayChillPlace.RestClient;
using AltayChillPlace.Services;
using AltayChillPlace.ViewModels;
using AltayChillPlace.ViewModels.Admin;
using AltayChillPlace.Views.Admin;
using Microsoft.Extensions.DependencyInjection;
using System;

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
            ConfigureSingletonServices(services, apiClient);
            ConfigureTransientServices(services);
        }

        private void ConfigureSingletonServices(IServiceCollection services, ApiClient apiClient)
        {
            services.AddSingleton<IAuthService, AuthService>();
            services.AddSingleton<IRegistrationService, RegistrationService>();
            services.AddSingleton<IMessageService, MessageService>();
            services.AddSingleton<IDataTransferService, DataTransferService>();
            services.AddSingleton<IHouseDataService, HouseDataSevice>();
            services.AddSingleton<IServicesService, ServiceServices>();
            services.AddSingleton<IBookingService, BookingServices>();
            services.AddSingleton<IHistoryService, HistoryService>();
            services.AddSingleton<IBlogServices,  BlogServices>();
            services.AddSingleton<IProfileService, ProfileService>();
            services.AddSingleton<IAdminService, AdminServices>();
            services.AddSingleton<ApiClient>(provider => apiClient);
        }

        private void ConfigureTransientServices(IServiceCollection services)
        {
            services.AddTransient<AutorizationVM>();
            services.AddTransient<RegistrationModel>();
            services.AddTransient<RegistrationVM>();
            services.AddTransient<LentaVM>();
            services.AddTransient<HouseModel>();
            services.AddTransient<HouseInfoPageVM>();
            services.AddTransient<CarouselVM>();
            services.AddTransient<HousesVM>();
            services.AddTransient<BookingVM>();
            services.AddTransient<BlogVM>(); 
            services.AddTransient<MainMenuVM>();
            services.AddTransient<BookingRequestsAdminVM>();
            services.AddTransient<ProfileVM>();
            services.AddTransient<MainMenuAdminVM>();
            services.AddTransient<HistoryBookingVM>();
            services.AddTransient<ServiceModel>();
            services.AddTransient<AuthClient>(provider => new AuthClient(provider.GetService<ApiClient>()));
            services.AddTransient<RegistrationClient>(provider => new RegistrationClient(provider.GetService<ApiClient>()));
            services.AddTransient<HousesDataClient>(provider => new HousesDataClient(provider.GetService<ApiClient>()));
            services.AddTransient<ServiceClient>(provider => new ServiceClient(provider.GetService<ApiClient>()));
            services.AddTransient<BookingClient>(provider => new BookingClient(provider.GetService<ApiClient>()));
            services.AddTransient<HistoryClient>(provider => new HistoryClient(provider.GetService<ApiClient>()));
            services.AddTransient<BlogClient>(provider => new BlogClient(provider.GetService<ApiClient>()));
            services.AddTransient<ProfileClient>(provider => new ProfileClient(provider.GetService<ApiClient>()));
            services.AddTransient<AdminClient>(provider => new AdminClient(provider.GetService<ApiClient>()));
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
