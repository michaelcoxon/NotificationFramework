using System.IO;
using System.Threading.Tasks;

namespace NotificationFramework
{
    public interface INotificationContent
    {
        Task ExecuteAsync(TextWriter textWriter);
    }
}