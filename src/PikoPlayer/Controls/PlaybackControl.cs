using WindowsInput;
using WindowsInput.Native;

namespace PikoPlayer.Controls
{
    public class PlaybackControl
    {
        private readonly InputSimulator _inputSimulator;

        public PlaybackControl()
        {
            _inputSimulator = new InputSimulator();
        }

        public void ControlPlayback(PlaybackControlAction action)
        {
            switch (action)
            {
                case PlaybackControlAction.Prev:
                    _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MEDIA_PREV_TRACK);
                    break;
                case PlaybackControlAction.Play:
                    _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MEDIA_PLAY_PAUSE);
                    break;
                case PlaybackControlAction.Next:
                    _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MEDIA_NEXT_TRACK);
                    break;
            }
        }
    }
}