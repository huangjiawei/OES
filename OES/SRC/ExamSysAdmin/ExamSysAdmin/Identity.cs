
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
namespace ExamSysAdmin
{
    class Identity
    {
        public static async Task<bool> VerifyUserNamePassword(string userName, string password)
        {
            var usermanager = new UserManager<IdentityUser>(new UserStore<IdentityUser>(new IdentityDbContext()));
            return await usermanager.FindAsync(userName, password) != null;
        }
    }
}
