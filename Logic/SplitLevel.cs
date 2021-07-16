using System.Collections.Generic;
using System.ComponentModel;

namespace LiveSplit.Evergate {
    public enum SplitLevel {
        [Description("Secret Garden 1")]
        C11,
        [Description("Secret Garden 2")]
        C12,
        [Description("Secret Garden 3")]
        C13,
        [Description("Secret Garden 4")]
        C14,
        [Description("Secret Garden 5")]
        C15,
        [Description("Secret Garden 6")]
        C16,
        [Description("Secret Garden 7")]
        C17,
        [Description("Darkwoods 1")]
        C21,
        [Description("Darkwoods 2")]
        C22,
        [Description("Darkwoods 3")]
        C23,
        [Description("Darkwoods 4")]
        C24,
        [Description("Darkwoods 5")]
        C25,
        [Description("Darkwoods 6")]
        C26,
        [Description("Darkwoods 7")]
        C27,
        [Description("Lunar Festival 1")]
        C31,
        [Description("Lunar Festival 2")]
        C32,
        [Description("Lunar Festival 3")]
        C33,
        [Description("Lunar Festival 4")]
        C34,
        [Description("Lunar Festival 5")]
        C35,
        [Description("Lunar Festival 6")]
        C36,
        [Description("Lunar Festival 7")]
        C37,
        [Description("The Blizzard 1")]
        A11,
        [Description("The Blizzard 2")]
        A12,
        [Description("The Blizzard 3")]
        A13,
        [Description("The Blizzard 4")]
        A14,
        [Description("The Blizzard 5")]
        A15,
        [Description("The Blizzard 6")]
        A16,
        [Description("The Blizzard 7")]
        A17,
        [Description("Summer Hunt 1")]
        A21,
        [Description("Summer Hunt 2")]
        A22,
        [Description("Summer Hunt 3")]
        A23,
        [Description("Summer Hunt 4")]
        A24,
        [Description("Summer Hunt 5")]
        A25,
        [Description("Summer Hunt 6")]
        A26,
        [Description("Summer Hunt 7")]
        A27,
        [Description("Northern Lights 1")]
        A31,
        [Description("Northern Lights 2")]
        A32,
        [Description("Northern Lights 3")]
        A33,
        [Description("Northern Lights 4")]
        A34,
        [Description("Northern Lights 5")]
        A35,
        [Description("Northern Lights 6")]
        A36,
        [Description("Northern Lights 7")]
        A37,
        [Description("School Yard 1")]
        E11,
        [Description("School Yard 2")]
        E12,
        [Description("School Yard 3")]
        E13,
        [Description("School Yard 4")]
        E14,
        [Description("School Yard 5")]
        E15,
        [Description("School Yard 6")]
        E16,
        [Description("School Yard 7")]
        E17,
        [Description("Falling Sky 1")]
        E21,
        [Description("Falling Sky 2")]
        E22,
        [Description("Falling Sky 3")]
        E23,
        [Description("Falling Sky 4")]
        E24,
        [Description("Falling Sky 5")]
        E25,
        [Description("Falling Sky 6")]
        E26,
        [Description("Falling Sky 7")]
        E27,
        [Description("Neon Alley 1")]
        N11,
        [Description("Neon Alley 2")]
        N12,
        [Description("Neon Alley 3")]
        N13,
        [Description("Neon Alley 4")]
        N14,
        [Description("Neon Alley 5")]
        N15,
        [Description("Neon Alley 6")]
        N16,
        [Description("Neon Alley 7")]
        N17,
        [Description("Underlands 1")]
        N21,
        [Description("Underlands 2")]
        N22,
        [Description("Underlands 3")]
        N23,
        [Description("Underlands 4")]
        N24,
        [Description("Underlands 5")]
        N25,
        [Description("Underlands 6")]
        N26,
        [Description("Underlands 7")]
        N27,
        [Description("The Storm 1")]
        VW1,
        [Description("The Storm 2")]
        VW2,
        [Description("The Storm 3")]
        VW3,
        [Description("The Storm 4")]
        VW4,
        [Description("The Storm 5")]
        VW5,
        [Description("The Storm 6")]
        VW6,
        [Description("The Storm 7")]
        VW7,
    }

    public class LUT {
        public static Dictionary<int, string> LevelLUT = new Dictionary<int, string>() {
            [0] = "prod-c1-1",
            [1] = "prod-c1-2",
            [2] = "prod-c1-3",
            [3] = "prod-c1-4",
            [4] = "prod-c1-5",
            [5] = "prod-c1-6",
            [6] = "prod-c1-7",
            [7] = "prod-c2-1",
            [8] = "prod-c2-2",
            [9] = "prod-c2-3",
            [10] = "prod-c2-4",
            [11] = "prod-c2-5",
            [12] = "prod-c2-6",
            [13] = "prod-c2-7",
            [14] = "prod-c3-1",
            [15] = "prod-c3-2",
            [16] = "prod-c3-3",
            [17] = "prod-c3-4",
            [18] = "prod-c3-5",
            [19] = "prod-c3-6",
            [20] = "prod-c3-7",
            [21] = "prod-i1-1",
            [22] = "prod-i1-2",
            [23] = "prod-i1-3",
            [24] = "prod-i1-4",
            [25] = "prod-i1-5",
            [26] = "prod-i1-6",
            [27] = "prod-i1-7",
            [28] = "prod-i2-1",
            [29] = "prod-i2-2",
            [30] = "prod-i2-3",
            [31] = "prod-i2-4",
            [32] = "prod-i2-5",
            [33] = "prod-i2-6",
            [34] = "prod-i2-7",
            [35] = "prod-i3-1",
            [36] = "prod-i3-2",
            [37] = "prod-i3-3",
            [38] = "prod-i3-4",
            [39] = "prod-i3-5",
            [40] = "prod-i3-6",
            [41] = "prod-i3-7",
            [42] = "prod-e1-1",
            [43] = "prod-e1-2",
            [44] = "prod-e1-3",
            [45] = "prod-e1-4",
            [46] = "prod-e1-5",
            [47] = "prod-e1-6",
            [48] = "prod-e1-7",
            [49] = "prod-e2-1",
            [50] = "prod-e2-2",
            [51] = "prod-e2-3",
            [52] = "prod-e2-4",
            [53] = "prod-e2-5",
            [54] = "prod-e2-6",
            [55] = "prod-e2-7",
            [56] = "prod-n1-1",
            [57] = "prod-n1-2",
            [58] = "prod-n1-3",
            [59] = "prod-n1-4",
            [60] = "prod-n1-5",
            [61] = "prod-n1-6",
            [62] = "prod-n1-7",
            [63] = "prod-n2-1",
            [64] = "prod-n2-2",
            [65] = "prod-n2-3",
            [66] = "prod-n2-4",
            [67] = "prod-n2-5",
            [68] = "prod-n2-6",
            [69] = "prod-n2-7",
            [70] = "prod-hub-2",
            [71] = "prod-v1-1",
            [72] = "prod-v1-2",
            [73] = "prod-v1-3",
            [74] = "prod-b1-1",
            [75] = "prod-b1-2",
        };
    }
}