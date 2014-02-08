using Domain;
using KenKenBuilder;
using NUnit.Framework;

namespace KenKenBuilderTests
{
    [TestFixture]
    public class KenKenGroupGeneratorTests
    {
        [Test]
        public void TestAThing()
        {
            var builder = new KenKenPuzzleBuilder();
            var puzzle = builder.Build(DifficultyLevel.Easy, GridSize.FiveByFive);
        }
    }
}
