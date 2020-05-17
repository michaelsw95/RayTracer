using System;
using System.Text;
using RayTracer.Utility;

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
            if (!Numeric.IsWithinRange(0, Width - 1, xPosition) ||
                !Numeric.IsWithinRange(0, Height - 1, yPosition))
            {
                throw new IndexOutOfRangeException();
            }

            var colour = _canvas[xPosition, yPosition];

            return colour ?? new Colour(0, 0, 0);
        }

        public void SetPixel(Colour colour, int xPosition, int yPosition)
        {
            if (!Numeric.IsWithinRange(0, Width - 1, xPosition) ||
                !Numeric.IsWithinRange(0, Height - 1, yPosition))
            {
                throw new IndexOutOfRangeException();
            }

            _canvas[xPosition, yPosition] = colour;
        }

        public string ConvertToPPM()
        {
            var ppmFileStringBuilder = new StringBuilder();
            ppmFileStringBuilder = AddHeaderLinesForPPM(ppmFileStringBuilder);

            var fileLine = string.Empty;
            for (int i = 0; i < Height; i++)
            {
                for (int j = 0; j < Width; j++)
                {
                    var colour = GetPixel(j, i);

                    fileLine += PlotColourPart(colour.Red);
                    AddNewLineIfNeeded();

                    fileLine += PlotColourPart(colour.Green);
                    AddNewLineIfNeeded();

                    fileLine += PlotColourPart(colour.Blue);
                    AddNewLineIfNeeded();
                }

                ppmFileStringBuilder.AppendLine(fileLine.Trim());
                fileLine = "";
            }

            return ppmFileStringBuilder.ToString();

            void AddNewLineIfNeeded()
            {
                const int Line_Break_Length = 65;
                if (fileLine.Length > Line_Break_Length)
                {
                    ppmFileStringBuilder.AppendLine(fileLine.Trim());
                    fileLine = "";
                }
            }
        }

        private StringBuilder AddHeaderLinesForPPM(StringBuilder stringBuilder)
        {
            const string PPM_FileType_Header = "P3";
            var fileSizeHeader = string.Format("{0} {1}", Width, Height);

            return stringBuilder
                .AppendLine(PPM_FileType_Header)
                .AppendLine(fileSizeHeader)
                .AppendLine(Max_Colour_Header.ToString());
        }

        private string PlotColourPart(float colourPart)
        {
            return ScailColourForCanvas(colourPart).ToString() + " ";
        }

        private int ScailColourForCanvas(float colourValue)
        {
            var scailedValue = Convert.ToInt32(colourValue * Max_Colour_Header);

            var clamped = Numeric.Clamp(0, Max_Colour_Header, scailedValue);

            return clamped;
        }

        private const int Max_Colour_Header = 255;
        private readonly Colour[,] _canvas;
    }
}
