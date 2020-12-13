using System.Collections.Generic;
using UrlShortener.Models;

namespace UrlShortener.Data
{
    public interface IUrlShortenerRepo
    {
        bool SaveChanges();

        void CreateAccount(Account acc);
        void CreateMap(UrlMap map);
        IReadOnlyCollection<UrlMap> GetStatisticsByAccountId(string userName);
        UrlMap GetRedirectRule(string shortUrl);
        Account GetAccount(string userName, string password);
        bool AccountExists(string userName);
        bool UrlMapExists(int accountId, string url);
        bool ShortUrlExists(string shorUrl);
    }
}
