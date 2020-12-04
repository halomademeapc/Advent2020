using System;
using System.Collections.Generic;
using System.Linq;

namespace Advent2020
{
    public class Day04_Passport : IPuzzleResult<int>
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

        public int GetResult() => passports.Count(p => p.IsValid());

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

            public bool IsValid()
            {
                var fields = Enum.GetValues(typeof(PassportField)).Cast<PassportField>();
                var missingFields = fields
                    .Where(f => !ContainsKey(f))
                    .Where(f => f != PassportField.Unknown) // ignore unknown
                    .Where(f => f != PassportField.CountryId); // don't care if this is this only missing one

                return !missingFields.Any();
            }
        }
    }
}
