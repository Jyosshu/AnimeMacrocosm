using Microsoft.Extensions.DependencyInjection;
using AnimeMacrocosm.Interface;
using AnimeMacrocosm.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace AnimeMacrocosm
{
    public static class IoC
    {
        public static void RegisterDependencies(IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostsRepository>();
        }
    }
}
