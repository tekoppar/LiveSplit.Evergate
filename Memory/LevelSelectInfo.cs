using System;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct LevelSelectInfoPtr {
        [FieldOffset(0x18)]
        public IntPtr sceneName;
        [FieldOffset(0x20)]
        public IntPtr levelLabel;
        [FieldOffset(0x38)]
        public bool visited;
        [FieldOffset(0x39)]
        public bool completed;
        [FieldOffset(0x31)]
        public bool initialized;
    }

    public class LevelSelectInfo {
        public string sceneName = "null";
        public string levelLabel;
        public bool visited;
        public bool completed;
        public bool initialized;

        public LevelSelectInfo() {

        }

        public LevelSelectInfo(LevelSelectInfoPtr ptr, string sceneName, string levelLabel) {
            this.visited = ptr.visited;
            this.completed = ptr.completed;
            this.sceneName = sceneName;
            this.levelLabel = levelLabel;
            this.initialized = ptr.initialized;
        }
    }
}