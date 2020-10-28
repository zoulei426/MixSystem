using Mix.Core.Mapping;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mix.Library.Services
{
    /// <summary>
    /// 属性映射服务
    /// </summary>
    public class PropertyMappingService : IPropertyMappingService
    {
        private readonly Dictionary<string, PropertyMappingValue> _employeePropertyMapping =
            new Dictionary<string, PropertyMappingValue>(StringComparer.OrdinalIgnoreCase)
            {
                {"Name", new PropertyMappingValue(new List<string> {"FirstName", "LastName"})},
                {"Age", new PropertyMappingValue(new List<string>{"DateOfBirth"}, true)}
            };

        private readonly IList<IPropertyMapping> _propertyMappings = new List<IPropertyMapping>();

        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyMappingService"/> class.
        /// </summary>
        public PropertyMappingService()
        {
            _propertyMappings.Add(new PropertyMapping<EmployeeDto, Employee>(_employeePropertyMapping));
        }

        /// <summary>
        /// Gets the property mapping.
        /// </summary>
        /// <typeparam name="TSouce">The type of the souce.</typeparam>
        /// <typeparam name="TDestination">The type of the destination.</typeparam>
        /// <returns></returns>
        /// <exception cref="Exception">无法找到唯一的映射关系：{typeof(TSouce)}，{typeof(TDestination)}</exception>
        public Dictionary<string, PropertyMappingValue> GetPropertyMapping<TSouce, TDestination>()
        {
            var matchingMapping = _propertyMappings.OfType<PropertyMapping<TSouce, TDestination>>();

            var propertyMappings = matchingMapping.ToList();
            if (propertyMappings.Count() != 1)
            {
                throw new Exception($"无法找到唯一的映射关系：{typeof(TSouce)}，{typeof(TDestination)}");
            }

            return propertyMappings.First().MappingDictionary;
        }

        //public bool ValidMappingExistsFor<TSource, TDestination>(string fields)
        //{
        //    if (fields.IsNullOrWhiteSpace())
        //        return true;

        //    var propertyMapping = GetPropertyMapping<TSource, TDestination>();

        //    var splitedFields = fields.Split(",");
        //    foreach (var field in splitedFields)
        //    {
        //        var trimmedField = field.Trim();
        //        var indexOfFirstSpace = trimmedField.IndexOf(" ");
        //        var propertyName = indexOfFirstSpace == -1
        //            ? trimmedField : trimmedField.Remove(indexOfFirstSpace);

        //        if (!propertyMapping.ContainsKey(propertyName))
        //            return false;
        //    }

        //    return true;
        //}
    }
}