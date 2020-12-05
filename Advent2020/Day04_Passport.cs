using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;

namespace Advent2020
{
    public class Day04_Passport : IPuzzleResult<int>
    {
        private readonly IEnumerable<Passport> passports;

        public Day04_Passport()
        {
            passports = Resources.Day4Values.Split($"{Environment.NewLine}{Environment.NewLine}").Select(Passport.Parse); // passports separated by blank line
        }

        public int GetResult() => passports.Count(p => p.IsValid());

        public class Passport
        {
            [Required, Range(1920, 2002)]
            public int? BirthYear { get; set; }

            [Required, Range(2010, 2020)]
            public int? IssuanceYear { get; set; }

            [Required, Range(2020, 2030)]
            public int? ExpirationYear { get; set; }

            [Required, RegularExpression(@"^\d+(in|cm)$")]
            public string Height { get; set; }

            [Range(150, 193)]
            public int? HeightInCentimeters => ParseHeight("cm");

            [Range(59, 76)]
            public int? HeightInInches => ParseHeight("in");

            private int? ParseHeight(string unit) => !string.IsNullOrEmpty(Height) && Height.EndsWith(unit) && int.TryParse(Height.Substring(0, Height.Length - unit.Length), out var h)
                ? (int?)h
                : null;

            [Required, RegularExpression(@"^\#[abcdef0-9]{6}$")]
            public string HairColor { get; set; }

            [Required, RegularExpression(@"^(amb|blu|brn|gry|grn|hzl|oth)$")]
            public string EyeColor { get; set; }

            [Required, RegularExpression(@"^\d{9}$")]
            public string PassportId { get; set; }

            public string CountryId { get; set; }

            public static Passport Parse(string raw)
            {
                var passport = new Passport();
                var values = raw.Replace(Environment.NewLine, " ") // treat line breaks and spaces the same
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries) // split into key value pairs
                    .Select(s =>
                    {
                        var segments = s.Split(':');
                        return (Field: segments.First(), Value: segments.Last());
                    }) // parse out key:value format
                    .ToDictionary(p => p.Field, p => p.Value);

                SetInt("byr", p => p.BirthYear);
                SetInt("iyr", p => p.IssuanceYear);
                SetInt("eyr", p => p.ExpirationYear);
                SetString("hgt", p => p.Height);
                SetString("hcl", p => p.HairColor);
                SetString("ecl", p => p.EyeColor);
                SetString("pid", p => p.PassportId);
                SetString("cid", p => p.CountryId);

                return passport;

                void SetInt(string key, Expression<Func<Passport, int?>> property)
                {
                    if (values.ContainsKey(key) && int.TryParse(values[key], out var parsed))
                        passport.Set(property, parsed);
                }

                void SetString(string key, Expression<Func<Passport, string>> property)
                {
                    if (values.ContainsKey(key))
                        passport.Set(property, values[key]);
                }
            }
        }
    }
}