using System;
using System.Reflection;

namespace CarRentService.Common
{
    class AssemblyHelper
    {
        public static Assembly[] GetAllAssemblies()
        {
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
