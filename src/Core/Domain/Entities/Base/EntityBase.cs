namespace Domain.Entities.Base
{
    public abstract class EntityBase
    {
        public int Id { get; set; }
        public Guid Codigo { get; set; }

        // Declarar propriedades para paginação
    }
}
