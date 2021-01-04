using CoreAudioApi;

namespace WindowsMicrophoneMuteLibrary
{
    public static class CoreAudioMicMuteHelper
    {
        public static void SetMute(this MMDevice mmDevice, bool mute)
        {
            mmDevice.AudioEndpointVolume.Mute = mute;
        }
    }
}