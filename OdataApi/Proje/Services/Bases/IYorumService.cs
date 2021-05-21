using System.Linq;
using OdataApi.Proje.Models;

namespace OdataApi.Proje.Services.Bases
{
    public interface IYorumService
    {
        IQueryable<YorumModel> Query();
        void Add(YorumModel model);
        void Update(YorumModel model);
        void Delete(int id);
    }
}
