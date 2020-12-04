using System;

namespace Advent2020
{
    public class EnumValueAttribute : Attribute
    {
        public string Value { get; private set; }
        public EnumValueAttribute(string value)
        {
            Value = value;
        }
    }
}
