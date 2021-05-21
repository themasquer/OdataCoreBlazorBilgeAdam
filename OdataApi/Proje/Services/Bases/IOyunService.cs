using OdataApi.Proje.Models;
using System.Linq;

namespace OdataApi.Proje.Services.Bases
{
    public interface IOyunService
    {
        IQueryable<OyunModel> Query();
        void Add(OyunModel model);
        void Update(OyunModel model);
        void Delete(int id);
    }
}
