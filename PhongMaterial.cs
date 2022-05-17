using System;

namespace PhotorealisticRenderer
{
    public abstract class PhongMaterialBase
    {
        protected LightIntensity ambient;
        protected LightIntensity diffuse;
        protected LightIntensity specular;
        protected double specularExponent; // Shininess

        protected PhongMaterialBase(double ambient, double diffuse, double specular, double specularExponent) 
            : this(new LightIntensity(ambient), new LightIntensity(diffuse), new LightIntensity(specular), specularExponent)
        { }
        
        protected PhongMaterialBase(LightIntensity ambient, LightIntensity diffuse, LightIntensity specular, double specularExponent)
        { 
            this.ambient = ambient;
            this.diffuse = diffuse;
            this.specular = specular;
            this.specularExponent = specularExponent;
        }

        public abstract LightIntensity GetColor();

        public virtual LightIntensity Radiance(PointLight light, RayHit hit)
        {
            Vector3 inDirection = (light.Position - hit.HitPoint).Normalized;
            double diffuseFactor = inDirection.Dot(hit.Normal);
            
            LightIntensity result = ambient * GetColor(); // Ambient
            
            if (diffuseFactor > 0) 
            { result += diffuse * diffuseFactor; } // Diffuse
            
            double phongFactor = PhongFactor(inDirection, hit.Normal, hit.Ray.Direction);
            if (phongFactor != 0)
            { result += specular * phongFactor; } // Specular
            
            return result * light.Color;
        }

        protected double PhongFactor(Vector3 inDirection, Vector3 normal, Vector3 toCameraDirection)
        {
            Vector3 reflected = Vector3.Reflect(inDirection, normal);
            double cosAngle = reflected.Dot(toCameraDirection);
            if (cosAngle <= 0) { return 0; }
            return Math.Pow(cosAngle, specularExponent);
        }
    }
    
    public class PhongMaterial : PhongMaterialBase
    {
        protected LightIntensity materialColor;

        public PhongMaterial(LightIntensity materialColor, double ambient, double diffuse, double specular, double specularExponent) 
            : this(materialColor, new LightIntensity(ambient), new LightIntensity(diffuse), new LightIntensity(specular), specularExponent)
        { }

        public PhongMaterial(LightIntensity materialColor, LightIntensity ambient, LightIntensity diffuse, LightIntensity specular, double specularExponent) 
            : base(ambient, diffuse, specular, specularExponent)
        {
            this.materialColor = materialColor / 255;
        }

        public override LightIntensity GetColor() => materialColor;
    }
}