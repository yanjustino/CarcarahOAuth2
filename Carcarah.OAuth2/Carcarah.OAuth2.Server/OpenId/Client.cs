using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Carcarah.OAuth2.Server.OpenId
{
    public class Client
    {
        public bool Enabled { get; set; }
        public string ClientId { get; set; }
        public List<string> ClientSecrets { get; set; }
        public string ClientName { get; set; }
        public string LogoUri { get; set; }
        public List<string> RedirectUris { get; set; }
        public List<string> PostLogoutRedirectUris { get; set; }
        public bool AllowAccessToAllScopes { get; set; }
        public List<string> AllowedScopes { get; set; }
        public int IdentityTokenLifetime { get; set; }
        public int AccessTokenLifetime { get; set; }
        public int AuthorizationCodeLifetime { get; set; }
        public int AbsoluteRefreshTokenLifetime { get; set; }
        public int SlidingRefreshTokenLifetime { get; set; }
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }
        public List<string> IdentityProviderRestrictions { get; set; }
        public List<Claim> Claims { get; set; }
        public bool AllowAccessToAllCustomGrantTypes { get; set; }
        public List<string> AllowedCustomGrantTypes { get; set; }

        public Client()
        {
            ClientSecrets = new List<string>();
            AllowedScopes = new List<string>();
            RedirectUris = new List<string>();
            PostLogoutRedirectUris = new List<string>();
            IdentityProviderRestrictions = new List<string>();
            AllowedCustomGrantTypes = new List<string>();

            Enabled = true;
            AllowAccessToAllScopes = false;
            AllowAccessToAllCustomGrantTypes = false;

            // client claims settings
            Claims = new List<Claim>();

            // 5 minutes
            AuthorizationCodeLifetime = 300;
            IdentityTokenLifetime = 300;

            // one hour
            AccessTokenLifetime = 3600;

            // 30 days
            AbsoluteRefreshTokenLifetime = 2592000;

            // 15 days
            SlidingRefreshTokenLifetime = 1296000;
        }

        public static Func<Client, bool> ClientIdScope(string clientId) =>
            x => x.ClientId == clientId;
    }
}
