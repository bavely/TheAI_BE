// Program.cs
// Place ALL using directives at the very top of the file.
// Remove duplicates and ensure correct ones are present.

// Standard .NET Core usings
using Microsoft.EntityFrameworkCore;
using THEAI_BE.Data;
using THEAI_BE.Services; // Assuming this contains OpenAIService and GeminiService
using THEAI_BE.Data;
using THEAI_BE.Mutation;
// HotChocolate specific usings
using HotChocolate.AspNetCore; // For .MapGraphQL and AddGraphQLServer
using HotChocolate.Execution.Configuration; // Often used for ModifyRequestOptions or other configurations
using THEAI_BE.GraphQL.Queries; // To reference ChatQuery and UserQuery
using Microsoft.IdentityModel.Tokens;

// using THEAI_BE.GraphQL.Types; // Uncomment if you are using specific types directly here

// Ensure you don't have a 'using GraphQL;' here if you're using HotChocolate.
// Also, remove duplicate 'using Microsoft.EntityFrameworkCore;'

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.Authority = "https://dev-agu00qti8bfvpoxi.us.auth0.com";
        options.Audience = "https://dev-agu00qti8bfvpoxi.us.auth0.com/api/v2/";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization();



builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:3000") // Allow your React dev server
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Optional, if using cookies or Auth0 silent refresh
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<OpenAIOptions>(builder.Configuration.GetSection("OpenAI"));
builder.Services.Configure<GeminiOptions>(builder.Configuration.GetSection("Gemini"));

builder.Services.AddHttpClient<OpenAIService>();
builder.Services.AddHttpClient<GeminiService>();

builder.Services.AddScoped<OpenAIService>();
builder.Services.AddScoped<GeminiService>();

builder.Services
    .AddGraphQLServer()
    .AddQueryType(d => d.Name("Query")) // root query
    .AddTypeExtension<UserQuery>()      // extension query
    .AddTypeExtension<ChatQuery>()
    .AddMutationType<UserMutation>()      // extension query
    .AddProjections()
    .AddFiltering()
    .AddSorting()
    .ModifyRequestOptions(o => o.IncludeExceptionDetails = true);

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting(); // Optional in minimal API, but needed if present
app.UseCors("AllowFrontend");
app.MapGraphQL("/graphql");

app.Run();