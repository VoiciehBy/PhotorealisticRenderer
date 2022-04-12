using System.Drawing;

namespace PhotorealisticRenderer
{
    public class Material
    {
        private Color diffuse;
        private float specularAmount;//[0,inf] 0 == matt
        private float specularCoefficent;//(0,inf) for Phong lighting called shineness

        private float reflectionFraction;//[0,1] 1 == total reflection i.e. "perfect mirror"

        public Material(Color diffuse, float specularAmount, float specularCoefficent, float reflectionFraction)
        {
            this.diffuse = diffuse;
            this.specularAmount = specularAmount;
            this.specularCoefficent = specularCoefficent;
            this.reflectionFraction = reflectionFraction;
        }
    }
}
