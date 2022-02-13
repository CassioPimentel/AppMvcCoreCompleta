﻿using Dev.App.Data;
using Dev.App.Extensions;
using Dev.Business.Interfaces;
using Dev.Data.Context;
using Dev.Data.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Globalization;

namespace Dev.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<AppDbContext>();
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<IFornecedorRepository, FornecedorRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();
            services.AddSingleton<IValidationAttributeAdapterProvider, MoedaValidationAttributeAdapterProvider>();

            return services;
        }
    }

    public static class MvcConfig
    {
        public static IServiceCollection AddMvcConfiguration(this IServiceCollection services)
        {
            services.AddMvc(o =>
            {
                o.ModelBindingMessageProvider.SetAttemptedValueIsInvalidAccessor((x, y) => "O valor preenchido não é válido para este campo.");
                o.ModelBindingMessageProvider.SetMissingBindRequiredValueAccessor(x => "Não foi fornecido um valor para o campo.");
                o.ModelBindingMessageProvider.SetMissingKeyOrValueAccessor(() => "Campo obrigatório.");
                o.ModelBindingMessageProvider.SetMissingRequestBodyRequiredValueAccessor(() => "É necessário que o body na requisição não esteja vazio.");
                o.ModelBindingMessageProvider.SetNonPropertyAttemptedValueIsInvalidAccessor((x) => "O valor preenchido não é válido.");
                o.ModelBindingMessageProvider.SetNonPropertyUnknownValueIsInvalidAccessor(() => "O valor fornecido é inválido.");
                o.ModelBindingMessageProvider.SetNonPropertyValueMustBeANumberAccessor(() => "O campo deve ser um número.");
                o.ModelBindingMessageProvider.SetUnknownValueIsInvalidAccessor((x) => "O valor fornecido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueIsInvalidAccessor((x) => "O valor fornecido é inválido para este campo.");
                o.ModelBindingMessageProvider.SetValueMustBeANumberAccessor(x => "Este campo deve ser um número.");
                o.ModelBindingMessageProvider.SetValueMustNotBeNullAccessor(x => "O valor nulo é inválido.");
            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            return services;
        }
    }

    public static class IdentityConfig
    {
        public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<IdentityUser>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }
    }

    public static class GlobalizationConfig
    {
        public static IApplicationBuilder UserGlobalizationConfig(this IApplicationBuilder app)
        {
            var defaultCulture = new CultureInfo("pt-BR");

            var localizationOptions = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(defaultCulture),
                SupportedCultures = new List<CultureInfo> { defaultCulture },
                SupportedUICultures = new List<CultureInfo> { defaultCulture }
            };

            app.UseRequestLocalization(localizationOptions);

            return app;
        }
    }
}