var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy("Prog3", builder =>
            {
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            }));
builder.Services.AddRouting(r => r.SuppressCheckForUnhandledSecurityMetadata = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("Prog3");

// app.Use((context,next)=>
// {
//     context.Items("__CorsMiddlewareInvoked")=true;
//     return next;
// });

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseStaticFiles();

app.UseRouting();

app.MapControllers();

app.Run();
