﻿using PpeManager.Domain.AggregatesModel.AggregatePpe;
using PpeManager.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PpeManager.Infrastructure.Repositories
{
    public class PpeRepository : TempRepository<Ppe>, IPpeRepository
    {
        public IUnitOfWork UnitOfWork => throw new NotImplementedException();
    }
}
