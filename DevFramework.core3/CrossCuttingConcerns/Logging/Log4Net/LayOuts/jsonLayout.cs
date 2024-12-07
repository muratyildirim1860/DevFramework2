using log4net.Core;
using log4net.Layout;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFramework.core3.CrossCuttingConcerns.Logging.Log4Net.LayOuts
{
    //3

    //Loglamalarımızı json formatında tutacagız.Bunun bir log4net layot olabilmesi için LayoutSkeleton iskeletinden implemente edilir.
    //
    public class jsonLayout : LayoutSkeleton
    {
        public override void ActivateOptions()
        {
           
        }

        public override void Format(TextWriter writer, LoggingEvent loggingEvent)
        {
            //parametreyi gönderdigimde loggingEvent masseg datayı yani loglanacak datayı yani username SerializableLogEvent gecirmiş olacak.
            var logEvent = new SerializableLogEvent(loggingEvent);
            //Bunu jsona cevirmem gerekiyor aşagıdaki kod da onu uyguluyor olacagız.
            var json = JsonConvert.SerializeObject(logEvent,Formatting.Indented);
            writer.WriteLine(json);
        }
    }
}
