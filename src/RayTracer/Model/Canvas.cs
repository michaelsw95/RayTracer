using System;
using System.Text;

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

        public string ConvertToPPM()
        {
            const string PPM_FileType_Header = "P3";
            const int Max_Colour_Header = 255;
            var fileSizeHeader = string.Format("{0} {1}", Width, Height);

            var fileBuilder = new StringBuilder()
                .AppendLine(PPM_FileType_Header)
                .AppendLine(fileSizeHeader)
                .AppendLine(Max_Colour_Header.ToString());

            var newLine = string.Empty;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var colour = GetPixel(j, i);

                    newLine += ScailColourForOutput(colour.Red).ToString() + " ";

                    if (newLine.Length > 65)
                    {
                        fileBuilder.AppendLine(newLine.Trim());
                        newLine = "";
                    }

                    newLine += ScailColourForOutput(colour.Green).ToString() + " ";

                    if (newLine.Length > 65)
                    {
                        fileBuilder.AppendLine(newLine.Trim());
                        newLine = "";
                    }

                    newLine += ScailColourForOutput(colour.Blue).ToString() + " ";

                    if (newLine.Length > 65)
                    {
                        fileBuilder.AppendLine(newLine.Trim());
                        newLine = "";
                    }
                }

                fileBuilder.AppendLine(newLine.Trim());
                newLine = "";
            }

            if (newLine.Length > 0)
            {
                fileBuilder.AppendLine(newLine);
            }

            return fileBuilder.ToString();

            static int ScailColourForOutput(float colourValue)
            {
                var scailedValue = Convert.ToInt32(
                    Math.Round(colourValue * Max_Colour_Header)
                );

                if (scailedValue > 255)
                {
                    scailedValue = 255;
                }
                else if (scailedValue < 0)
                {
                    scailedValue = 0;
                }

                return scailedValue;
            }
        }

        private bool WidthIsWithinRange(int width) => width <= (Width - 1) & width >= 0;
        private bool HeightIsWithinRange(int height) => height <= (Height - 1) & height >= 0;

        private readonly Colour[,] _canvas;
    }
}
