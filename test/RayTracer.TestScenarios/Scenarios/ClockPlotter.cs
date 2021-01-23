using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RayTracer.Model;
using RayTracer.Utility;
using RayTracer.Utility.Model;

namespace RayTracer.TestScenarios.Scenarios
{
    public class ClockPlotter : IScenario
    {
        public void Run()
        {
            var canvas = new Canvas(500, 500);
            var midpoint = canvas.Width / 2;
            var clockRadius = (float)3 / 8 * canvas.Width;
            var plotColour = new Colour(1, 0, 0);

            var hourRotation = Transformation.GetRotationMatrix(
                RotationAxis.Y, 
                (float)Math.PI / 6
            );

            var twelve = new RayPoint(0, 0, 1);
            var hours = new List<RayPoint>(12) { twelve };

            while (hours.Count < 12)
            {
                hours.Add(hours.Last().Multiply(hourRotation) as RayPoint);
            }

            for (int i = 0; i < hours.Count; i++)
            {
                var xPlot = (hours[i].X * clockRadius) + midpoint;
                var zPlot = (hours[i].Z * clockRadius) + midpoint;

                canvas.SetPixel(
                    plotColour,
                    Convert.ToInt32(Math.Round(xPlot)),
                    Convert.ToInt32(Math.Round(zPlot)));
            }

            var toOutput = canvas.ConvertToPPM();

            File.WriteAllText("clock.ppm", toOutput);
        }
    }
}
