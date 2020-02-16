using DryIoc;
using System;

namespace IocHelper
{
    public abstract class BootstrapperBase : IDisposable
    {
        private IContainer _container;

        protected BootstrapperBase()
        {
            _container = new Container();
        }

        public abstract void Build();

        /// <summary>
        /// RegisterSingleton
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="serviceKey"></param>
        protected void RegisterSingleton<TService, TImplementation>(object serviceKey= null) where TImplementation : TService
        {
            _container.Register<TService, TImplementation>(Reuse.Singleton, serviceKey: serviceKey);
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="IService"></typeparam>
        /// <returns></returns>
        public TService Resolve<TService>()
        {
            return _container.Resolve<TService>();
        }

        /// <summary>
        /// Resolve
        /// </summary>
        /// <typeparam name="IService"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TService Resolve<TService>(string key)
        {
            return _container.Resolve<TService>(serviceKey: key);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            _container.Dispose();
        }
    }
}
