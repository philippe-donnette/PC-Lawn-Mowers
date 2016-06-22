using PC.LawnMowers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.LawnMowers.Validators
{
    public interface IInputValidator
    {
        bool ValidateLawnSize(string inputLine);
        bool ValidateMowerPosition(string inputLine);
        bool ValidateMowerPosition(Position position, int lawnWidth, int lawnLength);
        bool ValidateMowerCommands(string inputLine);
    }
}
