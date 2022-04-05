using PhotorealisticRenderer.Shapes;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

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

            var sphere = new Sphere(new Vector(), new Vector(), 0.1) { color = new Intensity(0, 0, 255) };
            var secondSphere = new Sphere(new Vector(0.1, 0, 0.1), new Vector(), 0.07) { color = new Intensity(255, 0, 0) };
            // Ray ray = new Ray(new Vector(0, 0, -20), new Vector(0, 0, 1));
            // Ray ray2 = new Ray(new Vector(0, 0, -20), new Vector(0, 0, 1));
            // Ray ray3 = new Ray(new Vector(0, 10, 0), new Vector(1, 0, 0));
            // Plane plane = new Plane(new Vector(0, 0, 0), new Vector(0, 0.5, 0.5));

            // double dist2; double dist1;
            // int one = sphere.CheckIntersection(ray, out dist1, out dist2);
            //
            // double dist3; double dist4;
            // int two = sphere.CheckIntersection(ray2, out dist3, out dist4);
            //
            // double dist5; double dist6;
            // int three = sphere.CheckIntersection(ray3, out dist5, out dist6);

            // double dist7;
            // bool four = plane.CheckIntersection(ray2, out dist7);

            var xsize = 1024;
            var ysize = 1024;

            var perspective = new Bitmap(xsize, ysize, PixelFormat.Format32bppArgb);
            var orthogonal = new Bitmap(xsize, ysize, PixelFormat.Format32bppArgb);
            var antiAliasing = new AntiAliasingGrid(5);

            var scene = new Scene { BackgroundColor = new Intensity(0, 255, 0) };
            scene.Spheres.Add(sphere);
            scene.Spheres.Add(secondSphere);
            // scene.Planes.Add(plane);

            for (var i = 0; i < 4; i++)
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

                var pos = new Vector(x, 0, z);

                var perspectiveCamera = new PerspectiveCamera(pos, new Vector()) { AntiAliasing = antiAliasing };
                perspectiveCamera.DrawScene(scene, perspective);

                var orthogonalCamera = new OrthogonalCamera(pos, new Vector()) { AntiAliasing = antiAliasing };
                orthogonalCamera.DrawScene(scene, orthogonal);

                Save(perspective, $"{nameof(perspective)}_{i}");
                Save(orthogonal, $"{nameof(orthogonal)}_{i}");
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