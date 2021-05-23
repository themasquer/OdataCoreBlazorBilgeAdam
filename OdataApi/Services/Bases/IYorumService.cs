using System.Linq;
using OdataApi.Models;

namespace OdataApi.Services.Bases
{
    public interface IYorumService
    {
        IQueryable<YorumModel> Query();
        void Add(YorumModel model);
        void Update(YorumModel model);
        void Delete(int id);
    }
}
