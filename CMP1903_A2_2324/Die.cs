using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMP1903_A2_2324
{
    /// <summary>
    /// Class <c>Die</c> creates a Dice object that can generate a random number between 1 and 6
    /// </summary>
    internal class Die
    {
        /// <summary>
        /// The <c>Random</c> object used by the <c>Roll()</c> function
        /// </summary>
        private static Random _random = new Random();

        /// <summary>
        /// Represents the die's current value
        /// </summary>
        public int DieValue { get; private set; }

        /// <summary>
        /// Randomly generates a new value between 1 and 6 (inclusive) and updates <c>DieValue</c> accordingly
        /// </summary>
        /// <returns>The Die's new value, also accessible using <c>DieValue</c></returns>
        public int Roll()
        {
            // Generate new random number, between 1-6 (maxValue is non-inclusive so it is 7
            DieValue = _random.Next(minValue: 1, maxValue: 7);
            
            // Return new value
            return DieValue;
        }

        /// <summary>
        /// Override of ToString that creates a symbolic representation of a dice
        /// </summary>
        /// <returns>A string that represents the dice's current value</returns>
        public override string ToString()
        {
            // Create empty 2D array
            string[,] dieGrid = new string[3, 3] {{" ", " ", " "}, {" ", " ", " "},{" ", " ", " "}};

            // Change the values in the die grid depending on the die's current value
            switch (DieValue)
            {
                // If die value is one, the middle item should be O
                case 1:
                    dieGrid[1, 1] = "O";
                    break;
                // If die value is two, opposite corners are O
                case 2:
                    dieGrid[0, 0] = "O";
                    dieGrid[2, 2] = "O";
                    break;
                // If die value is three, there is a diagonal of O
                case 3:
                    dieGrid[0, 0] = "O";
                    dieGrid[1, 1] = "O";
                    dieGrid[2, 2] = "O";
                    break;
                // If die value is four, all corners are O
                case 4:
                    dieGrid[0, 0] = "O";
                    dieGrid[2, 0] = "O";
                    dieGrid[0, 2] = "O";
                    dieGrid[2, 2] = "O";
                    break;
                // If die value is five, all corners + middle are O
                case 5:
                    dieGrid[0, 0] = "O";
                    dieGrid[2, 0] = "O";
                    dieGrid[0, 2] = "O";
                    dieGrid[2, 2] = "O";
                    dieGrid[1, 1] = "O";
                    break;
                // If die value is six, two columns of O at each end of the die
                case 6:
                    dieGrid[0, 0] = "O";
                    dieGrid[1, 0] = "O";
                    dieGrid[2, 0] = "O";
                    dieGrid[0, 2] = "O";
                    dieGrid[1, 2] = "O";
                    dieGrid[2, 2] = "O";
                    break;
            }

            // Generate a string value using our die grid
            string dieReturn = "";

            for (int i = 0; i < dieGrid.GetLength(0); i++)
            {
                string rowReturn = "|";
                for (int j = 0; j < dieGrid.GetLength(0); j++)
                {
                    rowReturn += " " + dieGrid[i, j] + " ";
                }

                rowReturn += "|\n";
                dieReturn += rowReturn;
            }

            // Return generated string
            return dieReturn;
        }
    }
}
