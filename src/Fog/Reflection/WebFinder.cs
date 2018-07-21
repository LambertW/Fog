using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fog.Reflection
{
    public class WebFinder : Finder
    {
        public override List<Assembly> GetAssemblies()
        {
            LoadAssemblies(AppContext.BaseDirectory);

            return base.GetAssemblies();
        }
    }
}
