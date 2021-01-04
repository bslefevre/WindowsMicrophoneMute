namespace WindowsMicrophoneMuteLibrary
{
    public class WindowsMicMute
    {
        private readonly CoreAudioMicMute _VistaMicMute;

        public WindowsMicMute()
        {
            // Voor debug perposes kun je het Output Type aanpassen van [Windows Application] naar [Console Application]
            this._VistaMicMute = new CoreAudioMicMute();
        }

        public bool IsMicMuted => this._VistaMicMute.IsMicMuted;

        public void MuteMic()
        {
            this._VistaMicMute.SetMute(true);
        }

        public void UnMuteMic()
        {
            this._VistaMicMute.SetMute(false);
        }
    }
}