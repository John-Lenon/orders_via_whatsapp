namespace Application.Utilities
{
    public static class CustomMapper
    {
        public static TDestination MapToDTO<TEntity, TDestination>(this TEntity entidade)
            where TDestination : class, new()
        {
            if (entidade is null) return null;
            var dto = new TDestination();
            CopyPropertyValues(entidade, dto);
            return dto;
        }

        public static TEntity MapToEntity<TDTO, TEntity>(this TDTO dto)
            where TEntity : class, new()
        {
            if (dto is null) return null;
            var entity = new TEntity();
            CopyPropertyValues(dto, entity);
            return entity;
        }

        public static void GetValuesFrom<TSource, TDestination>(this TDestination destination, TSource source,
            bool ignoreNullValues = false)
        {
            CopyPropertyValues(source, destination, ignoreNullValues);
        }

        private static void CopyPropertyValues<TSource, TDestination>(TSource source, TDestination destination,
            bool ignoreNullValues = false)
        {
            if (source is null || destination is null) return;

            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();

            foreach (var destinationProperty in destinationProperties)
            {
                var sourceProperty = sourceProperties.FirstOrDefault(x =>
                    x.Name == destinationProperty.Name
                    && x.PropertyType.FullName == destinationProperty.PropertyType.FullName);

                if (sourceProperty is null) continue;

                var sourceValue = sourceProperty.GetValue(source);
                if (ignoreNullValues && sourceValue is null) continue;

                destinationProperty.SetValue(destination, sourceValue);
            }
        }
    }
}
