using System.Linq;
using OdataApi.Proje.Models;

namespace OdataApi.Proje.Services.Bases
{
    public interface IYorumService
    {
        IQueryable<YorumModel> Query();
    }
}
