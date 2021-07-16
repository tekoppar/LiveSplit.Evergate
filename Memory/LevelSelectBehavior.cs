using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct LevelSelectBehaviorPtr {
        [FieldOffset(0x18)]
        public IntPtr worldName;
        [FieldOffset(0x20)]
        public IntPtr subtitle;
        [FieldOffset(0x28)]
        public IntPtr levelInfos;
    }

    public class LevelSelectBehavior {
        public string worldName;
        public string subtitle;
        public List<LevelSelectInfo> levelInfos;

        public LevelSelectBehavior() {
            
        }

        public LevelSelectBehavior(string name, string subtitle, List<LevelSelectInfo> list) {
            this.worldName = name;
            this.subtitle = subtitle;
            this.levelInfos = list;
        }
    }
}