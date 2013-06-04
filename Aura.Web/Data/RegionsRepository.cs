using System.Linq;
using Aura.Web.Common;
using Aura.Web.Models;

namespace Aura.Web.Data
{
    public class RegionsRepository : Repository
    {
        public RegionsModel GetRegions()
        {
            var regions = Execute<RegionModel>("SELECT [ID], [Name] FROM [regions]");

            var model = new RegionsModel
                {
                    Regons = regions
                };

            return model;
        }

        public void Update(int id, string name)
        {
            if (!Execute<RegionModel>("SELECT [ID] FROM [regions] WHERE [ID] = {0}", id).Any())
            {
                throw new PresentationException("Ќельз€ обновить несуществующий регион.");
            }

            if (Execute<RegionModel>("SELECT [ID] FROM [regions] WHERE [Name] = '{0}'", name).Any())
            {
                throw new PresentationException("–егион с таким именем уже существует.");
            }

            Execute("UPDATE [regions] SET [Name] = '{0}' WHERE [ID] = {1}", name, id);
        }

        public void Add(string name)
        {
            var regions = Execute<RegionModel>("SELECT [ID] FROM [regions] WHERE [Name] = '{0}'", name);

            if (regions.Any())
            {
                throw new PresentationException("–егион с таким именем уже существует.");
            }

            Execute("INSERT INTO [regions] ([Name]) VALUES ('{0}')", name);
        }

        public void Delete(int id)
        {
            if (!Execute<RegionModel>("SELECT [ID] FROM [regions] WHERE [ID] = {0}", id).Any())
            {
                throw new PresentationException("Ќельз€ удалить несуществующий регион.");
            }

            Execute("DELETE FROM [regions] WHERE [ID] = {0}", id);
        }
    }
}