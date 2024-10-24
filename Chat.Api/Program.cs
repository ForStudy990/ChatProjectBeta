using System.Text;
using Microsoft.EntityFrameworkCore;
using Chat.Api.Context;
using Chat.Api.Helpers;
using Chat.Api.Managers;
using Chat.Api.Repositories;
using Chat.Api.Repositories.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ChatDb")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IChatRepository, ChatRepository>();
builder.Services.AddScoped<IUserChatRepository, UserChatRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<UserManager>();
builder.Services.AddScoped<ChatManager>();
builder.Services.AddScoped<JwtManager>();
builder.Services.AddScoped<MessageManager>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var jwtparam = builder.Configuration.GetSection("JwtParameters").Get<JwtParameters>();
    var key = Encoding.UTF32.GetBytes(jwtparam.Key);
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = jwtparam.Issuer,
        ValidAudience = jwtparam.Audience,
        ValidateIssuer = true,
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true
    };

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
app.UseAuthentication();

app.MapControllers();

app.Run();