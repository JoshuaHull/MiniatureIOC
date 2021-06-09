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

namespace MiniatureIOC
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class MiniIOCDependencyAttribute: Attribute
    {
        public Lifetime Lifetime { get; }

        public Type? ConcreteType { get; }

        public Type? ServiceType { get; }

        public MiniIOCDependencyAttribute(
            Lifetime lifetime = Lifetime.Transient
        ) {
            Lifetime = lifetime;
        }

        public MiniIOCDependencyAttribute(
            Type serviceType,
            Lifetime lifetime = Lifetime.Transient
        ) {
            ServiceType = serviceType;
            Lifetime = lifetime;
        }

        public MiniIOCDependencyAttribute(
            Type serviceType,
            Type concreteType,
            Lifetime lifetime = Lifetime.Transient
        ) {
            ServiceType = serviceType;
            ConcreteType = concreteType;
            Lifetime = lifetime;
        }
    }
}
