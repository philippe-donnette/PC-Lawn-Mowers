using PC.LawnMowers.Model;
using System.Collections.Generic;

namespace PC.LawnMowers.Controllers
{
    public interface IRuleController
    {
        bool HasMowerOnTheWay(Position position, List<Mower> mowers);
        bool IsOutOfLawn(int x, int y, Lawn lawn);
    }
}
