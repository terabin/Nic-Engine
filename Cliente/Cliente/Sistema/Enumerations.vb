Module Enumerations
    Enum Telas
        None
        Login
        Registro
        SelectChar
        CriarChar
        InGame
    End Enum

    Enum ItemType
        None
        Equipamento
        Poção
    End Enum

    Enum NpcType
        AttackAll
        AttackSafe
        Friendly
        [Event]
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

    Enum Dirs
        UP
        DOWN
        LEFT
        RIGHT
    End Enum

    Enum DragType
        None
        Inventory
    End Enum

    Enum AnimLayer
        Ground
        Fringe

        ' Quantia
        Count
    End Enum

    Enum EffectType
        Rouba_HP
        Rouba_MP
        Cura_HP
        Dano
    End Enum
End Module
