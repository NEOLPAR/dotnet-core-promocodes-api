using System;

namespace PromocodesApp.Authentication
{
    public class LoginResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public LoginResponse(string jwtToken, DateTime expiration)
        {
            Token = jwtToken;
            Expiration = expiration;
        }
    }
}
