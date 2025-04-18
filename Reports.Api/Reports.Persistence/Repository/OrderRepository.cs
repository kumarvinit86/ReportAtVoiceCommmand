using Microsoft.EntityFrameworkCore;
using Reports.Persistence.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reports.Persistence.Repository;

public class OrderRepository : IOrderRepository
{
    private readonly ReportsDbContext _context;
    public OrderRepository(ReportsDbContext reportsDbContext)
    {
        _context = reportsDbContext;
    }

    public List<Order> GetOrders(string query)
    {
        try
        {
            var result = _context.Orders.FromSqlRaw(query).ToList();
            if (result == null)
            {
                return new List<Order>();
            }
            else
            {
                return result;
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions as needed
            return new List<Order>();
        }
    }
}
