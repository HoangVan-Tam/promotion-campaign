﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IContestRepository : IGenericRepository<Contest>
    {
        Task<Contest> GetContestWithContestFieldDetailsAndRegexValidationsAsync(string contestUniqueCode);
    }
}
