using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorWasmUI.Helpers.Classes
{
    public static class DataAnnotationsHelper
    {
        public static string GetDisplayName<T>(Expression<Func<T>> forPropertyExpression)
        {
            var expression = (MemberExpression)forPropertyExpression.Body;
            var attribute = expression.Member.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
            return attribute?.DisplayName ?? expression.Member.Name;
        }

        public static string GetDisplayName<T>(Expression<Func<T, object>> propertyExpression)
        {
            var expression = propertyExpression.Body;
            MemberExpression memberExpression;
            DisplayNameAttribute displayNameAttribute;
            if (expression is MemberExpression)
            {
                // Reference type property or field
                memberExpression = (MemberExpression)expression;
                displayNameAttribute = memberExpression.Member.GetCustomAttribute<DisplayNameAttribute>();
                return displayNameAttribute == null ? memberExpression.Member.Name : displayNameAttribute.DisplayName;
            }
            if (expression is UnaryExpression)
            {
                // Property, field of method returning value type
                var unaryExpression = (UnaryExpression)expression;
                if (!(unaryExpression.Operand is MethodCallExpression))
                {
                    memberExpression = (MemberExpression)unaryExpression.Operand;
                    displayNameAttribute = memberExpression.Member.GetCustomAttribute<DisplayNameAttribute>();
                    return displayNameAttribute == null ? memberExpression.Member.Name : displayNameAttribute.DisplayName;
                }
            }
            return null;
        }

        public static List<string> GetDisplayNameAttributes(PropertyInfo[] properties)
        {
            List<string> displayNameAttributes = null;
            List<Attribute> attributes;
            bool displayNameAttributeFound;
            if (properties != null && properties.Length > 0)
            {
                displayNameAttributes = new List<string>();
                foreach (var property in properties)
                {
                    displayNameAttributeFound = false;
                    attributes = property.GetCustomAttributes().ToList();
                    if (attributes != null && attributes.Count > 0)
                    {
                        foreach (var attribute in attributes)
                        {
                            if (attribute.GetType() == typeof(DisplayNameAttribute))
                            {
                                displayNameAttributes.Add(((DisplayNameAttribute)attribute).DisplayName);
                                displayNameAttributeFound = true;
                                break;
                            }
                        }
                    }
                    if (!displayNameAttributeFound)
                    {
                        displayNameAttributes.Add(property.Name);
                    }
                }
            }
            return displayNameAttributes;
        }
    }
}
