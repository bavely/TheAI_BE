
using HotChocolate;
using THEAI_BE.Services;
namespace THEAI_BE.GraphQL.Queries{
[ExtendObjectType(Name = "Query")]    
public class ChatQuery
    {
        [GraphQLName("askOpenAI")]
        public async Task<string> AskOpenAIAsync(
            [Service] OpenAIService openAIService,
            string input)
        {
            return await openAIService.AskAssistantAsync(input);
        }
        [GraphQLName("askGemini")]
        public async Task<string> AskGeminiAsync(
                [Service] GeminiService geminiService,
                string input)
        {
            return await geminiService.AskGeminiAsync(input);
        }



    }
}