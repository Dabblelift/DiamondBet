namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using DiamondBet.Web.ViewModels.Users;

    public interface IUsersService
    {
        UserByIdViewModel GetById(string id);

        Task ResetUsersCoinsAsync();
    }
}
