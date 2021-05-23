using System.Linq;
using OdataApi.Models;

namespace OdataApi.Services.Bases
{
    public interface IYapimciService
    {
        IQueryable<YapimciModel> Query();
        void Add(YapimciModel model);
        void Update(YapimciModel model);
        void Delete(int id);
    }
}
