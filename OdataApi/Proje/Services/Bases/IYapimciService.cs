using OdataApi.Proje.Models;
using System.Linq;

namespace OdataApi.Proje.Services.Bases
{
    public interface IYapimciService
    {
        IQueryable<YapimciModel> Query();
        void Add(YapimciModel model);
        void Update(YapimciModel model);
        void Delete(int id);
    }
}
