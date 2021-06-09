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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace MiniatureIOC.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMiniIOCDependenciesFromAssembliesContaining(
            this IServiceCollection services, params Type[] types
        ) {
            if (types == null) return services;
            if (types.Length == 0) return services;

            var typeRegistrations = types
                .Select(Assembly)
                .SelectMany(Types)
                .Where(HaveMiniIOCAttribute)
                .Select(concreteType => new {
                    concreteType,
                    attributes = concreteType.GetCustomAttributes(typeof(MiniIOCDependencyAttribute), true),
                });

            foreach (var typeRegistration in typeRegistrations)
                foreach (MiniIOCDependencyAttribute attribute in typeRegistration.attributes)
                    attribute.Lifetime.InvokeServiceMethod(
                        services,
                        attribute.ServiceType ?? typeRegistration.concreteType,
                        attribute.ConcreteType ?? typeRegistration.concreteType
                    );

            return services;
        }

        private static Func<Type, Assembly> Assembly =>
            type => type.Assembly;

        private static Func<Assembly, IEnumerable<Type>> Types =>
            assembly => assembly.GetTypes();

        private static Func<Type, bool> HaveMiniIOCAttribute =>
            type => type.GetCustomAttributes(typeof(MiniIOCDependencyAttribute), true).Length > 0;
    }
}
