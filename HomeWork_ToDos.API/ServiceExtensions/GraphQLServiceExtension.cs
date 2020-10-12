using HomeWork_ToDos.API.GraphQl;
using HomeWork_ToDos.API.GraphQl.Mutations;
using HotChocolate;
using Microsoft.Extensions.DependencyInjection;

namespace HomeWork_ToDos.API.Services
{
    /// <summary>
    /// Extensionn method for configure IService Collection for adding GraphQl services.
    /// </summary>
    public static class GraphQLServiceExtension
    {
        public static IServiceCollection AddGraphQLServices(this IServiceCollection service)
        {
            return service.AddGraphQL(s => SchemaBuilder.New()
                    .AddServices(s)
                    .AddQueryType<Query>()
                    .AddMutationType<Mutation>()
                    .Create());
        }
    }
}
