using System;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct LevelSelectHUDControllerPtr {
        [FieldOffset(0x70)]
        public IntPtr evergate;
        [FieldOffset(0x78)]
        public int levelSelectIndex;
    }

    public class LevelSelectHUDController {
        public EvergateController evergate;
        public int levelSelectIndex;

        public LevelSelectHUDController() {

        }

        public LevelSelectHUDController(LevelSelectHUDControllerPtr ptr, EvergateController evergate) {
            this.levelSelectIndex = ptr.levelSelectIndex;
            this.evergate = evergate;
        }
    }
}