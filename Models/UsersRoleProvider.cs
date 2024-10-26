using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace ThongDong_MVC.Models
{
    public class UsersRoleProvider : RoleProvider
    {
        public override string ApplicationName
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }
        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }
        public override string[] GetRolesForUser(string username)
        {
            using (ThongDongDBContext context = new ThongDongDBContext())
            {
                var userRoles = (from user in context.ACCOUNTs
                                 join roleMapping in context.USER_ROLES_MAPPING on user.ACCOUNT_ID equals roleMapping.UserID
                                 join role in context.ROLE_MASTER on roleMapping.RoleID equals role.ID
                                 where user.USERNAME == username
                                 select role.RollName).ToArray();
                return userRoles;
            }
        }
        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            using (ThongDongDBContext context = new ThongDongDBContext())
            {
                var userRoles = (from user in context.ACCOUNTs
                                 join roleMapping in context.USER_ROLES_MAPPING on user.ACCOUNT_ID equals roleMapping.UserID
                                 join role in context.ROLE_MASTER on roleMapping.RoleID equals role.ID
                                 where user.USERNAME == username && role.RollName == roleName
                                 select role).Any();
                return userRoles;
            }
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}