using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.VisualBasic.CompilerServices;

namespace ApiServer.Config
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string Moderator = "Moderator";
        public const string BasicUser = "BasicUser";
    }
}
