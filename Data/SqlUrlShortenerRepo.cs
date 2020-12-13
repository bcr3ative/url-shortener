using System;
using System.Collections.Generic;
using System.Linq;
using UrlShortener.Models;
using UrlShortener.Utils;

namespace UrlShortener.Data
{
    public class SqlUrlShortenerRepo : IUrlShortenerRepo
    {
        private readonly UrlShortenerContext _context;

        public SqlUrlShortenerRepo(UrlShortenerContext context)
        {
            _context = context;
        }

        public void CreateAccount(Account acc)
        {
            if (acc == null)
            {
                throw new ArgumentNullException(nameof(acc));
            }

            _context.Accounts.Add(acc);
        }

        public void CreateMap(UrlMap map)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            _context.UrlMaps.Add(map);
        }

        public IReadOnlyCollection<UrlMap> GetStatisticsByAccountId(string userName)
        {
            // Fetch a read only list of maps based on username
            return _context.UrlMaps.Where(map => map.Account.UserName == userName)
                .ToList()
                .AsReadOnly();
        }

        public UrlMap GetRedirectRule(string shortUrl)
        {
            // Fetch the url map based on the short url
            return _context.UrlMaps.FirstOrDefault(map => map.ShortUrl == shortUrl);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Account GetAccount(string userName, string password)
        {
            // Fetch account based on username which has a unique index
            var acc = _context.Accounts.FirstOrDefault(a => a.UserName == userName);

            // Return null if no account or unable to verify the user
            if (acc == null || !SecurePasswordHasher.Verify(password, acc.Password)) return null;

            return acc;
        }

        public bool AccountExists(string userName)
        {
            var acc = _context.Accounts.FirstOrDefault(a => a.UserName == userName);

            if (acc == null) return false;

            return true;
        }

        public bool UrlMapExists(int accountId, string url)
        {
            var map = _context.UrlMaps.FirstOrDefault(m => m.AccountId == accountId && m.Url == url);

            if (map == null) return false;

            return true;
        }

        public bool ShortUrlExists(string shorUrl)
        {
            var map = _context.UrlMaps.FirstOrDefault(m => m.ShortUrl == shorUrl);

            if (map == null) return false;

            return true;
        }
    }
}
