using System;
using System.Collections.Generic;
using PC.LawnMowers.Model;
using System.Linq;

namespace PC.LawnMowers.Controllers
{
    public class RuleController : IRuleController
    {
        public bool HasMowerOnTheWay(Position position, List<Mower> mowers)
        {
            if(mowers != null)
                return mowers.Count(m => m.Position.X == position.X && m.Position.Y == position.Y) > 1;
            else
                return false;
        }

        public bool IsOutOfLawn(int x, int y, Lawn lawn)
        {
            return x <= -1 || y <= -1 || x > lawn.Width || y > lawn.Length;
        }
    }
}
