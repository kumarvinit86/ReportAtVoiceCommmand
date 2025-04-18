
namespace Reports.External;

public interface IQueryExtractor
{
    Task<Query> ExtractQuery(string prompt);
}
