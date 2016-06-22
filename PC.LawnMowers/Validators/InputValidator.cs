using PC.LawnMowers.Model;
using System;
using System.Text.RegularExpressions;

namespace PC.LawnMowers.Validators
{
    public class InputValidator : IInputValidator
    {
        public bool ValidateLawnSize(string inputLine)
        {
            return Regex.IsMatch(inputLine, @"^[0-9]+\s[0-9]+$");
        }

        public bool ValidateMowerPosition(string inputLine)
        {
            return Regex.IsMatch(inputLine, @"^[0-9]+\s[0-9]+\s[NEWS]$");
        }

        public bool ValidateMowerPosition(Position position, int lawnWidth, int lawnLength)
        {
            return !(position.X > lawnWidth || position.Y > lawnLength);
        }

        public bool ValidateMowerCommands(string inputLine)
        {
            return Regex.IsMatch(inputLine, @"^[LMR]+$");
        }
    }
}
