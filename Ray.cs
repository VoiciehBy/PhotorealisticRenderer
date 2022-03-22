using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotorealisticRenderer
{
    public class Ray
    {
        public Vector direction;
        public Vector origin;

        public Ray(Vector origin, Vector direction)
        {
            this.direction = direction;
            this.origin = origin;
        }
    }
}
