/*
    GPL 3.0 License

    MiniatureIOC - Miniature Inversion of Control for DotNet

    Copyright (C) 2021 Joshua Hull

    This program is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace MiniatureIOC
{
    public enum Lifetime
    {
        Transient,
        Scoped,
        Singleton,
    }

    public static class LifetimeExtensions
    {
        public static string GetAddServiceMethodName(this Lifetime serviceType) => serviceType switch
        {
            Lifetime.Transient => nameof(ServiceCollectionServiceExtensions.AddTransient),
            Lifetime.Scoped => nameof(ServiceCollectionServiceExtensions.AddScoped),
            Lifetime.Singleton => nameof(ServiceCollectionServiceExtensions.AddSingleton),
            _ => throw new NotImplementedException(),
        };

        public static MethodInfo? GetServiceMethod(this Lifetime serviceType)
            => typeof(ServiceCollectionServiceExtensions).GetMethod(
                serviceType.GetAddServiceMethodName(),
                new[] {typeof(IServiceCollection), typeof(Type), typeof(Type)}
            );

        public static object? InvokeServiceMethod(
            this Lifetime lifetime,
            IServiceCollection services,
            Type serviceType,
            Type concreteType
        ) {
            return lifetime.GetServiceMethod()?.Invoke(
                services, new object[] { services, serviceType, concreteType }
            );
        }
    }
}
