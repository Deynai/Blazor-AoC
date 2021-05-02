using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Text.RegularExpressions;

namespace Blazor_AoC.Code._2020
{
    [Puzzle(19, "Monster Messages")]
    public class Day19EP : Solution
    {
        private string inputString = string.Empty;

        // numbers will be non-terminal, letters "a" and "b" are terminal
        private Dictionary<string, string[][]> grammar;
        private HashSet<string> messages;

        private HashSet<State> hashQueue = new HashSet<State>();

        private class State
        {
            public string rule_id;
            public int subrule_index;
            public int token_index;
            public int message_index;
            public int k_state;

            public State(string rule_id, int subrule_index = 0, int token_index = 0, int message_index = 0, int k_state = 0)
            {
                this.rule_id = rule_id;
                this.subrule_index = subrule_index;
                this.token_index = token_index;
                this.message_index = message_index;
                this.k_state = k_state;
            }

            public bool isEnd(Dictionary<string, string[][]> grammar)
            {
                return (grammar[rule_id][subrule_index].Length.Equals(token_index));
            }

            public string GetToken(Dictionary<string, string[][]> grammar)
            {
                return grammar[rule_id][subrule_index][token_index];
            }

            public override int GetHashCode()
            {
                return (rule_id, subrule_index, token_index, message_index, k_state).GetHashCode();
            }

            public override bool Equals(object obj)
            {
                if (obj == null || !this.GetType().Equals(obj.GetType())) { return false; }
                else
                {
                    State st = (State)obj;
                    return (rule_id, subrule_index, token_index, message_index, k_state) == (st.rule_id, st.subrule_index, st.token_index, st.message_index, st.k_state);
                }
            }
        }

        public Day19EP(string inputBox)
        {
            inputString = inputBox;
            ParseInput();
        }

        public override string GetPart2()
        {
            return base.GetPart2();
        }

        public override string GetPart1()
        {
            int sum = 0;
            foreach (string message in messages)
            {
                HashSet<State>[] states = new HashSet<State>[message.Length + 1];
                states = states.Select(s => new HashSet<State>()).ToArray();
                hashQueue = new HashSet<State>();

                if (ParseMessage(states, message))
                {
                    sum++;
                }
            }
            return sum.ToString();
        }

        private bool ParseMessage(HashSet<State>[] states, string message)
        {
            Queue<State> queue = new Queue<State>();
            QueueNewState(queue, states, new State("0", 0, 0, 0, 0));

            while (queue.Count > 0)
            {
                State state = queue.Dequeue();
                hashQueue.Remove(state);

                // if the dot is at the end
                if (state.isEnd(grammar))
                {
                    Completion(queue, states, state, message);
                }
                // else if the dot is before a terminal
                else if (state.GetToken(grammar).Equals("a") || state.GetToken(grammar).Equals("b"))
                {
                    Scanning(queue, states, state, message);
                }
                // else the dot is before a nonterminal (assert)
                else
                {
                    Prediction(queue, states, state, message);
                }

                // there may be a better time to check this
                if (state.k_state.Equals(message.Length) && CheckFinished(queue, states, state, message))
                {
                    return true;
                }
            }
            return false;
        }

        private void Completion(Queue<State> queue, HashSet<State>[] states, State state, string message)
        {
            // Completion: For every state in S(k) of the form (Y → γ •, j), find all states in S(j) of the form (X → α • Y β, i) and add (X → α Y • β, i) to S(k).
            foreach (State jstate in states[state.message_index])
            {
                if (!jstate.isEnd(grammar))
                {
                    if (jstate.GetToken(grammar).Equals(state.rule_id))
                    {
                        State newState = new State(jstate.rule_id, jstate.subrule_index, jstate.token_index + 1, jstate.message_index, state.k_state);
                        QueueNewState(queue, states, newState);
                    }
                }
            }
        }

        private void Scanning(Queue<State> queue, HashSet<State>[] states, State state, string message)
        {
            // Scanning: If a is the next symbol in the input stream, for every state in S(k) of the form(X → α • a β, j), add(X → α a • β, j) to S(k + 1).
            if (state.GetToken(grammar).Equals(message[state.message_index].ToString()))
            {
                State newState = new State(state.rule_id, state.subrule_index, state.token_index + 1, state.message_index, state.k_state + 1);
                if (newState.k_state > states.Length) { return; } // don't care about states past message length
                QueueNewState(queue, states, newState);
            }
        }

        private void Prediction(Queue<State> queue, HashSet<State>[] states, State state, string message)
        {
            // Prediction: For every state in S(k) of the form (X → α • Y β, j), add (Y → • γ, k) to S(k) for every production in the grammar with Y on the left-hand side (Y → γ).
            string child_id = state.GetToken(grammar);
            for (int i = 0; i < grammar[child_id].Length; i++)
            {
                State newState = new State(child_id, i, 0, state.k_state, state.k_state);
                QueueNewState(queue, states, newState);
            }
        }

        private bool CheckFinished(Queue<State> queue, HashSet<State>[] states, State state, string message)
        {
            foreach (State end_state in states[message.Length])
            {
                // if we find (0 → γ •, 0) in S[n] we have a match
                if (end_state.rule_id.Equals("0") && end_state.isEnd(grammar))
                {
                    return true;
                }
            }
            return false;
        }

        private void QueueNewState(Queue<State> queue, HashSet<State>[] states, State newState)
        {
            states[newState.k_state].Add(newState);
            if (hashQueue.Add(newState))
            {
                queue.Enqueue(newState);
            }
        }

        private void ParseInput()
        {
            Regex reg_number = new Regex(@"\w+");

            messages = inputString.Replace("\r", "").Split("\n\n")[1].Split("\n").ToHashSet();

            grammar = inputString.Replace("\r", "").Split("\n\n")[0].Split("\n")
                                    .Select(line =>
                                    {
                                        // we're going to format, i.e: "4: 3 8 21 | 17 5" into (4, {{3, 8, 21}, {17, 5}}), a (string, string[][]) tuple
                                        string[][] subrules = line.Substring(line.IndexOf(":") + 1).Replace("\"", "").Split("|")
                                                                        .Select(or => reg_number.Matches(or)
                                                                            .Select(m => m.Value).ToArray())
                                                                        .ToArray();

                                        return (id: line.Substring(0, line.IndexOf(":")), subrules: subrules);
                                    })
                                    .ToDictionary(l => l.id, l => l.subrules);
        }
    }
}
