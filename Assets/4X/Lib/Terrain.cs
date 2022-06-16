using System;

namespace Civ {
    [Flags]
    public enum Terrain {
        None = 0b_0000_0000,
        
    }
}

namespace League { 
    [Flags]
    public enum Creep {
        None   = 0b_0000_0000,
        Caster = 0b_0000_0001,
        Melee  = 0b_0000_0010,
        Cannon = 0b_0000_0100,
        Super  = 0b_0000_1000,
        Raptor = 0b_0001_0000,
        Wolf   = 0b_0010_0000,
        Krug   = 0b_0100_0000,
        Gromp  = 0b_1000_0000,
    }
}
