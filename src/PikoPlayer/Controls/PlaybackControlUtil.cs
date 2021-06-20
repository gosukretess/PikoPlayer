using WindowsInput;
using WindowsInput.Native;

namespace PikoPlayer.Controls
{
    public class PlaybackControlUtil
    {
        private readonly InputSimulator _inputSimulator;

        public PlaybackControlUtil()
        {
            _inputSimulator = new InputSimulator();
        }

        public void ControlPlayback(ControlAction action)
        {
            switch (action)
            {
                case ControlAction.Prev:
                    _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MEDIA_PREV_TRACK);
                    break;
                case ControlAction.Play:
                    _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MEDIA_PLAY_PAUSE);
                    break;
                case ControlAction.Next:
                    _inputSimulator.Keyboard.KeyDown(VirtualKeyCode.MEDIA_NEXT_TRACK);
                    break;
            }
        }
    }
}