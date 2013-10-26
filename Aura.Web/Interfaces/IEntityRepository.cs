using Aura.Web.Models;

namespace Aura.Web.Interfaces
{
    public interface IEntityRepository<out T>
    {
        T GetStocks();

        T GetUse();

        ResultsViewModel GetResult();
    }
}