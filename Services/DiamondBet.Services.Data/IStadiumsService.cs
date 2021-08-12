namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using DiamondBet.Web.ViewModels.Stadiums;

    public interface IStadiumsService
    {
        IEnumerable<StadiumsInListViewModel> GetAllStadiums();

        StadiumByIdViewModel GetById(int id);

        Task AddStadiumAsync(AddStadiumInputModel inputModel);

        Task EditStadiumAsync(EditStadiumInputModel inputModel, int id);

        EditStadiumInputModel GetStadiumDataForEdit(int id);

        Task DeleteStadiumAsync(int id);
    }
}
