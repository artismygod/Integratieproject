using IntegratieProject.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IntegratieProject.Startup))]
namespace IntegratieProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        private void createRolesandUsers()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            // In Startup iam creating first SuperAdmin Role and creating a default SuperAdmin User    
            if (!roleManager.RoleExists("SuperAdmin"))
            {

                // first we create SuperAdmin role   
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "SuperAdmin";
                roleManager.Create(role);

                //Here we create a SuperAdmin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.UserName = "Sebastian";
                user.Email = "sebastian.cox@student.kdg.be";

                string userPWD = "Admin123+";

                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role SuperAdmin   
                if (chkUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "SuperAdmin");

                }
            }

            // creating Creating Admin role    
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);

            }
        }
    }
}