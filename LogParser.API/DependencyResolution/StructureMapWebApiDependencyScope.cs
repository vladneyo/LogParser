using System.Web.Http.Dependencies;
using StructureMap;

namespace LogParser.API.DependencyResolution
{
    /// <summary>
    /// The structure map web api dependency scope.
    /// </summary>
    public class StructureMapWebApiDependencyScope : StructureMapDependencyScope, IDependencyScope
    {
        public StructureMapWebApiDependencyScope(IContainer container)
            : base(container) {
        }
    }
}
