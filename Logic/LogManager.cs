using System;
using System.Collections.Generic;

namespace LiveSplit.Evergate {
    public enum LogObject {
        None,
        CurrentSplit,
        Pointers,
        Position,
        Keystones,
        AllocatedKeys,
        Ore,
        MapCompletion,
        Abilities,
        Shards,
        Area,
        Dead,
        GameState,
        TitleScreen,
        LoadingGame,
        WorldStates,
        Scene,
        UberState,
        Patches,
        Stats,
        Version
    }
    public class LogManager {
        public List<ILogEntry> LogEntries = new List<ILogEntry>();
        private Dictionary<LogObject, string> currentValues = new Dictionary<LogObject, string>();
        public bool EnableLogging;

        public LogManager() {
            EnableLogging = false;
            Clear();
            AddEntryUnlocked(new EventLogEntry("Autosplitter Initialized"));
        }
        public void Clear() {
            lock (LogEntries) {
                LogEntries.Clear();
                foreach (LogObject key in Enum.GetValues(typeof(LogObject))) {
                    currentValues[key] = null;
                }
            }
        }
        public void AddEntry(ILogEntry entry) {
            lock (LogEntries) {
                AddEntryUnlocked(entry);
            }
        }
        private void AddEntryUnlocked(ILogEntry entry) {
            LogEntries.Add(entry);
            Console.WriteLine(entry.ToString());
        }
        public void Update(LogicManager logic, SplitterSettings settings) {
            if (!EnableLogging) { return; }

            lock (LogEntries) {
                DateTime date = DateTime.Now;
                bool isDead = false;
                bool isLoading = logic.Memory.IsLoadingGame();
                bool dontCheckValue = isDead || isLoading;
                foreach (LogObject key in Enum.GetValues(typeof(LogObject))) {
                    string previous = currentValues[key];
                    string current = null;

                    switch (key) {
                        case LogObject.CurrentSplit: current = $"{logic.CurrentSplit} ({GetCurrentSplit(logic, settings)})"; break;
                        case LogObject.Dead: current = isDead.ToString(); break;
                        case LogObject.LoadingGame: current = isLoading.ToString(); break;
                        case LogObject.Version: current = MemoryManager.Version.ToString(); break;
                            //case LogObject.Position: Vector2 point = logic.Memory.Position(); current = $"{point.X:0}, {point.Y:0}"; break;
                    }

                    if (previous != current) {
                        AddEntryUnlocked(new ValueLogEntry(date, key, previous, current));
                        currentValues[key] = current;
                    }
                }
            }
        }
        private string GetCurrentSplit(LogicManager logic, SplitterSettings settings) {
            if (logic.CurrentSplit >= settings.Autosplits.Count) { return "N/A"; }
            return settings.Autosplits[logic.CurrentSplit].ToString();
        }
    }
    public interface ILogEntry { }
    public class ValueLogEntry : ILogEntry {
        public DateTime Date;
        public LogObject Type;
        public object PreviousValue;
        public object CurrentValue;

        public ValueLogEntry(DateTime date, LogObject type, object previous, object current) {
            Date = date;
            Type = type;
            PreviousValue = previous;
            CurrentValue = current;
        }

        public override string ToString() {
            return string.Concat(
                Date.ToString(@"HH\:mm\:ss.fff"),
                ": (",
                Type.ToString(),
                ") ",
                PreviousValue,
                " -> ",
                CurrentValue
            );
        }
    }
    public class EventLogEntry : ILogEntry {
        public DateTime Date;
        public string Event;

        public EventLogEntry(string description) {
            Date = DateTime.Now;
            Event = description;
        }
        public EventLogEntry(DateTime date, string description) {
            Date = date;
            Event = description;
        }

        public override string ToString() {
            return string.Concat(
                Date.ToString(@"HH\:mm\:ss.fff"),
                ": ",
                Event
            );
        }
    }
}
