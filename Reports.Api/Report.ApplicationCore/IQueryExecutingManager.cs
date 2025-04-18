using Reports.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Application
{
    public interface IQueryExecutingManager
    {
        Task<List<Order>> GetReport(string userCommand);
    }
}
