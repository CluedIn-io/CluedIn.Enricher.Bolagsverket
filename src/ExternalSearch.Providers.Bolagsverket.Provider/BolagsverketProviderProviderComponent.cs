﻿using Castle.MicroKernel.Registration;
using System.Reflection;
using CluedIn.Core;
using CluedIn.Core.Server;
using CluedIn.Core.Providers;
using CluedIn.ExternalSearch.Providers.Bolagsverket;
using ComponentHost;

namespace CluedIn.ExternalSearch.Providers.Bolagsverket.Provider
{
    [Component(BolagsverketConstants.ComponentName, "Providers", ComponentType.Service, ServerComponents.ProviderWebApi, Components.Server, Components.DataStores, Isolation = ComponentIsolation.NotIsolated)]

    public sealed class BolagsverketProviderProviderComponent : ServiceApplicationComponent<IServer>
    {
        /**********************************************************************************************************
         * CONSTRUCTOR
         **********************************************************************************************************/

        /// <summary>
        /// Initializes a new instance of the <see cref="GoogleMapsProviderProviderComponent" /> class.
        /// </summary>
        /// <param name="componentInfo">The component information.</param>
        public BolagsverketProviderProviderComponent(ComponentInfo componentInfo) : base(componentInfo)
        {
            // Dev. Note: Potential for compiler warning here ... CA2214: Do not call overridable methods in constructors
            //   this class has been sealed to prevent the CA2214 waring being raised by the compiler
            Container.Register(Component.For<BolagsverketProviderProviderComponent>().Instance(this));
        }

        /**********************************************************************************************************
         * METHODS
         **********************************************************************************************************/

        /// <summary>Starts this instance.</summary>
        public override void Start()
        {
            var asm = Assembly.GetAssembly(typeof(BolagsverketProviderProviderComponent));
            Container.Register(Types.FromAssembly(asm).BasedOn<IProvider>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());
            Container.Register(Types.FromAssembly(asm).BasedOn<IEntityActionBuilder>().WithServiceFromInterface().If(t => !t.IsAbstract).LifestyleSingleton());

            State = ServiceState.Started;
        }

        /// <summary>Stops this instance.</summary>
        public override void Stop()
        {
            if (State == ServiceState.Stopped)
                return;

            State = ServiceState.Stopped;
        }
    }
}
