using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Advent2020
{
    public class Day2_PasswordAudit : IPuzzleResult
    {
        private readonly IEnumerable<PasswordEvaluation> Passwords;

        public Day2_PasswordAudit()
        {
            Passwords = Resources.Day2Values.Split(Environment.NewLine).Select(row => new PasswordEvaluation(row));
        }

        public object GetResult() => Passwords.Count(pass => pass.IsValid());

        public struct PasswordEvaluation
        {
            public char RequiredCharacter;
            public (int, int) Indexes;
            public string Password;

            public bool IsValid() => IndexesAreValid() && CharPositionsAreValid();

            private bool IndexesAreValid() => Password.Length >= Indexes.Item1 && Password.Length >= Indexes.Item2;

            private bool CharPositionsAreValid() => Password[Indexes.Item1] == RequiredCharacter ^ Password[Indexes.Item2] == RequiredCharacter;

            /// <summary>
            /// A password where exactly one instance of a specified character must exist in range
            /// </summary>
            public PasswordEvaluation(string serialized)
            {
                var serializationFormat = new Regex(@"^(\d+)-(\d+)\ (\w):\ (.+)$");
                var match = serializationFormat.Match(serialized);
                if (!match.Success)
                    throw new ArgumentException($"Unable to parse {serialized}");

                //provided indexes start at 1, not 0
                Indexes = (int.Parse(match.Groups[1].Value) - 1, int.Parse(match.Groups[2].Value) - 1);
                RequiredCharacter = match.Groups[3].Value[0];
                Password = match.Groups[4].Value;
            }
        }
    }
}
