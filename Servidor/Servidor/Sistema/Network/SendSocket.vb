Module SendSocket
    Public Enum ServerPacket
        Alerta = 1
        LoginOK
        ClasseData
        InGame
        PlayerData
        PlayerDir
        PlayerMove
        PlayerXY
        CheckMap
        Map
        UpdateNpc
        NpcSpawn
        ItemUpdate
        DropItem
        InvSlotUpdate
        MsgAnim
        Attack
        UpdateAnim
        NpcMove
        NpcVital
        NpcDir
        PlayerVitalHP
    End Enum

    Public ReadOnly Property PlayerData(ByVal Index As Integer) As Byte()
        Get
            Dim Buffer As New ByteBuffer()
            Buffer.WriteString(Player(Index).Nome)
            Buffer.WriteShort(Player(Index).Access)
            Buffer.WriteShort(Player(Index).Sprite)
            Buffer.WriteShort(Player(Index).Classe)
            Buffer.WriteShort(Player(Index).Dir)
            Buffer.WriteShort(Player(Index).Map)
            Buffer.WriteShort(Player(Index).X)
            Buffer.WriteShort(Player(Index).Y)
            Buffer.WriteInteger(Player(Index).HP)
            Buffer.WriteInteger(Player(Index).MaxHP)
            Return Compress(Buffer.ToArray)
        End Get
    End Property

    Public ReadOnly Property MapaDataPacket(ByVal MapID As Integer) As Byte()
        Get
            Dim Buffer As New ByteBuffer
            Buffer.WriteString(Mapa(MapID).Nome)
            Buffer.WriteInteger(Mapa(MapID).Revision)
            Buffer.WriteShort(Mapa(MapID).Zona)
            Buffer.WriteString(Mapa(MapID).Musica)
            Buffer.WriteInteger(Mapa(MapID).Top)
            Buffer.WriteInteger(Mapa(MapID).Left)
            Buffer.WriteInteger(Mapa(MapID).Bottom)
            Buffer.WriteInteger(Mapa(MapID).Right)
            Buffer.WriteShort(Mapa(MapID).MaxX)
            Buffer.WriteShort(Mapa(MapID).MaxY)

            For X As Short = 0 To Mapa(MapID).MaxX
                For Y As Short = 0 To Mapa(MapID).MaxY
                    Buffer.WriteShort(Mapa(MapID).Tile(X, Y).Type)
                    Buffer.WriteString(Mapa(MapID).Tile(X, Y).Value1)

                    For i As Short = 1 To Layers.Count - 1
                        Buffer.WriteShort(Mapa(MapID).Tile(X, Y).Layer(i).TileSet)
                        Buffer.WriteShort(Mapa(MapID).Tile(X, Y).Layer(i).BufferX)
                        Buffer.WriteShort(Mapa(MapID).Tile(X, Y).Layer(i).BufferY)
                    Next
                Next
            Next

            For i As Short = 0 To Options.MAX_MAP_NPCS - 1
                Buffer.WriteInteger(Mapa(MapID).Spawn(i))
            Next
            Return Buffer.ToArray
        End Get
    End Property

    Public Sub SendPlayerVitalHP(ByVal Index As Integer)
        Using Buffer As New ByteBuffer(ServerPacket.PlayerVitalHP)
            Buffer.WriteInteger(Index)
            Buffer.WriteInteger(Player(Index).HP)
            Buffer.WriteInteger(Player(Index).MaxHP)
            SendDataToMap(Player(Index).Map, Buffer.ToArray)
        End Using
    End Sub

    Public Sub SendNpcDir(ByVal mapID As Integer, ByVal spawnID As Short)
        Using Buffer As New ByteBuffer(ServerPacket.NpcDir)
            Buffer.WriteShort(spawnID)
            Buffer.WriteShort(MapNpc(mapID)(spawnID).Dir)
            SendDataToMap(mapID, Buffer.ToArray)
        End Using
    End Sub

    Public Sub SendNpcVital(ByVal mapID As Integer, ByVal spawnID As Short)
        Dim Buffer As New ByteBuffer(ServerPacket.NpcVital)
        Buffer.WriteShort(spawnID)
        Buffer.WriteInteger(MapNpc(mapID)(spawnID).HP)
        SendDataToMap(mapID, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendNpcMove(ByVal mapID As Integer, ByVal spawnID As Short)
        Dim Buffer As New ByteBuffer(ServerPacket.NpcMove)
        Buffer.WriteShort(spawnID)
        Buffer.WriteShort(MapNpc(mapID)(spawnID).Dir)
        Buffer.WriteShort(MapNpc(mapID)(spawnID).X)
        Buffer.WriteShort(MapNpc(mapID)(spawnID).Y)
        SendDataToMap(mapID, Buffer.ToArray())
        Buffer.Dispose()
    End Sub

    Public Sub SendAnimations(ByVal Index As Integer)
        For i As Short = 1 To Options.MAX_ANIMATION
            SendUpdateAnim(Index, i)
        Next
    End Sub

    Public Sub SendUpdateAnimToAll(ByVal AnimNum As Integer)
        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then SendUpdateAnim(i, AnimNum)
        Next
    End Sub

    Public Sub SendUpdateAnim(ByVal Index As Integer, ByVal AnimNum As Integer)
        Dim Buffer As New ByteBuffer()

        Buffer.WriteInteger(AnimNum)
        Buffer.WriteString(Animation(AnimNum).Nome)
        Buffer.WriteShort(Animation(AnimNum).Layer)
        Buffer.WriteShort(Animation(AnimNum).AnimID)
        Buffer.WriteShort(Animation(AnimNum).FrameCount)
        Buffer.WriteShort(Animation(AnimNum).FrameX)
        Buffer.WriteShort(Animation(AnimNum).FrameY)
        Buffer.WriteShort(Animation(AnimNum).SpeedMS)
        Buffer.WriteInteger(Animation(AnimNum).Colour.ToArgb)
        Buffer.WriteShort(Animation(AnimNum).BlendMode)
        Dim b() As Byte = Compress(Buffer.ToArray)
        Buffer.Dispose()

        Buffer = New ByteBuffer(ServerPacket.UpdateAnim)
        Buffer.WriteInteger(b.Length)
        Buffer.WriteBytes(b)
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendAttack(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer(ServerPacket.Attack)
        Buffer.WriteInteger(Index)
        SendDataToMapBut(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendMsgAnim(ByVal Index As Integer, ByVal Text As String, ByVal X As Short, ByVal Y As Short, ByVal Colour As Integer)
        Dim Buffer As New ByteBuffer(ServerPacket.MsgAnim)
        Buffer.WriteString(Text)
        Buffer.WriteShort(X)
        Buffer.WriteShort(Y)
        Buffer.WriteInteger(Colour)
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendInventoy(ByVal Index As Integer)
        For i As Short = 0 To Options.MAX_INV - 1
            SendInvSlotUpdate(Index, i)
        Next
    End Sub

    Public Sub SendInvSlotUpdate(ByVal Index As Integer, ByVal InvID As Short)
        Dim Buffer As New ByteBuffer(ServerPacket.InvSlotUpdate)
        Buffer.WriteShort(InvID)
        Buffer.WriteInteger(Inventory(Index, InvID).Num)
        Buffer.WriteInteger(Inventory(Index, InvID).Value)

        For i As Short = 0 To Options.MAX_INV_GEMA - 1
            Buffer.WriteInteger(Inventory(Index, InvID).GemaSlot(i))
        Next
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendDropItem(ByVal MapID As Integer, ByVal DropID As Short)
        Dim Buffer As New ByteBuffer(ServerPacket.DropItem)
        Buffer.WriteShort(DropID)
        Buffer.WriteInteger(Drop(MapID)(DropID).Num)
        Buffer.WriteInteger(Drop(MapID)(DropID).Value)
        Buffer.WriteShort(Drop(MapID)(DropID).X)
        Buffer.WriteShort(Drop(MapID)(DropID).Y)
        SendDataToMap(MapID, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendItems(ByVal Index As Integer)
        For i As Integer = 1 To Options.MAX_ITEM
            SendUpdateItem(Index, i)
        Next
    End Sub

    Public Sub SendUpdateItemToAll(ByVal ID As Integer)
        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then
                SendUpdateItem(i, ID)
            End If
        Next
    End Sub

    Public Sub SendUpdateItem(ByVal Index As Integer, ByVal ID As Integer)
        Dim Buffer As New ByteBuffer()
        Buffer.WriteInteger(ID)
        Buffer.WriteString(Item(ID).Nome)
        Buffer.WriteShort(Item(ID).Tipo)
        Buffer.WriteShort(Item(ID).Icon)
        Buffer.WriteShort(Item(ID).isDrop)
        Buffer.WriteShort(Item(ID).isStack)
        Buffer.WriteString(Item(ID).Desc)
        Buffer.WriteInteger(Item(ID).Peso)
        Buffer.WriteShort(Item(ID).PotionMode)
        Buffer.WriteInteger(Item(ID).PotionVital)
        Buffer.WriteShort(Item(ID).PotionCD)
        Buffer.WriteShort(Item(ID).PotionEffect)
        Buffer.WriteShort(Item(ID).EquipTipo)
        Buffer.WriteShort(Item(ID).EquipPaper)
        Buffer.WriteInteger(Item(ID).EquipHP)
        Buffer.WriteInteger(Item(ID).EquipMP)
        Buffer.WriteInteger(Item(ID).EquipFOR)
        Buffer.WriteInteger(Item(ID).EquipCON)

        Dim d() As Byte = Compress(Buffer.ToArray)
        Buffer.Dispose()

        Buffer = New ByteBuffer(ServerPacket.ItemUpdate)
        Buffer.WriteInteger(d.Length)
        Buffer.WriteBytes(d)
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendNpcSpawnsAll(ByVal mapID As Integer)
        For i As Integer = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then
                If Player(i).Map = mapID Then
                    SendNpcSpawns(i)
                End If
            End If
        Next
    End Sub

    Public Sub SendNpcSpawns(ByVal Index As Integer)
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            If MapNpc(Player(Index).Map)(i).Num > 0 Then
                SendNpcSpawn(Index, i)
            End If
        Next
    End Sub

    Public Sub SendNpcSpawnAll(ByVal mapID As Integer, ByVal ID As Short)
        For i As Integer = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then
                If Player(i).Map = mapID Then SendNpcSpawn(i, ID)
            End If
        Next
    End Sub

    Public Sub SendNpcSpawn(ByVal Index As Integer, ByVal ID As Short)
        Dim Buffer As New ByteBuffer()
        Dim data As SpawnData.SpawnItemData = MapNpc(Player(Index).Map)(ID)

        Buffer.WriteShort(ID)
        Buffer.WriteInteger(data.Num)
        Buffer.WriteLong(data.HP)
        Buffer.WriteShort(data.X)
        Buffer.WriteShort(data.Y)
        Buffer.WriteShort(CByte(data.Dead))
        Buffer.WriteShort(data.Dir)

        Dim b() As Byte = Compress(Buffer.ToArray)
        Buffer.Dispose()
        Buffer = New ByteBuffer(ServerPacket.NpcSpawn)
        Buffer.WriteInteger(b.Length)
        Buffer.WriteBytes(b)
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendNpcs(ByVal Index As Integer)
        For i As Integer = 1 To Options.MAX_NPC
            SendUpdateNpc(Index, i)
        Next
    End Sub

    Public Sub SendUpdateNpcToAll(ByVal ID As Integer)
        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then
                SendUpdateNpc(i, ID)
            End If
        Next
    End Sub

    Public Sub SendUpdateNpc(ByVal Index As Integer, ByVal ID As Integer)
        Dim Buffer As New ByteBuffer()
        Buffer.WriteInteger(ID)
        Buffer.WriteString(Npc(ID).Nome)
        Buffer.WriteShort(Npc(ID).Tipo)
        Buffer.WriteShort(Npc(ID).Level)
        Buffer.WriteLong(Npc(ID).EXP)
        Buffer.WriteLong(Npc(ID).HP)
        Buffer.WriteInteger(Npc(ID).STR)
        Buffer.WriteInteger(Npc(ID).CON)
        Buffer.WriteShort(Npc(ID).Sprite)
        Buffer.WriteShort(Npc(ID).Paperdoll)
        Buffer.WriteShort(Npc(Index).SpawnTime)
        Buffer.WriteShort(Npc(ID).NpcSpellIntervalo)

        ' Drops
        For i As Short = 0 To 3
            Buffer.WriteInteger(Npc(ID).Drop(i).Num)
            Buffer.WriteInteger(Npc(ID).Drop(i).Value)
            Buffer.WriteShort(Npc(ID).Drop(i).Chance)
        Next

        ' Spells
        For i As Short = 0 To 3
            Buffer.WriteInteger(Npc(ID).NpcSpell(i).Num)
            Buffer.WriteShort(Npc(ID).NpcSpell(i).CD)
        Next
        Dim data() As Byte = Compress(Buffer.ToArray)
        Buffer.Dispose()

        Buffer = New ByteBuffer(ServerPacket.UpdateNpc)
        Buffer.WriteInteger(data.Length)
        Buffer.WriteBytes(data)
        SendDataTo(Index, Buffer.ToArray)
    End Sub

    Public Sub SendMap(ByVal Index As Integer, ByVal newMap As Byte)
        Dim Buffer As New ByteBuffer(ServerPacket.Map)
        Buffer.WriteShort(newMap)
        If newMap = 1 Then
            Dim b() As Byte = Compress(MapaDataPacket(Player(Index).Map))
            Buffer.WriteInteger(b.Length)
            Buffer.WriteBytes(b)
        End If

        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendCheckMap(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer(ServerPacket.CheckMap)
        Buffer.WriteInteger(Mapa(Player(Index).Map).Revision)
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendPlayerXY(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer(ServerPacket.PlayerXY)
        Buffer.WriteInteger(Index)
        Buffer.WriteShort(Player(Index).Map)
        Buffer.WriteShort(Player(Index).X)
        Buffer.WriteShort(Player(Index).Y)
        SendDataToMap(Player(Index).Map, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendPlayerMove(ByVal Index As Integer, ByVal move As Byte)
        Dim Buffer As New ByteBuffer(ServerPacket.PlayerMove)
        Buffer.WriteInteger(Index)
        Buffer.WriteShort(move)
        Buffer.WriteShort(Player(Index).Dir)
        Buffer.WriteShort(Player(Index).X)
        Buffer.WriteShort(Player(Index).Y)
        SendDataToMapBut(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendPlayerDir(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer(ServerPacket.PlayerDir)
        Buffer.WriteInteger(Index)
        Buffer.WriteShort(Player(Index).Dir)
        SendDataToMap(Player(Index).Map, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendPlayerData(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer(ServerPacket.PlayerData)
        Buffer.WriteInteger(Index)
        Dim b() As Byte = PlayerData(Index)
        Buffer.WriteInteger(b.Length)
        Buffer.WriteBytes(b)
        SendDataToMap(Player(Index).Map, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendInGame(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer(ServerPacket.InGame)
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendClasseData(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer
        Buffer.WriteLong(ServerPacket.ClasseData)
        Buffer.WriteShort(Classes.Count - 1)

        If Classes.Count - 1 > Classes.None Then
            For i As Short = 1 To Classes.Count - 1
                Buffer.WriteString(Classe(i).Nome)
                Buffer.WriteShort(Classe(i).Sprite)
                Buffer.WriteInteger(Classe(i).STR)
                Buffer.WriteInteger(Classe(i).CON)
            Next
        End If
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendAlerta(ByVal Index As Integer, ByVal text As String)
        Dim Buffer As New ByteBuffer
        Buffer.WriteLong(ServerPacket.Alerta)
        Buffer.WriteString(text)
        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendLogin(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer
        Buffer.WriteLong(ServerPacket.LoginOK)
        Buffer.WriteInteger(Index)


        Dim charData As clsPlayer
        Dim o As Short = 0
        For i As Short = 0 To 4
            o = -1
            If Len(Conta(Index).CharID(i)) > 0 Then o = 0
            Buffer.WriteInteger(o)
            If o > -1 Then
                charData = New clsPlayer
                charData.Nome = Conta(Index).CharID(i)
                charData.Load()
                Buffer.WriteString(charData.Nome)
                Buffer.WriteShort(charData.Sprite)
                charData = Nothing
            End If
        Next

        SendDataTo(Index, Buffer.ToArray)
        Buffer.Dispose()
    End Sub
End Module
