//----------------------------------
//<copyright>FanClass</copyright>
//<author>zhuangjl</author>
//<email>3046524346@qq.com</email>
//<log date="2024-08-05">����</log>
//---------------------------------

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblies = AssemblyTool.Load(n => n.Equals("Api") || n.EndsWith("Application")).ToArray();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    foreach (var assembly in assemblies)
    {
        var xmlFileName = Path.Combine(AppContext.BaseDirectory, $"{assembly.GetName().Name}.xml");
        if (File.Exists(xmlFileName))
        {
            c.IncludeXmlComments(xmlFileName, true);
        }
    }
});

//��������
builder.Services.AddCors(options =>
    options.AddPolicy("AllowCors", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    }));

#region Autofacע��

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory(containerBuilder =>
{
    foreach (var assembly in assemblies)
    {
        containerBuilder.RegisterAssemblyTypes(assembly).
                AsImplementedInterfaces()
                .SingleInstance();
    }
})).ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterBuildCallback(container =>
    {
    });
});

#endregion Autofacע��

var app = builder.Build();

//����
app.UseCors("AllowCors");

// Configure the HTTP request pipeline.
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