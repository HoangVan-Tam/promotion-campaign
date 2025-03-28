using AutoMapper;
using DAL.Interface;
using Entities.DTO;
using Entities.Models;
using Services.Helpers;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class WinnersService : IWinnersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WinnersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<FunctionResults<List<Dictionary<string, object>>>> PickWinnersAsync(PickWinnerModel option)
        {
            await _unitOfWork.BeginEfTransactionAsync();
            FunctionResults<List<Dictionary<string, object>>> response = new FunctionResults<List<Dictionary<string, object>>>();
            try
            {
                var winners = new List<WinnerModel>();
                var entriesTableName = "BC_" + option.ContestUniqueCode;
                var pickedEntries = await _unitOfWork.SQL.PickWinners(entriesTableName, option, _unitOfWork.CurrentEfTransaction);

                var winnerTableName = "BC_" + option.ContestUniqueCode + "_Winners";
                var tableWinnerColumns = await _unitOfWork.SQL.GetTableColumnsAsync(winnerTableName, _unitOfWork.CurrentEfTransaction);
                if (pickedEntries.Any())
                {
                    foreach (var entry in pickedEntries)
                    {
                        var winner = new WinnerModel()
                        {
                            DateWon = DateTime.UtcNow,
                            EntryID = Convert.ToInt32(entry["EntryID"]?.ToString()),
                            GroupName = option.GroupName,
                            MobileNo = entry["MobileNumber"]?.ToString()
                        };
                        await _unitOfWork.SQL.InsertWinnersAsync(winnerTableName, winner.ToDictionary(), tableWinnerColumns.Where(p => p.ColumnName != "WinnerID").ToList(), _unitOfWork.CurrentEfTransaction);
                        var data = await _unitOfWork.SQL.FindDataAsync(winnerTableName, new Dictionary<string, object> { { "EntryID", winner.EntryID } }, tableWinnerColumns, _unitOfWork.CurrentEfTransaction);
                        response.Data = data;
                    }
                }
                else
                {
                    response.Message = "No Entries Found!";
                }
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                response.IsSuccess = false;
                response.Error = ex.Message;
            }
            return response;
        }
    }
}
