using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Core.Entity.Repositories
{
      //this.Configuration.LazyLoadingEnabled = false;
    public interface IAccountRepository : IRepository<User>
    {
        int CheckAccount(string username, string password);
        User GetAccountByUsername(string username, string password);
        User GetAccountByEmployeeId(int employeeId);
        User GetAccountByCustomId(int custormerId);
        User GetAccountByEmail(string email);
        Role GetRole(User ac);
        bool ConfirmCode(User ac, string code);
        int ValidAccount(User ac);
        bool UpdateRole(int roleId, string username,int cusOrEmpId);
        bool LookAccount(string username);
        bool checkMail(string email);    
    }
}