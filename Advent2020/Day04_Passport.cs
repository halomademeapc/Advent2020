using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day04_Passport : IPuzzleResult<string>
    {
        private readonly IEnumerable<Passport> passports;

        public Day04_Passport()
        {
            passports = Resources.Day4Values.Split("\r\n\r\n").Select(Parse); // passports separated by blank line
        }

        private static Passport Parse(string passportString) => new Passport(passportString
                .Replace(Environment.NewLine, " ") // treat line breaks and spaces the same
                .Split(" ", StringSplitOptions.RemoveEmptyEntries) // split into key value pairs
                .Select(s =>
                {
                    var segments = s.Split(':');
                    return (Field: segments.First(), Value: segments.Last());
                }) // parse out key:value format
                .ToDictionary(p => p.Field.ToEnum<PassportField>(), p => p.Value));

        public string GetResult() => @$"Checking fields only: {passports.Count(p => p.ContainsRequiredFields())}
Doing some validation: {passports.Count(p => p.IsValid())}";

        public enum PassportField
        {
            Unknown,
            [EnumValue("byr")] BirthYear,
            [EnumValue("iyr")] IssuanceYear,
            [EnumValue("eyr")] ExpirationYear,
            [EnumValue("hgt")] Height,
            [EnumValue("hcl")] HairColor,
            [EnumValue("ecl")] EyeColor,
            [EnumValue("pid")] PassportId,
            [EnumValue("cid")] CountryId
        }

        public class Passport : Dictionary<PassportField, string>
        {
            public Passport(Dictionary<PassportField, string> dict) : base(dict) { }

            public bool IsValid() => ContainsRequiredFields()
                && IsValidBirthYear
                && IsValidIssuanceYear
                && IsValidExpirationYear
                && IsValidHeight
                && IsValidHair
                && IsValidEye
                && IsValidId;

            public bool IsValidBirthYear => IsInRange(PassportField.BirthYear, 1920, 2002);

            public bool IsValidIssuanceYear => IsInRange(PassportField.IssuanceYear, 2010, 2020);

            public bool IsValidExpirationYear => IsInRange(PassportField.ExpirationYear, 2020, 2030);

            public bool ContainsRequiredFields()
            {
                var fields = Enum.GetValues(typeof(PassportField)).Cast<PassportField>();
                var missingFields = fields
                    .Where(f => !ContainsKey(f))
                    .Where(f => f != PassportField.Unknown) // ignore unknown
                    .Where(f => f != PassportField.CountryId); // don't care if this is this only missing one

                return !missingFields.Any();
            }

            private bool IsInRange(string value, int min, int max)
            {
                if (int.TryParse(value, out var number))
                {
                    return number >= min && number <= max;
                }
                return false;
            }

            private bool IsInRange(PassportField field, int min, int max) =>
                IsInRange(this[field], min, max);

            public bool IsValidHeight
            {
                get
                {
                    try
                    {
                        var height = this[PassportField.Height];
                        var unit = height.Substring(height.Length - 2, 2);
                        var value = height[0..^2];
                        return unit switch
                        {
                            "cm" => IsInRange(value, 150, 193),
                            "in" => IsInRange(value, 59, 76),
                            _ => false
                        };
                    }
                    catch
                    {
                        return false;
                    }
                }
            }

            public bool IsValidHair => Regex.IsMatch(this[PassportField.HairColor], @"^\#[abcdef0-9]{6}$");

            public bool IsValidEye
            {
                get
                {

                    var colors = new string[] { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                    return colors.Contains(this[PassportField.EyeColor]);
                }
            }

            public bool IsValidId => Regex.IsMatch(this[PassportField.PassportId], @"^\d{9}$");
        }
    }
}