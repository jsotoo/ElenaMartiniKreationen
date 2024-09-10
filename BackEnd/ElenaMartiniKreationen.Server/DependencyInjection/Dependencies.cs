using ElenaMartiniKreationen.Repositories.Implementations;
using ElenaMartiniKreationen.Repositories.Interfaces;
using ElenaMartiniKreationen.Server.Services;

namespace ElenaMartiniKreationen.Server.DependencyInjection
{
    public static class Dependencies
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<ICartItemRepository, CartItemRepository>()
                    .AddTransient<ICartRepository, CartRepository>()
                    .AddTransient<ICategoryRepository, CategoryRepository>()
                    .AddTransient<IOrderItemRepository, OrderItemRepository>()
                    .AddTransient<IOrderRepository, OrderRepository>()
                    .AddTransient<IPaymentMethodRepository, PaymentMethodRepository>()
                    .AddTransient<IProductRepository, ProductRepository>()
                    .AddTransient<IReviewRepository, ReviewRepository>()
                    .AddTransient<IShippingAddressRepository, ShippingAddressRepository>()
                    .AddTransient<IUserProfileRepository, UserProfileRepository>();
                  

            return services;
        }
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            return services.AddTransient<IFileUploader, FileUploader>()
                           .AddTransient<IUserService, UserService>();



        }
    }
}
