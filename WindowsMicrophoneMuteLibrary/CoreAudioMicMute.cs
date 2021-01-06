using System;
using System.Collections.Generic;
using System.Linq;
using CoreAudioApi;

namespace WindowsMicrophoneMuteLibrary
{
    internal class CoreAudioMicMute
    {
        protected List<MMDevice> MicrophoneDeviceList = new List<MMDevice>();

        public CoreAudioMicMute()
        {
            var devEnum = new MMDeviceEnumerator();

            var devices = devEnum.EnumerateAudioEndPoints(EDataFlow.eCapture, EDeviceState.DEVICE_STATE_ACTIVE);

            for (var i = 0; i < devices.Count; i++)
            {
                MicrophoneDeviceList.Add(devices[i]);
            }

            if (!MicrophoneDeviceList.Any())
            {
                throw new InvalidOperationException("Microphone not found by MicMute Library!");
            }
        }

        public bool IsMicMuted => MicrophoneDeviceList.Any(x => x.AudioEndpointVolume.Mute);

        public void SetMute(bool mute)
        {
            MicrophoneDeviceList.ForEach(x => x.SetMute(mute));
        }
    }
}