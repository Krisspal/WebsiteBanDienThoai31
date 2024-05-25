using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WebDT.BLL
{
    public class AdminOnlyAuth : IAuthorizationRequirement
    {
        public AdminOnlyAuth(int isAdmin) 
        {
            isAdmin = isAdmin;
        }

        public int IsAdmin { get; }
    }

    public class AdminOnlyAuthHandler : AuthorizationHandler<AdminOnlyAuth>
    {
        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context, 
            AdminOnlyAuth requirement)
        {
            if (!context.User.HasClaim(u=>u.Type == "isAdmin"))
                return Task.CompletedTask;

            var user = context.User.FindFirst(x => x.Type == "isAdmin").Value;
            if (int.Parse(user) == 1)
            {
                context.Succeed(requirement);

            }   
            return Task.CompletedTask;
        }
    }
}
