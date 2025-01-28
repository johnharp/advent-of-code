using System;
using System.Linq;
using System.Collections.Generic;

namespace day21
{
    public class Game
    {
        public int DieSize = 100;
        public int LastRoll = 0;
        public int MaxSpaceNumber = 10;

        public int GoalScore = 1000;

        public int TotalDieRolls = 0;

        // Index of the player that will take the next turn
        public int NextTurnPlayerIndex = 0;

        public int TurnsTaken = 0;

        public List<Player> Players;

        public Game()
        {
        }

        public void UpdateNextPlayer()
        {
            NextTurnPlayerIndex++;
            if (NextTurnPlayerIndex > Players.Count-1)
            {
                NextTurnPlayerIndex = 0;
            }
        }

        public void AdvancePlayerBoardPosition(Player player, int numSpaces)
        {
            player.CurrentPosition += numSpaces;
            while (player.CurrentPosition > MaxSpaceNumber)
            {
                player.CurrentPosition -= MaxSpaceNumber;
            }

            player.Score += player.CurrentPosition;
            Console.WriteLine(player);
        }
        // returns true if the player taking this turn wins
        public bool TakeATurn()
        {
            Player currentPlayer = Players[NextTurnPlayerIndex];

            int totalRoll = 0;
            for (int i = 0; i<3; i++)
            {
                int roll = Roll();
                totalRoll += roll;
            }
            AdvancePlayerBoardPosition(currentPlayer, totalRoll);

            UpdateNextPlayer();
            return currentPlayer.Score >= GoalScore;
        }

        public int Roll()
        {
            TotalDieRolls++;
            int roll = LastRoll + 1;
            if (roll > DieSize) roll = 1;

            LastRoll = roll;
            return roll;
        }

        public Player GetLowestScoringPlayer()
        {
            var sortedPlayers =
                Players.OrderBy(p => p.Score)
                .ToList();

            return sortedPlayers[0];
        }
    }
}
