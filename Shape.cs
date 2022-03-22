namespace PhotorealisticRenderer.Shapes
{
    public class Shape
    {
        public Vector origin;
        public Vector rotation;

        public Shape(Vector root, Vector eulerRotation)
        {
            origin = root;
            rotation = eulerRotation;
        }
    }
}
