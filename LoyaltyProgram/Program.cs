using LoyaltyProgram.Areas.Admin;
using LoyaltyProgram.Auth;
using LoyaltyProgram.Models;
using LoyaltyProgram.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var connectionStrings = configuration["ConnectionStrings:DefaultConnection"];
builder.Services.AddDbContext<DatabaseContext>(option => option.UseSqlServer(connectionStrings));

//builder.Services.AddDbContext<DatabaseContext>(option => option.UseLazyLoadingProxies().UseSqlServer(connectionStrings));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 
        ClockSkew = TimeSpan.Zero,

        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddApiVersioning(o =>
{
    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    //o.ApiVersionReader = ApiVersionReader.Combine(
    //    new QueryStringApiVersionReader("api-version"),
    //    new HeaderApiVersionReader("X-Version"),
    //    new MediaTypeApiVersionReader("ver"));
});

builder.Services.AddVersionedApiExplorer(setup =>
{
    setup.GroupNameFormat = "'v'VVV";
    setup.SubstituteApiVersionInUrl = true;
});

//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Loyalty API", Version = "v1" });
//});

builder.Services.AddSwaggerGen();

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();




builder.Services.AddCors();

builder.Services.AddScoped<BrandService, BrandServiceImpl>();
builder.Services.AddScoped<OrganizationService, OrganizationServiceImpl>();
builder.Services.AddScoped<LoyaltyProgramService, LoyaltyProgramServiceImpl>();
builder.Services.AddScoped<CurrencyService, CurrencyServiceImpl>();
builder.Services.AddScoped<MembershipService, MembershipServiceImpl>();
builder.Services.AddScoped<TierService, TierServiceImpl>();
builder.Services.AddScoped<MemberTierService, MemberTierServiceImpl>();
builder.Services.AddScoped<VoucherDefinitionService, VoucherDefinitionServiceImpl>();
builder.Services.AddScoped<MemberReferrerLevelService, MemberReferrerLevelServiceImpl>();
builder.Services.AddScoped<MemberCurrencyService, MemberCurrencyServiceImpl>();
builder.Services.AddScoped<ActionService, ActionServiceImpl>();
builder.Services.AddScoped<EventSourceService, EventSourceServiceImpl>();
builder.Services.AddScoped<RewardService, RewardServiceImpl>();
builder.Services.AddScoped<ConditionRuleService, ConditionRuleServiceImpl>();
builder.Services.AddScoped<ConditionGroupService, ConditionGroupServiceImpl>();
builder.Services.AddScoped<OrderAmountConditionService, OrderAmountConditionServiceImpl>();
builder.Services.AddScoped<VoucherWalletService, VoucherWalletServiceImpl>();
builder.Services.AddScoped<TransactionService, TransactionServiceImpl>();
builder.Services.AddScoped<CardService, CardServiceImpl>();
builder.Services.AddScoped<AuthService, AuthServiceImpl>();

var app = builder.Build();

var provider = builder.Services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

// Configure the HTTP request pipeline.
app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowed((host) => true).AllowCredentials());

app.UseStaticFiles();

app.UseRouting();

//app.UseSwagger(options =>
//{
//    options.SerializeAsV2 = true;
//});

app.UseSwagger();
//app.UseSwaggerUI(c =>
//{ 
//    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loyalty API V1");
//    c.RoutePrefix = string.Empty;
//});

app.UseSwaggerUI(options =>
{
    foreach (var description in provider.ApiVersionDescriptions)
    {
        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        options.RoutePrefix = string.Empty;
    }
});

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
