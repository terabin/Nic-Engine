Module SendSocket
    Public Enum ClientPacket
        None
        Registro
        Login
        CriarChar
        UseChar
        PlayerMove
        ChangeDir
        RequestMap
        MapComplete
        SaveMap
        PlayerWarpMap
        NpcUpdate
        ChangeSprite
        ItemUpdate
        DropAdmin
        CheckItem
        DropInv
        ChangeInv
        Attack
        UpdateAnim
    End Enum

    Public Sub SendUpdateAnim(ByVal animNum As Integer)
        Dim Buffer As New ByteBuffer()

        Buffer.WriteInteger(AnimNum)
        Buffer.WriteString(Animation(AnimNum).Nome)
        Buffer.WriteShort(Animation(AnimNum).Layer)
        Buffer.WriteShort(Animation(AnimNum).AnimID)
        Buffer.WriteShort(Animation(AnimNum).FrameCount)
        Buffer.WriteShort(Animation(AnimNum).FrameX)
        Buffer.WriteShort(Animation(AnimNum).FrameY)
        Buffer.WriteShort(Animation(AnimNum).SpeedMS)
        Buffer.WriteInteger(Animation(animNum).ColourGDI.ToArgb)
        Buffer.WriteShort(Animation(AnimNum).BlendMode)
        Dim b() As Byte = Compress(Buffer.ToArray)
        Buffer.Dispose()

        Buffer = New ByteBuffer(ClientPacket.UpdateAnim)
        Buffer.WriteInteger(b.Length)
        Buffer.WriteBytes(b)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendAttack()
        Dim Buffer As New ByteBuffer(ClientPacket.Attack)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendChangeInv(ByVal oldSlot As Short, ByVal newSlot As Short)
        Dim Buffer As New ByteBuffer(ClientPacket.ChangeInv)
        Buffer.WriteShort(oldSlot)
        Buffer.WriteShort(newSlot)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendDropInv(ByVal invID As Short)
        Dim Buffer As New ByteBuffer(ClientPacket.DropInv)
        Buffer.WriteShort(invID)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendCheckItem()
        Dim Buffer As New ByteBuffer(ClientPacket.CheckItem)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendDropAdmin(ByVal ItemID As Integer, ByVal Valor As Integer)
        Dim Buffer As New ByteBuffer(ClientPacket.DropAdmin)
        Buffer.WriteInteger(ItemID)
        Buffer.WriteInteger(Valor)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendItemUpdate(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer()
        Buffer.WriteInteger(Index)
        Buffer.WriteString(Item(Index).Nome)
        Buffer.WriteShort(Item(Index).Tipo)
        Buffer.WriteShort(Item(Index).Icon)
        Buffer.WriteShort(Item(Index).isDrop)
        Buffer.WriteShort(Item(Index).isStack)
        Buffer.WriteString(Item(Index).Desc)
        Buffer.WriteInteger(Item(Index).Peso)
        Buffer.WriteShort(Item(Index).PotionMode)
        Buffer.WriteInteger(Item(Index).PotionVital)
        Buffer.WriteShort(Item(Index).PotionCD)
        Buffer.WriteShort(Item(Index).PotionEffect)
        Buffer.WriteShort(Item(Index).EquipTipo)
        Buffer.WriteShort(Item(Index).EquipPaper)
        Buffer.WriteInteger(Item(Index).EquipHP)
        Buffer.WriteInteger(Item(Index).EquipMP)
        Buffer.WriteInteger(Item(Index).EquipFOR)
        Buffer.WriteInteger(Item(Index).EquipCON)

        Dim d() As Byte = Compress(Buffer.ToArray)
        Buffer.Dispose()

        Buffer = New ByteBuffer(ClientPacket.ItemUpdate)
        Buffer.WriteInteger(d.Length)
        Buffer.WriteBytes(d)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendChangeSprite(ByVal idSprite As Integer)
        Dim Buffer As New ByteBuffer(ClientPacket.ChangeSprite)
        Buffer.WriteInteger(idSprite)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendNpcUpdate(ByVal Index As Integer)
        Dim Buffer As New ByteBuffer()
        Buffer.WriteInteger(Index)
        Buffer.WriteString(Npc(Index).Nome)
        Buffer.WriteShort(Npc(Index).Tipo)
        Buffer.WriteShort(Npc(Index).Level)
        Buffer.WriteLong(Npc(Index).EXP)
        Buffer.WriteLong(Npc(Index).HP)
        Buffer.WriteInteger(Npc(Index).STR)
        Buffer.WriteInteger(Npc(Index).CON)
        Buffer.WriteShort(Npc(Index).Sprite)
        Buffer.WriteShort(Npc(Index).Paperdoll)
        Buffer.WriteShort(Npc(Index).SpawnTime)
        Buffer.WriteShort(Npc(Index).NpcSpellIntervalo)

        ' Drops
        For i As Short = 0 To 3
            Buffer.WriteInteger(Npc(Index).Drop(i).Num)
            Buffer.WriteInteger(Npc(Index).Drop(i).Value)
            Buffer.WriteShort(Npc(Index).Drop(i).Chance)
        Next

        ' Spells
        For i As Short = 0 To 3
            Buffer.WriteInteger(Npc(Index).NpcSpell(i).Num)
            Buffer.WriteShort(Npc(Index).NpcSpell(i).CD)
        Next
        Dim data() As Byte = Compress(Buffer.ToArray)

        Buffer.Dispose()

        Buffer = New ByteBuffer(ClientPacket.NpcUpdate)
        Buffer.WriteInteger(data.Length)
        Buffer.WriteBytes(data)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendPlayerWarpMap()
        Dim Buffer As New ByteBuffer(ClientPacket.PlayerWarpMap)
        Buffer.Send()
        Buffer.Dispose()
    End Sub

    Public Sub SendSaveMap()
        Dim Buffer As New ByteBuffer()
        Buffer.WriteString(Map.Nome)
        Buffer.WriteInteger(Map.Revision)
        Buffer.WriteShort(Map.Zona)
        Buffer.WriteString(Map.Musica)
        Buffer.WriteInteger(Map.Top)
        Buffer.WriteInteger(Map.Left)
        Buffer.WriteInteger(Map.Bottom)
        Buffer.WriteInteger(Map.Right)
        Buffer.WriteShort(Map.MaxX)
        Buffer.WriteShort(Map.MaxY)

        For X As Short = 0 To Map.MaxX
            For Y As Short = 0 To Map.MaxY
                Buffer.WriteShort(Map.Tile(X, Y).Type)
                Buffer.WriteString(Map.Tile(X, Y).Value1)

                For i As Short = 1 To Layers.Count - 1
                    Buffer.WriteShort(Map.Tile(X, Y).Layer(i).TileSet)
                    Buffer.WriteShort(Map.Tile(X, Y).Layer(i).BufferX)
                    Buffer.WriteShort(Map.Tile(X, Y).Layer(i).BufferY)
                Next
            Next
        Next

        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            Buffer.WriteInteger(Map.Spawn(i))
        Next

        ' Comprimir
        Dim b() As Byte = Buffer.ToArray
        b = Compress(b)
        Buffer.Dispose()

        ' Novo pacote
        Buffer = New ByteBuffer(ClientPacket.SaveMap)
        Buffer.WriteInteger(b.Length)
        Buffer.WriteBytes(b)

        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendMapComplete()
        Dim Buffer As New ByteBuffer(ClientPacket.MapComplete)
        Buffer.Send()
        Buffer.Dispose()
    End Sub

    Public Sub SendRequestMap(ByVal value As Byte)
        Dim Buffer As New ByteBuffer(ClientPacket.RequestMap)
        Buffer.WriteShort(value)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendPlayerMove()
        Dim Buffer As New ByteBuffer(ClientPacket.PlayerMove)
        Buffer.WriteShort(Player(MyIndex).Move)
        Buffer.Send()
        Buffer.Dispose()
    End Sub

    Public Sub SendChangeDir(ByVal dir As Byte)
        Dim Buffer As New ByteBuffer(ClientPacket.ChangeDir)
        Buffer.WriteShort(dir)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendUseChar(ByVal slotChar As Short)
        Dim Buffer As New ByteBuffer(ClientPacket.UseChar)
        Buffer.WriteShort(slotChar)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendCriarChar(ByVal Nome As String, ByVal classeNum As Short, ByVal CharSlot As Short)
        Dim Buffer As New ByteBuffer
        Buffer.WriteLong(ClientPacket.CriarChar)
        Buffer.WriteString(Nome)
        Buffer.WriteShort(classeNum)
        Buffer.WriteShort(CharSlot)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendRegistro(ByVal Nome As String, ByVal Senha As String)
        Dim Buffer As New ByteBuffer
        Buffer.WriteLong(ClientPacket.Registro)
        Buffer.WriteString(Nome)
        Buffer.WriteString(Senha)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

    Public Sub SendLogin(ByVal Nome As String, ByVal Senha As String)
        Dim Buffer As New ByteBuffer
        Buffer.WriteLong(ClientPacket.Login)
        Buffer.WriteString(Nome)
        Buffer.WriteString(Senha)
        SendData(Buffer.ToArray)
        Buffer.Dispose()
    End Sub

End Module
