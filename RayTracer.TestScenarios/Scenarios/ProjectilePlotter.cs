using System;
using System.IO;
using RayTracer.Model;

namespace RayTracer.TestScenarios.Scenarios
{
    public class ProjectilePlotter : IScenario
    {
        public void Run()
        {
            var canvas = new Canvas(900, 550);
            var startPoint = new RayPoint(0, 1, 0);
            var startVelocity = new RayVector(1, 1.8F, 0)
                .Normalise()
                .Multiply(11.25F);

            var projectile = new Projectile(startPoint, startVelocity as RayVector);
            var environment = new Environment(new RayVector(0, -0.1F, 0), new RayVector(-0.01F, 0, 0));
            var plotColour = new Colour(1, 0, 0);

            do
            {
                projectile = Tick(environment, projectile);

                if (projectile.Position.Y < 0)
                {
                    break;
                }

                canvas.SetPixel(plotColour,
                    Convert.ToInt32(Math.Round(projectile.Position.X)),
                    Convert.ToInt32(Math.Round(canvas.Height - projectile.Position.Y)));
            } while (projectile.Position.Y >= 0);

            var toOutput = canvas.ConvertToPPM();

            File.WriteAllText("projectile.ppm", toOutput);
        }

        private static Projectile Tick(Environment environment, Projectile projectile)
        {
            var position = projectile.Position.Add(projectile.Velocity);
            var velocity = projectile.Velocity.Add(environment.Gravity).Add(environment.Wind);

            return new Projectile(position as RayPoint, velocity as RayVector);
        }

        private struct Environment
        {
            public Environment(RayVector gravity, RayVector wind)
            {
                Gravity = gravity;
                Wind = wind;
            }

            public RayVector Gravity { get; }
            public RayVector Wind { get; }
        }

        private struct Projectile
        {
            public Projectile(RayPoint position, RayVector velocity)
            {
                Position = position ?? throw new ArgumentNullException(nameof(position));
                Velocity = velocity;
            }

            public RayPoint Position { get; }
            public RayVector Velocity { get; }
        }
    }
}