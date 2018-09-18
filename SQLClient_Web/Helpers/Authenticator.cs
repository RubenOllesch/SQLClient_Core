using Microsoft.Extensions.Options;
using SQLClient_Web.Models;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SQLClient_Web.Helpers
{
    public class Authenticator : IAuthenticator
    {
        private User[] users;
        public Authenticator(IOptions<DataBaseSettings> options)
        {
            users = options.Value.Credentials;
        }
        public bool IsAuthenticated(string auth)
        {
            if (new Regex("Basic*").IsMatch(auth))
            {
                string base64data = auth.Split(" ")[1];
                return IsAuthenticatedBasic(base64data);
            }
            else
            {
                throw new AuthorizationException<AuthorizationError>(AuthorizationError.INVALID_AUTHORIZATION_TYPE);
            }
        }

        private bool IsAuthenticatedBasic(string base64data)
        {
            string credentials = Base64Decode(base64data);

            if (credentials.Count(x => x == ':') != 1)
            {
                throw new AuthorizationException<AuthorizationError>(AuthorizationError.INVALID_AUTHORIZATION_HEADER);
            }

            string userName = credentials.Split(":")[0];
            string password = credentials.Split(":")[1];

            return Array.Exists(users, user => user.UserName == userName && user.Password == password);
        }

        private string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
