using System;

namespace SIS.MvcFramework.Mappping
{
    public static class ModelMapper
    {
        public static TDestination ProjectTo<TDestination>(object obj, TDestination destination)
        {
            var destinationInstance = (TDestination)Activator.CreateInstance(destination.GetType());
            //todo implement
            return destinationInstance;
        }
    }
}