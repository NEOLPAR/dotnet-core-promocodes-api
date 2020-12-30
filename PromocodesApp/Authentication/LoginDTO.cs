using System;

namespace PromocodesApp.Authentication
{
    public class LoginDTO
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }

        public LoginDTO(string jwtToken, DateTime expiration)
        {
            Token = jwtToken;
            Expiration = expiration;
        }
    }
}
