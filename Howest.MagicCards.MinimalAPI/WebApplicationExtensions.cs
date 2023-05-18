namespace Howest.MagicCards.MinimalAPI
{
    public static class WebApplicationExtensions
    {
        public static void Deconstruct(this WebApplicationBuilder builder,
            out WebApplicationBuilder applicationBuilder,
            out IServiceCollection services,
            out ConfigurationManager configuration)
        {
            applicationBuilder = builder;
            services = builder.Services;
            configuration = builder.Configuration;
        }
    }
}
