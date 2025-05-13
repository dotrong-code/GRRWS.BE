using FirebaseAdmin;
using FluentValidation;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using GRRWS.Application.Common.Validator.UserVali;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.User.Login;
using GRRWS.Infrastructure.DTOs.User.Register;
using GRRWS.Infrastructure.DTOs.User.Update;
using GRRWS.Infrastructure.Implement.Repositories;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Host.Starup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "Keys",
                "koiveterinaryservicecent-925db-firebase-adminsdk-vus2r-bd418169a6.json");

            try
            {
                // Initialize Firebase with the credentials from the JSON file
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(credentialPath)
                });
            }
            catch (Exception ex)
            {
                // Log or handle the exception as necessary
                throw new Exception("Failed to initialize Firebase.", ex);
            }
            services.AddSingleton(StorageClient.Create(GoogleCredential.FromFile(credentialPath)));

            #region Validator
            services.AddTransient<IValidator<LoginRequest>, LoginValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddTransient<IValidator<UpdateUserRequest>, UpdateUserValidator>();
            #endregion

            #region Common
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Service
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<IFirebaseService, FirebaseService>();
            services.AddTransient<IUserService, UserService>();
            #endregion

            #region Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFirebaseRepository, FirebaseRepository>();
            services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
            #endregion

            #region GenericRepositories

            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            #endregion




            return services; // Ensure the IServiceCollection is returned
        }
    }
}
