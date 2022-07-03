using AoC.Code.Solutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AoC.Code.Solutions._2020
{
    [Puzzle(2020, 8, "Handheld Halting")]
    public class Day08 : Solution
    {
        private string inputString = string.Empty;

        private List<(string op, int val)> input_instructions;
        private Dictionary<int, (string op, int val)> visited_part1 = new Dictionary<int, (string, int)>(); // this will store lines visited in part 1

        public Day08(string inputBox)
        {
            inputString = inputBox.Replace("\r", string.Empty);
        }

        public override async Task<string> GetPart1(CancellationToken cancellationToken)
        {
            ParseInput();

            int accum;
            RunProgram(input_instructions, out accum, visited_part1);
            return accum.ToString();
        }

        public override async Task<string> GetPart2(CancellationToken cancellationToken)
        {
            // add the termination operation to the instructions
            input_instructions.Add(("end", 0));

            // we need only consider nop and jmp operations that were in the initial loop
            foreach(int index in visited_part1.Keys)
            {
                if (input_instructions[index].op.Equals("acc")) { continue; }

                Swap_NopJmp(index);
                int accum;
                if(RunProgram(input_instructions, out accum))
                {
                    return accum.ToString();
                }
                // Swap back
                Swap_NopJmp(index);
            }

            return "No fix found";
        }

        private void Swap_NopJmp(int index)
        {
            if (input_instructions[index].op.Equals("nop"))
            {
                input_instructions[index] = ("jmp", input_instructions[index].val);
            }
            else if (input_instructions[index].op.Equals("jmp"))
            {
                input_instructions[index] = ("nop", input_instructions[index].val);
            }
        }

        // returns true if program terminates
        private bool RunProgram(List<(string op, int val)> instructions, out int accumulator, Dictionary<int, (string, int)> visited = null)
        {
            if (visited == null)
            {
                visited = new Dictionary<int, (string, int)>();
            }

            accumulator = 0;
            int index = 0;

            while (!visited.ContainsKey(index))
            {
                (string op, int val) instruction = instructions[index];
                visited.Add(index, instruction);

                switch (instruction.op)
                {
                    case "nop":
                        index++;
                        break;
                    case "acc":
                        accumulator += instruction.val;
                        index++;
                        break;
                    case "jmp":
                        index += instruction.val;
                        break;
                    case "end":
                        return true;
                    default: // input error
                        throw new InvalidOperationException("Invalid Instruction");
                }
            }

            return false;
        }

        private void ParseInput()
        {
            input_instructions = inputString.Split("\n")
                                            .Select(line =>
                                                {
                                                    var split = line.Split(" "); 
                                                    return (op: split[0], val: int.Parse(split[1])); 
                                                })
                                            .ToList();
        }
    }
}
