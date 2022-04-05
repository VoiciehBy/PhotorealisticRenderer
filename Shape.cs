namespace PhotorealisticRenderer.Shapes
{
    public abstract class Shape
    {
        public Vector origin;
        public Vector rotation;
        public Intensity color;

        protected Shape(Vector root, Vector eulerRotation)
        {
            origin = root;
            rotation = eulerRotation;
        }
    }
}
