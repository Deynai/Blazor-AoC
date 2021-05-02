using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(18, "Operation Order")]
    public class Day18 : Solution
    {
        private string inputString = string.Empty;
        private string[] expressions;

        // Define binary operations and their precedence
        private Dictionary<string, int> op_precedence = new Dictionary<string, int> { { "+", 1 }, { "*", 1 } };
        private static HashSet<string> binop = new HashSet<string> { "+", "*" };

        // We're going to use a parser class to help format expressions and retrieve each token
        private class Parser
        {
            public string current_token;
            public bool end;
            private int current_index = 0;
            private string[] tokens;
            private static Regex reg_token = new Regex(@"\d+|[\+\*]|\(|\)");

            public Parser(string expression)
            {
                tokens = reg_token.Matches(expression).Select(t => t.Value).ToArray();
                current_token = tokens[0];
            }

            public string GetNextToken()
            {
                try
                {
                    current_index++;
                    current_token = tokens[current_index];
                }
                catch (Exception)
                {
                    end = true;
                    return null;
                }

                return current_token;
            }
        }

        public Day18(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override string GetPart1()
        {
            return SumExpressions().ToString();
        }

        public override string GetPart2()
        {
            op_precedence["+"] = 2;
            return SumExpressions().ToString();
        }

        private long SumExpressions()
        {
            long sum = 0;
            foreach (string exp in expressions)
            {
                Parser parser = new Parser(exp);
                sum += ResolveExpression(parser, 0);
            }
            return sum;
        }

        // ParsePrimary returns a number to be operated on with binary ops
        // Either directly or by resolving a sub-expression in parenthesis
        private long ParsePrimary(Parser parser)
        {
            string token = parser.current_token;
            long primary;

            if (token.Equals("("))
            {
                parser.GetNextToken();
                primary = ResolveExpression(parser, 1);
                if(!parser.current_token.Equals(")")){
                    throw new Exception("Imbalanced Parenthesis");
                }
                parser.GetNextToken();
                return primary;
            }
            else
            {
                try
                {
                    primary = long.Parse(token);
                }
                catch (Exception)
                {
                    throw new Exception($"Expected token to be a number: {token}");
                }

                parser.GetNextToken();
                return primary;
            }
        }

        // Resolve a full expression using the Precedence Climbing method
        // in a nutshell: set lhs as first primary
        //                if current operation has higher precedence than next operation => compute operation => advance
        //                else recursively compute after that operation until a lower precedence operation is found
        // i.e: 
        // expression = 3 * 5 + 4 + 2 * 6 with op precedence + = 1, * = 2
        // set lhs = 3
        // * has higher precedence than +, so rhs = 5 and lhs := lhs*rhs = 15 
        // + and + have equal precedence, so rhs = 4 and lhs := lhs+rhs = 19
        // + has lower precedence than *, so recurse rhs with new min precedence 2
        //      > set r_lhs = 2
        //      > * has precedence of 2 and is the last operation, so r_rhs = 6 and we return rhs := r_lhs * r_rhs = 12
        // finally compute previous + with lhs = 19 and rhs = 12. Expression resolves to 31.

        private long ResolveExpression(Parser parser, int precedence)
        {
            long lhs = ParsePrimary(parser);
            while (!parser.end)
            {
                string token = parser.current_token;
                if(token == null || !binop.Contains(token) || op_precedence[token] < precedence) { break; }
                // token should be either "+" or "*" at this point

                parser.GetNextToken();
                long rhs = ResolveExpression(parser, op_precedence[token]+1);

                lhs = Operation(lhs, rhs, token);
            }

            return lhs;
        }

        private long Operation(long lhs, long rhs, string op)
        {
            switch (op)
            {
                case "+": return lhs + rhs;
                case "*": return lhs * rhs;
                default: return -1;
            }
        }

        private void ParseInput()
        {
            expressions = inputString.Trim().Replace("\r", "").Replace(" ", "").Split("\n");
        }
    }
}
