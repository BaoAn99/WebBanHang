using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSiteBanDienThoai.Common.Utils
{
    public static class SessionKey
    { 
        public const string User = "user";
        public const string RegUser = "RegUser";
        public const string Admin = "admin";
    }

    public static class StatusBillKey
    {
        public const int Cancel = -1;
        public const int InProcess = 0;
        public const int Processed = 1;

    }

    public static class StatusCartKey
    {
        public const int Cancel = -1;
        public const int Pending = 0;
        public const int Success = 1;
         
    }

    public static class RoleKey
    {
        public const int Admin = 1;
        public const int Employee = 2;
        public const int Customer = 3;
        public const int User = 4;

        public static string GetRole(int role)
        {
            switch (role)
            {
                case 1:
                return "Quản trị";
                case 2:
                    return "Nhân Viên";
                case 3:
                    return "Khách hàng";
                case 4:
                    return "Người dùng";
                default:
                    return "Unknown";
            }
        }
    }

    public static class EmployeeTypeKey
    {
        public const int Sale = 0;
        public const int Delivery = 1; 

        public static string GetEmployeeType(int type)
        {
            switch (type)
            {
                case 0:
                    return "NV Bán hàng";
                case 1:
                    return "NV Giao hàng";
                default:
                    return "Unknown";
            }
        }
    }

    public static class VariableExtensions
    {
        public static int pageSize = 2;
    }

    public enum StatusCode
    {
        Success = 200,
        NotFound = 404,
        NotForbidden = 403,
        ServerError = 500
    }
}