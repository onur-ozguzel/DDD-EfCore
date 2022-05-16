using CSharpFunctionalExtensions;
using DddAndEfCore.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DddAndEfCore.School
{
    public class Email : ValueObject
    {
        public string Value { get; set; }
        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Create(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return Result.Failure<Email>("Email should not be empty");

            email = email.Trim();

            if (email.Length > 200) return Result.Failure<Email>("Email is too long");
            if (!Regex.IsMatch(email, @"^(.+)@(.+)$")) return Result.Failure<Email>("Email is invald");

            return Result.Success(new Email(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public static implicit operator string(Email email)
        {
            return email.Value;
        }
    }
}
