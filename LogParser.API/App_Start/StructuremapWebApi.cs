using System.Web.Http;
using LogParser.API.DependencyResolution;

[assembly: WebActivatorEx.PostApplicationStartMethod(typeof(LogParser.API.App_Start.StructuremapWebApi), "Start")]

namespace LogParser.API.App_Start {
    public static class StructuremapWebApi {
        public static void Start() {
			var container = StructuremapMvc.StructureMapDependencyScope.Container;
            GlobalConfiguration.Configuration.DependencyResolver = new StructureMapWebApiDependencyResolver(container);
        }
    }
}