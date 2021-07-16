using System;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    public enum State // TypeDefIndex: 5501
    {
        NONE = 0,
        IN_GAME = 1,
        PAUSE_MENU = 2,
        DIALOGUE = 3,
        LEVEL_SELECT = 4,
        REWARD_PORTAL = 5,
        REWARDS = 6,
        CREDITS = 7,
        MAIN_MENU = 8,
        OPTIONS = 9,
        ARTIFACTS = 10
    }

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct HUDManager {
        [FieldOffset(0x18)]
        public IntPtr inGameHUD;
        [FieldOffset(0x20)]
        public IntPtr pauseMenuHUD;
        [FieldOffset(0x28)]
        public IntPtr dialogueHUD;
        [FieldOffset(0x30)]
        public IntPtr levelSelectHUD;
        [FieldOffset(0x38)]
        public IntPtr rewardsPortalHUD;
        [FieldOffset(0x40)]
        public IntPtr creditsHUD;
        [FieldOffset(0x48)]
        public IntPtr mainMenuHUD;
        [FieldOffset(0x50)]
        public IntPtr optionsHUD;
        [FieldOffset(0x58)]
        public IntPtr artifactsHUD;
        [FieldOffset(0x60)]
        public State state;
        [FieldOffset(0x68)]
        public IntPtr currentHUD;
    }
}