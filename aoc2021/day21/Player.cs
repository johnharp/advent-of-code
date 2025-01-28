using System;
using System.Text.RegularExpressions;

namespace day21
{
    public class Player
    {
        public int Number;
        public int StartingPosition;
        public int CurrentPosition;

        public int Score = 0;

        // str is in this format:
        // Player 1 starting position: 4
        public Player(string str)
        {
            Regex rx = new Regex(
                @"^\s*Player\s+(\d+) starting position:\s*(\d+)",
                RegexOptions.Compiled | RegexOptions.IgnoreCase);

            MatchCollection matches = rx.Matches(str);

            if (matches.Count == 1)
            {
                GroupCollection groups = matches[0].Groups;

                string numberStr = groups[1].Value;
                string startingPositionStr = groups[2].Value;

                Number = int.Parse(numberStr);
                StartingPosition = int.Parse(startingPositionStr);
                CurrentPosition = StartingPosition;
            }
            else
            {
                throw new Exception($"Invalid player string: {str}");
            }
        }

        public override string ToString()
        {
            return
                $"Player {Number} is currently at {CurrentPosition} and has {Score} points";
        }
    }

}
