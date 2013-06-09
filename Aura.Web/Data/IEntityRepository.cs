using Aura.Web.Models;

namespace Aura.Web.Data
{
    public interface IEntityRepository<T>
    {
        T GetStocks();

        T GetUse();

        ResultsViewModel GetResult();
    }
}