Imports SFML.Graphics
Imports SFML.System

Partial Public Class Tela_InGame
    Public Overrides Sub Draw()

        ' Mapa Ground
        For X As Short = TileView.Left To TileView.Width
            For Y As Short = TileView.Top To TileView.Height
                If isTryingMap(X, Y) Then RenderMapTile(X, Y)
            Next
        Next

        ' Pré Grafico do Tile
        If frmEditor_Map.Visible And frmEditor_Map.LayerNum < Layers.Fringe Then RenderMapCursor()

        ' Drops | No Anim
        For i As Short = 1 To Options.MAX_MAP_ITEM
            If Drop(i).Num > 0 And Drop(i).yOff = 0 Then RenderDrop(i)
        Next

        ' Animation | Ground
        For i As Short = 1 To Options.MAX_ANIMATION_BUFFER
            If AnimationBuffer(i).AnimationID > 0 And AnimationBuffer(i).Timer > 0 Then
                If AnimationBuffer(i).Dados.Layer = AnimLayer.Ground Then RenderAnimation(i)
            End If
        Next

        For Y As Short = TileView.Top To TileView.Height
            For i As Short = 1 To Options.MAX_PLAYERS
                If Len(Player(i).Nome.Trim) > 0 Then
                    If Player(i).Y = Y Then
                        RenderPlayer(i)
                        RenderPlayerName(i)
                    End If
                End If
            Next

            For i As Short = 0 To Options.MAX_MAP_NPCS - 1
                If Map.Spawn(i) > 0 And Spawn(i).Num > -1 And Not Spawn(i).Dead Then
                    If Spawn(i).Y = Y Then
                        RenderNpc(i)
                        RenderNpcName(i)
                    End If
                End If
            Next
        Next

        ' Drops | Yes Anim
        For i As Short = 1 To Options.MAX_MAP_ITEM
            If Drop(i).Num > 0 And Drop(i).yOff < 0 Then RenderDrop(i)
        Next

        ' Animation | Fringe
        For i As Short = 1 To Options.MAX_ANIMATION_BUFFER
            If AnimationBuffer(i).AnimationID > 0 And AnimationBuffer(i).Timer > 0 Then
                If AnimationBuffer(i).Dados.Layer = AnimLayer.Fringe Then RenderAnimation(i)
            End If
        Next

        ' Mapa Fringe
        For X As Short = TileView.Left To TileView.Width
            For Y As Short = TileView.Top To TileView.Height
                If isTryingMap(X, Y) Then RenderMapFringe(X, Y)
            Next
        Next

        ' Message Anim
        For i As Short = 1 To Options.MAX_MSGANIM
            If MsgAnim(i).TimerEnd > 0 Then
                MsgAnim(i).Y -= MSGANIM_VELO
                RenderText(MsgAnim(i).Text, MapX(MsgAnim(i).X), MapY(MsgAnim(i).Y), MsgAnim(i).Color, , , True)
                If GetTickCount > MsgAnim(i).TimerEnd Then
                    MsgAnim.Remove(i)
                End If
            End If
        Next

        RenderUI()
        If frmEditor_Npc.Visible Then RenderEditor_NPCSprite()
        If frmEditor_Map.Visible And frmEditor_Map.gpAtr.Visible Then RenderMapAtributtes()
        If frmEditor_Map.Visible And frmEditor_Map.LayerNum >= Layers.Fringe Then RenderMapCursor()
        If frmEditor_Map.Visible And frmEditor_Map.chkGrid.Checked Then RenderMapGrid()
        If frmEditor_Item.Visible Then RenderEditor_ItemIcon()
        If frmEditor_Anim.Visible Then RenderEditor_Animation()
        MyBase.Draw()
    End Sub

    Public Overrides Sub DrawFront()
        If Not frmEditor_Map.Visible Then
            Drag.Draw()
        End If
        MyBase.DrawFront()
    End Sub

    Private Sub RenderPlayer(ByVal index As Integer)
        If Player(index).Nome.Length > 0 Then
            Dim X As Short, Y As Short
            Dim imgRec As IntRect
            Dim size As Vector2f = GetTextureSize(texChar(Player(index).Sprite))
            X = MapX((Player(index).X * 32) + ((32 - (size.X / 4)) / 2) + Player(index).XOffSet)
            Y = MapY((Player(index).Y * 32) + 32 - (size.Y / 4) + Player(index).YOffSet)

            ' Default
            imgRec = New IntRect(0, 0, size.X / 4, size.Y / 4)

            Select Case Player(index).Dir
                Case Dirs.UP
                    imgRec.Top = 3 * (size.Y / 4)
                    If Player(index).YOffSet > 12 Then imgRec.Left = ((1 + (2 * Player(index).MoveStep)) * (size.X / 4))
                Case Dirs.DOWN
                    imgRec.Top = 0
                    If Player(index).YOffSet < -12 Then imgRec.Left = ((1 + (2 * Player(index).MoveStep)) * (size.X / 4))
                Case Dirs.LEFT
                    imgRec.Top = 1 * (size.Y / 4)
                    If Player(index).XOffSet > 12 Then imgRec.Left = ((1 + (2 * Player(index).MoveStep)) * (size.X / 4))
                Case Dirs.RIGHT
                    imgRec.Top = 2 * (size.Y / 4)
                    If Player(index).XOffSet < -12 Then imgRec.Left = ((1 + (2 * Player(index).MoveStep)) * (size.X / 4))
            End Select

            ' Attack anim
            If Player(index).Attack > 0 Then
                If GetTickCount > Player(index).AttackTimer - 500 Then imgRec.Left = 1 * (size.X / 4)
                If GetTickCount > Player(index).AttackTimer Then
                    Player(index).AttackTimer = 0
                    Player(index).Attack = 0
                End If
            End If

            RenderTexture(texChar(Player(index).Sprite), New IntRect(X, Y, size.X / 4, size.Y / 4), imgRec)

            ' Hero Aura
            X = MapX((Player(index).X * 32) + ((32 - 96) / 2) + Player(index).XOffSet)
            Y = MapY((Player(index).Y * 32) - (96 / 2) + 16 + Player(index).YOffSet)
            imgRec = New IntRect(192 * ((10 + Player(index).Hero.Frame) Mod 5), 192 * ((10 + Player(index).Hero.Frame) \ 5), 192, 192)
            RenderTexture(texAnimation(1), New IntRect(X, Y, 96, 96), imgRec, New Color(255, 255, 255, 150), True)

            If GetTickCount > Player(index).Hero.Timer Then
                If Player(index).Hero.X = 0 Then
                    Player(index).Hero.Frame += 1
                    If Player(index).Hero.Frame > 6 Then Player(index).Hero.X = 1
                Else
                    Player(index).Hero.Frame -= 1
                    If Player(index).Hero.Frame = 0 Then Player(index).Hero.X = 0
                End If
                Player(index).Hero.Timer = GetTickCount + 40
            End If
        End If
    End Sub

    Private Sub RenderMapTile(ByVal X As Short, ByVal Y As Short)
        On Error Resume Next
        For i As Short = 1 To Layers.MaskAnim2
            If Map.Tile(X, Y)(i).TileSet > 0 Then
                If i = Layers.MaskAnim Or i = Layers.MaskAnim2 Then
                    If Map.Anim Then
                        RenderTexture(texTile(Map.Tile(X, Y)(i).TileSet), New IntRect(MapX(X * 32), MapY(Y * 32), 32, 32), New IntRect(Map.Tile(X, Y)(i).BufferX, Map.Tile(X, Y)(i).BufferY, 32, 32))
                    End If
                Else
                    RenderTexture(texTile(Map.Tile(X, Y)(i).TileSet), New IntRect(MapX(X * 32), MapY(Y * 32), 32, 32), New IntRect(Map.Tile(X, Y)(i).BufferX, Map.Tile(X, Y)(i).BufferY, 32, 32))
                End If

            End If
        Next
    End Sub

    Private Sub RenderMapFringe(ByVal X As Short, ByVal Y As Short)
        On Error Resume Next
        For i As Short = Layers.Fringe To Layers.FringeAnim2
            If Map.Tile(X, Y)(i).TileSet > 0 Then
                If i = Layers.FringeAnim Or i = Layers.FringeAnim2 Then
                    If Map.Anim Then
                        RenderTexture(texTile(Map.Tile(X, Y)(i).TileSet), New IntRect(MapX(X * 32), MapY(Y * 32), 32, 32), New IntRect(Map.Tile(X, Y)(i).BufferX, Map.Tile(X, Y)(i).BufferY, 32, 32))
                    End If
                Else
                    RenderTexture(texTile(Map.Tile(X, Y)(i).TileSet), New IntRect(MapX(X * 32), MapY(Y * 32), 32, 32), New IntRect(Map.Tile(X, Y)(i).BufferX, Map.Tile(X, Y)(i).BufferY, 32, 32))
                End If

            End If
        Next
    End Sub

    Private Sub RenderMapCursor()
        Dim X As Short, Y As Short

        X = Int(Cursor.X / 32) * 32
        Y = Int(Cursor.Y / 32) * 32
        If frmEditor_Map.gpLayer.Visible Then
            RenderTexture(texTile(frmEditor_Map.scrlMapTile.Value), New IntRect(X, Y, frmEditor_Map.MousePos.Width * 32, frmEditor_Map.MousePos.Height * 32), New IntRect(frmEditor_Map.MousePos.Left * 32, frmEditor_Map.MousePos.Top * 32, frmEditor_Map.MousePos.Width * 32, frmEditor_Map.MousePos.Height * 32), New Color(255, 255, 255, 200))
        Else
            Dim s As Shape = New RectangleShape(New Vector2f(32, 32))
            s.Position = New Vector2f(X, Y)
            s.FillColor = New Color(180, 30, 30, 150)
            DeviceGame.Draw(s)
        End If
    End Sub

    Private Sub RenderMapGrid()
        Dim vertex As VertexArray

        For X As Short = 1 To Options.MAX_X - 1
            vertex = New VertexArray(PrimitiveType.Lines)
            vertex.Append(New Vertex(New Vector2f(X * 32, 0), Color.Black))
            vertex.Append(New Vertex(New Vector2f(X * 32, DeviceGame.Size.Y), Color.Black))
            DeviceGame.Draw(vertex)
        Next

        For Y As Short = 1 To Options.MAX_Y - 1
            vertex = New VertexArray(PrimitiveType.Lines)
            vertex.Append(New Vertex(New Vector2f(0, Y * 32), Color.Black))
            vertex.Append(New Vertex(New Vector2f(DeviceGame.Size.X, Y * 32), Color.Black))
            DeviceGame.Draw(vertex)
        Next
    End Sub

    Private Sub RenderUI()
        If Not frmEditor_Map.Visible Then
            RenderHotbar()
            Chat.Draw()
        End If
    End Sub

    Private Sub RenderHotbar()
        For i As Short = 0 To 11
            RenderBox(New Color(255, 255, 255, 100), New IntRect(((DeviceGame.Size.X - (34 * 12)) / 2) + 34 * i, DeviceGame.Size.Y - 40, 32, 32))
            RenderText(GetKeyName(Options.Atalhos(i + 1)), 4 + ((DeviceGame.Size.X - (34 * 12)) / 2) + 34 * i, DeviceGame.Size.Y - 40 + 32 - 16, Color.White, , , True)
        Next
    End Sub

    Private Sub RenderNpc(ByVal Index As Integer)
        Dim Sprite As Integer = Spawn(Index).Dados.Sprite
        Dim size As Vector2f = GetTextureSize(texChar(Sprite))
        size.X = size.X / 4
        size.Y = size.Y / 4

        Dim X As Short, Y As Short
        X = (Spawn(Index).X * 32) + Spawn(Index).xOffSet + ((32 - size.X) / 2)
        Y = (Spawn(Index).Y * 32) + Spawn(Index).yOffSet - size.Y + 32

        Dim tempD As Short = Spawn(Index).Dir - 1
        If Spawn(Index).Dir = Dirs.UP Then tempD = 3
        Dim imgRec As IntRect = New IntRect(0, size.Y * tempD, size.X, size.Y)

        Select Case Spawn(Index).Dir
            Case Dirs.UP
                If Spawn(Index).yOffSet > 12 Then imgRec.Left = ((1 + (2 * Spawn(Index).MoveStep)) * (size.X))
            Case Dirs.DOWN
                If Spawn(Index).yOffSet < -12 Then imgRec.Left = ((1 + (2 * Spawn(Index).MoveStep)) * (size.X))
            Case Dirs.LEFT
                If Spawn(Index).xOffSet > 12 Then imgRec.Left = ((1 + (2 * Spawn(Index).MoveStep)) * (size.X))
            Case Dirs.RIGHT
                If Spawn(Index).xOffSet < -12 Then imgRec.Left = ((1 + (2 * Spawn(Index).MoveStep)) * (size.X))
        End Select
        RenderTexture(texChar(Sprite), New IntRect(MapX(X), MapY(Y), size.X, size.Y), imgRec)

        ' Bar
        If Spawn(Index).HP > 0 Then
            X = MapX((Spawn(Index).X * 32) + Spawn(Index).xOffSet)
            Y = MapY((Spawn(Index).Y * 32) + Spawn(Index).yOffSet + 38)
            Dim s As Shape
            s = New RectangleShape(New Vector2f(32, 4))
            s.Position = New Vector2f(X, Y)
            s.FillColor = Color.Black
            DeviceGame.Draw(s)

            Dim calcBar As Short

            calcBar = Int((Spawn(Index).HP * 100) / Spawn(Index).Dados.HP) ' %
            calcBar = Int((calcBar * 30) / 100)
            s = New RectangleShape(New Vector2f(calcBar, 2))
            s.Position = New Vector2f(X + 1, Y + 1)
            s.FillColor = Color.Green
            DeviceGame.Draw(s)
        End If

    End Sub

    Private Sub RenderDrop(ByVal Index As Integer)
        Dim itemPic As Short = Drop(Index).Dados.Icon

        If itemPic > 0 Then
            Dim X As Single = (Drop(Index).X * 32) + ((32 - GetTextureSize(texItem(itemPic)).X) / 2)
            Dim Y As Single = (Drop(Index).Y * 32) + ((32 - GetTextureSize(texItem(itemPic)).Y) / 2) + Drop(Index).yOff

            RenderTexture(texItem(itemPic), New IntRect(MapX(X), MapY(Y), GetTextureSize(texItem(itemPic)).X, GetTextureSize(texItem(itemPic)).Y), New IntRect(0, 0, GetTextureSize(texItem(itemPic)).X, GetTextureSize(texItem(itemPic)).Y))
        End If

        If Drop(Index).yOff < 0 Then
            Drop(Index).yOff += DROP_VELOY
            If Drop(Index).yOff > 0 Then Drop(Index).yOff = 0
        End If
    End Sub

    Private Sub RenderPlayerName(ByVal Index As Integer)
        If Player(Index).Nome.Length = 0 Then Return

        Dim X As Short, Y As Short, Colour As Color = Color.White

        Select Case Player(Index).Access
            Case 4 ' Admin
                Colour = Color.Cyan
        End Select

        X = (Player(Index).X * 32) + ((32 - GetTextWidth(Player(Index).Nome)) / 2) + Player(Index).XOffSet
        Y = (Player(Index).Y * 32) + 16 - (GetTextureSize(texChar(Player(Index).Sprite)).Y / 4) + Player(Index).YOffSet
        RenderText(Player(Index).Nome, MapX(X), MapY(Y), Colour, , , True)
    End Sub

    Private Sub RenderMapAtributtes()
        For X As Short = 0 To Options.MAX_X
            For Y As Short = 0 To Options.MAX_Y
                Select Case Map.Tile(X, Y).Type
                    Case TileType.Block
                        RenderText("B", X * 32 + ((32 - GetTextWidth("B")) / 2), Y * 32 + 8, Color.Red, , , True)
                End Select
            Next
        Next
    End Sub

    Private Sub RenderNpcName(ByVal Index As Integer)
        If Spawn(Index).Dados.Nome.Length = 0 Then Return
        Dim X As Short, Y As Short
        X = (Spawn(Index).X * 32) + Spawn(Index).xOffSet + ((32 - GetTextWidth(Spawn(Index).Dados.Nome)) / 2)
        Y = (Spawn(Index).Y * 32) + Spawn(Index).yOffSet - (GetTextureSize(texChar(Spawn(Index).Dados.Sprite)).Y / 4) + 16
        RenderText(Spawn(Index).Dados.Nome, MapX(X), MapY(Y), Color.White, , , True)
    End Sub

    Private Sub RenderAnimation(ByVal Index As Integer)
        Dim Anim As Integer = AnimationBuffer(Index).Dados.AnimID
        Dim Size As Vector2f = GetTextureSize(texAnimation(Anim))
        Size = New Vector2f(Size.X / AnimationBuffer(Index).Dados.FrameX, Size.Y / AnimationBuffer(Index).Dados.FrameY)
        Dim imgRec As New IntRect(Size.X * (AnimationBuffer(Index).Frame Mod AnimationBuffer(Index).Dados.FrameX), Size.Y * (AnimationBuffer(Index).Frame \ AnimationBuffer(Index).Dados.FrameX), Size.X, Size.Y)

        RenderTexture(texAnimation(Anim), New IntRect(MapX(AnimationBuffer(Index).X * 32) + ((32 - Size.X) / 2), MapY(AnimationBuffer(Index).Y * 32) + ((32 - Size.Y) / 2), Size.X, Size.Y), imgRec, AnimationBuffer(Index).Dados.Colour, CBool(AnimationBuffer(Index).Dados.BlendMode))
    End Sub

