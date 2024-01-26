using Kendo.Mvc;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tacmin.Core.Extensions
{
    public static class DatasourceRequestMapper
    {
        public static DataSourceRequest ReMap<TSource, TDestination>(this DataSourceRequest request)
        {
            var mappings = GetPropertyMappings(new Dictionary<string, string>(), typeof(TSource), typeof(TDestination));

            request.Filters = MapFilterDescriptors<TSource, TDestination>(mappings, request.Filters).ToList();
            request.Sorts = MapSortDescriptors(mappings, request.Sorts).ToList();

            return request;
        }

        private static IDictionary<string, string> GetPropertyMappings(IDictionary<string, string> mappings, Type sourceType, Type destinationType)
        {
            var config = Engine.Instance.AutoMap.ConfigurationProvider;
            var map = config.FindTypeMapFor(sourceType, destinationType);

            // We are only interested in custom expressions because they do not map field to field
            foreach (var propertyMap in map.PropertyMaps.Where(pm => pm.CustomMapExpression != null))
            {
                // Get the linq expression body
                var body = propertyMap.CustomMapExpression.Body.ToString();

                // Get the item tag
                var tag = propertyMap.CustomMapExpression.Parameters[0].Name;

                var destination = body.Replace(string.Format("{0}.", tag), string.Empty);
                var source = propertyMap.DestinationMember.Name;

                mappings.Add(source, destination);
            }

            return mappings;
        }

        private static IEnumerable<IFilterDescriptor> MapFilterDescriptors<TModel, TResult>(IDictionary<string, string> mappings, IEnumerable<IFilterDescriptor> filters)
        {
            foreach (var filter in filters)
            {
                if (filter is FilterDescriptor)
                {
                    var normalFilter = (FilterDescriptor)filter;
                    if (mappings.ContainsKey(normalFilter.Member))
                    {
                        normalFilter.Member = mappings[normalFilter.Member];
                    }
                }
                else if (filter is CompositeFilterDescriptor)
                {
                    var compositeFilter = (CompositeFilterDescriptor)filter;
                    MapFilterDescriptors<TModel, TResult>(mappings, compositeFilter.FilterDescriptors);
                }
            }

            return filters;
        }

        private static IEnumerable<SortDescriptor> MapSortDescriptors(IDictionary<string, string> mappings, IEnumerable<SortDescriptor> sortDescriptors)
        {
            foreach (var sortDescriptor in sortDescriptors)
            {
                var sourceMemberName = sortDescriptor.Member;
                if (mappings.ContainsKey(sourceMemberName))
                {
                    sortDescriptor.Member = mappings[sourceMemberName];
                }
            }

            return sortDescriptors;
        }
    }
}
