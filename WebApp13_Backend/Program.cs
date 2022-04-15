using Blazored.LocalStorage;
using Glory.Domain;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApp13_Backend;
using WebApp13_Backend.MVC_Filters;
using WebApp13_Backend.UoW;

var builder = WebApplication.CreateBuilder(args);

string dbPath = "kw13.db";
builder.Services.AddDbContext<ModelDbContext>(
    options => options.UseSqlite($"Data Source={dbPath}"));
JwtConfig jwtConfig = builder.Configuration
    .GetSection("JwtConfig")
    .Get<JwtConfig>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add<MyAuthorizationFilter>();
    options.Filters.Add<MyExceptionFilter>();
});

builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(jwtConfig.SigningKeyBytes),
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            RequireSignedTokens = true,
          
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidAudiences = new[] { jwtConfig.Audience },
            ValidIssuer = jwtConfig.Issuer
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddScoped(typeof(IRepository<ProductDTO>), typeof(EfRepository<ProductDTO>));

builder.Services.AddScoped<ICatalogRepository, CatalogRepository>();
builder.Services.AddScoped<CatalogService>();

builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<CartService>();

builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddSingleton(jwtConfig);
builder.Services.AddScoped<AccountService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();

builder.Services.AddCors();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpLogging(option  =>
    option.LoggingFields = HttpLoggingFields.ResponseHeaders | 
                           HttpLoggingFields.RequestHeaders | 
                           HttpLoggingFields.RequestBody |
                           HttpLoggingFields.ResponseBody);

var configSection = builder.Configuration.GetSection("SMTPUserData");
builder.Services.Configure<SMTPUserData>(configSection);
builder.Services.AddSingleton<IEmailSender, SMTPEmailSender>();

var app = builder.Build();

app.MapControllers();
app.UseCors(policy => policy
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(_ => true)
    .AllowCredentials());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<BackMiddleware>();
app.UseMiddleware<BrowserMiddleware>();
app.UseHttpLogging();

app.UseAuthentication();
app.UseAuthorization();

// app.Use(async (context, next) =>
// {
//     if (context.Request.Headers["sec-ch-ua"]
//         .Any( s => s.Contains("Edge") || s.Contains("Chrome")))
//     {
//         await next();
//     }
//     else
//     {
//         await context.Response.WriteAsync("Error browser");
//     }
// });

//app.MapGet("/products", async (CatalogService service) => 
//    await service.GetAll());
//
// app.MapPost("/addProduct/{name}/{price}", 
//     async (/*Product product -> при использовании PostAsJsonAsync(product)*/
//         CatalogService service, string name, int price) =>
//     await service.Add(name, price));
//
// app.MapPost("/addToCart/{id}",
//     async (CatalogService service, int id) =>
//         await service.AddToCart(id));
//
// app.MapPost("/deleteFromCart/{id}", async (CatalogService service, int id) =>
//     await service.DeleteFromCart(id));

app.Run();

