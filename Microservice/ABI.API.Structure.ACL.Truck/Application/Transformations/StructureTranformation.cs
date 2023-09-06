using ABI.API.Structure.ACL.Truck.Application.Transformations.Base;
using ABI.Framework.MS.Helpers.Extensions;
using System;
using System.Reflection;

namespace ABI.API.Structure.ACL.Truck.Application.Transformations
{
    public static class StructureTranformation
    {
        public static TransformationBase InstanciateTransformation(TypeTrasformation transformation)
        {
            Assembly currentAssembly = Assembly.GetExecutingAssembly();
            Type transformationType = currentAssembly.GetType(string.Format("ABI.API.Structure.ACL.Truck.Application.Transformations.{0}", transformation.Description()));
            object transformationClass = Activator.CreateInstance(transformationType);

            return (TransformationBase)transformationClass;

        }

    }
}
