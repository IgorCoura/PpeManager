using Microsoft.EntityFrameworkCore;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc(config =>
{
    config.Filters.Add<NotificationFilter>();   
});


builder.Services.AddDbContext<PpeManagerContext>(options =>
options.UseNpgsql("Server=192.168.15.34;Port=5432;Database=ppemanager;User Id=admin;Password=mysecretpassword;",
npgsqlOptionsAction: sqlOptions =>
{
    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorCodesToAdd: null);
}

), ServiceLifetime.Scoped);




// Call UseServiceProviderFactory on the Host sub property 
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Call ConfigureContainer on the Host sub property 
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
{
    builder.RegisterModule(new ApplicationModule());
    builder.RegisterModule(new MediatorModule());
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
