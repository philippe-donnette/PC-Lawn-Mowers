using PC.LawnMowers.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.LawnMowers.Model
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
        public CompassEnum CompassPoint { get; set; }

        public Position(int x, int y, CompassEnum compassPoint)
        {
            X = x;
            Y = y;
            CompassPoint = compassPoint;
        }

        public Position(int x, int y, string compassPoint)
        {
            X = x;
            Y = y;
            switch(compassPoint)
            {
                case "N":
                    CompassPoint = CompassEnum.North;
                    break;
                case "E":
                    CompassPoint = CompassEnum.East;
                    break;
                case "W":
                    CompassPoint = CompassEnum.West;
                    break;
                case "S":
                    CompassPoint = CompassEnum.South;
                    break;
                default:
                    break;
            }
        }

        public override string ToString()
        {
            char compassPoint = char.MinValue;
            switch (CompassPoint)
            {
                case CompassEnum.East:
                    compassPoint = 'E';
                    break;
                case CompassEnum.North:
                    compassPoint = 'N';
                    break;
                case CompassEnum.South:
                    compassPoint = 'S';
                    break;
                case CompassEnum.West:
                    compassPoint = 'W';
                    break;
                default:
                    break;
            }
            return string.Format("{0} {1} {2}", X, Y, compassPoint);
        }
    }
}
