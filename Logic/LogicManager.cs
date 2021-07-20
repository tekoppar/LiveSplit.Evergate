using System;
using System.Collections.Generic;
using Tem.TemClass;

namespace LiveSplit.Evergate {
    public class LogicManager {
        public bool ShouldSplit { get; private set; }
        public bool ShouldReset { get; private set; }
        public int CurrentSplit { get; private set; }

        private bool _running;
        public bool Running {
            get => _running;
            private set {
                if (!_running && value) {
                    hadDebug = false;
                }
                _running = value;
            }
        }

        public bool Paused { get; private set; }
        public float GameTime { get; private set; }
        public MemoryManager Memory { get; private set; }
        public SplitterSettings Settings { get; private set; }
        private bool lastBoolValue = true;
        private bool hadDebug;
        private int lastIntValue, lastIntValue2;
        private string lastStrValue;
        private DateTime splitLate;
        private string CurrentScene = null;
        private State PreviousState = State.NONE;

        public LogicManager(SplitterSettings settings) {
            Memory = new MemoryManager();
            Settings = settings;
            splitLate = DateTime.MaxValue;
        }

        public void Reset() {
            splitLate = DateTime.MaxValue;
            Paused = false;
            Running = false;
            CurrentSplit = 0;
            InitializeSplit();
            ShouldSplit = false;
            ShouldReset = false;
            if (Settings.DisableDebug) {
            }
        }
        public void Decrement() {
            CurrentSplit--;
            splitLate = DateTime.MaxValue;
            InitializeSplit();
        }
        public void Increment() {
            Running = true;
            splitLate = DateTime.MaxValue;
            CurrentSplit++;

            InitializeSplit();
        }
        private void InitializeSplit() {
            if (CurrentSplit < Settings.Autosplits.Count) {
                bool temp = ShouldSplit;
                CheckSplit(Settings.Autosplits[CurrentSplit], true);
                ShouldSplit = temp;
            }
        }
        public bool IsHooked() {
            bool hooked = Memory.HookProcess();
            Paused = !hooked;
            ShouldSplit = false;
            ShouldReset = false;
            GameTime = -1;
            return hooked;
        }
        public void Update(int currentSplit) {
            if (Settings.DisableDebug && Running) {
            }

            if (currentSplit != CurrentSplit) {
                CurrentSplit = currentSplit;
                Running = CurrentSplit > 0;
                InitializeSplit();
            }

            if (CurrentSplit < Settings.Autosplits.Count) {
                CheckSplit(Settings.Autosplits[CurrentSplit], false);

                if (!Running) {
                    Paused = true;
                    if (ShouldSplit) {
                        Running = true;
                    }
                }

                if (ShouldSplit) {
                    Increment();
                }
            }
        }

        private void CheckSplit(Split split, bool updateValues) {
            Paused = Memory.IsLoadingGame();
            if (IsHooked() == false)// || Paused)
                return;

            ShouldSplit = false;
            HUDManager hudManager = Memory.GetHUDManager();
            CurrentScene = Memory.GetSceneName();
            if (split.Type == SplitType.GameStart) {
                SaveSlot save = Memory.GetSaveSlot();
                ShouldSplit = !lastBoolValue && save.newGame && hudManager.state == State.MAIN_MENU;
                lastBoolValue = save.newGame;
                PreviousState = hudManager.state;
            } else { 
                if (!updateValues && (Paused && hudManager.state != State.IN_GAME)) {
                    PreviousState = hudManager.state;
                    return;
                }

                switch (split.Type) {
                    case SplitType.ManualSplit:
                        break;
                    case SplitType.SelectedBook:
                        CheckBook(split);
                        break;
                    case SplitType.LevelStart:
                        CheckLevelStart(split);
                        break;
                    case SplitType.LevelCompleted:
                        CheckLevelCompleted(split);
                        break;
                    case SplitType.Hitbox:
                        Vector4 hitbox = new Vector4(split.Value);
                        CheckHitboxSplit(hitbox);
                        break;
                    case SplitType.GameEnd:
                        lastBoolValue = ShouldSplit = hudManager.state == State.CREDITS && !lastBoolValue;
                        break;
                }

                if (split.Type == SplitType.Hitbox) {
                    ShouldSplit = false;
                } else if (DateTime.Now > splitLate) {
                    ShouldSplit = true;
                    splitLate = DateTime.MaxValue;
                }
            }
            PreviousState = hudManager.state;
            return;
        }

