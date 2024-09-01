using FluentValidation.Internal;
using System.Linq.Expressions;

namespace Application.Utilities
{
    public static class CustomMapper
    {
        public static TDestination MapToDTO<TEntity, TDestination>(this TEntity entidade,
            bool ignoreNullValues = false,
            params Expression<Func<TEntity, object>>[] fieldsToIgnore)
            where TDestination : class, new()
        {
            if (entidade is null) return null;
            var dto = new TDestination();
            CopyPropertyValues(entidade, dto, ignoreNullValues, fieldsToIgnore);
            return dto;
        }

        public static TEntity MapToEntity<TDTO, TEntity>(this TDTO dto,
            bool ignoreNullValues = false,
            params Expression<Func<TDTO, object>>[] fieldsToIgnore)
            where TEntity : class, new()
        {
            if (dto is null) return null;
            var entity = new TEntity();
            CopyPropertyValues(dto, entity, ignoreNullValues, fieldsToIgnore);
            return entity;
        }

        public static void GetValuesFrom<TSource, TDestination>(this TDestination destination, TSource source,
            bool ignoreNullValues = false, params Expression<Func<TSource, object>>[] fieldsToIgnore)
        {
            CopyPropertyValues(source, destination, ignoreNullValues, fieldsToIgnore);
        }

        private static void CopyPropertyValues<TSource, TDestination>(TSource source, TDestination destination,
            bool ignoreNullValues = false, params Expression<Func<TSource, object>>[] fieldsToIgnore)
        {
            if (source is null || destination is null) return;

            var sourceProperties = source.GetType().GetProperties();
            var destinationProperties = destination.GetType().GetProperties();
            var listNameFieldsToIgnore = fieldsToIgnore is not null ? fieldsToIgnore.Select(x => x.GetMember().Name) : null;

            foreach (var destinationProperty in destinationProperties)
            {
                var sourceProperty = sourceProperties.FirstOrDefault(x =>
                    x.Name == destinationProperty.Name
                    && x.PropertyType.FullName == destinationProperty.PropertyType.FullName);

                if (sourceProperty is null) continue;

                var sourceValue = sourceProperty.GetValue(source);

                if (listNameFieldsToIgnore is not null
                    && listNameFieldsToIgnore.Any()
                    && listNameFieldsToIgnore.Contains(sourceProperty.Name)) continue;

                if (ignoreNullValues && sourceValue is null) continue;

                destinationProperty.SetValue(destination, sourceValue);
            }
        }
    }
}
