using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using WarehouseManagement.DbContexts;
using WarehouseManagement.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using Microsoft.AspNetCore.Identity;
using System.Text;
using WarehouseManagement.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<WarehouseManagmentContext>(options =>
{
    options.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=WarehouseManagmentDB;Trusted_Connection=True;");
});


builder.Services.AddControllers(setupAction =>
{
    setupAction.ReturnHttpNotAcceptable = true;
    //setupAction.OutputFormatters.Add(
    //    new XmlDataContractSerializerOutputFormatter());
})
.AddNewtonsoftJson(setupAction =>
{
    setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
})
  .AddXmlDataContractSerializerFormatters()
  .ConfigureApiBehaviorOptions(setupAction =>
  {
      //  not valid áãÇ íßæä modelState åæä ãäÍÓä äÍÏÏ ÇÔ íÓÇæí Çá 
      setupAction.InvalidModelStateResponseFactory = context =>
      {
          // create a problem details object because The RFC implementation
          // for Problem Details when dealing with validation errors
          // is a validation problem details object
          var problemDetailsFactory = context.HttpContext.RequestServices
          .GetRequiredService<ProblemDetailsFactory>();
          //to get an instance of that we call CreateValidationProblemDetails
          // and we pass the current context and the model state
          var problemDetails = problemDetailsFactory.CreateValidationProblemDetails(
              context.HttpContext,
              context.ModelState);
          // this will thus translate the validation errors from the modelState
          // to the RFC Format

          // we can add additional info not added by default
          problemDetails.Detail = "See the errors field for details.";
          problemDetails.Instance = context.HttpContext.Request.Path; // as an instance link

          // next we need to choose the status code to use
          var actionExecutingContext =
                        context as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

          // because by default all are returned as 400 bad request
          // except when there is a validation error in the request body
          // so we check if ther are modelstate errors &&& all arguments were correctly found/parsed
          // we're dealing with validation errors
          if ((context.ModelState.ErrorCount > 0) &&
              (actionExecutingContext?.ActionArguments.Count == context.ActionDescriptor.Parameters.Count))
          {
              problemDetails.Type = "https://courselibrary.com/modelvalidationproblem";
              problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
              problemDetails.Title = "One or more validation errors occured.";

              return new UnprocessableEntityObjectResult(problemDetails)
              {
                  ContentTypes = { "application/problem+json" }
              };
          };

          // if one of the arguments wasn't correctly found / couldn't be parsed
          // we're dealing with null/unparseable input
          problemDetails.Status = StatusCodes.Status400BadRequest;
          problemDetails.Title = "One or more errors on input occured.";

          return new BadRequestObjectResult(problemDetails)
          {
              ContentTypes = { "application/problem+json" }
          };
      };
  });

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IWarehouseManagmentRepository, WarehouseManagmentRepository>();

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddIdentity<ApplicationUser,IdentityRole>().AddEntityFrameworkStores<WarehouseManagmentContext>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme= JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience= builder.Configuration["JWT:Audience"],
            IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
            ClockSkew=TimeSpan.Zero
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.CustomSchemaIds(type => type.ToString());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler(appBuilder =>
    {
        appBuilder.Run(async context =>
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsync("An unexpected fault happend. Try again");
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
