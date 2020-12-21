using System.Collections.Generic;
using System.Threading.Tasks;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public interface IUrlShortenerRepo
    {
        Task<bool> SaveChanges();

        Task CreateAccount(Account acc);
        Task CreateMap(UrlMap map);
        Task<IList<UrlMap>> GetStatisticsByAccountId(string userName);
        Task<UrlMap> GetRedirectRule(string shortUrl);
        Task<Account> GetAccount(string userName, string password);
        Task<bool> AccountExists(string userName);
        Task<bool> UrlMapExists(int accountId, string url);
        Task<bool> ShortUrlExists(string shorUrl);
    }
}
