using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Tacmin.Core.Extensions
{
    public static class AutoMapperExtensions
    {
        public static IEnumerable<Attribute> GetMappedAttributes(this IConfigurationProvider mapper, Type sourceType, string propertyName, IEnumerable<Attribute> existingAttributes)
        {
            if (sourceType != null)
            {
                foreach (var typeMap in mapper.GetAllTypeMaps().Where(i => i.SourceType == sourceType))
                {
                    foreach (var propertyMap in typeMap.PropertyMaps)
                    {
                        if (propertyMap.Ignored || propertyMap.SourceMember == null)
                            continue;

                        if (propertyMap.SourceMember.Name == propertyName)
                        {
                            foreach (Attribute attribute in propertyMap.DestinationMember.GetCustomAttributes(typeof(Attribute), true))
                            {
                                if (!existingAttributes.Any(i => i.GetType() == attribute.GetType()))
                                    yield return attribute;
                            }
                        }
                    }
                }
            }

            if (existingAttributes != null)
            {
                foreach (var attribute in existingAttributes)
                {
                    yield return attribute;
                }
            }
        }

        public static IMappingExpression<TSource, TDestination> Ignore<TSource, TDestination>(
            this IMappingExpression<TSource, TDestination> map, Expression<Func<TDestination, object>> selector)
        {
            map.ForMember(selector, config => config.Ignore());
            return map;
        }
    }
}
