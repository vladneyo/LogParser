using LogParser.Business.Services;

namespace LogParser.API.DependencyResolution {
    using Business.Contracts.Services;
    using StructureMap;

    public class DefaultRegistry : Registry {
        #region Constructors and Destructors

        public DefaultRegistry() {
            Scan(
                scan => {
                    scan.AssembliesFromApplicationBaseDirectory();
                    scan.TheCallingAssembly();
                    scan.LookForRegistries();
                    scan.WithDefaultConventions();
                });
            For<ICacheService<string>>().Use(x => x.GetInstance<GeoCacheService>()).Singleton();
        }

        #endregion
    }
}