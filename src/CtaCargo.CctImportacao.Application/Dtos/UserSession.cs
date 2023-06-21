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

        public UserSession(int userId, int companyId)
        {
            UserId = userId;
            CompanyId = companyId;
        }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
