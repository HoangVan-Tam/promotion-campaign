using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interface
{
    public interface IWinnersService
    {
        Task<FunctionResults<List<Dictionary<string, object>>>> PickWinnersAsync(PickWinnerModel option);
    }
}
