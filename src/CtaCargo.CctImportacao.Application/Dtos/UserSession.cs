using System;
using System.Collections.Generic;
using System.Text;

namespace CtaCargo.CctImportacao.Application.Dtos
{
    public class UserSession
    {
        public UserSession()
        {
        }

        public UserSession(int userId, string userName, int companyId, string environment)
        {
            UserId = userId;
            UserName = userName;
            CompanyId = companyId;
            Environment = environment;
        }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int CompanyId { get; set; }
        public string Environment { get; set; }
    }
}
