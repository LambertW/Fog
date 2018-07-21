using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

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
