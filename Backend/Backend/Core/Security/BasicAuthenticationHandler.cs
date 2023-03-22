using Backend.Core.RepositoryInterface;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text;

namespace Backend.Core.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _repository;

        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger,
            UrlEncoder encoder, ISystemClock clock, IUserRepository repository)
            : base(options, logger, encoder, clock)
        {
            _repository = repository;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            try
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers.Authorization);

                var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter ?? string.Empty))
                    .Split(":");

                if (credentials.Length == 0)
                    throw new ArgumentException("Invalid Credetials");

                string username = credentials.FirstOrDefault() ?? string.Empty;
                string password = credentials.LastOrDefault() ?? string.Empty;

                if (!_repository.CheckValidUser(username, password))
                    throw new ArgumentException("Invalid Credentias");

                var claims = new[] { new Claim(ClaimTypes.Name, username) };

                var identity = new ClaimsIdentity(claims, Scheme.Name);

                var principal = new ClaimsPrincipal(identity);

                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }
    }
}
