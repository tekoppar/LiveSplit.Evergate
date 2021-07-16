using System;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct GameStateManager {
        [FieldOffset(0x18)]
        public IntPtr gameState;
        [FieldOffset(0x20)]
        public IntPtr activeSaveSlot;
        [FieldOffset(0x28)]
        public IntPtr slots;
    }
}