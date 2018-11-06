using System;

namespace Manina.Windows.Forms
{
    public enum NodeType
    {
        Drive,
        Directory,
        File
    }

    [Flags]
    public enum DriveType
    {
        None = 0,
        Fixed = 1,
        Removable = 2,
        Network = 4,
        CDRom = 8,
        Ram = 16,
        All = Fixed | Removable | Network | CDRom | Ram
    }
}
