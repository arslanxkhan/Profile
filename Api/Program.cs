using Api;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Data.Services.BaseServices;
using Database;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCors(opt =>
{
    opt.AddPolicy("AllowAllOrigin", x =>
    {
        x.AllowAnyHeader();
        x.AllowAnyMethod();
        x.WithOrigins("http://localhost:4200");
        x.AllowCredentials();
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("con"), o => o.EnableRetryOnFailure());
});



builder.Services.Configure<AppSettingModel>(Configuration);

// auto mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


//auto fac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(
   builder =>
   {
       builder.RegisterType<BaseService>().PropertiesAutowired();
       builder.RegisterAssemblyTypes(Assembly.Load("Data")).Where(z => z.Name.EndsWith("Service")).AsImplementedInterfaces().PropertiesAutowired();
       builder.RegisterGeneric(typeof(Repository<,>)).As(typeof(IRepository<,>)).InstancePerLifetimeScope();
       builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().PropertiesAutowired().InstancePerLifetimeScope();


       var mapper = AutoMapperInit.InitMappings();
       builder.RegisterInstance(mapper).SingleInstance();
   });


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<ApplicationDbContext>();

    if (dbContext.Database.GetPendingMigrations().Any())
    {
        dbContext.Database.Migrate();
    }
}



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();

}
else
{
    // Add security headers middleware in production
    // app.UseMiddleware<SecurityHeadersMiddleware>();
}

app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();

app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseCors("AllowAllOrigin");


app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
