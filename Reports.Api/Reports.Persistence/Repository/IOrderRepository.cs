using Reports.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Persistence.Repository;

public interface IOrderRepository
{
    List<Order> GetOrders(string query);
}
