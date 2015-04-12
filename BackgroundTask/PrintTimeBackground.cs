using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;

namespace BackgroundTask
{
    sealed public class PrintTimeBackground : IBackgroundTask
    {
        public async void Run(IBackgroundTaskInstance taskInstance)
        {
            var def = taskInstance.GetDeferral();

            var result = await BackgroundExecutionManager.RequestAccessAsync();

            if (result == BackgroundAccessStatus.Denied) {
                Debug.WriteLine("Sorry :(");
                return;
            }
            //This is our simply background task, it will print the current time
            Debug.WriteLine("Time is {0}",DateTime.Now.TimeOfDay);

            HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.GetAsync("http://time.jsontest.com/");

            String content = await response.Content.ReadAsStringAsync();

            Debug.WriteLine(content);

            //notify task is complete
            def.Complete();
        }
    }
}