#Region "Events Draw"
    Private Sub cmbMochila_OnDraw(ByVal X As Short, ByVal Y As Short) Handles cmbMochila.OnDraw
        RenderTexture(texGUI(1), New IntRect(X + ((32 - GetTextureSize(texGUI(1)).X) / 2), Y + ((32 - GetTextureSize(texGUI(1)).Y) / 2), GetTextureSize(texGUI(1)).X, GetTextureSize(texGUI(1)).Y), New IntRect(0, 0, GetTextureSize(texGUI(1)).X, GetTextureSize(texGUI(1)).Y), New Color(255, 255, 255, 255))
    End Sub

    Private Sub cmbOption_OnDraw(ByVal X As Short, ByVal Y As Short) Handles cmbOption.OnDraw
        RenderTexture(texGUI(2), New IntRect(X, Y, 32, 32), New IntRect(0, 0, 128, 128))
    End Sub

    Private Sub winOption_OnDraw() Handles winOption.OnDraw
        RenderText("Atalhos", winOption.X + 10, winOption.Y + 25, Color.White, 14, , True)
        For i As Short = 1 To 12
            RenderText(i & "º:", winOption.X + 30 - GetTextWidth(i & "º:"), winOption.Y + 25 + 22 + (20 * (i - 1)), New Color(240, 240, 255), , , False)
            RenderBox(New Color(60, 60, 60), New IntRect(winOption.X + 40, winOption.Y + 25 + 20 + (20 * (i - 1)), 50, 18))
            If SelectChangedAtalho = i Then
                RenderText("' '", winOption.X + 40 + ((50 - GetTextWidth("' '")) / 2), winOption.Y + 25 + 20 + (20 * (i - 1)), Color.White)
            Else
                RenderText(GetKeyName(Options.Atalhos(i)), winOption.X + 40 + ((50 - GetTextWidth(GetKeyName(Options.Atalhos(i)))) / 2), winOption.Y + 25 + 20 + (20 * (i - 1)), Color.White)
            End If
        Next
    End Sub

    Private Sub winMochila_OnDraw() Handles winMochila.OnDraw
        Dim size As Vector2f
        For i As Short = 1 To Options.MAX_INV
            RenderBox(New Color(70, 70, 70), New IntRect(winMochila.X + 16 + (34 * ((i - 1) Mod 5)), winMochila.Y + 32 + (34 * ((i - 1) \ 5)), 32, 32))

            If Inventory(i - 1).Num > 0 Then
                Dim icon As Short = Inventory(i - 1).Dados.Icon

                If icon > 0 Then
                    size = GetTextureSize(texItem(icon))
                    RenderTexture(texItem(icon), New IntRect(winMochila.X + 16 + (34 * ((i - 1) Mod 5)) + ((32 - size.X) / 2), winMochila.Y + 32 + (34 * ((i - 1) \ 5)) + ((32 - size.Y) / 2), size.X, size.Y), New IntRect(New Vector2f(0, 0), size))

                    If Inventory(i - 1).Value > 1 Then
                        RenderText(Inventory(i - 1).Value, winMochila.X + 16 + (34 * ((i - 1) Mod 5)) + 4, winMochila.Y + 32 + (34 * ((i - 1) \ 5)) + 19, Color.Yellow, , , True)
                    End If
                End If
            End If
        Next

        ' Description
        If InventoryDesc > -1 Then
            If Inventory(InventoryDesc).Num = 0 Then Return
            ' Dados
            Dim w As Short = 150
            Dim h As Short = 36
            Dim yOff As Short = 0
            Dim desc As String = Trim(Inventory(InventoryDesc).Dados.Desc)
            Dim nome As String = Trim(Inventory(InventoryDesc).Dados.Nome)
            Dim existAtr As Boolean = False
            If Inventory(InventoryDesc).Value > 1 Then nome += " (" & Inventory(InventoryDesc).Value & ")"
            If 10 + GetTextWidth(nome) > w - 4 Then w = 40 + GetTextWidth(nome)

            ' Processar Altura
            desc = desc.Replace("[vital]", Inventory(InventoryDesc).Dados.PotionVital).Replace("[cd]", Inventory(InventoryDesc).Dados.PotionCD)
            Dim descBuffer() As String = WordWrap_Array(desc, w - 30)
            If UBound(descBuffer) > 0 Then
                h += 14 * UBound(descBuffer) + 14
            End If
            Select Case Inventory(InventoryDesc).Dados.Tipo
                Case ItemType.Equipamento
                    If Inventory(InventoryDesc).Dados.EquipHP > 0 Then
                        h += 14
                        yOff += 1
                        existAtr = True
                    End If
                    If Inventory(InventoryDesc).Dados.EquipMP > 0 Then
                        h += 14
                        yOff += 1
                        existAtr = True
                    End If
                    If Inventory(InventoryDesc).Dados.EquipFOR > 0 Then
                        h += 14
                        yOff += 1
                        existAtr = True
                    End If
                    If Inventory(InventoryDesc).Dados.EquipCON > 0 Then
                        h += 14
                        yOff += 1
                        existAtr = True
                    End If

                    If existAtr Then
                        h += 14
                        yOff += 1
                    End If
            End Select


            ' Localização
            Dim x As Short = winMochila.X - w
            Dim y As Short = winMochila.Y + 18
            If x < 0 Then x = winMochila.X + winMochila.Width
            RenderBox(New Color(50, 50, 50, 220), New IntRect(x, y, w, h))

            ' Icone
            RenderBox(New Color(255, 104, 0), New IntRect(x + 2, y + 2, 32, 32))
            Dim Icon As Short = Inventory(InventoryDesc).Dados.Icon
            If Icon > 0 Then
                size = GetTextureSize(texItem(Icon))
                RenderTexture(texItem(Icon), New IntRect(x + 2 + ((32 - size.X) / 2), y + 2 + ((32 - size.Y) / 2), size.X, size.Y), New IntRect(New Vector2f(0, 0), size))
            End If

            ' Nome
            RenderText(nome, x + 38, y + 2, New Color(220, 220, 220))

            ' Tipo de item
            Dim itemTipo As String = "Desconhecido"
            Select Case Inventory(InventoryDesc).Dados.Tipo
                Case ItemType.None : itemTipo = "Desconhecido"
                Case ItemType.Equipamento
                    Select Case Inventory(InventoryDesc).Dados.EquipTipo
                        Case 0 : itemTipo = "Arma"
                        Case 1 : itemTipo = "Capacete"
                        Case 2 : itemTipo = "Armadura"
                        Case 3 : itemTipo = "Luvas"
                        Case 4 : itemTipo = "Botas"
                        Case 5 : itemTipo = "Anel"
                        Case 6 : itemTipo = "Colar"
                    End Select
                    Dim off As Short = 1
                    RenderText("Atributos", x + 5, y + 36, Color.White)
                    If Inventory(InventoryDesc).Dados.EquipHP > 0 Then
                        RenderText("HP:", x + 15, y + 36 + (14 * off), New Color(200, 200, 200), 11)
                        RenderText(Inventory(InventoryDesc).Dados.EquipHP, x + 15 + 4 + GetTextWidth("HP:"), y + 36 + (14 * off), New Color(120, 120, 255), 11)
                        off += 1
                    End If
                    If Inventory(InventoryDesc).Dados.EquipMP > 0 Then
                        RenderText("MP:", x + 15, y + 36 + (14 * off), New Color(200, 200, 200), 11)
                        RenderText(Inventory(InventoryDesc).Dados.EquipMP, x + 15 + 4 + GetTextWidth("MP:"), y + 36 + (14 * off), New Color(120, 120, 255), 11)
                        off += 1
                    End If
                    If Inventory(InventoryDesc).Dados.EquipFOR > 0 Then
                        RenderText("FOR:", x + 15, y + 36 + (14 * off), New Color(200, 200, 200), 11)
                        RenderText(Inventory(InventoryDesc).Dados.EquipFOR, x + 15 + 4 + GetTextWidth("FOR:"), y + 36 + (14 * off), New Color(120, 120, 255), 11)
                        off += 1
                    End If
                    If Inventory(InventoryDesc).Dados.EquipCON > 0 Then
                        RenderText("CON:", x + 15, y + 36 + (14 * off), New Color(200, 200, 200), 11)
                        RenderText(Inventory(InventoryDesc).Dados.EquipCON, x + 15 + 4 + GetTextWidth("CON:"), y + 36 + (14 * off), New Color(120, 120, 255), 11)
                        off += 1
                    End If
            End Select
            RenderText(itemTipo, x + 38, y + 18, New Color(220, 150, 150))

            ' Descrição
            If UBound(descBuffer) > 0 Then
                For i As Short = 1 To UBound(descBuffer)
                    RenderText(descBuffer(i), x + 5, y + 34 + (14 * (i + yOff)), New Color(255, 150, 0))
                Next
            End If
        End If
    End Sub
#End Region
End Class
