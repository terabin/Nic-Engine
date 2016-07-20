
Module HandleSocket
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
        AdminDrop
        CheckItem
        DropInv
        ChangeInv
        Attack
        UpdateAnim
        UpdateSkill
    End Enum

    Public Sub HandleDataPackets(ByVal Index As Integer, ByVal Data() As Byte)
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(Data)

        Dim packetID As ClientPacket = Buffer.ReadLong

        Select Case packetID
            Case ClientPacket.Registro : HandleRegistro(Index, Data)
            Case ClientPacket.Login : HandleLogin(Index, Data)
            Case ClientPacket.CriarChar : HandleCriarChar(Index, Data)
            Case ClientPacket.UseChar : HandleUseChar(Index, Data)
            Case ClientPacket.PlayerMove : HandlePlayerMove(Index, Data)
            Case ClientPacket.ChangeDir : HandleChangeDir(Index, Data)
            Case ClientPacket.RequestMap : HandleRequestMap(Index, Data)
            Case ClientPacket.MapComplete : HandleMapComplete(Index)
            Case ClientPacket.SaveMap : HandleSaveMap(Index, Data)
            Case ClientPacket.PlayerWarpMap : HandlePlayerWarpMap(Index)
            Case ClientPacket.NpcUpdate : HandleNpcUpdate(Index, Data)
            Case ClientPacket.ChangeSprite : HandleChangeSprite(Index, Data)
            Case ClientPacket.ItemUpdate : HandleItemUpdate(Index, Data)
            Case ClientPacket.AdminDrop : HandleAdminDrop(Index, Data)
            Case ClientPacket.CheckItem : HandleCheckItem(Index)
            Case ClientPacket.DropInv : HandleDropInv(Index, Data)
            Case ClientPacket.ChangeInv : HandleChangeInv(Index, Data)
            Case ClientPacket.Attack : HandleAttack(Index)
            Case ClientPacket.UpdateAnim : HandleUpdateAnim(Index, Data)
            Case ClientPacket.UpdateSkill : HandleUpdateSkill(Index, Data)
            Case Else
                ' Nothing
        End Select
        Buffer.Dispose()
    End Sub

    Public Sub HandleUpdateSkill(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim len As Integer = Buffer.ReadInteger
        Dim b() As Byte = Decompress(Buffer.ReadBytes(len - 1))
        Buffer.Dispose()

        Buffer = New ByteBuffer
        Dim id As Integer = Buffer.ReadInteger

        With Skill(id)
            .Nome = Buffer.ReadString
            .CustoMP = Buffer.ReadInteger
            .CoolDown = Buffer.ReadShort
            .MaxEffect = Buffer.ReadShort
            .Icon = Buffer.ReadShort

            For i As Short = 1 To MAX_EFEITOS
                .Effect(i).Tipo = Buffer.ReadShort
                .Effect(i).CastAnimation = Buffer.ReadShort
                .Effect(i).CastTimer = Buffer.ReadShort
                .Effect(i).Anim = Buffer.ReadShort
                .Effect(i).Vital = Buffer.ReadInteger
                .Effect(i).isAOE = Buffer.ReadShort
                .Effect(i).Range = Buffer.ReadShort
                .Effect(i).Roubo = Buffer.ReadShort
            Next
            .Save()
            SendUpdateSkillToAll(id)
        End With
    End Sub

    Public Sub HandleUpdateAnim(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim len As Integer = Buffer.ReadInteger
        Dim b() As Byte = Decompress(Buffer.ReadBytes(len - 1))
        Buffer.Dispose()

        Buffer = New ByteBuffer(b)
        Dim i As Integer = Buffer.ReadInteger
        Animation(i).Nome = Buffer.ReadString
        Animation(i).Layer = Buffer.ReadShort
        Animation(i).AnimID = Buffer.ReadShort
        Animation(i).FrameCount = Buffer.ReadShort
        Animation(i).FrameX = Buffer.ReadShort
        Animation(i).FrameY = Buffer.ReadShort
        Animation(i).SpeedMS = Buffer.ReadShort
        Animation(i).Colour = Drawing.Color.FromArgb(Buffer.ReadInteger)
        Animation(i).BlendMode = Buffer.ReadShort
        Buffer.Dispose()
        Animation(i).Save()
        SendUpdateAnimToAll(i)
    End Sub

    Public Sub HandleAttack(ByVal Index As Integer)
        ProcessPlayerAttack(Index)
    End Sub

    Public Sub HandleChangeInv(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim oldSlot As Short = Buffer.ReadShort
        Dim newSlot As Short = Buffer.ReadShort
        Buffer.Dispose()

        ' Copy Values NewSlot
        Dim newInv As New clsInv
        newInv.Num = Inventory(Index, newSlot).Num
        newInv.Value = Inventory(Index, newSlot).Value
        For i As Short = 0 To Options.MAX_INV_GEMA - 1
            newInv.GemaSlot(i) = Inventory(Index, newSlot).GemaSlot(i)
        Next

        ' Copy to new Slot
        With Inventory(Index, newSlot)
            .Num = Inventory(Index, oldSlot).Num
            .Value = Inventory(Index, oldSlot).Value

            For i As Short = 0 To Options.MAX_INV_GEMA - 1
                .GemaSlot(i) = Inventory(Index, oldSlot).GemaSlot(i)
            Next
        End With

        ' Copy to old Slot
        With Inventory(Index, oldSlot)
            .Num = newInv.Num
            .Value = newInv.Value

            For i As Short = 0 To Options.MAX_INV_GEMA - 1
                .GemaSlot(i) = newInv.GemaSlot(i)
            Next
        End With

        newInv = Nothing
        SendInvSlotUpdate(Index, oldSlot)
        SendInvSlotUpdate(Index, newSlot)
    End Sub

    Public Sub HandleDropInv(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim invID As Short = Buffer.ReadShort
        Dim value As Integer = 1
        Buffer.Dispose()

        SendDropItem(Player(Index).Map, Drop(Player(Index).Map).Add(Player(Index).Inv(invID).Num, value, Player(Index).X, Player(Index).Y))
        Player(Index).RemoveInvSlot(invID, value)
    End Sub

    Public Sub HandleCheckItem(ByVal Index As Integer)
        For i As Short = 1 To Options.MAX_MAP_ITEM
            If Drop(Player(Index).Map)(i).Num > 0 Then
                If Drop(Player(Index).Map)(i).X = Player(Index).X And Drop(Player(Index).Map)(i).Y = Player(Index).Y Then
                    Player(Index).AddInvItem(Drop(Player(Index).Map)(i).Num, Drop(Player(Index).Map)(i).Value)

                    Dim Nome As String = Trim(Drop(Player(Index).Map)(i).Dados.Nome)
                    If Drop(Player(Index).Map)(i).Value > 1 Then Nome = Drop(Player(Index).Map)(i).Value & " " & Nome
                    SendMsgAnim(Index, Nome, (Player(Index).X * 32) + 16, (Player(Index).Y * 32) - 32, Color.Yellow.ToArgb)
                    Drop(Player(Index).Map).Remove(i)
                    SendDropItem(Player(Index).Map, i)
                    Return
                End If
            End If
        Next
    End Sub

    Public Sub HandleAdminDrop(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim itemID As Integer = Buffer.ReadInteger
        Dim itemVal As Integer = Buffer.ReadInteger
        Buffer.Dispose()

        Dim id As Short = Drop(Player(Index).Map).Add(itemID, itemVal, Player(Index).X, Player(Index).Y)
        SendDropItem(Player(Index).Map, id)
    End Sub

    Public Sub HandleItemUpdate(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim len As Integer = Buffer.ReadInteger
        Dim d() As Byte = Decompress(Buffer.ReadBytes(len))
        Buffer.Dispose()

        Buffer = New ByteBuffer(d)
        Dim id As Short = Buffer.ReadInteger
        Item(id).Nome = Buffer.ReadString
        Item(id).Tipo = Buffer.ReadShort
        Item(id).Icon = Buffer.ReadShort
        Item(id).isDrop = Buffer.ReadShort
        Item(id).isStack = Buffer.ReadShort
        Item(id).Desc = Buffer.ReadString
        Item(id).Peso = Buffer.ReadInteger
        Item(id).PotionMode = Buffer.ReadShort
        Item(id).PotionVital = Buffer.ReadInteger
        Item(id).PotionCD = Buffer.ReadShort
        Item(id).PotionEffect = Buffer.ReadShort
        Item(id).EquipTipo = Buffer.ReadShort
        Item(id).EquipPaper = Buffer.ReadShort
        Item(id).EquipHP = Buffer.ReadInteger
        Item(id).EquipMP = Buffer.ReadInteger
        Item(id).EquipFOR = Buffer.ReadInteger
        Item(id).EquipCON = Buffer.ReadInteger
        Buffer.Dispose()

        Item(id).Save()
        SendUpdateItemToAll(id)
    End Sub

    Public Sub HandleChangeSprite(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim idSprite As Integer = Buffer.ReadInteger
        Buffer.Dispose()

        Player(Index).Sprite = idSprite
        SendPlayerData(Index)
    End Sub

    Public Sub HandleNpcUpdate(ByVal Index As Integer, ByVal Data() As Byte)
        Dim Buffer As New ByteBuffer(Data)
        Buffer.ReadLong()

        Dim len As Integer = Buffer.ReadInteger
        Dim d() As Byte = Decompress(Buffer.ReadBytes(len - 1))
        Buffer.Dispose()
        Buffer = New ByteBuffer(d)

        Dim id As Integer = Buffer.ReadInteger
        Npc(id).Nome = Buffer.ReadString
        Npc(id).Tipo = Buffer.ReadShort
        Npc(id).Level = Buffer.ReadShort
        Npc(id).EXP = Buffer.ReadLong
        Npc(id).HP = Buffer.ReadLong
        Npc(id).STR = Buffer.ReadInteger
        Npc(id).CON = Buffer.ReadInteger
        Npc(id).Sprite = Buffer.ReadShort
        Npc(id).Paperdoll = Buffer.ReadShort
        Npc(id).SpawnTime = Buffer.ReadShort
        Npc(id).NpcSpellIntervalo = Buffer.ReadShort

        For i As Short = 0 To 3
            Npc(id).Drop(i).Num = Buffer.ReadInteger
            Npc(id).Drop(i).Value = Buffer.ReadInteger
            Npc(id).Drop(i).Chance = Buffer.ReadShort
        Next

        For i As Short = 0 To 3
            Npc(id).NpcSpell(i).Num = Buffer.ReadInteger
            Npc(id).NpcSpell(i).CD = Buffer.ReadShort
        Next

        Buffer.Dispose()

        Npc(id).Save()
    End Sub

    Public Sub HandlePlayerWarpMap(ByVal Index As Integer)
        Dim dir As Short = Player(Index).Dir
        Dim mapID As Integer = Player(Index).Map
        Dim offCalc As Short = 0
        Select Case dir
            Case Dirs.UP
                If Mapa(mapID).Top > 0 Then
                    offCalc = Player(Index).X
                    If offCalc > Mapa(Mapa(mapID).Top).MaxX - 1 Then offCalc = Mapa(Mapa(mapID).Top).MaxX - 1
                    PlayerWarp(Index, Mapa(mapID).Top, offCalc, Mapa(Mapa(mapID).Top).MaxY - 1)
                End If
            Case Dirs.LEFT
                If Mapa(mapID).Left > 0 Then
                    offCalc = Player(Index).Y
                    If offCalc > Mapa(Mapa(mapID).Left).MaxY - 1 Then offCalc = Mapa(Mapa(mapID).Left).MaxY - 1
                    PlayerWarp(Index, Mapa(mapID).Left, Mapa(Mapa(mapID).Left).MaxX - 1, offCalc)
                End If
            Case Dirs.DOWN
                If Mapa(mapID).Bottom > 0 Then
                    offCalc = Player(Index).X
                    If offCalc > Mapa(Mapa(mapID).Bottom).MaxX - 1 Then offCalc = Mapa(Mapa(mapID).Bottom).MaxX - 1
                    PlayerWarp(Index, Mapa(mapID).Bottom, offCalc, 0)
                End If
            Case Dirs.RIGHT
                If Mapa(mapID).Right > 0 Then
                    offCalc = Player(Index).Y
                    If offCalc > Mapa(Mapa(mapID).Right).MaxY - 1 Then offCalc = Mapa(Mapa(mapID).Right).MaxY - 1
                    PlayerWarp(Index, Mapa(mapID).Right, 0, offCalc)
                End If
        End Select
    End Sub

    Public Sub HandleSaveMap(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim len As Integer = Buffer.ReadInteger

        Dim newByte() As Byte = Buffer.ReadBytes(len - 1)
        newByte = Decompress(newByte)
        Buffer.Dispose()

        Buffer = New ByteBuffer(newByte)

        Dim mapId As Integer = Player(Index).Map
        Mapa(mapId).Nome = Buffer.ReadString
        Mapa(mapId).Revision = Buffer.ReadInteger
        Mapa(mapId).Revision += 1
        Mapa(mapId).Zona = Buffer.ReadShort
        Mapa(mapId).Musica = Buffer.ReadString
        Mapa(mapId).Top = Buffer.ReadInteger
        Mapa(mapId).Left = Buffer.ReadInteger
        Mapa(mapId).Bottom = Buffer.ReadInteger
        Mapa(mapId).Right = Buffer.ReadInteger
        Mapa(mapId).MaxX = Buffer.ReadShort
        Mapa(mapId).MaxY = Buffer.ReadShort
        ReDim Mapa(mapId).Tile(Mapa(mapId).MaxX + 1, Mapa(mapId).MaxY + 1)
        For X As Short = 0 To Mapa(mapId).MaxX
            For Y As Short = 0 To Mapa(mapId).MaxY
                Mapa(mapId).Tile(X, Y) = New TileData
                Mapa(mapId).Tile(X, Y).Type = Buffer.ReadShort
                Mapa(mapId).Tile(X, Y).Value1 = Buffer.ReadString

                For i As Short = 1 To Layers.Count - 1
                    Mapa(mapId).Tile(X, Y).Layer(i).TileSet = Buffer.ReadShort
                    Mapa(mapId).Tile(X, Y).Layer(i).BufferX = Buffer.ReadShort
                    Mapa(mapId).Tile(X, Y).Layer(i).BufferY = Buffer.ReadShort
                Next
            Next
        Next

        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            Mapa(mapId).Spawn(i) = Buffer.ReadInteger
        Next
        Buffer.Dispose()

        SaveMapa(mapId)

        MapNpc(mapId).ClearAll()
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            If Mapa(mapId).Spawn(i) > 0 Then
                MapNpc(mapId).Add(Mapa(mapId).Spawn(i), Rand(0, Mapa(mapId).MaxX), Rand(0, Mapa(mapId).MaxY))
            End If
        Next
        SendNpcSpawns(Index)

        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then
                If Player(i).Map = mapId Then
                    SendMap(i, 1)
                End If
            End If
        Next
    End Sub

    Public Sub HandleMapComplete(ByVal Index As Integer)
        Dim Buffer As ByteBuffer
        ' Receber dados de todos
        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) And Index <> i Then
                If Player(i).Map = Player(Index).Map Then
                    Buffer = New ByteBuffer(ServerPacket.PlayerData)
                    Buffer.WriteInteger(i)
                    Dim b() As Byte = PlayerData(i)
                    Buffer.WriteInteger(b.Length)
                    Buffer.WriteBytes(b)
                    SendDataTo(Index, Buffer.ToArray)
                    Buffer.Dispose()
                End If
            End If
        Next

        ' Enviar meus dados a todos
        SendPlayerData(Index)

        ' Enviar Spawn
        SendNpcSpawns(Index)
    End Sub

    Public Sub HandleRequestMap(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim ok As Byte = Buffer.ReadShort
        Buffer.Dispose()

        SendMap(Index, ok)
    End Sub

    Public Sub HandleChangeDir(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()

        Dim dir As Byte = Buffer.ReadShort
        Buffer.Dispose()

        Player(Index).Dir = dir
        SendPlayerDir(Index)
    End Sub

    Public Sub HandlePlayerMove(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim move As Byte = Buffer.ReadShort

        Dim X As Short = Player(Index).X
        Dim Y As Short = Player(Index).Y
        Select Case Player(Index).Dir
            Case Dirs.UP : Y -= 1
            Case Dirs.DOWN : Y += 1
            Case Dirs.LEFT : X -= 1
            Case Dirs.RIGHT : X += 1
        End Select

        Player(Index).X = X
        Player(Index).Y = Y
        SendPlayerMove(Index, move)
    End Sub

    Public Sub HandleUseChar(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim idSlot As Short = Buffer.ReadShort
        Buffer.Dispose()
        Player(Index).Clear()
        Player(Index).Nome = Conta(Index).CharID(idSlot)
        Player(Index).Load()
        JoinGame(Index)
    End Sub

    Public Sub HandleCriarChar(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(data)
        Buffer.ReadLong()
        Dim Nome As String = Buffer.ReadString
        Dim ClasseID As Short = Buffer.ReadShort
        Dim CharSlot As Short = Buffer.ReadShort - 1
        Buffer.Dispose()

        If IO.File.Exists(Application.StartupPath & "\Data\Personagem\" & Nome & ".bin") Then
            SendAlerta(Index, "Ja existe um personagem com este nome!")
            Return
        End If

        Dim newChar As clsPlayer = New clsPlayer()
        newChar.Nome = Nome
        newChar.Classe = ClasseID
        newChar.Sprite = Classe(ClasseID).Sprite
        newChar.STR(True) = Classe(ClasseID).STR
        newChar.CON(True) = Classe(ClasseID).CON
        newChar.Map = Classe(ClasseID).Mapa
        newChar.X = Classe(ClasseID).X
        newChar.Y = Classe(ClasseID).Y
        newChar.Save()
        newChar = Nothing
        Conta(Index).CharID(CharSlot) = Nome
        Conta(Index).Save()

        SendLogin(Index)
        SendAlerta(Index, "Personagem criado com sucesso!")
    End Sub

    Public Sub HandleLogin(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(data)
        Buffer.ReadLong()
        Dim Nome As String = Buffer.ReadString
        Dim Senha As String = Buffer.ReadString
        Buffer.Dispose()

        If Not IO.File.Exists(Application.StartupPath & "\Data\Conta\" & Nome & ".bin") Then
            SendAlerta(Index, "Conta não existe!")
            Return
        End If

        Dim c As New clsConta
        c.Clear()
        c.Nome = Nome
        c.Load()

        If Senha.ToLower <> c.Senha.ToLower Then
            SendAlerta(Index, "Senha inválida!")
            Conta(Index).Clear()
            Return
        End If

        For i As Integer = 1 To Options.MAX_PLAYERS
            If Conta(i).Nome = Nome Then
                SendAlerta(Index, "Está conta está logada!")
                c = Nothing
                CloseSocket(i)
                Return
            End If
        Next

        c = Nothing
        Conta(Index).Clear()
        Conta(Index).Nome = Nome
        Conta(Index).Load()

        ' OK
        SendClasseData(Index)
        SendLogin(Index)

    End Sub

    Public Sub HandleRegistro(ByVal Index As Integer, ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(data)
        Buffer.ReadLong()
        Dim Nome As String = Buffer.ReadString
        Dim Senha As String = Buffer.ReadString
        Buffer.Dispose()

        If IO.File.Exists(Application.StartupPath & "\data\conta\" & Nome & ".bin") Then
            SendAlerta(Index, "Já existe uma conta com esse nome!")
            Return
        End If

        Conta(Index).Nome = Nome
        Conta(Index).Senha = Senha
        Conta(Index).Save()
        SendAlerta(Index, "Conta Criada!")
    End Sub
End Module
