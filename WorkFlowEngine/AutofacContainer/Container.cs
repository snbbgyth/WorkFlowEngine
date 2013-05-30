using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkFlowHandle.BLL;

namespace AutofacContainer
{
    using Autofac;
    using Autofac.Core;
    using WorkFlowService.BLL;

    public static  class Container
    {
        private static object objLock = new object();
        private static ContainerBuilder _containerBuilder;
        private static IContainer _container;

        static  Container()
        {
            _containerBuilder = new ContainerBuilder();
            _containerBuilder.RegisterModule(new WorkflowHandleModule());
            _containerBuilder.RegisterModule(new WorkflowServiceModule());
            _container = _containerBuilder.Build();
        }

        /// <summary>
        /// Resolve an instance of the default requested type from the container.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of object to get from the container.</typeparam>
        /// <returns>The retrieved object.</returns>
        public static T Resolve<T>()
        {
            try
            {
                lock (objLock)
                {

                    return _container.Resolve<T>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Resolve an instance of the requested type with the given name from the container.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of object to get from the container.</typeparam>
        /// <param name="name">Name of the object to retrieve.</param>
        /// <returns>The retrieved object.</returns>
        //public static T Resolve<T>()
        //{
        //    try
        //    {
        //        lock (objLock)
        //        {
        //            var container = _containerBuilder.Build();
        //            return container.Resolve<T>();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        public static void RegisterInstance<TInterface, TType>()
        {
            try
            {
                lock (objLock)
                {
                    _containerBuilder.RegisterType<TType>().As<TInterface>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void Register<TType>()
        {
            try
            {
                lock (objLock)
                {
                    _containerBuilder.RegisterType<TType>();

                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
