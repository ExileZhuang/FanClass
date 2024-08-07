///----------------------------------
///<copyright>FanClass</copyright>
///<author>zhuangjl</author>
///<email>3046524346@qq.com</email>
///<log date="2024-08-05">¥¥Ω®</log>
///---------------------------------

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

foreach (var assembly in AssemblyTool.Load(s => s.Equals("Api")))
{
    Console.WriteLine(assembly.FullName);
}

///øÁ”Ú…Ë÷√
builder.Services.AddCors(options =>
    options.AddPolicy("AllowCors", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    }));

var app = builder.Build();

///øÁ”Ú
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