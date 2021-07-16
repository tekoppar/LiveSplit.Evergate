using System.ComponentModel;

namespace LiveSplit.Evergate {
    public enum SplitType {
        [Description("Manual Split")]
        ManualSplit,
        [Description("Selected Book")]
        SelectedBook,
        [Description("Level Started")]
        LevelStart,
        [Description("Level Completed")]
        LevelCompleted,
        [Description("Game Start")]
        GameStart,
        [Description("Game End")]
        GameEnd,
        [Description("Hitbox")]
        Hitbox
    }
    public class Split {
        public string Name { get; set; }
        public SplitType Type { get; set; }
        public string Value { get; set; }

        public override string ToString() {
            return $"{Type}|{Value}";
        }
    }
}