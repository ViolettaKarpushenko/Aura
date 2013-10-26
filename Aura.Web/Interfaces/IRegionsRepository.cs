using Aura.Web.Models;

namespace Aura.Web.Interfaces
{
    public interface IRegionsRepository
    {
        RegionsModel GetRegions();

        void Update(int id, string name);

        void Add(string name);

        void Delete(int id);
    }
}