using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct EvergateControllerPtr {
        [FieldOffset(0x108)]
        public IntPtr books;
        [FieldOffset(0x170)]
        public IntPtr allLevels;
        [FieldOffset(0xF0)]
        public IntPtr portal;
        [FieldOffset(0x200)]
        public int selectedBookIndex;
    }

    public class EvergateController {
        public List<EvergateBookControllerPtr> books;
        public List<LevelSelectBehavior> allLevels;
        public EvergatePortalControllerPtr portal;
        public int selectedBookIndex;

        public EvergateController() {

        }

        public EvergateController(EvergateControllerPtr ptr, List<EvergateBookControllerPtr> books, List<LevelSelectBehavior> levels, EvergatePortalControllerPtr portal) {
            this.selectedBookIndex = ptr.selectedBookIndex;
            this.books = books;
            this.allLevels = levels;
            this.portal = portal;
        }

        public void WriteFile() {
            string content = "";

            foreach (LevelSelectBehavior level in allLevels) {
                content += level.worldName + Environment.NewLine;
                content += level.subtitle + Environment.NewLine;

                content += Environment.NewLine + Environment.NewLine + "Levels";
                foreach (LevelSelectInfo info in level.levelInfos) {
                    content += info.sceneName + " - " + info.levelLabel + Environment.NewLine;
                }

                content += Environment.NewLine;
            }

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + "\\levelinfo.txt", content);
        }
    }
}