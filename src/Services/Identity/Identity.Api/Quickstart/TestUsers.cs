using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Json;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Test;

namespace IdentityServerHost.Quickstart.UI
{
    public class TestUsers
    {
        public static List<TestUser> Users
        {
            get
            {
                var address = new
                {
                    street_address = "Florianska",
                    locality = "Cracow",
                    postal_code = 31553,
                    country = "Poland"
                };
                
                return new List<TestUser>
                {
                    new TestUser
                    {
                        SubjectId = "1234567890",
                        Username = "admin",
                        Password = "admin",
                        Claims =
                        {
                            new Claim(JwtClaimTypes.Name, "Administrator"),
                            new Claim(JwtClaimTypes.GivenName, "Administrator"),
                            new Claim(JwtClaimTypes.FamilyName, "Administrator"),
                            new Claim(JwtClaimTypes.Email, "admin@wizload-shop.com"),
                            new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                            new Claim(JwtClaimTypes.Address, JsonSerializer.Serialize(address), IdentityServerConstants.ClaimValueTypes.Json)
                        }
                    }
                };
            }
        }
    }
}