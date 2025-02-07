
using System.Text.Json.Serialization;
using DoctorTime.API.Context;
using DoctorTime.API.Repository;
using DoctorTime.API.Repository.Interfaces;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using DoctorTime.API.Security.Interfaces;
using DoctorTime.API.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using DoctorTime.API.Services.Interfaces;
using DoctorTime.API.Services;
using DoctorTime.API.DTO.Mapping;


namespace DoctorTime.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string variable = Environment.GetEnvironmentVariable("DATABASE_URL");
            Console.WriteLine("Variable: ", variable);
            builder.Services.AddControllers().AddJsonOptions(opt => opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(
                            swagger =>
                            {
                                swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "Dr.Time", Description = "Dr.Time is an API for Appointment Management", Version = "v1" });
                                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                                {
                                    Name = "Authorization",
                                    Type = SecuritySchemeType.ApiKey,
                                    Scheme = "Bearer",
                                    In = ParameterLocation.Header,
                                    Description = "This is a JWT Bearer Token Authentication, Insert your Token with 'Bearer' at the begininng \\\n" +
                                    "Example: Bearer ywowknqwqytt12816gstar",
                                });
                                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                          new OpenApiSecurityScheme
                          {
                              Reference = new OpenApiReference
                              {
                                  Type = ReferenceType.SecurityScheme,
                                  Id = "Bearer"
                              }
                          },
                         new string[] {}
                    }
                     });
                            }
                 );



            string connection = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<PostgreSQL>(opt => opt.UseNpgsql(variable ?? connection));

            builder.Services.AddScoped<IAuthenticateService, AuthenticateService>();
            builder.Services.AddScoped<ILoginService, LoginService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IWorkerService, WorkerService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();

            builder.Services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(
                opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["jwt:issuer"],
                        ValidAudience = builder.Configuration["jwt:audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                        .GetBytes(builder.Configuration["jwt:secretkey"])),
                        RoleClaimType = "role"
                    };
                    opt.MapInboundClaims = false;
                }
                );

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddAutoMapper(typeof(DTOMapping));
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            //{
            //    app.UseSwagger();
            //    app.UseSwaggerUI();
            //}

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PostgreSQL>();
                dbContext.Database.Migrate();
            }
            builder.Services.AddCors(opt =>
            {
                opt.AddPolicy("AllowedList", policy =>
                {

                    policy.WithOrigins("http://localhost:4200", "https://drtime.vercel.app")
                    .AllowAnyMethod()
                     .AllowAnyHeader();
                    //.AllowCredentials();


                });
            }
                );
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors("AllowedList");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();



            app.MapControllers();


            app.Run();
        }
    }
}
