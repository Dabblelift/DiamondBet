namespace DiamondBet.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public interface ISelectItemsService
    {
        public IEnumerable<KeyValuePair<string, string>> GetAllTeamsNames();

        public IEnumerable<KeyValuePair<string, string>> GetAllCountriesNames();

        public IEnumerable<KeyValuePair<string, string>> GetAllCompetitionsNames();

        public IEnumerable<KeyValuePair<string, string>> GetAllStadiumsNames();
    }
}
