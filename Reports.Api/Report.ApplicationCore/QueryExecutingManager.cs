using Reports.External;
using Reports.Persistence.Entities;
using Reports.Persistence.Repository;

namespace Reports.Application
{
    public class QueryExecutingManager : IQueryExecutingManager
    {
        private readonly IQueryExtractor queryExtractor;
        private readonly IOrderRepository orderRepository;
        public QueryExecutingManager(IQueryExtractor queryExtractor, IOrderRepository orderRepository) {
            this.queryExtractor = queryExtractor;
            this.orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetReport(string userCommand)
        {
            var query = await queryExtractor.ExtractQuery(userCommand);

            if (query != null && query.SqlQuery!=null)
            {
                var result = orderRepository.GetOrders(query.SqlQuery);
                return result;
            }
            return new List<Order>();
        }

    }
}
