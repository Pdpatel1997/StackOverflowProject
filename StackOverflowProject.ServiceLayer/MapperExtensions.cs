using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;
using System.Text;
using AutoMapper;

namespace StackOverflowProject.ServiceLayer
{
    public static class MapperExtensions
    {
        public static void IgnoreUnmappedProperties(TypeMap map, IMappingExpression expr)
        {
            foreach (string propName  in map.GetUnmappedPropertyNames())
            {
                if (map.SourceType.GetProperties() != null)
                {
                    expr.ForMember(propName,opt=>opt.Ignore());
                }
                if (map.DestinationType.GetProperties() != null)
                {
                    expr.ForMember(propName, opt => opt.Ignore());
                }
            }
        }
        public static void IgnoreUnmapped(this IProfileExpression profile)
        {
            profile.ForAllMaps(IgnoreUnmappedProperties);
        }
    }

    
}
