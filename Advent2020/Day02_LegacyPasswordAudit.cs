using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2020
{
    [Obsolete]
    public class Day02_LegacyPasswordAudit : IPuzzleResult
    {
        private readonly IEnumerable<PasswordEvaluation> Passwords;

        public Day02_LegacyPasswordAudit()
        {
            Passwords = Resources.Day2Values.Split(Environment.NewLine).Select(row => new PasswordEvaluation(row));
        }

        public object GetResult() => Passwords.Count(pass => pass.IsValid());

        /// <summary>
        /// A password with minimum instances of a specific character
        /// </summary>
        /// <remarks>Based on incorrect requirement logic</remarks>
        public struct PasswordEvaluation
        {
            public char RequiredCharacter;
            public int MinimumOccurrences;
            public int MaximumOccurrences;
            public string Password;

            public bool IsValid()
            {
                var requiredChar = RequiredCharacter;
                var occurrences = Password.Count(@char => @char == requiredChar);
                return occurrences <= MaximumOccurrences && occurrences >= MinimumOccurrences;
            }

            public PasswordEvaluation(string serialized)
            {
                var serializationFormat = new Regex(@"^(\d+)-(\d+)\ (\w):\ (.+)$");
                var match = serializationFormat.Match(serialized);
                if (!match.Success)
                    throw new ArgumentException($"Unable to parse {serialized}");

                MinimumOccurrences = int.Parse(match.Groups[1].Value);
                MaximumOccurrences = int.Parse(match.Groups[2].Value);
                RequiredCharacter = match.Groups[3].Value[0];
                Password = match.Groups[4].Value;
            }
        }
    }
}
