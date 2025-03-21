﻿using AutoMapper;
using Azure;
using DAL.Interface;
using Entities.Constants;
using Entities.DTO;
using Entities.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Implement
{
    public class ContestService : IContestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ITransactionService _transactionService;
        private IConfiguration _config;
        public ContestService(IUnitOfWork unitOfWork, IMapper mapper, SqlConnection sqlConnection, IConfiguration config, ITransactionService transactionService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _config = config;
            _transactionService = transactionService;
        }

        public async Task<FunctionResults<List<ContestOverView>>> LoadAllContestAsync()
        {
            FunctionResults<List<ContestOverView>> response = new FunctionResults<List<ContestOverView>>();
            try
            {
                var lstContests = await _unitOfWork.Contest.GetAllAsync();
                if (lstContests.Count() > 0)
                {
                    response.Data = _mapper.Map<List<ContestOverView>>(lstContests);
                }
                else
                {
                    response.Message = "No Contests Found!";
                }
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }

        public async Task<FunctionResults<List<string>>> CreateNewContestAsync(NewContestInfomation newContestIfno)
        {
            await _unitOfWork.BeginEfTransactionAsync();
            FunctionResults<List<string>> response = new FunctionResults<List<string>>();
            try
            {
                var newContest = _mapper.Map<Contest>(newContestIfno);
                var contestUniqueCode = (newContest.StartDate.ToString("yyMMdd") + "_" + newContest.Keyword).ToUpper();
                newContest.ContestUniqueCode = contestUniqueCode;
                await _unitOfWork.Contest.InsertAsync(newContest);
                foreach (var col in newContestIfno.contestFields)
                {
                    col.ContestUniqueCode = contestUniqueCode;
                    var contestColumnDetail = _mapper.Map<ContestFieldDetails>(col);
                    contestColumnDetail.ContestID = newContest.ContestID;
                    await _unitOfWork.ContestFieldDetail.InsertAsync(contestColumnDetail);
                }
                await _unitOfWork.SaveAsync();
                await _unitOfWork.SQL.CreateContestTableAsync(contestUniqueCode, newContestIfno.contestFields, GlobalConstants.TYPETABLE.ENTRIES, _unitOfWork.CurrentEfTransaction);
                await _unitOfWork.SQL.CreateContestTableAsync(contestUniqueCode, null, GlobalConstants.TYPETABLE.LOG, _unitOfWork.CurrentEfTransaction);
                await _unitOfWork.SQL.CreateContestTableAsync(contestUniqueCode, null, GlobalConstants.TYPETABLE.WINNERS, _unitOfWork.CurrentEfTransaction);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                // Rollback transaction in case of an error
                await _unitOfWork.RollbackAsync();
                response.IsSuccess = false;
                response.Error = $"Error inserting entry: {ex.Message}";
            }
            return response;
        }

        public async Task<FunctionResults<object>> CheckDoesContestExist(string contestUniqueCode)
        {
            FunctionResults<object> response = new FunctionResults<object>();
            try
            {
                var contest = await _unitOfWork.Contest.FindAsync(p => p.ContestUniqueCode == contestUniqueCode);
                if (contest != null)
                {
                    response.Data = true;
                }
                else
                {
                    response.Data = false;
                }
            }
            catch (Exception ex)
            {
                response.Error = ex.Message;
                response.IsSuccess = false;
            }
            return response;
        }
    }
}
