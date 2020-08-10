using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Owin;

using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace Bet.AspNet.DependencyInjection.Owin
{
    public class ContainerMiddleware
    {
        private readonly AppFunc _nextFunc;
        private readonly IAppBuilder _app;

        public ContainerMiddleware(AppFunc nextFunc, IAppBuilder app)
        {
            _nextFunc = nextFunc;
            _app = app;
        }

        public async Task Invoke(IDictionary<string, object> env)
        {
            using var scope = env.SetRequestProvider(_app);
            await _nextFunc(env);
        }
    }
}
