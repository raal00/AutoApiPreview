using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BLL.DomainModels;
using BLL.Services;
using BLL.Services.Abstraction;
using DAL.Converters;
using DAL.Converters.Abstraction;
using DAL.PersistModels;
using DAL.Repository.Abstraction;
using DAL.Repository.Implementation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AutoApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option => {
                option.LoginPath = "/";
                option.Events = new CookieAuthenticationEvents()
                {
                    OnSigningIn = async context =>
                    {
                        var principal = context.Principal;
                        if (principal.HasClaim(x => x.Type == ClaimTypes.NameIdentifier))
                        {
                            if (principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value == "adminspec")
                            {
                                var claimIdentity = principal.Identity as ClaimsIdentity;
                                claimIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                            }
                        }
                        await Task.CompletedTask;
                    }
                };
            });
            services.AddAuthorization();
            services.AddControllers();

            services.AddTransient<IDomainToPersistConverter<AssociateLoginPersist, AssociateLoginDomain>, 
                                   DomainToPersistConverter<AssociateLoginPersist, AssociateLoginDomain>>();
            services.AddTransient<IDomainToPersistConverter<AssociatePersist, AssociateDomain>,
                                   DomainToPersistConverter<AssociatePersist, AssociateDomain>>();
            services.AddTransient<IDomainToPersistConverter<AssociateRolePersist, AssociateRoleDomain>,
                                   DomainToPersistConverter<AssociateRolePersist, AssociateRoleDomain>>();
            services.AddTransient<IDomainToPersistConverter<CarPersist, CarDomain>,
                                   DomainToPersistConverter<CarPersist, CarDomain>>();
            services.AddTransient<IDomainToPersistConverter<OrderPersist, OrderDomain>,
                                   DomainToPersistConverter<OrderPersist, OrderDomain>>();
            services.AddTransient<IDomainToPersistConverter<RolePersist, RoleDomain>,
                                   DomainToPersistConverter<RolePersist, RoleDomain>>();

            services.AddTransient<IPersistToDomainConverter<AssociateLoginPersist, AssociateLoginDomain>,
                                   PersistToDomainConverter<AssociateLoginPersist, AssociateLoginDomain>>();
            services.AddTransient<IPersistToDomainConverter<AssociatePersist, AssociateDomain>,
                                   PersistToDomainConverter<AssociatePersist, AssociateDomain>>();
            services.AddTransient<IPersistToDomainConverter<AssociateRolePersist, AssociateRoleDomain>,
                                   PersistToDomainConverter<AssociateRolePersist, AssociateRoleDomain>>();
            services.AddTransient<IPersistToDomainConverter<CarPersist, CarDomain>,
                                   PersistToDomainConverter<CarPersist, CarDomain>>();
            services.AddTransient<IPersistToDomainConverter<OrderPersist, OrderDomain>,
                                   PersistToDomainConverter<OrderPersist, OrderDomain>>();
            services.AddTransient<IPersistToDomainConverter<RolePersist, RoleDomain>,
                                   PersistToDomainConverter<RolePersist, RoleDomain>>();



            services.AddTransient<IAssociateLoginRepository<AssociateLoginDomain>, AssociateLoginRepository<AssociateLoginDomain>>();
            services.AddTransient<IAssociateRepository<AssociateDomain>, AssociateRepository<AssociateDomain>>();
            services.AddTransient<IAssociateRoleRepository<AssociateRoleDomain>, AssociateRoleRepository<AssociateRoleDomain>>();
            services.AddTransient<ICarRepository<CarDomain>, CarRepository<CarDomain>>();
            services.AddTransient<IOrderRepository<OrderDomain>, OrderRepository<OrderDomain>>();
            services.AddTransient<IRoleRepository<RoleDomain>, RoleRepository<RoleDomain>>();

            services.AddTransient<IAuthService, AuthService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
