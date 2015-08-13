using System.Collections.Generic;
using System.Security.Claims;

namespace Carcarah.OnAuth.OpenId
{
    public enum TokenUsage
    {
        ReUse,
        OneTime,
        OneTimeOnly
    }

    public enum TokenExpiration
    {
        Absolute,
        Sliding
    }

    public class Client
    {
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public List<string> ClientSecrets { get; set; }
        public string LogoUri { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public List<string> ScopeRestrictions { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public TokenUsage RefreshTokenUsage { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public TokenExpiration RefreshTokenExpiration { get; set; }
        public bool EnableLocalLogin { get; set; }
        public List<string> IdentityProviderRestrictions { get; set; }

        public Client()
        {
            ClientSecrets = new List<string>();
            ScopeRestrictions = new List<string>();
            PostLogoutRedirectUris = new List<string>();
            IdentityProviderRestrictions = new List<string>();

            Enabled = true;
            EnableLocalLogin = true;

            // 5 minutes
            AuthorizationCodeLifetime = 300;
            IdentityTokenLifetime = 300;

            // one hour
            AccessTokenLifetime = 3600;

            // 30 days
            AbsoluteRefreshTokenLifetime = 2592000;

            // 15 days
            SlidingRefreshTokenLifetime = 1296000;

            RefreshTokenUsage = TokenUsage.OneTimeOnly;
            RefreshTokenExpiration = TokenExpiration.Absolute;
        }
    }
}
