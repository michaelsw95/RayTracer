namespace RayTracer.Utility.Model
{
    public record ShearingTransform
    {
        public float XinProportionToY { get; init; }
        public float XinProportionToZ { get; init; }
        public float YinProportionToX { get; init; }
        public float YinProportionToZ { get; init; }
        public float ZinProportionToX { get; init; }
        public float ZinProportionToY { get; init; }
    }
}
