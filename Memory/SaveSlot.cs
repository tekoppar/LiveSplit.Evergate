using System;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct SaveSlotPtr {
        [FieldOffset(0x10)]
        public bool newGame;
        [FieldOffset(0x18)]
        public IntPtr gameState;
        [FieldOffset(0x20)]
        public IntPtr _title;
        [FieldOffset(0x28)]
        public IntPtr _time;
        [FieldOffset(0x30)]
        public int _essence;
        [FieldOffset(0x34)]
        public int _slotNumber;
        [FieldOffset(0x38)]
        public IntPtr _filename_k__BackingField;
    }

    public class SaveSlot {
        public bool newGame;
        public GameState gameState;
        public string _title;
        public string _time;
        public int _essence;
        public int _slotNumber;
        public string _filename_k__BackingField;

        public SaveSlot() {

        }

        public SaveSlot(SaveSlotPtr ptr, GameState gameState, string _title, string _time, string _filename_k__BackingField) {
            this._essence = ptr._essence;
            this._slotNumber = ptr._slotNumber;
            this.newGame = ptr.newGame;
            this.gameState = gameState;
            this._title = _title;
            this._time = _time;
            this._filename_k__BackingField = _filename_k__BackingField;
        }
    }
}