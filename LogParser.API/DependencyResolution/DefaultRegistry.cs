namespace LogParser.API.DependencyResolution {
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
        }

        #endregion
    }
}