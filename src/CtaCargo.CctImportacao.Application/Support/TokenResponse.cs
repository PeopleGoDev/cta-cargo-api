using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Support
{
    public class TokenResponse
    {
        public string SetToken { get; set; }
        public string XCSRFToken { get; set; }
        public DateTime ExpirationTokenTime { get; set; }
        //public int expires_in { get; set; }
        //public string scope { get; set; }
        //public string token_type { get; set; }
        //public string access_token { get; set; }
        //public string jwt_token { get; set; }
        //public string jwt_pucomex { get; set; }
    }
}
