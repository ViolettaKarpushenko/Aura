namespace Aura.Web.Interfaces
{
    public interface ICommonRepository
    {
        void UpdateValue(string tableName, int tableId, int columnId, int regionId, double value);
    }
}