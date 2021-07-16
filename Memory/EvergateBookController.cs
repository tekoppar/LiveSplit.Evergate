using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct EvergateBookControllerPtr {
        [FieldOffset(0x6D)]
        public bool _isRevealed;
        [FieldOffset(0x6E)]
        public bool _isOpened;
        [FieldOffset(0x6F)]
        public bool _isHighlighted;
    }

    public class EvergateBookController {


        public EvergateBookController() {

        }
    }
}