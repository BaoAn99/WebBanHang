using System;
using System.Linq;
using WebSiteBanDienThoai.Common.Utils;
using WebSiteBanDienThoai.Core.Entity.Repositories;
using WebSiteBanDienThoai.Entity;

namespace WebSiteBanDienThoai.Persistence.Repositories
{
    public class AccountRepository : Repository<User>, IAccountRepository
    {
        public AccountRepository(QLBHDienThoaiEntities context)
            : base(context)
        {
        }

        public int CheckAccount(string username, string password)
        {
            var isLocked = LookAccount(username);
            if (isLocked)
            {
                return -2; //Tài khoản bị khóa
            }

            var db = QLBHDienThoaiEntities;
            var account =
                   db.Users.FirstOrDefault(
                       x => x.Username.ToLower() == username.ToLower()
                       && x.Password == password
                       && x.Status
                       && !x.IsLocked
                       && x.EmailConfirmed);
            if (account == null)
            {
                return -1; //Không có tài khoản
            }
            else if (account.EmailConfirmed)
            {
                return 1; //Đăng nhập thành công
            }
            else
            {
                return 0; // chưa xác thực
            }
        }

        public Role GetRole(User ac)
        {
            var db = QLBHDienThoaiEntities;
            var employee = db.Employees.Find(ac);
            if (employee == null)
            {
                return new Role();
            }
            else
            {
                return db.Roles.FirstOrDefault(x => x.RoleID == employee.RoleID);
            }
        }

        public User GetAccountByUsername(string username, string password)
        {
            var db = QLBHDienThoaiEntities;
            var account =
                   db.Users.SingleOrDefault(
                       x => x.Username.ToLower() == username.ToLower() && x.Password == password);
            if (account != null)
            {
                return account;
            }
            else
            {
                return null;
            }
        }

        public bool ConfirmCode(User ac, string code)
        {
            var db = QLBHDienThoaiEntities;
            var existAcount = db.Users.SingleOrDefault(x => x.Username == ac.Username);
            if (existAcount != null)
            {
                if (existAcount.Code.Trim() == code.Trim())
                {
                    existAcount.Code = "";
                    existAcount.EmailConfirmed = true;
                    existAcount.Status = true;
                    existAcount.IsLocked = false;
                    existAcount.RoleId = RoleKey.Customer;
                    db.SaveChanges();
                    return true;
                }
                return false;
            }
            {
                throw new NotImplementedException();
            }
        }

        public User GetAccountByEmail(string email)
        {
            var db = QLBHDienThoaiEntities;
            var account = db.Users.SingleOrDefault(x => x.Email.ToLower() == email.ToLower());
            return account;
        }

        public int ValidAccount(User ac)
        {
            var db = QLBHDienThoaiEntities;
            var existEmail = db.Users.Any(x => x.Email.ToLower() == ac.Email.ToLower());
            var existUsername = db.Users.Any(x => x.Username.ToLower() == ac.Username.ToLower());
            if (existEmail)
            {
                return -1;//Trùng mail
            }
            else if (existUsername)
            {
                return 0;//Trùng Username
            }
            else
            {
                return 1; //valid
            }
        }
        public User Get(string username)
        {
            var db = QLBHDienThoaiEntities;
            return db.Users.Single(x => x.Username.Equals(username));
        }
        public bool UpdateRole(int roleId, string username, int cusOrEmpId)
        {
            try
            {
                var db = QLBHDienThoaiEntities;
                var user = Get(username);
                user.RoleId = roleId;
                if (roleId == RoleKey.Employee)
                {
                    user.EmployeeId = cusOrEmpId;
                    var hadEmployees = db.Users.Where(x => x.EmployeeId == cusOrEmpId);
                    foreach (var item in hadEmployees)
                    {
                        item.EmployeeId = null;
                        item.RoleId = RoleKey.Customer;
                    }
                    user.CustormerId = null;
                }
                else if (roleId == RoleKey.Customer)
                {
                    user.CustormerId = cusOrEmpId;
                    var hadCustomers = db.Users.Where(x => x.CustormerId == cusOrEmpId);
                    foreach (var item in hadCustomers)
                    {
                        item.CustormerId = null;
                        item.RoleId = RoleKey.Customer;
                    }

                    user.EmployeeId = null;
                }
                else
                {
                    user.EmployeeId = null;
                    user.CustormerId = null;
                }
                Put(user);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }
        public bool checkMail(string email)
        {
            var db = QLBHDienThoaiEntities;
            if (db.Users.ToList().Exists(x => x.Email.ToLower() == email.ToLower()))
                return true;
            return false;
        }


        public User GetAccountByEmployeeId(int employeeId)
        {
            var db = QLBHDienThoaiEntities;
            return db.Users.FirstOrDefault(x => x.EmployeeId == employeeId);
        }

        public User GetAccountByCustomId(int custormerId)
        {
            var db = QLBHDienThoaiEntities;
            return db.Users.FirstOrDefault(x => x.CustormerId == custormerId);
        }

        public bool LookAccount(string username)
        {
            var db = QLBHDienThoaiEntities;
            var user = db.Users.FirstOrDefault(
                x => x.Username.ToLower() == username.ToLower() 
                     && x.Status 
                     && x.EmailConfirmed);
            if (user != null)
            {
                var userCarts = db.Carts.Where(x => x.CustomerID == user.CustormerId).Select(x => x.CartID);
                var userBill = db.BillOfSales.Count(x => userCarts.Contains(x.CartID ?? 0) && x.Status == StatusBillKey.Cancel);
                if (userBill > 3)
                {
                    user.IsLocked = true;
                    user.Status = false;
                    db.SaveChanges();
                    return true;
                }

            }

            return false;
        }

        public QLBHDienThoaiEntities QLBHDienThoaiEntities
        {
            get { return Context as QLBHDienThoaiEntities; }
        }
    }
}