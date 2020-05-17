namespace RayTracer.Model
{
    public class Colour
    {
        public Colour(float red, float green, float blue)
        {
            _backingTuple = new RayTuple(red, green, blue, 0);
        }

        public float Red { get => _backingTuple.X; }
        public float Green { get => _backingTuple.Y; }
        public float Blue { get => _backingTuple.Z; }

        public Colour Add(Colour other)
        {
            var newBackingTuple = _backingTuple.Add(other._backingTuple);

            return new Colour(
                newBackingTuple.X,
                newBackingTuple.Y,
                newBackingTuple.Z);
        }

        public Colour Subtract(Colour other)
        {
            var newBackingTuple = _backingTuple.Subtract(other._backingTuple);

            return new Colour(
                newBackingTuple.X,
                newBackingTuple.Y,
                newBackingTuple.Z);
        }

        public Colour Multiply(float scaler)
        {
            var newBackingTuple = _backingTuple.Multiply(scaler);

            return new Colour(
                newBackingTuple.X,
                newBackingTuple.Y,
                newBackingTuple.Z);
        }

        public Colour HadamardProduct(Colour other)
        {
            return new Colour(
                Red * other.Red,
                Green * other.Green,
                Blue * other.Blue);
        }

        private readonly RayTuple _backingTuple;
    }
}
