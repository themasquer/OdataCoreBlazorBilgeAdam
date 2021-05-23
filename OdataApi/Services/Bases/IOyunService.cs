using System.Linq;
using OdataApi.Models;

namespace OdataApi.Services.Bases
{
    public interface IOyunService
    {
        IQueryable<OyunModel> Query();
        void Add(OyunModel model);
        void Update(OyunModel model);
        void Delete(int id);
    }
}
