using PhotorealisticRenderer.Shapes;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace PhotorealisticRenderer
{
    public class MainProgram
    {
        public static void Main()
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine($"Due to reliance on {nameof(System.Drawing)}, this app requires a Windows machine.");
                return;
            }

            // PhongMaterialBase Mat = new PhongMaterial(new LightIntensity(1, 1, 255), 0.5, 0.4, 2.5, 50);
            PhongMaterialBase Mat = new PhongMaterial(
                new LightIntensity(1, 1, 255), 
                new LightIntensity(0.1),
                new LightIntensity(0.2, 0.2, 0.7), 
                new LightIntensity(2.5), 
                50);

            var scene = new Scene { BackgroundColor = new LightIntensity(0, 1, 0) };
            scene.Shapes.Add(new Sphere(new Vector3(0, 0, 0), 2));
            scene.Shapes[0].Material = Mat;
            scene.Lights.Add(new PointLight(new Vector3(10, 10, 10), Color.White));
            
            for (int i = 0, max = 1; i < max; i++)
            {
                Console.WriteLine($"Creating image {i + 1} out of {max}...");
                PerspectiveCamera perspectiveCam = new(new Vector3(0, 0, 10), Vector3.Zero, new Vector3(0, -1, 0), 2);

                Bitmap perspective = perspectiveCam.Raytrace(scene, new Size(1024, 1024));

                Save(perspective, $"{nameof(perspective)}_{i}");
            }
            
            Console.WriteLine();
            Console.WriteLine("Operation finished.");
        }

        public static void Save(Bitmap bitmap, string name)
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine($"Due to reliance on {nameof(System.Drawing)}, this app requires a Windows machine.");
                return;
            }

            if (bitmap == null)
                return;

            var dir = new DirectoryInfo(@"C:\png");
            if (!dir.Exists)
                dir.Create();
            bitmap.Save($@"C:\png\{name}.png", ImageFormat.Png);
        }
    }
}