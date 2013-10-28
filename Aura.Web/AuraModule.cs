using Aura.Web.Data;
using Aura.Web.Interfaces;
using Aura.Web.Models;
using Ninject.Modules;

namespace Aura.Web
{
    public class AuraModule : NinjectModule
    {
        public override void Load()
        {
            Bind<ICommonRepository>().To<CommonRepository>().InTransientScope();

            Bind<IEntityRepository<AnimalViewModel>>().To<AnimalRepository>().InTransientScope();
            Bind<IEntityRepository<BiologicalViewModel>>().To<BiologicalRepository>().InTransientScope();
            Bind<IEntityRepository<MineralViewModel>>().To<MineralRepository>().InTransientScope();
            Bind<IEntityRepository<TerritorialViewModel>>().To<TerritorialRepository>().InTransientScope();
            Bind<IEntityRepository<WaterViewModel>>().To<WaterRepository>().InTransientScope();

            Bind<IRegionsRepository>().To<RegionsRepository>().InTransientScope();

            Bind<IEcologicalRepository>().To<EcologicalRepository>().InTransientScope();
        }
    }
}