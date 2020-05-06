using System;

namespace RayTracer.Model
{
    public class Canvas
    {
        public Canvas(int width, int height)
        {
            _canvas = new Colour[width, height];
        }

        public int Width { get => _canvas.GetLength(0); }
        public int Height { get => _canvas.GetLength(1); }

        public Colour GetPixel(int xPosition, int yPosition)
        {
            if (!WidthIsWithinRange(xPosition) || !HeightIsWithinRange(yPosition))
            {
                throw new IndexOutOfRangeException();
            }

            var colour = _canvas[xPosition, yPosition];

            return colour ?? new Colour(0, 0, 0);
        }

        public void SetPixel(Colour colour, int xPosition, int yPosition)
        {
            if (!WidthIsWithinRange(xPosition) || !HeightIsWithinRange(yPosition))
            {
                throw new IndexOutOfRangeException();
            }

            _canvas[xPosition, yPosition] = colour;
        }

        private readonly Colour[,] _canvas;
        private bool WidthIsWithinRange(int width) => width <= (Width - 1) & width >= 0;
        private bool HeightIsWithinRange(int height) => height <= (Height - 1) & height >= 0;
    }
}
