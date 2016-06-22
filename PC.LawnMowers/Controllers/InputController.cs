using System;
using PC.LawnMowers.Model;
using PC.LawnMowers.Validators;
using System.Collections.Generic;
using System.Linq;

namespace PC.LawnMowers.Controllers
{
    public class InputController : IInputController
    {
        private readonly IInputValidator _inputValidator;

        public InputController(IInputValidator inputValidator)
        {
            _inputValidator = inputValidator;
        }

        public Lawn ReadInput(string input)
        {
            Lawn lawn = new Lawn();
            int i = 0;
            bool instructions = false;
            Mower mower = null;
            foreach(string line in input.Split(new string[] { Environment.NewLine }, StringSplitOptions.None))
            {
                if(i == 0)
                {
                    //Lawn Width and Length
                    if (!_inputValidator.ValidateLawnSize(line))
                        throw new NotSupportedException("Lawn width and length input is invalid");
                    string[] lawnSize = line.Split(' ');
                    lawn.Width = Convert.ToInt32(lawnSize[0]);
                    lawn.Length = Convert.ToInt32(lawnSize[1]);
                    lawn.Mowers = new List<Mower>();
                }
                else
                {
                    if(!instructions)
                    {
                        //Mower Position (eg: 1 3 N)
                        if(!_inputValidator.ValidateMowerPosition(line))
                            throw new NotSupportedException("Mower position input is invalid");
                        string[] position = line.Split(' ');
                        mower = new Mower
                        {
                            Position = new Position(Convert.ToInt32(position[0]), Convert.ToInt32(position[1]), position[2])
                        };
                        if (!_inputValidator.ValidateMowerPosition(mower.Position, lawn.Width, lawn.Length))
                            throw new NotSupportedException("Mower position input is outside of range");
                    }
                    else
                    {
                        //Mower Commands (eg: LMLMLMMM)
                        if (!_inputValidator.ValidateMowerCommands(line))
                            throw new NotSupportedException("Mower commands input is invalid");
                        mower.Instructions = line.ToCharArray().OfType<char>().ToList();
                        lawn.Mowers.Add(mower);
                    }
                    instructions = !instructions;
                }
                i++;
            }
            return lawn;
        }
    }
}
