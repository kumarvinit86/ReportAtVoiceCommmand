using Azure.AI.OpenAI;
using OpenAI.Chat;
using System.Text.Json;

namespace Reports.External;

public class QueryExtractor : IQueryExtractor
{
    public AzureOpenAIClient openAIClient;
    public QueryExtractor(AzureOpenAIClient openAIClient)
    {
        this.openAIClient = openAIClient;
    }

    public async Task<Query> ExtractQuery(string prompt)
    {
        try
        {
            ChatClient chatClient = openAIClient.GetChatClient("gpt-4o-mini");

            var requestOptions = new ChatCompletionOptions()
            {
                MaxOutputTokenCount = 4096,
                Temperature = 1.0f,
                TopP = 1.0f
            };

            List<ChatMessage> messages = new List<ChatMessage>()
            {
                new SystemChatMessage("You are a SQL query writer user will give you text " +
                "you must identify the Table name and write sql query as per the user need and return an " +
                "Table name is Orders with attribute [Id, OrderNumber, TotalAmount, Discription, OrderDate], " +
                "OrderDate is yyyy-mm-dd format." +
                "JSON object containing two property  SqlQuery and the table name. " +
                "Example {\"SqlQuery\":\"SELECT * FROM ORDER\", \"TableName\":\"ORDER\"}. " +
                "must parse the sql query it must not have any error. " +
                "must not use Limit key word instead you Top" +
                "must not add any special symbol which cause sql error "+
                "must add Id column in every query, it is default column, whether user asked for it or not "),
                new UserChatMessage(prompt),
            };

            var response = chatClient.CompleteChat(messages, requestOptions);

            if (response != null && response.Value != null && response.Value.Content != null)
            {
                var queryObject = response.Value.Content[0]?.Text;
                var query = JsonSerializer.Deserialize<Query>(queryObject);
                return query;
            }
            else
            {
                return new Query();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions as needed
            return new Query();

        }
    }
}