        private void CheckBook(Split split) {
            SplitBook spiritTrial = Utility.GetEnumValue<SplitBook>(split.Value);
            EvergateController evergate = Memory.GetEvergateController();

            if (evergate.allLevels.Count > 0) {
                switch (spiritTrial) {
                    case SplitBook.ChinaLevel1: ShouldSplit = evergate.selectedBookIndex == 0; break;
                    case SplitBook.ChinaLevel2: ShouldSplit = evergate.selectedBookIndex == 1; break;
                    case SplitBook.ChinaLevel3: ShouldSplit = evergate.selectedBookIndex == 2; break;
                    case SplitBook.Alaska1: ShouldSplit = evergate.selectedBookIndex == 3; break;
                    case SplitBook.Alaska2: ShouldSplit = evergate.selectedBookIndex == 4; break;
                    case SplitBook.Alaska3: ShouldSplit = evergate.selectedBookIndex == 5; break;
                    case SplitBook.England1: ShouldSplit = evergate.selectedBookIndex == 6; break;
                    case SplitBook.England2: ShouldSplit = evergate.selectedBookIndex == 7; break;
                    case SplitBook.NewYork1: ShouldSplit = evergate.selectedBookIndex == 8; break;
                    case SplitBook.NewYork2: ShouldSplit = evergate.selectedBookIndex == 9; break;
                    case SplitBook.VoidWorld: ShouldSplit = evergate.selectedBookIndex == 10; break;
                }
            } else {
                ShouldSplit = false;
            }
        }

        private void CheckLevelStart(Split split) {
            SplitLevel level = Utility.GetEnumValue<SplitLevel>(split.Value);
            List<string> levels = Memory.GetVisitedLevels();
            string sceneName = Memory.GetSceneName();

            if (LUT.LevelLUT.ContainsKey((int)level) == true && lastStrValue != null) {
                foreach (string s in levels) {
                    if (s == LUT.LevelLUT[(int)level]) {
                        ShouldSplit = sceneName == s && sceneName != lastStrValue && !lastBoolValue;
                        lastStrValue = sceneName;
                        return;
                    }
                }
            }

            ShouldSplit = false;
            lastBoolValue = false;
            lastStrValue = sceneName;
        }

        private void CheckLevelCompleted(Split split) {
            SplitLevel level = Utility.GetEnumValue<SplitLevel>(split.Value);
            LevelSelectInfo info = Memory.GetLevel(Memory.GetSelectedLevel(), (int)level);
            Dictionary<string, float> bestTimes = Memory.GetBestTimes();

            if (LUT.LevelLUT.ContainsKey((int)level) && bestTimes.ContainsKey(LUT.LevelLUT[(int)level]) == true) {
                ShouldSplit = !lastBoolValue && lastBoolValue == false;
            }

            if (level == SplitLevel.VW1 && CurrentScene == "prod-v1-1")
                ShouldSplit = true;

                lastBoolValue = info.completed;
        }

        private bool CheckHitbox(Vector4 hitbox) {
            Vector3 playerPosition = Memory.GetPlayerPosition();
            Vector4 ori = new Vector4(playerPosition.ToVector2(), 1f, 13f, true);
            bool containsOri = hitbox.Intersects(ori);
            return containsOri;
        }

        private void CheckHitboxSplit(Vector4 hitbox) {
            Vector3 playerPosition = Memory.GetPlayerPosition();
            Vector4 ori = new Vector4(playerPosition.ToVector2(), 0f, 0f, true);
            bool containsOri = hitbox.Intersects(ori);
            ShouldSplit = containsOri && !lastBoolValue;
            lastBoolValue = containsOri;
        }
    }
}