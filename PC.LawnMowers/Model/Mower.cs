using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PC.LawnMowers.Model
{
    public class Mower
    {
        public Position Position { get; set; }
        public List<char> Instructions { get; set; }
    }
}
