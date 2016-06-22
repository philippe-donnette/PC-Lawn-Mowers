using System.Collections.Generic;

namespace PC.LawnMowers.Model
{
    public class Lawn
    {
        //eq Position.X
        public int Width { get; set; }
        //eq Position.Y
        public int Length { get; set; }
        public List<Mower> Mowers { get; set; }

        public Lawn()
        { }

        public Lawn(int width, int length)
        {
            Width = width;
            Length = length;
        }
    }
}
