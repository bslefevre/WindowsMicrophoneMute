using System;
using System.Drawing;
using System.IO;
using System.Threading;
using Windows.UI.Notifications;
using WindowsMicrophoneMuteLibrary;
using CUE.NET;
using CUE.NET.Devices.Generic.Enums;

namespace WindowsMicrophoneMute
{
    internal class Program
    {
        public const string ApplicationId = "Microphone mute settings";

        private static void Main()
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

            try
            {
                CueSettingsSdk(micMute.IsMicMuted);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }

            ToastNotification("Mic is " + micStatus, micStatus);
        }

        private static void CueSettingsSdk(bool isMicMuted)
        {
            var color = isMicMuted ? Color.Red : Color.Green;
            CueSDK.Initialize();
            CueSDK.UpdateMode = UpdateMode.Manual;
            var keyboard = CueSDK.KeyboardSDK;
            if (CueSDK.IsInitialized && keyboard != null)
            {
                keyboard[CorsairLedId.Mute].Color = color;
                keyboard[CorsairLedId.Mute].IsLocked = true;
                keyboard.Update();
            }
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

                ToastNotificationManager.CreateToastNotifier(ApplicationId)
                    .Show(toast);

                Thread.Sleep(2000);

                ToastNotificationManager.CreateToastNotifier(ApplicationId)
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