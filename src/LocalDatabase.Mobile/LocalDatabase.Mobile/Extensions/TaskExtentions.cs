using System;
using System.Threading.Tasks;

namespace LocalDatabase.Mobile.Extensions
{
    public static class TaskExtentions
    {
        public static async void SaveFireAndForget(this Task task, bool returnToCallingContext,
            Action<Exception> onException = null)
        {
            try
            {
                await task.ConfigureAwait(returnToCallingContext);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}