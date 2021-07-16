using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LiveSplit.Evergate {

    [StructLayout(LayoutKind.Explicit, Size = 8, Pack = 1)]
    public struct GameStatePtr {
        [FieldOffset(0x10)]
        public IntPtr roomName;
        [FieldOffset(0x18)]
        public IntPtr roomStartKey;
        [FieldOffset(0x20)]
        public long slots;
        [FieldOffset(0x28)]
        public float playTime;
        [FieldOffset(0x80)]
        public bool finishedGame;
        [FieldOffset(0x81)]
        public bool hasDash;
        [FieldOffset(0x30)]
        public IntPtr cuesFinished;
        [FieldOffset(0x60)]
        public IntPtr pinningMomentsCompleted;
    }

    public class GameState {
        public string roomName;
        public string roomStartKey;
        public long slots;
        public float playTime;
        public bool finishedGame;
        public bool hasDash;
        public List<string> cuesFinished;
        public List<string> pinningMomentsCompleted;

        public GameState() {

        }

        public GameState(GameStatePtr ptr, string roomName, string startKey, List<string> cuesFinished, List<string> pinningMomentsCompleted) {
            this.slots = ptr.slots;
            this.playTime = ptr.playTime;
            this.finishedGame = ptr.finishedGame;
            this.hasDash = ptr.hasDash;
            this.roomName = roomName;
            this.roomStartKey = startKey;
            this.cuesFinished = cuesFinished;
            this.pinningMomentsCompleted = pinningMomentsCompleted;
        }
    }
}