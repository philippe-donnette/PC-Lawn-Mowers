using System;
using PC.LawnMowers.Model;
using PC.LawnMowers.Validators;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace PC.LawnMowers.Controllers
{
    public class MainController : IMainController
    {
        private readonly IInputController _inputController;
        private readonly IRuleController _ruleController;
        private readonly IMowerController _mowerController;

        public MainController(IInputController inputController,
            IRuleController ruleController,
            IMowerController mowerController)
        {
            _inputController = inputController;
            _ruleController = ruleController;
            _mowerController = mowerController;
        }

        public string Start(string input)
        {
            Lawn lawn = _inputController.ReadInput(input);
            string output = null;
            foreach(Mower mower in lawn.Mowers)
            {
                foreach(char command in mower.Instructions)
                {
                    switch(command)
                    {
                        case 'M':
                            mower.Position = _mowerController.MoveForward(mower.Position);
                            break;
                        case 'L':
                        case 'R':
                            mower.Position = _mowerController.ChangeDirection(mower.Position, command);
                            break;
                        default:
                            break;
                    }
                    if (_ruleController.HasMowerOnTheWay(mower.Position, lawn.Mowers))
                    {
                        output += "Cannot go further, another mower is on the way" + Environment.NewLine;
                        break;
                    }
                    if (_ruleController.IsOutOfLawn(mower.Position.X, mower.Position.Y, lawn))
                    {
                        output += "Cannot go further, mower will be outside of lawn" + Environment.NewLine;
                        break;
                    }
                }
                output += mower.Position.ToString() + Environment.NewLine; 
            }
            return output;
        }
    }
}
