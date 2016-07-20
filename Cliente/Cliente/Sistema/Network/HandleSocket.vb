Module HandleSocket

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
        UpdateSkill
    End Enum

    Public Sub HandleDataPackets(ByVal Data() As Byte)
        Dim Buffer As New ByteBuffer
        Dim packetID As ServerPacket
        Buffer.WriteBytes(Data)

        packetID = Buffer.ReadLong

        Select Case packetID
            Case ServerPacket.Alerta : HandleAlerta(Data)
            Case ServerPacket.LoginOK : HandleLoginOK(Data)
            Case ServerPacket.ClasseData : HandleClasseData(Data)
            Case ServerPacket.InGame : HandleInGame()
            Case ServerPacket.PlayerData : HandlePlayerData(Data)
            Case ServerPacket.PlayerDir : HandlePlayerDir(Data)
            Case ServerPacket.PlayerMove : HandlePlayerMove(Data)
            Case ServerPacket.PlayerXY : HandlePlayerXY(Data)
            Case ServerPacket.CheckMap : HandleCheckMap(Data)
            Case ServerPacket.Map : HandleMap(Data)
            Case ServerPacket.UpdateNpc : HandleUpdateNpc(Data)
            Case ServerPacket.NpcSpawn : HandleNpcSpawn(Data)
            Case ServerPacket.ItemUpdate : HandleItemUpdate(Data)
            Case ServerPacket.DropItem : HandleDropItem(Data)
            Case ServerPacket.InvSlotUpdate : HandleInvSlotUpdate(Data)
            Case ServerPacket.MsgAnim : HandleMsgAnim(Data)
            Case ServerPacket.Attack : HandleAttack(Data)
            Case ServerPacket.UpdateAnim : HandleUpdateAnim(Data)
            Case ServerPacket.NpcMove : HandleNpcMove(Data)
            Case ServerPacket.NpcVital : HandleNpcVital(Data)
            Case ServerPacket.NpcDir : HandleNpcDir(Data)
            Case ServerPacket.PlayerVitalHP : HandlePlayerVitalHP(Data)
            Case ServerPacket.UpdateSkill : HandleUpdateSkill(Data)
            Case Else
                'Nothing
        End Select
        Buffer.Dispose()
    End Sub

    Public Sub HandleUpdateSkill(ByVal data() As Byte)
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
        End With
        Buffer.Dispose()
    End Sub

    Public Sub HandlePlayerVitalHP(ByVal data() As Byte)
        Using Buffer As New ByteBuffer(data)
            Buffer.ReadLong()
            Dim Index As Integer = Buffer.ReadInteger
            Player(Index).HP = Buffer.ReadInteger
            Player(Index).MaxHP = Buffer.ReadInteger
        End Using
    End Sub

    Public Sub HandleNpcDir(ByVal data() As Byte)
        Using Buffer As New ByteBuffer(data)
            Buffer.ReadLong()
            Dim spawnId As Short = Buffer.ReadShort
            Spawn(spawnId).Dir = Buffer.ReadShort
        End Using
    End Sub

    Public Sub HandleNpcVital(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim spawnID As Short = Buffer.ReadShort
        Spawn(spawnID).HP = Buffer.ReadInteger
        Buffer.Dispose()
    End Sub

    Public Sub HandleNpcMove(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim i As Short = Buffer.ReadShort
        Spawn(i).Dir = Buffer.ReadShort
        Spawn(i).X = Buffer.ReadShort
        Spawn(i).Y = Buffer.ReadShort
        Buffer.Dispose()

        ' Mover
        Spawn(i).move = 1
        Select Case Spawn(i).Dir
            Case Dirs.UP : Spawn(i).yOffSet = 32
            Case Dirs.DOWN : Spawn(i).yOffSet = -32
            Case Dirs.LEFT : Spawn(i).xOffSet = 32
            Case Dirs.RIGHT : Spawn(i).xOffSet = -32
        End Select


    End Sub

    Public Sub HandleUpdateAnim(ByVal data() As Byte)
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
        Animation(i).ColourGDI = Drawing.Color.FromArgb(Buffer.ReadInteger)
        Animation(i).BlendMode = Buffer.ReadShort
        Buffer.Dispose()
    End Sub

    Public Sub HandleAttack(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim index As Integer = Buffer.ReadInteger
        Buffer.Dispose()

        Player(index).Attack = 1
        Player(index).AttackTimer = GetTickCount + ATTACK_TIMER_DEFAULT
    End Sub

    Public Sub HandleMsgAnim(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim Text As String = Buffer.ReadString
        Dim X As Single = Buffer.ReadShort
        Dim Y As Single = Buffer.ReadShort
        Dim Colour As Color = Color.FromArgb(Buffer.ReadInteger)
        Buffer.Dispose()

        MsgAnim.Add(Text, X, Y, New SFML.Graphics.Color(Colour.R, Colour.G, Colour.B, Colour.A))
    End Sub

    Public Sub HandleInvSlotUpdate(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim InvId As Short = Buffer.ReadShort

        Inventory(InvId).Num = Buffer.ReadInteger
        Inventory(InvId).Value = Buffer.ReadInteger

        For i As Short = 0 To Options.MAX_INV_GEMA - 1
            Inventory(InvId).GemaSlot(i) = Buffer.ReadInteger
        Next
        Buffer.Dispose()
    End Sub

    Public Sub HandleDropItem(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()

        Dim id As Short = Buffer.ReadShort
        Drop(id).Num = Buffer.ReadInteger
        Drop(id).Value = Buffer.ReadInteger
        Drop(id).X = Buffer.ReadShort
        Drop(id).Y = Buffer.ReadShort
        Buffer.Dispose()

        Drop(id).yOff = -48
    End Sub

    Public Sub HandleItemUpdate(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim len As Integer = Buffer.ReadInteger
        Dim d() As Byte = Decompress(Buffer.ReadBytes(len))
        Buffer.Dispose()

        Buffer = New ByteBuffer(d)
        Dim id As Integer = Buffer.ReadInteger
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
    End Sub

    Public Sub HandleNpcSpawn(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim len As Integer = Buffer.ReadInteger
        Dim d() As Byte = Decompress(Buffer.ReadBytes(len - 1))
        Buffer.Dispose()

        Buffer = New ByteBuffer(d)
        Dim id As Short = Buffer.ReadShort
        Spawn.Change(id, Buffer.ReadInteger, Buffer.ReadLong, Buffer.ReadShort, Buffer.ReadShort, CBool(Buffer.ReadShort), Buffer.ReadShort)
        Buffer.Dispose()
    End Sub

    Public Sub HandleUpdateNpc(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
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
    End Sub

    Public Sub HandleMap(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim newMap As Byte = Buffer.ReadShort
        If newMap = 1 Then
            Dim newB As New ByteBuffer
            Dim len As Integer = Buffer.ReadInteger
            Dim b() As Byte = Buffer.ReadBytes(len - 1)
            b = Decompress(b)
            newB.WriteBytes(b)


            Map.Nome = newB.ReadString
            Map.Revision = newB.ReadInteger
            Map.Zona = newB.ReadShort
            Map.Musica = newB.ReadString
            Map.Top = newB.ReadInteger
            Map.Left = newB.ReadInteger
            Map.Bottom = newB.ReadInteger
            Map.Right = newB.ReadInteger
            Map.MaxX = newB.ReadShort
            Map.MaxY = newB.ReadShort
            ReDim Map.Tile(Map.MaxX + 1, Map.MaxY + 1)
            For X As Short = 0 To Map.MaxX
                For Y As Short = 0 To Map.MaxY
                    Map.Tile(X, Y) = New TileData
                    Map.Tile(X, Y).Type = newB.ReadShort
                    Map.Tile(X, Y).Value1 = newB.ReadString

                    For i As Short = 1 To Layers.Count - 1
                        Map.Tile(X, Y).Layer(i).TileSet = newB.ReadShort
                        Map.Tile(X, Y).Layer(i).BufferX = newB.ReadShort
                        Map.Tile(X, Y).Layer(i).BufferY = newB.ReadShort
                    Next
                Next
            Next

            For i As Short = 0 To Options.MAX_MAP_NPCS - 1
                Map.Spawn(i) = newB.ReadInteger
            Next

            SalvarMapa()
        End If

        Buffer.Dispose()

        If Map.Musica.Length > 0 Then Musica.Play(Map.Musica)
        SendMapComplete()
    End Sub

    Public Sub HandleCheckMap(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim rev As Short = Buffer.ReadInteger
        Buffer.Dispose()

        Dim ok As Byte = 1
        LoadMapa()
        If rev = Map.Revision Then ok = 0

        ' Resetar Spawns
        Spawn.ClearAll()

        SendRequestMap(ok)
    End Sub

    Public Sub HandlePlayerXY(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim Index As Integer = Buffer.ReadInteger
        Player(Index).Map = Buffer.ReadShort
        Player(Index).X = Buffer.ReadShort
        Player(Index).Y = Buffer.ReadShort
        Buffer.Dispose()
    End Sub

    Public Sub HandlePlayerMove(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim Index As Integer = Buffer.ReadInteger
        Player(Index).Move = Buffer.ReadShort
        Player(Index).Dir = Buffer.ReadShort
        Player(Index).X = Buffer.ReadShort
        Player(Index).Y = Buffer.ReadShort
        Buffer.Dispose()

        Select Case Player(Index).Dir
            Case Dirs.UP
                Player(Index).YOffSet = 32
            Case Dirs.DOWN
                Player(Index).YOffSet = -32
            Case Dirs.LEFT
                Player(Index).XOffSet = 32
            Case Dirs.RIGHT
                Player(Index).XOffSet = -32
        End Select
    End Sub

    Public Sub HandlePlayerDir(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim Index As Integer = Buffer.ReadInteger
        Dim dir As Byte = Buffer.ReadShort
        Buffer.Dispose()
        Player(Index).Dir = dir
    End Sub

    Public Sub HandlePlayerData(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()
        Dim index As Integer = Buffer.ReadInteger
        Dim len As Integer = Buffer.ReadInteger
        Dim b() As Byte = Buffer.ReadBytes(len)
        b = Decompress(b)
        Buffer.Dispose()

        Buffer = New ByteBuffer(b)
        Player(index).Nome = Buffer.ReadString
        Player(index).Access = Buffer.ReadShort
        Player(index).Sprite = Buffer.ReadShort
        Player(index).Classe = Buffer.ReadShort
        Player(index).Dir = Buffer.ReadShort
        Player(index).Map = Buffer.ReadShort
        Player(index).X = Buffer.ReadShort
        Player(index).Y = Buffer.ReadShort
        Player(index).HP = Buffer.ReadInteger
        Player(index).MaxHP = Buffer.ReadInteger
        Buffer.Dispose()
    End Sub

    Public Sub HandleInGame()
        Tela_General.Open(Telas.InGame)
    End Sub

    Public Sub HandleClasseData(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer(data)
        Buffer.ReadLong()

        Dim maxClasse As Short = Buffer.ReadShort

        ReDim Classe(0 To maxClasse)

        If Classe.Length > 1 Then
            For i As Short = 1 To UBound(Classe)
                Classe(i) = New clsClasse
                Classe(i).Nome = Buffer.ReadString
                Classe(i).Sprite = Buffer.ReadShort
                Classe(i).STR = Buffer.ReadInteger
                Classe(i).CON = Buffer.ReadInteger
            Next
        End If
        Buffer.Dispose()
    End Sub

    Public Sub HandleLoginOK(ByVal data() As Byte)
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(data)
        Buffer.ReadLong()
        MyIndex = Buffer.ReadInteger

        ' Char Data
        For i As Short = 0 To 4
            CharData(i).ID = Buffer.ReadInteger
            If CharData(i).ID > -1 Then
                CharData(i).Nome = Buffer.ReadString
                CharData(i).Sprite = Buffer.ReadShort
            End If
        Next

        Buffer.Dispose()

        Tela_General.Open(Telas.SelectChar)
    End Sub

    Public Sub HandleAlerta(ByVal Data() As Byte)
        Dim Buffer As New ByteBuffer
        Buffer.WriteBytes(Data)
        Buffer.ReadLong()
        Tela_General.MessageString = Buffer.ReadString
        Buffer.Dispose()
    End Sub
End Module
