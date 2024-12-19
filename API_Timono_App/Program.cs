
using System.Text;
using API_Timono_App.Repository.Abstraction;
using API_Timono_App.Repository.Implementation;
using API_Timono_App.Service;
using API_Timono_App.Validation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SharedData.Data;
using SharedData.Data.Models;

namespace API_Timono_App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped(typeof(IAuthRepository), typeof(AuthRepositoryImp));
            builder.Services.AddScoped(typeof(ISubjectRepository), typeof(SubjectRepositoryImp));
            builder.Services.AddScoped(typeof(IStudentRepository), typeof(StudentRepositoryImp));
            builder.Services.AddScoped(typeof(IProgressRepository), typeof(ProgressRepositoryImp));
            builder.Services.AddScoped(typeof(ITokenService), typeof(TokenServiceImp));
            builder.Services.AddScoped(typeof(IValidationService), typeof(ValidationService));

            builder.Services.AddDbContext<AppDbContext>(options =>
            {

            });
            // stop default filter
            builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("constr"));
            });

            //identity config

            builder.Services.AddIdentity<Account, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();

            //add middleware for cors
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", policy =>
                {
                    policy.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();

                });

            });

            builder.Services.AddAuthentication(options =>
            {
                //check JWT token in header
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; // mean use default
                //so if this token not valid return unauthorized response
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options => //check this token is verified
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = builder.Configuration["JWT:IssuerIP"],
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:AudianceIP"],

                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecreteKey"])),
                };

            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            //add middeleware
            app.UseStaticFiles();
            app.UseCors("MyPolicy");

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
