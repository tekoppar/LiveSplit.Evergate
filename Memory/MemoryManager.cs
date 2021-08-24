using System;
using System.Collections.Generic;
using System.Diagnostics;
using Tem.TemClass;

namespace LiveSplit.Evergate {
    public partial class MemoryManager {
        public static PointerVersion Version { get; set; } = PointerVersion.All;
        public Process Program { get; set; }
        public Module64 GameAssembly;
        public bool IsHooked { get; set; }
        public DateTime LastHooked { get; set; }
        public int ControllerCounter { get; set; } = 0;
        private bool? noPausePatched = null;
        private bool? debugEnabled = null;
        private FPSTimer fpsTimer = new FPSTimer(200, 15);
        public int FrameCount = 0;
        private float LastGameTime = -1;
        private Dictionary<string, string> VisitedLevels = new Dictionary<string, string>();

        public MemoryManager() {
            LastHooked = DateTime.MinValue;
        }

        public float FPS() {
            return fpsTimer.FPS;
        }

        public float GetGameTime() {
            int gameStateManagerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: gameStateManagerOffset = 0x01989428; break;
                case PointerVersion.P2: gameStateManagerOffset = 0x01989488; break;
            }
            //GameStateManager, Static, GameManager, SceneName
            return MemoryReader.Read<float>(Program, GameAssembly.BaseAddress, gameStateManagerOffset, 0xb8, 0x28, 0x18, 0 + 0x28);
        }

