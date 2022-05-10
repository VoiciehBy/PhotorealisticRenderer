using PhotorealisticRenderer.Shapes;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using PhotorealisticRenderer.ObjReader;

namespace PhotorealisticRenderer
{
    public class MainProgram
    {
        public static void Main(string[] args)
        {
            if (!OperatingSystem.IsWindows())
            {
                Console.WriteLine($"Due to reliance on {nameof(System.Drawing)}, this app requires a Windows machine.");
                return;
            }

            var sphere = new Sphere(new Vector3(0, 0, 0), 1) { Color = new LightIntensity(0, 0, 255) };
            var secondSphere = new Sphere(new Vector3(1, 0, 1), 0.5) { Color = new LightIntensity(255, 0, 0) };

            var xsize = 1024;
            var ysize = 1024;

            var scene = new Scene { BackgroundColor = new LightIntensity(0, 1, 0) };
            //scene.Shapes.Add(sphere);
            //scene.Shapes.Add(secondSphere);

            var obj = ObjFile.Load(@"C:\png\box.obj");
            var rng = new Random();
            foreach (var triangle in obj.GetTriangles())
            {
                triangle.Color = new LightIntensity(rng.NextDouble(), rng.NextDouble(), rng.NextDouble());
                scene.Shapes.Add(triangle);
            }


            for (var i = 0; i < 1; i++)
            {
                Console.WriteLine($"Creating image {i + 1}...");

                var x = i switch
                {
                    1 => 20,
                    3 => -20,
                    _ => 0,
                };
                var z = i switch
                {
                    0 => -20,
                    2 => 20,
                    _ => 0,
                };

                //Camera camera = new Orthogonal(new Vector3(0, 0, 0), 0, new Vector2(5, 5));
                PerspectiveCamera perspectiveCam = new(new Vector3(x, 50, z), new Vector3(0, 0, 0), new Vector3(0, -1, 0), 2);

                Bitmap perspective = perspectiveCam.Raytrace(scene, new Size(1024, 1024));
                //Bitmap orthogonal = tracer.Raytrace(scene, camera, new Size(1024, 1024));

                Save(perspective, $"{nameof(perspective)}_{i}");
                //Save(orthogonal, $"{nameof(orthogonal)}_{i}");
            }
        }

        public static void Save(Bitmap bitmap, string name)
        {
            if (bitmap == null)
                return;

            var dir = new DirectoryInfo(@"C:\png");
            if (!dir.Exists)
                dir.Create();
            bitmap.Save($@"C:\png\{name}.png", ImageFormat.Png);
        }
    }
}