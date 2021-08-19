namespace DiamondBet.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DiamondBet.Services.Data;
    using Microsoft.AspNetCore.Mvc;

    public class UsersController : BaseController
    {
        private readonly IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }

        public IActionResult ById(string id)
        {
            if (id == null)
            {
                return this.RedirectToAction("Error", "Home");
            }

            var model = this.usersService.GetById(id);

            return this.View(model);
        }
    }
}
