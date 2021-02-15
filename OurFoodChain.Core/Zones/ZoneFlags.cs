using System;

namespace OurFoodChain.Zones {

    [Flags]
    public enum ZoneFlags {
        None = 0 << 0,
        Retired = 0 << 1,
    }

}