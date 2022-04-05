using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotorealisticRenderer
{
    public class Camera
    {
        private Vector position;
        public Vector Position
        {
            get { return position; }
            set { position = value; }
        }
        private Vector target;
        public Vector Target
        {
            get { return target; }
            set { target = value; }
        }
        private Vector up;
        public Vector Up
        {
            get { return up; }
            set { up = value; }
        }
        private float nearPlane;
        public float NearPlane
        {
            get { return nearPlane; }
            set { nearPlane = value; }
        }
        private float farPlane;
        
        /*public float FarPlane
        {
        }
        *///makes build fail
    }
}
