using Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Services
{
    public class CheckAuthentication
    {
        public static bool Check(BaseDapperContext baseDapperContext, Login login)
        {
            try
            {
                return baseDapperContext.Query<int>($"Select count(*) from [CMSUser] where UserName = '{login.UserName}' and [Password] = '{login.Password}'")
                .FirstOrDefault() > 0;
            }
            catch (Exception ex) {
                return false;
            }
        }
    }
}
