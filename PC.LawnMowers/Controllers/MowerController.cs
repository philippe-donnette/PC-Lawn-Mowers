using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PC.LawnMowers.Model;
using PC.LawnMowers.Settings;

namespace PC.LawnMowers.Controllers
{
    public class MowerController : IMowerController
    {
        public Position ChangeDirection(Position position, char direction)
        {
            switch(direction)
            {
                case 'L':
                    position.CompassPoint = (CompassEnum)((int)position.CompassPoint - 1 != -1 ? (int)position.CompassPoint - 1 : 3);
                    break;
                case 'R':
                    position.CompassPoint = (CompassEnum)((int)position.CompassPoint + 1 != 4 ? (int)position.CompassPoint + 1 : 0);
                    break;
                default:
                    break;
            }

            return position;
        }

        public Position MoveForward(Position position)
        {
            Position newPosition = new Position(position.X, position.Y, position.CompassPoint);
            switch (position.CompassPoint)
            {
                case CompassEnum.North:
                    newPosition.Y = position.Y + 1;
                    break;
                case CompassEnum.East:
                    newPosition.X = position.X + 1;
                    break;
                case CompassEnum.South:
                    newPosition.Y = position.Y - 1;
                    break;
                case CompassEnum.West:
                    newPosition.X = position.X - 1;
                    break;
                default:
                    break;
            }

            return newPosition;
        }
    }
}
