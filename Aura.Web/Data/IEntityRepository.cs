using Aura.Web.Models;

namespace Aura.Web.Data
{
    public interface IEntityRepository<out T>
    {
        T GetStocks();

        T GetUse();

        ResultsViewModel GetResult();
    }
}