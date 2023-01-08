using HorasRaras.Api.Filters;
using HorasRaras.Api.Handlers;
using HorasRaras.Api.Logs;
using HorasRaras.Api.Middlewares;
using HorasRaras.Domain.Contracts.Settings;
using HorasRaras.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

try 
{
    var builder = WebApplication.CreateBuilder(args);

    var connectionString = builder.Configuration.GetConnectionString("horas_raras_04");

    builder.Services.AddHttpContextAccessor();
    // Add services to the container.
    builder.Services.AddControllers();

    var emailConfig = builder.Configuration
            .GetSection("Smtp")
            .Get<EmailSettings>();
    builder.Services.AddSingleton(emailConfig);

    NativeInjectorBootStrapper.RegisterAppDependencies(builder.Services);
    NativeInjectorBootStrapper.RegisterAppDependenciesContext(builder.Services, connectionString);


    builder.Services.AddHttpContextAccessor();
    // Add services to the container.
    builder.Services.AddControllers();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddRouting(options => options.LowercaseUrls = true);

    #region Filters
    builder.Services
    .AddControllers(options =>
    {
        options.Filters.Add(typeof(ExceptionFilter));
        options.Filters.Add(typeof(ValidationFilter));

    })
    .AddJsonOptions(
        options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter())
    );

    #endregion

    #region Swagger

    builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Horas_RarasAPI", Version = "v1" });
        //Esse codigo foi adicionado para resolver ações conflitantes que é quando ações estão
        //usando a mesma rota
        options.ResolveConflictingActions(x => x.First());

        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
    });


    #endregion

    #region HttpContext

    builder.Services.AddHttpContextAccessor();

    #endregion

    #region JWT

    var _secretKey = builder.Configuration["SecretKey"];
    var secretKey = Encoding.ASCII.GetBytes(_secretKey);


    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
    {
        option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateIssuer = false,
            ValidateAudience = false

        };
    });

    #endregion

    #region Authorization

    builder.Services.AddAuthorization(options =>
    {
        options.InvokeHandlersAfterFailure = true;
    }).AddSingleton<IAuthorizationMiddlewareResultHandler, AuthorizationHandler>();

    #endregion

    builder.Services.AddHttpClient();
    SerilogExtension.AddSerilogApi(builder.Configuration);
    builder.Host.UseSerilog(Log.Logger);

    var app = builder.Build();

    app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseMiddleware<RequestSerilogMiddleware>();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }


    app.UseHttpsRedirection();

    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "encerrado inesperadamente");
}
finally
{
    Log.Information("servidor desligado...");
    Log.CloseAndFlush();
}

