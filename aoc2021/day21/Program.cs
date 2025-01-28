using System;
using System.Linq;
using System.Collections.Generic;

namespace day21
{
    class Program
    {
        // state has a key of:
        //   which player turn|p1 pos,p1 score|p2 pos,p2 score
        // and a value of the count of universes in that state
        private static Dictionary<(int, int, int, int, int), long> states =
            new Dictionary<(int, int, int, int, int), long>();

        public static long p1wins = 0;
        public static long p2wins = 0;



        static void Main(string[] args)
        {
            string filename =
                //"input.txt";
                "input-example.txt";

            var input = new PuzzleInput(filename);
            //Part1(input);

            Part2();
        }

        private static void DumpPlayers(List<Player> players)
        {
            foreach (var player in players)
            {
                Console.WriteLine(player);
            }
        }

        private static void Part1(PuzzleInput input)
        {
            var players = new List<Player>();
            foreach (var line in input.Lines)
            {
                players.Add(new Player(line));
            }
            var game = new Game();
            game.Players = players;

            bool win = false;
            while (!win)
            {
                win = game.TakeATurn();
            }

            int lowestScore = game.GetLowestScoringPlayer().Score;
            int numRolls = game.TotalDieRolls;

            Console.WriteLine($"Lowest score of {lowestScore} times num rolls of {numRolls} = {lowestScore * numRolls}");

        }

        private static void Part2()
        {
            // state is
            // (<current player number>,
            //  <player 1 position>,
            //  <player 1 score>,
            //  <player 2 position>,
            //  <player 2 score)
            (int, int, int, int, int) initialState = (
                1,
                2,
                0,
                5,
                0);

            // initially only one state exists and one univers
            states[initialState] = 1;

            long i = 0;
            // Continue this as long as states remain in the state dictionary
            while (states.Keys.Count > 0)
            {
                // * take one state out of the states dictionary
                (int, int, int, int, int) state = states.Keys.First();
                long numUniverses = states[state];
                states.Remove(state);

                Expand(state, 3, 1, numUniverses);
                Expand(state, 4, 3, numUniverses);
                Expand(state, 5, 6, numUniverses);
                Expand(state, 6, 7, numUniverses);
                Expand(state, 7, 6, numUniverses);
                Expand(state, 8, 3, numUniverses);
                Expand(state, 9, 1, numUniverses);

                MoveToWins();
                if (i % 1_000 == 0)
                    Console.WriteLine($"{p1wins} | {p2wins} | {states.Count}");

                i++;
            }
            Console.WriteLine($"{p1wins} | {p2wins} | {states.Count}");   
        }

        private static void Expand(
            (int, int, int, int, int) state, // starting state
            int roll, // the roll (combination of 3 rolls added)
            long numBaseUniverses,
            long numUniversesWithRoll // of the 27 new universes created in
                                      // this turn, how many got this roll
            )
        {
            var newState = Move(roll, state);

            if (!states.Keys.Contains(newState)) states[newState] = 0;

            states[newState] += numUniversesWithRoll * numBaseUniverses;
        }


        private static void MoveToWins()
        {
            int playerNum,
                p1pos,
                p1score,
                p2pos,
                p2score;
            var keysToRemove  = new List<(int, int, int, int, int)>();

            foreach (var key in states.Keys)
            {
                (playerNum, p1pos, p1score, p2pos, p2score) = key;

                if (p1score >= 21)
                {
                    p1wins += states[key];
                    keysToRemove.Add(key);
                }
                else if (p2score >= 21)
                {
                    p2wins += states[key];
                    keysToRemove.Add(key);
                }
            }

            foreach(var key in keysToRemove)
            {
                states.Remove(key);
            }
        }

        public static (int, int, int, int, int) Move(
            int n,
            (int, int, int, int, int) state)
        {
            int playerNum,
                p1pos,
                p1score,
                p2pos,
                p2score;

            (playerNum, p1pos, p1score, p2pos, p2score) = state;
            int pos = playerNum == 1 ? p1pos : p2pos;
            pos += n;
            while (pos > 10)
            {
                pos -= 10;
            }

            // score the player (and advance the proper one)
            if (playerNum == 1)
            {
                p1score += pos;
                p1pos = pos;
            }
            else
            {
                p2score += pos;
                p2pos = pos;
            }

            // advance to next player
            playerNum = playerNum == 1 ? 2 : 1;

            return (playerNum, p1pos, p1score, p2pos, p2score);
        }
    }
}
