var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configration = builder.Configuration;
ConfigureServices(builder.Services, configration);
var app = builder.Build();


app.UseRouting();
//app.UseCors("CorsPolicy");


// global cors policy
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials()); // allow credentials




var provider = new FileExtensionContentTypeProvider();
// Add new mappings
provider.Mappings[".apk"] = "application/vnd.android.package-archive";
provider.Mappings[".png"] = "image/png";
provider.Mappings[".jpeg"] = "image/jpeg";
provider.Mappings[".jpg"] = "image/jpg";



app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(
    Path.Combine(builder.Environment.ContentRootPath, "Assets")),
    //Path.Combine(Directory.GetCurrentDirectory(), @"Assets")),
    RequestPath = new PathString("/Assets"),
    ContentTypeProvider = provider
});

app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(
Path.Combine(builder.Environment.ContentRootPath, "Assets")),
    RequestPath = "/Assets"
});

//app.UseMyAuthMiddleware();

//app.UseUserStateMiddleware();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tree Ports Api v1");
});


app.UseHttpsRedirection();

//  app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<MessageHub>("/MessageHub");
});



app.Run();









void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
{
    //services.Configure<NotificationMetadata>(Configuration.GetSection("NotificationMetadata"));
    services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    services.AddDbContext<TreePortsDBContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("TreePortsDB")));
    AddDependancyInjectionServices(services);

    services.AddCors();
    services.AddDirectoryBrowser();
    services.AddSignalR();
    services.AddMemoryCache();

    services.AddHttpContextAccessor();
    services.AddAutoMapper(Assembly.GetExecutingAssembly());
    services.AddScoped(provider => new MapperConfiguration(cfg =>
    {
        cfg.AddProfile(new AutoMapperProfiles());
    }).CreateMapper());


    services.AddMvcCore().AddDataAnnotations();

    ConfigureSwagger(services);
}


void AddDependancyInjectionServices(IServiceCollection services)
{
    services.AddTransient<IUnitOfWork, UnitOfWork>();
    services.AddTransient<IMailService, MailService>();
    services.AddTransient<INotifyService, NotifyService>();
    services.AddTransient<IOrderService, OrderService>();
    services.AddTransient<IRMQService, RMQService>();
    services.AddHostedService<RMQReceiverService>();
}


void ConfigureSwagger(IServiceCollection services)
{
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Tree Ports API", Version = "v1" });
    });
}