        public string GetSceneName() {
            int gameStateManagerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: gameStateManagerOffset = 0x01989428; break;
                case PointerVersion.P2: gameStateManagerOffset = 0x01989488; break;
            }
            //GameStateManager, Static, GameManager, GameTime
            return MemoryReader.ReadString(Program, GameAssembly.BaseAddress, gameStateManagerOffset, 0xb8, 0x28, 0x18, 0 + 0x10, 0x0);
        }

        public bool IsPlayerPaused() {
            //GameManager -> static_fields -> isPaused
            int isPausedOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: isPausedOffset = 0x0199F7F0; break;
                case PointerVersion.P2: isPausedOffset = 0x0199F850; break;
            }

            return MemoryReader.Read<bool>(Program, GameAssembly.BaseAddress, isPausedOffset, 0xb8, 0x0);
        }

        private IntPtr GetPlayerPtr() {
            int playerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: playerOffset = 0x0198AD30; break;
                case PointerVersion.P2: playerOffset = 0x0198AD90; break;
            }

            //PlayerManager_2 -> static_fields -> player
            return MemoryReader.Read<IntPtr>(Program, GameAssembly.BaseAddress, playerOffset, 0xb8, 0x0);
        }

        private IntPtr GetAbilityManagerPtr() {
            int schedulerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: schedulerOffset = 0x01977758; break;
                case PointerVersion.P2: schedulerOffset = 0x019777B8; break;
            }

            return MemoryReader.Read<IntPtr>(Program, GameAssembly.BaseAddress, schedulerOffset, 0xC8, 0x28, 0xB8, 0x8) + 0x1c0;
        }

        public int CanJump() {
            IntPtr ptr = GetAbilityManagerPtr();
            return Program.Read<int>(ptr, 0x38, 0x20);
        }

        public PlayerScript GetPlayer() {
            IntPtr playerptr = GetPlayerPtr();
            PlayerScriptPtr ptr = Program.Read<PlayerScriptPtr>(playerptr, 0x0);
            //bool canJump = MemoryReader.Read<bool>(Program, GameAssembly.BaseAddress, 0x0198AD30, 0xb8, 0x8, 0x41);
            return new PlayerScript(ptr, CanJump() > 0);
        }

        public Vector3 GetPlayerPosition() {
            return MemoryReader.Read<Vector3>(Program, GetPlayerPtr(), 0x444);
        }

        public GameStateManager GetGameStateManager() {
            int gameStateManagerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: gameStateManagerOffset = 0x01989428; break;
                case PointerVersion.P2: gameStateManagerOffset = 0x01989488; break;
            }

            return MemoryReader.Read<GameStateManager>(Program, GameAssembly.BaseAddress, gameStateManagerOffset, 0xb8, 0x28, 0x0);
        }

        private IntPtr GetHUDManagerPtr() {
            int loadingOverlayManagerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: loadingOverlayManagerOffset = 0x01977268; break;
                case PointerVersion.P2: loadingOverlayManagerOffset = 0x019772C8; break;
            }

            //0xb8 have always so far offset into the static fields of the class
            //0xc8 im curious what this offsets into
            //LoadingOverlayManager -> ??? -> relative offset -> static fields -> singelton _instance -> HUDManager
            return MemoryReader.Read<IntPtr>(Program, GameAssembly.BaseAddress, loadingOverlayManagerOffset, 0xc8, 0x88, 0xb8, 0x8);
        }

        public HUDManager GetHUDManager() {
            return Program.Read<HUDManager>(GetHUDManagerPtr());
        }

        public List<EvergateBookControllerPtr> GetBooks(IntPtr ptr) {
            List<EvergateBookControllerPtr> levels = new List<EvergateBookControllerPtr>();

            int count = MemoryReader.Read<int>(Program, ptr, 0x18);
            byte[] data = Program.Read(ptr + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                EvergateBookControllerPtr levelPtr = Program.Read<EvergateBookControllerPtr>((IntPtr)BitConverter.ToUInt64(data, i * 0x8));
                levels.Add(levelPtr);
            }

            return levels;
        }

        public List<LevelSelectBehavior> GetBehaviors(IntPtr ptr) {
            List<LevelSelectBehavior> levels = new List<LevelSelectBehavior>();

            int count = MemoryReader.Read<int>(Program, ptr, 0x18);
            byte[] data = Program.Read(ptr + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                LevelSelectBehaviorPtr levelPtr = Program.Read<LevelSelectBehaviorPtr>((IntPtr)BitConverter.ToUInt64(data, i * 0x8));

                string worldName = MemoryReader.ReadString(Program, levelPtr.worldName, 0x0);
                string subtitle = MemoryReader.ReadString(Program, levelPtr.subtitle, 0x0);

                levels.Add(new LevelSelectBehavior(worldName, subtitle, GetLevelInfos(levelPtr.levelInfos)));
            }

            return levels;
        }

        public List<LevelSelectInfo> GetLevelInfos(IntPtr ptr) {
            List<LevelSelectInfo> levels = new List<LevelSelectInfo>();

            int count = MemoryReader.Read<int>(Program, ptr, 0x18);
            byte[] data = Program.Read(ptr + 0x20, count * 0x8);
            for (int i = 0; i < count; i++) {
                LevelSelectInfoPtr levelPtr = Program.Read<LevelSelectInfoPtr>((IntPtr)BitConverter.ToUInt64(data, i * 0x8));

                string sceneName = MemoryReader.ReadString(Program, levelPtr.sceneName, 0x0);
                string levelLabel = MemoryReader.ReadString(Program, levelPtr.levelLabel, 0x0);

                levels.Add(new LevelSelectInfo(levelPtr, sceneName, levelLabel));
            }

            return levels;
        }

        public bool HasVisitedLevel(string level) {
            if (VisitedLevels.ContainsKey(level)) {
                return true;
            } else {
                List<string> visitedLevels = GetVisitedLevels();

                foreach (string lvl in visitedLevels) {
                    if (VisitedLevels.ContainsKey(lvl) == false) {
                        VisitedLevels.Add(lvl, lvl);
                    }
                }
            }

            return VisitedLevels.ContainsKey(level);
        }

        public List<string> GetVisitedLevels() {
            int gameStateManagerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: gameStateManagerOffset = 0x01989428; break;
                case PointerVersion.P2: gameStateManagerOffset = 0x01989488; break;
            }

            IntPtr ptr = MemoryReader.Read<IntPtr>(Program, GameAssembly.BaseAddress, gameStateManagerOffset, 0xb8, 0x28, 0x18, 0x40);
            int count = MemoryReader.Read<int>(Program, ptr, 0x18, 0x18) * 2;
            IntPtr ptr1 = MemoryReader.Read<IntPtr>(Program, ptr, 0x18);

            List<string> levels = new List<string>();
            for (int i = 0; i < count; i++) {
                if ((i + 1) % 2 == 0 && i != 0) {
                    IntPtr ptrC1 = Program.Read<IntPtr>(ptr1 + 0x20 + (i * 0x8));
                    levels.Add(Program.ReadString(ptrC1));
                }
            }

            return levels;
        }

        public List<string> GetStringHashset(IntPtr ptr) {
            int count = MemoryReader.Read<int>(Program, ptr, 0x18, 0x18) * 2;
            IntPtr ptr1 = MemoryReader.Read<IntPtr>(Program, ptr, 0x18);

            List<string> values = new List<string>();
            for (int i = 0; i < count; i++) {
                if ((i + 1) % 2 == 0 && i != 0) {
                    IntPtr ptrC1 = Program.Read<IntPtr>(ptr1 + 0x20 + (i * 0x8));
                    values.Add(Program.ReadString(ptrC1));
                }
            }

            return values;
        }

        public Dictionary<string, float> GetBestTimes() {
            int gameStateManagerOffset = 0;

            switch (MemoryManager.Version) {
                case PointerVersion.All:
                case PointerVersion.P1: gameStateManagerOffset = 0x01989428; break;
                case PointerVersion.P2: gameStateManagerOffset = 0x01989488; break;
            }

            IntPtr ptr = MemoryReader.Read<IntPtr>(Program, GameAssembly.BaseAddress, gameStateManagerOffset, 0xb8, 0x28, 0x18, 0x48);
            int count = MemoryReader.Read<int>(Program, ptr, 0x18, 0x18) * 3;
            IntPtr ptr1 = MemoryReader.Read<IntPtr>(Program, ptr, 0x18);

            Dictionary<string, float> bestTimes = new Dictionary<string, float>();
            string levelName = "";
            float time = -1.0f;
            for (int i = 0; i < count; i += 3) {
                IntPtr lvlnamePtr = Program.Read<IntPtr>(ptr1 + 0x20 + ((i + 1) * 0x8));
                levelName = Program.ReadString(lvlnamePtr);
                time = Program.Read<float>(ptr1 + 0x20 + ((i + 2) * 0x8));

                if (levelName != "" && bestTimes.ContainsKey(levelName) == false) {
                    bestTimes.Add(levelName, time);
                }
            }

            return bestTimes;
        }

        public EvergatePortalControllerPtr GetPortal(IntPtr ptr) {
            return Program.Read<EvergatePortalControllerPtr>(ptr);
        }

        public EvergateController GetEvergateController() {
            HUDManager hud = GetHUDManager();
            EvergateControllerPtr evergatePtr = Program.Read<EvergateControllerPtr>(hud.levelSelectHUD, 0x70, 0x0);

            return new EvergateController(evergatePtr, GetBooks(evergatePtr.books), GetBehaviors(evergatePtr.allLevels), GetPortal(evergatePtr.portal));
        }

        public int GetSelectedLevel() {
            HUDManager hud = GetHUDManager();
            return Program.Read<int>(hud.levelSelectHUD, 0x78);
        }

        public LevelSelectInfo GetLevel(int book, int index) {
            EvergateController evergate = GetEvergateController();
            int levelBehaviour = Math.Max(index % 7, 0);

            if (evergate.allLevels.Count >= book)
                return evergate.allLevels[book].levelInfos[levelBehaviour];

            return new LevelSelectInfo();
        }

        public GameState GetGameState() {
            GameStateManager gameM = GetGameStateManager();
            return GetGameState(gameM.gameState);
        }

        public GameState GetGameState(IntPtr gameStatePtr) {
            GameStatePtr ptr = Program.Read<GameStatePtr>(gameStatePtr);
            string roomName = MemoryReader.ReadString(Program, ptr.roomName, 0x0);
            string startKey = MemoryReader.ReadString(Program, ptr.roomStartKey, 0x0);
            return new GameState(ptr, roomName, startKey, GetStringHashset(ptr.cuesFinished), GetStringHashset(ptr.pinningMomentsCompleted));
        }

        public SaveSlot GetSaveSlot() {
            GameStateManager gameM = GetGameStateManager();
            SaveSlotPtr ptr = Program.Read<SaveSlotPtr>(gameM.activeSaveSlot);
            GameState gameState = GetGameState(ptr.gameState);
            string _time = MemoryReader.ReadString(Program, ptr._time, 0x0);
            string _title = MemoryReader.ReadString(Program, ptr._title, 0x0);
            string _filename_k__BackingField = MemoryReader.ReadString(Program, ptr._filename_k__BackingField, 0x0);
            return new SaveSlot(ptr, gameState, _title, _time, _filename_k__BackingField);
        }

        public bool FinishedGame() {
            GameState state = GetGameState();
            return state.finishedGame;
        }

        public bool IsLoadingGame() {
            float gameTime = GetGameTime();
            if (fpsTimer.FPSShort == 0 && ControllerCounter > 30 || gameTime == LastGameTime) {
                LastGameTime = gameTime;
                return false;
            }
            LastGameTime = gameTime;

            //int m_isLoadingGame = FindIl2CppOffset.GetOffset(Program, "__mainWisp.GameController.m_isLoadingGame");
            int m_isLoadingGame = Version < PointerVersion.P1 ? 0x103 : Version <= PointerVersion.P2 ? 0x10b : 0x123;
            //GameController.FreezeFixedUpdate || GameController.Instance.m_isLoadingGame
            if (true == false) {
                return true;
            }
            return false;
        }

        public bool HookProcess() {
            IsHooked = Program != null && !Program.HasExited;
            if (!IsHooked && DateTime.Now > LastHooked.AddSeconds(1)) {
                LastHooked = DateTime.Now;

                Process[] processes = Process.GetProcessesByName("Evergate");
                Program = processes != null && processes.Length > 0 ? processes[0] : null;

                if (Program == null) {
                    processes = Process.GetProcessesByName("Evergate");
                    Program = processes != null && processes.Length > 0 ? processes[0] : null;
                }

                if (Program == null) {
                    processes = Process.GetProcessesByName("Evergate");
                    Program = processes != null && processes.Length > 0 ? processes[0] : null;
                }

                if (Program != null && !Program.HasExited) {
                    MemoryReader.Update64Bit(Program);
                    FindIl2Cpp.InitializeIl2Cpp(Program);
                    GameAssembly = Program.Module64("GameAssembly.dll");
                    MemoryManager.Version = PointerVersion.P1;
                    if (GameAssembly != null) {
                        switch (GameAssembly.MemorySize) {
                            case 77447168: MemoryManager.Version = PointerVersion.P1; break;
                            case 27942912: MemoryManager.Version = PointerVersion.P2; break;
                            case 81121280: MemoryManager.Version = PointerVersion.P3; break;
                            case 81129472: MemoryManager.Version = PointerVersion.P4; break;
                        }
                    }
                    noPausePatched = null;
                    debugEnabled = null;
                    IsHooked = true;
                    fpsTimer.Reset();
                }
            }

            fpsTimer.Update(IsHooked ? FrameCount : 0);
            FrameCount++;
            return IsHooked;
        }
        public void Dispose() {
            if (Program != null) {
                Program.Dispose();
            }
        }
    }
}