using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public interface INotificationAttachment
    {
        string Name { get; }

        Stream Data { get; }
    }
}
