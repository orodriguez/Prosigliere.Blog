using Microsoft.EntityFrameworkCore;
using Prosigliere.Blog.Comments;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.WebApi.Storage;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProsigliereBlogEntityFrameworkRepositories(this IServiceCollection services,
        ConfigurationManager config)
    {
        services.AddTransient<IRepository<Post>, PostsRepository>();
        services.AddTransient<IRepository<Comment>, CommentsRepository>();
        services.AddTransient<ICommentsRepository, CommentsRepository>();
        services.AddDbContext<ProsigliereBlogDbContext>(options => options.UseNpgsql(config["ConnectionString"]));
        return services;
    }
}