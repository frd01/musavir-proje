using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Tacmin.Core.Extensions;

namespace Tacmin.Core.Infrastructure
{
    public class MetadataProvider : DataAnnotationsModelMetadataProvider
    {
        protected override ModelMetadata CreateMetadata(IEnumerable<Attribute> attributes, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
        {
            var mappedAttributes = Engine.Instance.AutoMap.ConfigurationProvider.GetMappedAttributes(containerType, propertyName, attributes);
            return base.CreateMetadata(mappedAttributes, containerType, modelAccessor, modelType, propertyName);
        }
    }
}
