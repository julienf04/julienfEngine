using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace julienfEngine1
{
    class FigureFilter
    {
        public static bool[] GetSpecialCharacters(string[] figureText, out short[][] startIndexes, out int[][] lengthSpecialChars)
        {
            bool[] hasSpecialCharacters = new bool[figureText.Length];
            startIndexes = null;
            lengthSpecialChars = null;

            char specialChar = julienfEngine.SPECIAL_ASCII_CHARACTER;
            for (int i = 0; i < figureText.Length; i++)
            {
                hasSpecialCharacters[i] = figureText[i].Contains(specialChar);
                if (hasSpecialCharacters[i])
                {
                    if (startIndexes is null)
                    {
                        startIndexes = new short[figureText.Length][];
                        lengthSpecialChars = new int[figureText.Length][];
                    }
                    List<short> foreachStartIndex = new List<short>();
                    List<int> foreachLengthSpecialChars = new List<int>();
                    bool firstCharIsSpecial = figureText[i][0] == specialChar;
                    string figureLineIndexed = figureText[i];
                    int firstIndex = figureLineIndexed.IndexOf(specialChar);
                    foreachStartIndex.Add((short)(firstCharIsSpecial ? 1 : 0));
                    foreachLengthSpecialChars.Add(firstIndex - foreachStartIndex[0]);

                    figureLineIndexed = figureLineIndexed.Substring(firstIndex + 1);
                    int i2 = 0;
                    while (figureLineIndexed.Contains(specialChar))
                    {
                        int index = figureLineIndexed.IndexOf(specialChar);
                        if (foreachStartIndex[i2] != index)
                        {
                            foreachStartIndex.Add((short)(figureText[i].Length - figureLineIndexed.Length + index - 1));
                            foreachLengthSpecialChars.Add(index - foreachStartIndex[i2] - 1);
                            i2++;
                        }
                        figureLineIndexed = figureLineIndexed.Substring(index + 1);
                    }
                    if (figureLineIndexed.Length >= 0)
                    {
                        foreachStartIndex.Add((short)(figureText[i].Length - figureLineIndexed.Length));
                        foreachLengthSpecialChars.Add(figureLineIndexed.Length);
                    }


                    startIndexes[i] = foreachStartIndex.ToArray();
                    lengthSpecialChars[i] = foreachLengthSpecialChars.ToArray();
                }
            }

            return !hasSpecialCharacters.Contains(true) ? null : hasSpecialCharacters;
        }
    }
}
