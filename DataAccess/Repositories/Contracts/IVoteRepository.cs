﻿using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Contracts
{
    public interface IVoteRepository
    {
        Task AddVoteAsync(Vote vote);
        Task<bool> UserAlreadyVotedAsync(string userId, int electionId);

	}
}
