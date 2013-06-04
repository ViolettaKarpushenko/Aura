using System;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class ResultRepository : Repository
    {
        public WaterViewModel GetWaterModel()
        {
            var items = Execute<WaterModel>("SELECT [ID], [Region], [AreaIndex], [ResourcesIndex], [ResourcesShare], [ResourcesRatio] FROM [v_result_water_resources]");
            var model = new WaterViewModel { Items = items };

            return model;
        }

        public MineralViewModel GetMineralModel()
        {
            var items = Execute<MineralModel>("SELECT [ID], [Region], [AreaIndex], [ResourcesIndex], [ResourcesShare], [ResourcesRatio] FROM [v_result_mineral_resources]");
            var model = new MineralViewModel { Items = items };

            return model;
        }

        public TerritorialViewModel GetTerritorialModel()
        {
            var items = Execute<TerritorialModel>("SELECT [ID], [Region], [AreaIndex], [ResourcesIndex], [ResourcesShare], [ResourcesRatio] FROM [v_result_territorial_resources]");
            var model = new TerritorialViewModel { Items = items };

            return model;
        }

        public AnimalViewModel GetAnimalModel()
        {
            var items = Execute<AnimalModel>("SELECT [ID], [Region], [AreaIndex], [ResourcesIndex], [ResourcesShare], [ResourcesRatio] FROM [v_result_animals_resorces]");
            var model = new AnimalViewModel { Items = items };

            return model;
        }

        public BiologicalViewModel GetBiologicalModel()
        {
            var items = Execute<BiologicalModel>("SELECT [ID], [Region], [AreaIndex], [ResourcesIndex], [ResourcesShare], [ResourcesRatio] FROM [v_result_biological_resources]");
            var model = new BiologicalViewModel { Items = items };

            return model;
        }
    }
}