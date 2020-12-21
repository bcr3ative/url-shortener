using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public async Task CreateAccount(Account acc)
        {
            if (acc == null)
            {
                throw new ArgumentNullException(nameof(acc));
            }

            await _context.Accounts.AddAsync(acc);
        }

        public async Task CreateMap(UrlMap map)
        {
            if (map == null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            await _context.UrlMaps.AddAsync(map);
        }

        public async Task<IList<UrlMap>> GetStatisticsByAccountId(string userName)
        {
            return await _context.UrlMaps.Where(map => map.Account.UserName == userName)
                .ToListAsync();
        }

        public async Task<UrlMap> GetRedirectRule(string shortUrl)
        {
            // Fetch the url map based on the short url
            return await _context.UrlMaps.FirstOrDefaultAsync(map => map.ShortUrl == shortUrl);
        }

        public async Task<Account> GetAccount(string userName, string password)
        {
            // Fetch account based on username which has a unique index
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == userName);

            // Return null if no account or unable to verify the user
            if (acc == null || !SecurePasswordHasher.Verify(password, acc.Password)) return null;

            return acc;
        }

        public async Task<bool> AccountExists(string userName)
        {
            var acc = await _context.Accounts.FirstOrDefaultAsync(a => a.UserName == userName);

            if (acc == null) return false;

            return true;
        }

        public async Task<bool> UrlMapExists(int accountId, string url)
        {
            var map = await _context.UrlMaps.FirstOrDefaultAsync(m => m.AccountId == accountId && m.Url == url);

            if (map == null) return false;

            return true;
        }

        public async Task<bool> ShortUrlExists(string shorUrl)
        {
            var map = await _context.UrlMaps.FirstOrDefaultAsync(m => m.ShortUrl == shorUrl);

            if (map == null) return false;

            return true;
        }
    }
}
