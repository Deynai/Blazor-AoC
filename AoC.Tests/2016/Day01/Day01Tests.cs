using NUnit.Framework;
using AoC.Code.Solutions;
using AoC.Code.Solutions._2016;
using AoC.Code.Solutions.Shared;
using System.Threading.Tasks;
using System.Threading;

namespace AoC.Tests
{
    public class Day01Tests
    {
        private CancellationTokenSource _cts;
        private CancellationToken _token;

        [SetUp]
        public void Setup()
        {
            _cts = new CancellationTokenSource();
            _token = _cts.Token;
        }

        [Test]
        public async Task Day01_Created_And_Runs()
        {
            Solution soln = new Day01("");

            var p1 = await soln.GetPart1(_token);
            var p2 = await soln.GetPart2(_token);

            Assert.Pass();
        }

        [Test]
        public async Task InitialTransform_IsOrigin_AndFacingNorth()
        {
            Day01 soln = new Day01("");

            Assert.AreEqual(Int2.Zero, soln.Position);
            Assert.AreEqual(Int2.Up, soln.Direction);
        }

        [Test]
        public void ExecuteTurn()
        {
            Day01 soln = new Day01("");

            soln.TurnLeft();
            Assert.AreEqual(Int2.Left, soln.Direction);
            soln.TurnLeft();
            Assert.AreEqual(Int2.Down, soln.Direction);
            soln.TurnLeft();
            Assert.AreEqual(Int2.Right, soln.Direction);
            soln.TurnLeft();
            Assert.AreEqual(Int2.Up, soln.Direction);

            soln.TurnRight();
            Assert.AreEqual(Int2.Right, soln.Direction);
            soln.TurnRight();
            Assert.AreEqual(Int2.Down, soln.Direction);
            soln.TurnRight();
            Assert.AreEqual(Int2.Left, soln.Direction);
            soln.TurnRight();
            Assert.AreEqual(Int2.Up, soln.Direction);
        }

        [Test]
        public void ExecuteMove()
        {
            Day01 soln = new Day01("");

            soln.Move(5);
            Assert.AreEqual(new Int2(0,5), soln.Position);

            soln.TurnLeft();
            soln.Move(7);
            Assert.AreEqual(new Int2(-7,5), soln.Position);

            soln.TurnRight();
            soln.TurnRight();
            soln.Move(9);
            Assert.AreEqual(new Int2(2,5), soln.Position);
        }

        [Test]
        public void ExecuteSingleCommand_IsCorrectResult()
        {
            Day01 soln = new Day01("");

            soln.ExecuteSingleCommand("L5");
            Assert.AreEqual(new Int2(-5, 0), soln.Position);
            Assert.AreEqual(Int2.Left, soln.Direction);

            soln.ExecuteSingleCommand("R13");
            Assert.AreEqual(new Int2(-5, 13), soln.Position);
            Assert.AreEqual(Int2.Up, soln.Direction);

            soln.ExecuteSingleCommand("R7");
            Assert.AreEqual(new Int2(2, 13), soln.Position);
            Assert.AreEqual(Int2.Right, soln.Direction);

            soln.ExecuteSingleCommand("R18");
            Assert.AreEqual(new Int2(2, -5), soln.Position);
            Assert.AreEqual(Int2.Down, soln.Direction);
        }

        [Test]
        public async Task ExecuteMultiCommand_IsCorrectResult()
        {
            Day01 soln = new Day01("");
            string commands = "R5, L5, R5, R3";

            await soln.ExecuteMultiCommand(commands);
            Assert.AreEqual(new Int2(10,2), soln.Position);
            Assert.AreEqual(Int2.Down, soln.Direction);
        }

        [Test]
        public async Task Position_IsCorrectDistance()
        {
            Day01 soln = new Day01("");
            string commands = "R5, L5, R5, R3";

            await soln.ExecuteMultiCommand(commands);
            Assert.AreEqual(12, soln.Position.ManhattanDistance());
        }

        [TestCase("R5, L5, R5, R3", ExpectedResult = "12")]
        [TestCase("R2, R2, R2", ExpectedResult = "2")]
        [TestCase("R2, L3", ExpectedResult = "5")]
        [Test]
        public async Task<string> Part1_IsCorrect(string commands)
        {
            Day01 soln = new Day01(commands);

            string p1 = await soln.GetPart1(_token);

            return p1;
        }

        [Test]
        public void Command_AddsAllVisitedToList()
        {
            Day01 soln = new Day01("");

            Assert.Contains(new Int2(0, 0), soln._visitedPositionList);

            soln.ExecuteSingleCommand("R7");
            for (int i = 1; i <= 7; i++)
            {
                Assert.Contains(new Int2(i, 0), soln._visitedPositionList);
            }

            soln.ExecuteSingleCommand("L5");
            for (int i = 1; i <= 5; i++)
            {
                Assert.Contains(new Int2(7, i), soln._visitedPositionList);
            }
        }

        [TestCase("R8, R4, R4, R8", ExpectedResult = "4")]
        [Test]
        public async Task<string> Part2_IsCorrect(string commands)
        {
            Day01 soln = new Day01(commands);

            string p1 = await soln.GetPart1(_token);
            string p2 = await soln.GetPart2(_token);

            return p2;
        }
    }
}