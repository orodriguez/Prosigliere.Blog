using Microsoft.Extensions.DependencyInjection;
using Prosigliere.Blog.Entities;

namespace Prosigliere.Blog.Tests.Fakes;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddProsigliereBlogFakeRepositories(this IServiceCollection services)
    {
        services.AddSingleton<IRepository<Post>, FakeRepository<Post>>();
        services.AddSingleton<IRepository<Comment>, FakeCommentsRepository>();       
        return services;
    }
}