using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;
using Tacmin.Core.Extensions;

namespace Tacmin.Core.Infrastructure
{
    public class ValidatorProvider : DataAnnotationsModelValidatorProvider
    {
        protected override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context, IEnumerable<Attribute> attributes)
        {
            var mappedAttributes = Engine.Instance.AutoMap.ConfigurationProvider.GetMappedAttributes(metadata.ContainerType, metadata.PropertyName, attributes);
            foreach (var validator in base.GetValidators(metadata, context, mappedAttributes))
            {
                yield return validator;
            }
            foreach (var typeMap in Engine.Instance.AutoMap.ConfigurationProvider.GetAllTypeMaps().Where(i => i.SourceType == metadata.ModelType))
            {
                if (typeof(IValidatableObject).IsAssignableFrom(typeMap.DestinationType))
                {
                    var model = Engine.Instance.AutoMap.Map(metadata.Model, typeMap.SourceType, typeMap.DestinationType);
                    var modelMetadata = ModelMetadataProviders.Current.GetMetadataForType(() => model, typeMap.DestinationType);
                    yield return new ValidatableObjectAdapter(modelMetadata, context);
                }
            }
        }
    }
}