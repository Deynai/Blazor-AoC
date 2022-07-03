
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AoC.Code.Solutions.Shared;

namespace AoC.Code.Solutions._2016
{
    [Puzzle(2016, 1, "No Time for a Taxicab")]
    public class Day01 : Solution
    {
        public Int2 Position { get; set; }
        public Int2 Direction { get; set; }

        public List<Int2> _visitedPositionList = new List<Int2>();

        private readonly string inputString = string.Empty;

        public Day01(string inputBox)
        {
            inputString = inputBox;

            InitialiseStartingPosition();
            _visitedPositionList.Add(Position);
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            await ExecuteMultiCommand(inputString);
            return Position.ManhattanDistance().ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            HashSet<Int2> visitedPos = new HashSet<Int2>();
            for (int i = 0; i < _visitedPositionList.Count; i++)
            {
                if (visitedPos.Contains(_visitedPositionList[i]))
                    return _visitedPositionList[i].ManhattanDistance().ToString();
                else
                    visitedPos.Add(_visitedPositionList[i]);
            }

            return "No revisit";
        }

        private void InitialiseStartingPosition()
        {
            Position = Int2.Zero;
            Direction = Int2.Up;
        }

        public void TurnLeft()
        {
            Direction = new Int2(-Direction.y, Direction.x);
        }

        public void TurnRight()
        {
            Direction = new Int2(Direction.y, -Direction.x);
        }

        public void Move(int distance)
        {
            for(int i = 1; i <= distance; i++)
            {
                _visitedPositionList.Add(Position + i * Direction);
            }

            Position += distance * Direction;
        }

        public void ExecuteSingleCommand(string command)
        {
            if (command.Length < 2)
                return;

            if (command[0] == 'L')
                TurnLeft();
            else if (command[0] == 'R')
                TurnRight();

            if (int.TryParse(command[1..], out int val))
                Move(val);
        }

        public async Task ExecuteMultiCommand(string commands)
        {
            string[] splitCommands = commands.Split(", ");
            int maxBatch = 500;
            int currIndex = 0;

            while (currIndex < splitCommands.Length)
            {
                int batchSize = Maths.Min(maxBatch, splitCommands.Length - currIndex);
                ExecuteBatchCommand(currIndex, batchSize, ref splitCommands);
                currIndex += batchSize;
                await Task.Delay(1);
            }
        }

        private void ExecuteBatchCommand(int index, int batchSize, ref string[] splitCommands)
        {
            for (int i = index; i < index+batchSize; i++)
            {
                ExecuteSingleCommand(splitCommands[i]);
            }
        }
    }
}