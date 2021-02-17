using System.IO;
using System.Linq;
using RayTracer.Model;

namespace RayTracer.TestScenarios.Scenarios
{
    public class SpherePlotter : IScenario
    {
        public void Run()
        {
            var canvasSize = 500;

            var rayOrigin = new RayPoint(0, 0, -3);
            var canvas = new Canvas(canvasSize, canvasSize);
            var plotColour = new Colour(0, 0, 255);
            var sphere = new Sphere();

            var wallDepthPosition = 10;
            var wallSize = 10f;
            var pixelSize = wallSize / canvasSize;
            var midpoint = wallSize / 2;

            for (var y = 0; y < canvasSize -1; y++)
            {
                var worldPositionY = midpoint - pixelSize * y;

                for (var x = 0; x < canvasSize - 1; x++)
                {
                    var worldPositionX = (-midpoint) + pixelSize * x;

                    var rayWallHitPoint = new RayPoint(worldPositionX, worldPositionY, wallDepthPosition);

                    var ray = new Ray(rayOrigin, (rayWallHitPoint.Subtract(rayOrigin) as RayVector).Normalise());

                    var intersection = ray.GetIntersects(sphere);

                    if (intersection.Any())
                    {
                        canvas.SetPixel(plotColour, x, y);
                    }
                }
            }

            var toOutput = canvas.ConvertToPPM();

            File.WriteAllText("sphere.ppm", toOutput);
        }
    }
}