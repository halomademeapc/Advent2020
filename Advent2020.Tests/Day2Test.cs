using Xunit;

namespace Advent2020.Tests
{
    public class Day2Test
    {
        [Fact]
        public void Validate_Example_Cases()
        {
            var ex1 = new Day2_PasswordAudit.PasswordEvaluation("1-3 a: abcde");
            Assert.True(ex1.IsValid());
            var ex2 = new Day2_PasswordAudit.PasswordEvaluation("1-3 b: cdefg");
            Assert.False(ex2.IsValid());
            var ex3 = new Day2_PasswordAudit.PasswordEvaluation("2-9 c: ccccccccc");
            Assert.False(ex3.IsValid());
        }
    }
}
