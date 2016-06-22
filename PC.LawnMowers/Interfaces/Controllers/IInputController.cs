using PC.LawnMowers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.LawnMowers.Controllers
{
    public interface IInputController
    {
        Lawn ReadInput(string input);
    }
}
