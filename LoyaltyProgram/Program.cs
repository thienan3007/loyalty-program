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
using CorePush.Apple;
using CorePush.Google;
using Microsoft.AspNetCore.Authorization;
using LoyaltyProgram.Middlewares;
using LoyaltyProgram.Converters;
using LoyaltyProgram.Helpers;
using LoyaltyProgram.Handlers;
using LoyaltyProgram.Utils;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
var connectionStrings = configuration["ConnectionStrings:DefaultConnection"];
var appSettingsSection = configuration.GetSection("FcmNotification");
builder.Services.Configure<FcmNotificationSetting>(appSettingsSection);
builder.Services.AddDbContext<DatabaseContext>(option =>
{
    option.UseSqlServer(connectionStrings);
    option.UseTriggers(triggerOptions =>
    {
        triggerOptions.AddTrigger<AfterUpdateMembershipCurreny>();
        triggerOptions.AddTrigger<AfterAddMemberTier>();
    });
}); 

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

builder.Services.AddAuthorization(options =>
{
    //users must be logged in by default 
    options.DefaultPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

    //add policies for each permission, so that we can do [Authorize(Policy="CreateRecipePolicy")] on controllers
    //string[] permissions = Enum.GetNames<Permission>();

    //foreach (var permission in permissions)
    //{
    //    var policy = "";
    //    options.AddPolicy($"{policy}Permission",
    //        policy => policy.RequireClaim("permission", permission));
    //}
});

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    x.JsonSerializerOptions.Converters.Add(new DateConverter());
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddTransient<INotificationService, NotificationService>();
builder.Services.AddHttpClient<FcmSender>();
builder.Services.AddHttpClient<ApnSender>();

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

//builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddSingleton<IAuthorizationHandler, CardAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MemberCurrencyAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MembershipAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, MemberTierAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ListMemberTierAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, TransactionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ListTransactionAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, VoucherWalletAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, ListVoucherWalletAuthorizationHandler>();
builder.Services.AddSingleton<IAuthorizationHandler, NotificationAuthorizationHandler>();

builder.Services.AddCors();

builder.Services.AddScoped<BrandService, BrandServiceImpl>();
builder.Services.AddScoped<OrganizationService, OrganizationServiceImpl>();
builder.Services.AddScoped<LoyaltyProgramService, LoyaltyProgramServiceImpl>();
builder.Services.AddScoped<CurrencyService, CurrencyServiceImpl>();
builder.Services.AddScoped<MembershipService, MembershipServiceImpl>();
builder.Services.AddScoped<TierService, TierServiceImpl>();
builder.Services.AddScoped<MemberTierService, MemberTierServiceImpl>();
builder.Services.AddScoped<DeviceService, DeviceServiceImpl>();
builder.Services.AddScoped<VoucherDefinitionService, VoucherDefinitionServiceImpl>();
builder.Services.AddScoped<MemberReferrerLevelService, MemberReferrerLevelServiceImpl>();
builder.Services.AddScoped<MemberCurrencyService, MemberCurrencyServiceImpl>();
builder.Services.AddScoped<ActionService, ActionServiceImpl>();
builder.Services.AddScoped<EventSourceService, EventSourceServiceImpl>();
builder.Services.AddScoped<RewardService, RewardServiceImpl>();
builder.Services.AddScoped<ConditionRuleService, ConditionRuleServiceImpl>();
builder.Services.AddScoped<ConditionGroupService, ConditionGroupServiceImpl>();
builder.Services.AddScoped<OrderAmountConditionService, OrderAmountConditionServiceImpl>();
builder.Services.AddScoped<OrderItemConditionService, OrderItemConditionServiceImpl>();
builder.Services.AddScoped<VoucherWalletService, VoucherWalletServiceImpl>();
builder.Services.AddScoped<TransactionService, TransactionServiceImpl>();
builder.Services.AddScoped<CardService, CardServiceImpl>();
builder.Services.AddScoped<AuthService, AuthServiceImpl>();
builder.Services.AddScoped<AdminService, AdminServiceImpl>();
builder.Services.AddScoped<ProductService, ProductServiceImpl>();
builder.Services.AddScoped<ReferrerService, ReferrerServiceImpl>();
builder.Services.AddScoped<ISortHelper<Brand>, SortHelper<Brand>>();
builder.Services.AddScoped<ISortHelper<Card>, SortHelper<Card>>();
builder.Services.AddScoped<ISortHelper<ConditionGroup>, SortHelper<ConditionGroup>>();
builder.Services.AddScoped<ISortHelper<ConditionRule>, SortHelper<ConditionRule>>();
builder.Services.AddScoped<ISortHelper<Currency>, SortHelper<Currency>>();
builder.Services.AddScoped<ISortHelper<EventSource>, SortHelper<EventSource>>();
builder.Services.AddScoped<ISortHelper<LoyaltyProgram.Models.Program>, SortHelper<LoyaltyProgram.Models.Program>>();
builder.Services.AddScoped<ISortHelper<MemberReferrerLevel>, SortHelper<MemberReferrerLevel>>();
builder.Services.AddScoped<ISortHelper<Membership>, SortHelper<Membership>>();
builder.Services.AddScoped<ISortHelper<MembershipCurrency>, SortHelper<MembershipCurrency>>();
builder.Services.AddScoped<ISortHelper<MemberTier>, SortHelper<MemberTier>>();
builder.Services.AddScoped<ISortHelper<OrderAmountCondition>, SortHelper<OrderAmountCondition>>();
builder.Services.AddScoped<ISortHelper<OrderItemCondition>, SortHelper<OrderItemCondition>>();
builder.Services.AddScoped<ISortHelper<Organization>, SortHelper<Organization>>();
builder.Services.AddScoped<ISortHelper<Reward>, SortHelper<Reward>>();
builder.Services.AddScoped<ISortHelper<Tier>, SortHelper<Tier>>();
builder.Services.AddScoped<ISortHelper<Transaction>, SortHelper<Transaction>>();
builder.Services.AddScoped<ISortHelper<VoucherDefinition>, SortHelper<VoucherDefinition>>();
builder.Services.AddScoped<ISortHelper<VoucherWallet>, SortHelper<VoucherWallet>>();

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
app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<JwtMiddleware>();

//app.UseMiddleware<AuthContextMiddleware>();

app.MapControllers();

app.Run();
