using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace Tacmin.Core.Extensions
{
    public static class ModelStateExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Tin">ViewModel Type</typeparam>
        /// <typeparam name="Tout">Domain ModelType</typeparam>
        /// <param name="modelstate">controllers modelstate</param>
        /// <param name="results">Results of validation</param>
        public static void AddValidationResult<Tin, Tout>(this ModelStateDictionary modelstate, ValidationResult results)
            where Tout : class
            where Tin : class
        {
            var map = Engine.Instance.AutoMap.ConfigurationProvider.FindTypeMapFor<Tout, Tin>();
            var properties = map.PropertyMaps;
            var destinationPropertyName = string.Empty;
            foreach (var result in results.MemberNames)
            {
                var property = properties.FirstOrDefault(pm => pm.SourceMember != null && pm.SourceMember.Name.Equals(result));
                if (property != null)
                {
                    destinationPropertyName = property.DestinationMember.Name;
                }
                else
                {
                    destinationPropertyName = string.Empty;
                }
                modelstate.AddModelError(destinationPropertyName, result);
            }
        }
    }
}
