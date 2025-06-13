using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;

public class OpenAIService
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _assistantId;

    public OpenAIService(HttpClient httpClient, IOptions<OpenAIOptions> options)
    {
        _httpClient = httpClient;
        _apiKey = options.Value.ApiKey;
        _assistantId = options.Value.AssistantId;
    }

    public async Task<string> AskAssistantAsync(string userMessage)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", _apiKey);

        _httpClient.DefaultRequestHeaders.Add("OpenAI-Beta", "assistants=v2");

        // Step 1: Create a Thread
        var threadResponse = await _httpClient.PostAsync(
            "https://api.openai.com/v1/threads",
            new StringContent("{}", Encoding.UTF8, "application/json")
        );

        var threadJson = await threadResponse.Content.ReadAsStringAsync();
        var thread = JsonDocument.Parse(threadJson).RootElement;
        var threadId = thread.GetProperty("id").GetString();

        // Step 2: Add Message to Thread
        var messagePayload = JsonSerializer.Serialize(new
        {
            role = "user",
            content = userMessage
        });

        await _httpClient.PostAsync(
            $"https://api.openai.com/v1/threads/{threadId}/messages",
            new StringContent(messagePayload, Encoding.UTF8, "application/json")
        );

        // Step 3: Run Assistant
        var runPayload = JsonSerializer.Serialize(new { assistant_id = _assistantId });

        var runResponse = await _httpClient.PostAsync(
            $"https://api.openai.com/v1/threads/{threadId}/runs",
            new StringContent(runPayload, Encoding.UTF8, "application/json")
        );

        var runJson = await runResponse.Content.ReadAsStringAsync();
        var run = JsonDocument.Parse(runJson).RootElement;
        var runId = run.GetProperty("id").GetString();

        // Step 4: Poll Until Completion
        string status;
        do
        {
            await Task.Delay(1000);

            var poll = await _httpClient.GetAsync(
                $"https://api.openai.com/v1/threads/{threadId}/runs/{runId}"
            );

            var pollJson = await poll.Content.ReadAsStringAsync();
            var pollData = JsonDocument.Parse(pollJson).RootElement;
            status = pollData.GetProperty("status").GetString();
        } while (status != "completed");

        // Step 5: Fetch Messages
        var messagesResponse = await _httpClient.GetAsync(
            $"https://api.openai.com/v1/threads/{threadId}/messages"
        );

        var messagesJson = await messagesResponse.Content.ReadAsStringAsync();
        var messages = JsonDocument.Parse(messagesJson).RootElement;

        var content = messages
            .GetProperty("data")[0]
            .GetProperty("content")[0]
            .GetProperty("text")
            .GetProperty("value")
            .GetString();

        return content ?? "No response.";
    }
}
