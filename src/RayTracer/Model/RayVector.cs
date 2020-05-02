namespace RayTracer.Model
{
    public class RayVector : RayTuple
    {
        public RayVector(float x, float y, float z)
            : base(x, y, z, RayTupleType.Vector)
        {
        }
    }
}
