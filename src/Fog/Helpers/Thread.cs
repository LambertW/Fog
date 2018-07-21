using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fog.Helpers
{
    public static class Thread
    {
        public static void WaitAll(params Action[] actions)
        {
            if (actions == null)
                return;

            List<Task> tasks = new List<Task>();

            foreach (var action in actions)
            {
                tasks.Add(Task.Factory.StartNew(action, TaskCreationOptions.None));
            }

            Task.WaitAll(tasks.ToArray());
        }
    }
}
