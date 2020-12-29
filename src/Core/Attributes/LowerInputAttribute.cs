using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasySharp.Core.Attributes
{
    /// <summary>
    /// Returns a copy of this String object converted to lowercase using the casing rules of the invariant culture.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public class LowerInputAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            foreach (var arg in context.ActionArguments)
            {
                if (arg.Value is string)
                {
                    string val = arg.Value as string;
                    if (!string.IsNullOrEmpty(val))
                    {
                        context.ActionArguments[arg.Key] = val.Trim();
                    }

                    continue;
                }

                Type argType = arg.Value.GetType();
                if (!argType.IsClass)
                {
                    continue;
                }


                LowerAllStringsInObject(arg.Value, argType);
            }
        }

        private void LowerAllStringsInObject(object arg, Type argType)
        {
            var stringProperties = argType.GetProperties()
                                          .Where(p => p.PropertyType == typeof(string));

            foreach (var stringProperty in stringProperties)
            {
                string currentValue = stringProperty.GetValue(arg, null) as string;
                if (!string.IsNullOrEmpty(currentValue))
                {
                    stringProperty.SetValue(arg, currentValue.ToLowerInvariant(), null);
                }
            }
        }
    }
}
