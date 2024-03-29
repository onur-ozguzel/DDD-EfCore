﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DddAndEfCore.Common
{
    public abstract class Entity
    {
        public Guid Id { get; }

        protected Entity()
        {

        }

        protected Entity(Guid id) : this()
        {
            Id = id;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Entity other)) return false;
            if (ReferenceEquals(this, other)) return true;
            if (GetRealType() != other.GetRealType()) return false;
            if (Id == Guid.Empty || other.Id == Guid.Empty) return false;

            return Id == other.Id;
        }

        public static bool operator ==(Entity a, Entity b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;

            return a.Equals(b);
        }

        public static bool operator !=(Entity a, Entity b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            return (GetRealType().ToString() + Id).GetHashCode();
        }

        private Type GetRealType()
        {
            Type type = GetType();
            if (type.ToString().Contains("Castle.Proxies.")) return type.BaseType;

            return type;
        }
    }
}
