using AutoFixture;

namespace RayTracer.ModelTests.UnitTests
{
    public class BaseModelTests
    {
        protected readonly Fixture _fixture;

        public BaseModelTests()
        {
            _fixture = new Fixture();
        }

        protected (float x, float y, float z) CreateRandomPosition(Fixture fixture) =>
            (fixture.Create<float>(), fixture.Create<float>(), fixture.Create<float>());
    }
}
