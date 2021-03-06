using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using WebAPI.Helpers;
using ApplicationCore.Interfaces;
using ApplicationCore.Interfaces.Base;
using ApplicationCore.Interfaces.Services;
using ApplicationCore.Services;
using Infrastructure.Data;
using Infrastructure.Logging;
using Microsoft.AspNetCore.Http;
using ZNetCS.AspNetCore.Authentication.Basic;
using ZNetCS.AspNetCore.Authentication.Basic.Events;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using WebAPI.ViewModels;
using ApplicationCore.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Services;
using Audit.Core;
using Audit.PostgreSql.Configuration;

namespace WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthenticationCustom(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration.GetSection("TokenAuthentication:Issuer").Value,
                        ValidAudience = configuration.GetSection("TokenAuthentication:Audience").Value,
                        IssuerSigningKey = new SymmetricSecurityKey(Base64UrlEncoder.DecodeBytes(configuration.GetSection("TokenAuthentication:SecretKey").Value))
                    };
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy(JwtBearerDefaults.AuthenticationScheme, new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        public static void AddCorsCustom(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Constants.Cors,
                    builder =>
                    {
                        builder                                              
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()       
                            .AllowCredentials();
                    });
            });
        }

        public static void AddMvcCustom(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped(typeof(IRepository<,>), typeof(EfRepository<,>));
            services.AddScoped(typeof(IAsyncRepository<,>), typeof(EfRepository<,>));

            #region Serviços Domain
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserSetorService, UserSetorService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<ISetorService, SetorService>();
            services.AddScoped<IAssuntoService, AssuntoService>();
            services.AddScoped<IArquivoService, ArquivoService>();
            services.AddScoped<IParametroService, ParametroService>();
            services.AddScoped<IAssuntoArquivoService, AssuntoArquivoService>();
            services.AddScoped<IFluxoService, FluxoService>();
            services.AddScoped<IFluxoSituacaoService, FluxoSituacaoService>();
            services.AddScoped<IFluxoAcaoService, FluxoAcaoService>();
            services.AddScoped<IFluxoTipoAnexoService, FluxoTipoAnexoService>();
            services.AddScoped<IFluxoItemService, FluxoItemService>();
            services.AddScoped<IFluxoItemCheckListService, FluxoItemCheckListService>();
            services.AddScoped<IFluxoItemSetorService, FluxoItemSetorService>();
            services.AddScoped<IFluxoItemTipoAnexoService, FluxoItemTipoAnexoService>();
            services.AddScoped<ISituacaoService, SituacaoService>();
            services.AddScoped<IAcaoService, AcaoService>();
            services.AddScoped<ITipoAnexoService, TipoAnexoService>();
            services.AddScoped<IProcessoService, ProcessoService>();
            services.AddScoped<IProcessoAutorService, ProcessoAutorService>();
            services.AddScoped<IAutorService, AutorService>();
            services.AddScoped<ITramiteService, TramiteService>();
            services.AddScoped<ITramiteArquivoService, TramiteArquivoService>();
            services.AddScoped<IPaisService, PaisService>();
            services.AddScoped<IEstadoService, EstadoService>();
            services.AddScoped<IMunicipioService, MunicipioService>();
            services.AddScoped<IEnderecoService, EnderecoService>();
            services.AddScoped<ILogradouroService, LogradouroService>();
            services.AddScoped<IInsumoService, InsumoService>();
            services.AddScoped<ITipoInsumoService, TipoInsumoService>();
            services.AddScoped<IUnidadeMedidaService, UnidadeMedidaService>();
            services.AddScoped<IMarcaService, MarcaService>();
            services.AddScoped<IFornecedorService, FornecedorService>();
            services.AddScoped<IMonitoramentoBarragemService, MonitoramentoBarragemService>();
            services.AddScoped<IBarragemService, BarragemService>();
            services.AddScoped<INivelMonitoramentoService, NivelMonitoramentoService>();
            services.AddScoped<ISensorService, SensorService>();
            services.AddScoped<IConsultoriaService, ConsultoriaService>();
            services.AddScoped<ITipoMonitoramentoService, TipoMonitoramentoService>();
            #endregion

            #region Serviços Infra
            services.AddTransient(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddTransient<IEmailService, EmailService>();
            #endregion

            services.AddEntityFrameworkNpgsql()
            .AddDbContext<DaoContext>(
                options => options.UseNpgsql(
                    configuration.GetConnectionString("myapp")));

            Configuration.Setup()
            .UsePostgreSql(config => config
                .ConnectionString(configuration.GetConnectionString("myapp"))
                .TableName("Audit_Event")
                .Schema("SCA")
                .IdColumnName("Id")
                .DataColumn("Data", DataType.JSONB)
                .LastUpdatedColumnName("UpdatedDate"));

            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 3;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
            })
            .AddUserStore<UserStore<User, Role, DaoContext, long, IdentityUserClaim<long>, UserRole, IdentityUserLogin<long>, IdentityUserToken<long>, IdentityRoleClaim<long>>>()
            .AddRoleStore<RoleStore<Role, DaoContext, long, UserRole, IdentityRoleClaim<long>>>()
            .AddEntityFrameworkStores<DaoContext>()
            .AddDefaultTokenProviders();

            void MvcOptions(MvcOptions options)
            {
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }

            void MvcJsonOptions(MvcJsonOptions options)
            {
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }

            services.AddMvc(MvcOptions).AddJsonOptions(MvcJsonOptions);
        }
    }
}