using System;

namespace RayTracer.Model
{
    public class WorldObject
    {
        public Guid Id { get; init; }

        public WorldObject() => Id = Guid.NewGuid();
    }
}
