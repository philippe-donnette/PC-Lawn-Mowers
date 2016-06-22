using PC.LawnMowers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.LawnMowers.Controllers
{
    public interface IMowerController
    {
        Position MoveForward(Position position);
        Position ChangeDirection(Position position, char direction); 
    }
}
