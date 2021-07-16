using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct EvergatePortalControllerPtr {
        [FieldOffset(0x50)]
        public bool _isOpen;
        [FieldOffset(0x54)]
        public bool _selectedIndex;
    }

    public class EvergatePortalController {

        public EvergatePortalController() {

        }
    }
}