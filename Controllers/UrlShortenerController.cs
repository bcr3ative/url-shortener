using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Data;
using UrlShortener.Dtos;
using UrlShortener.Models;
using UrlShortener.Utils;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Authorize]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlShortenerRepo _repository;

        public UrlShortenerController(IUrlShortenerRepo repository)
        {
            _repository = repository;
        }

        // Account

        [HttpPost("account")]
        [AllowAnonymous]
        public async Task<ActionResult<AccountCreateDto>> CreateAccount(AccountCreateDto accountCreateDto)
        {
            string genPassword = KeyGenerator.GetUniqueKey(8);

            var response = new AccountCreateStatusDto()
            {
                success = "true",
                description = "Your account is opened.",
                password = genPassword
            };

            // Check if account already exists
            if (await _repository.AccountExists(accountCreateDto.AccountId))
            {
                response.success = "false";
                response.description = "Account with the same name already exists.";
                response.password = null;

                return Conflict(response);
            }

            var acc = new Account {
                UserName = accountCreateDto.AccountId,
                Password = SecurePasswordHasher.Hash(genPassword)
            };

            try
            {
                await _repository.CreateAccount(acc);
                await _repository.SaveChanges();
            }
            catch (System.Exception)
            {
                // Other unexpected server errors
                response.success = "false";
                response.description = "Unexpected server error has occured.";
                response.password = null;

                return StatusCode(500, response);
            }

            return Ok(response);
        }

        [HttpPost("account/login")]
        [AllowAnonymous]
        public async Task<ActionResult<Object>> Authenticate([FromBody] AccountLoginRequestDto requestAcc)
        {
            var acc = await _repository.GetAccount(requestAcc.AccountId, requestAcc.Password);

            if (acc == null)
            {
                return NotFound(new {message = "User or password is invalid."});
            }

            var token = TokenService.CreateToken(acc);

            return Ok(new
            {
                AccountId = acc.UserName,
                Token = token
            });
        }

        // Register

        [HttpPost("register")]
        public async Task<ActionResult<UrlMapReadDto>> CreateUrlMap(UrlMapCreateDto urlMapCreateDto)
        {
            // Extract Id from JWT token
            var accountId = Int32.Parse(User.FindFirst(ClaimTypes.Name)?.Value);

            // Generate a 8 character key to be used as a shortened url
            var shortUrlGen = KeyGenerator.GetUniqueKey(8);

            if (urlMapCreateDto.redirectType != 301
                && urlMapCreateDto.redirectType != 302
                && urlMapCreateDto.redirectType != 0)
            {
                return BadRequest(new {message = "Invalid redirect type provided."});
            }

            // Check if the same map already exists in the database
            if (await _repository.UrlMapExists(accountId, urlMapCreateDto.url))
            {
                return Conflict(new {message = "Url map with the same configuration already exists."});
            }

            // If there is a conflick on short url return 500
            if (await _repository.ShortUrlExists(shortUrlGen))
            {
                return StatusCode(500, new {message = "Short url with the same generated name already exists. Please try again."});
            }

            var response = new UrlMapReadDto()
            {
                shortUrl = $"{Request.Scheme}://{Request.Host}/{shortUrlGen}"
            };

            var map = new UrlMap()
            {
                Url = urlMapCreateDto.url,
                ShortUrl = shortUrlGen,
                RedirectType = (urlMapCreateDto.redirectType == 0) ? 302 : urlMapCreateDto.redirectType,
                AccountId = accountId
            };

            try
            {
                await _repository.CreateMap(map);
                await _repository.SaveChanges();
            }
            catch (System.Exception)
            {
                return StatusCode(500, new {message = "Unexpected server error has occured."});
            }

            return Ok(response);
        }

        // Statistic

        [HttpGet("statistic/{userName}")]
        public async Task<ActionResult<Object>> GetStatisticsByAccountId(string userName)
        {
            var statistics = await _repository.GetStatisticsByAccountId(userName);
            if (statistics.Count != 0)
            {
                var response = new Dictionary<string, int>();
                foreach (var item in statistics)
                {
                    response.Add(item.Url, item.TimesVisited);
                }

                return Ok(response);
            }

            return NotFound();
        }

        // Redirect

        [HttpGet("{shortUrl}")]
        [AllowAnonymous]
        public async Task<ActionResult> RedirectUrl(string shortUrl)
        {
            var redirectRule = await _repository.GetRedirectRule(shortUrl);
            if (redirectRule != null)
            {
                redirectRule.TimesVisited += 1;
                await _repository.SaveChanges();
                if (redirectRule.RedirectType == 301)
                {
                    return RedirectPermanent(redirectRule.Url);
                }
                else
                {
                    return Redirect(redirectRule.Url);
                }
            }

            return NotFound();
        }
    }
}
