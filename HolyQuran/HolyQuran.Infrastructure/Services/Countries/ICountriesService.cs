using HolyQuran.Core.Dtos;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModels;
using HolyQuran.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HolyQuran.Infrastructure.Services.Countries
{
    public interface ICountriesService

    {
        PagingAPIViewModel GetAll(int page);
        Task CreateDefaultData();
       
    }
}
