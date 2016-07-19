Module Enumeration
    Public Enum Dirs
        UP
        DOWN
        LEFT
        RIGHT
    End Enum

    Enum Layers
        Ground = 1
        Mask
        MaskAnim
        Mask2
        MaskAnim2
        Fringe
        FringeAnim
        Fringe2
        FringeAnim2

        ' Quantia
        Count
    End Enum

    Enum TileType
        None
        Block
        Warp

    End Enum

    Enum NpcType
        AttackAll
        AttackSafe
        Friendly
        [Event]
    End Enum
End Module
