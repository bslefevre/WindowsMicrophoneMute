using System;
using System.IO;
using System.Threading;
using Windows.UI.Notifications;
using WindowsMicrophoneMuteLibrary;

namespace WindowsMicrophoneMute
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var micMute = new WindowsMicMute();

            if (micMute.IsMicMuted)
            {
                micMute.UnMuteMic();
            }
            else
            {
                micMute.MuteMic();
            }

            var micStatus = (micMute.IsMicMuted ? string.Empty : "un") + "muted";

            ToastNotification("Mic is " + micStatus, micStatus);
        }

        private static void ToastNotification(string message, string image)
        {
            var toastXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);
            var stringElements = toastXml.GetElementsByTagName("text");
            stringElements.Item(0).AppendChild(toastXml.CreateTextNode(message));

            var imagePath = $"file:///{Path.Combine(Environment.CurrentDirectory, "Resources", image + ".png")}";
            var imageElements = toastXml.GetElementsByTagName("image");
            imageElements[0].Attributes.GetNamedItem("src").NodeValue = imagePath;

            try
            {
                var toast = new ToastNotification(toastXml);
                toast.Failed += (o, args) =>
                {
                    Console.WriteLine(args.ErrorCode);
                };

                ToastNotificationManager.CreateToastNotifier("Microphone settings")
                    .Show(toast);

                Thread.Sleep(2000);

                ToastNotificationManager.CreateToastNotifier("Microphone settings")
                    .Hide(toast);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}