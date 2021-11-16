using ElementLib.Models;

namespace ElementLib.Enties
{
    public static class EntityExtensions
    {
        public static ElementEntity ToEntity(this Element model) =>
            new()
            {
                Symbol = model.Symbol,
                Mass = model.Mass,
                Z = model.Z,
                Name = model.Name
            };

        public static Element ToModel(this ElementEntity entity) =>
            new()
            {
                Symbol = entity.Symbol,
                Mass = entity.Mass,
                Z = entity.Z,
                Name = entity.Name
            };

    }
}
