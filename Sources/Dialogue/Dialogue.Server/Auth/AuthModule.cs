using Dialogue.Portable;
using Nancy;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dialogue.Server.Auth
{
    public class AuthModule : NancyModule
    {
        private TimeSpan Expiration { get { return TimeSpan.FromHours(4); } }

        #region Get identity

        private static byte[] secretKey;

        private static byte[] SecretKey
        {
            get
            {
                if(secretKey == null)
                {
                    var buffer = new byte[512];
                    new Random().NextBytes(buffer);
                    secretKey = buffer;
                }

                return secretKey;
            }
        }

        public static IUserIdentity GetUserIdentity(NancyContext ctx)
        {
            var jwtToken = ctx.Request.Headers.Authorization;

            try
            {
                var payload = Jose.JWT.Decode<JwtToken>(jwtToken.Replace("Bearer ",string.Empty), SecretKey,Jose.JwsAlgorithm.HS512);

                var tokenExpires = DateTime.FromBinary(payload.Exp);

                if (tokenExpires > DateTime.UtcNow)
                {
                    return new UserIdentity() { UserName = "Guest" };
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        #endregion

        public AuthModule()
        {
            this.Get[Mapper.Current.AuthLoginPath, true] = async (p, ct) =>
            {
                var payload = new JwtToken()
                {
                    Exp = DateTime.UtcNow.Add(Expiration).ToBinary(),

                };

                var token = Jose.JWT.Encode(payload, SecretKey, Jose.JwsAlgorithm.HS512);

                return Response.AsJson(new Dictionary<string, string>
                {
                    { "access_token", token },
                });
            };
        }
    }
}
