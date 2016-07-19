Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Partial Public Class Tela_InGame
    Inherits clsTelas

    Public Cursor As Vector2f
    Public SelectChangedAtalho As Short = 0
    Public InventoryDesc As Short = -1

    Const SpeedScreen As Single = 1.2
    Public ScreenXOff As Single
    Public ScreenYOff As Single

#Region "Methods"
    Public Overrides Sub Core()

        ' Update Camera
        UpdateCamera()

        If GetTickCount > Map.AnimTimer Then
            Map.Anim = Not Map.Anim
            Map.AnimTimer = GetTickCount + 250
        End If

        ' Movimento
        CheckKeys()
        For i As Short = 1 To Options.MAX_PLAYERS
            If IsPlaying(i) Then ProcessPlayerMove(i)
        Next
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            If Spawn(i).Num > 0 Then ProcessNpcMove(i)
        Next

        ' Processar Animação
        For i As Short = 1 To Options.MAX_ANIMATION_BUFFER
            If AnimationBuffer(i).AnimationID > 0 And AnimationBuffer(i).Timer > 0 Then ProcessAnimation(i)
        Next

        MyBase.Core()
    End Sub

    Public Sub UpdateCamera()
#Region "Outro"
        Dim offsetX As Short
        Dim offsetY As Short
        Dim StartX As Short
        Dim StartY As Short
        Dim EndX As Short
        Dim EndY As Short
        offsetX = MyData.XOffSet + 32
        offsetY = MyData.YOffSet + 32
        StartX = MyData.X - ((Options.MAX_X + 1) \ 2) - 1
        StartY = MyData.Y - ((Options.MAX_Y + 1) \ 2) - 1

        If StartX < 0 Then
            offsetX = 0

            If StartX = -1 Then
                If MyData.XOffSet > 0 Then
                    offsetX = MyData.XOffSet
                End If
            End If

            StartX = 0
        End If

        If StartY < 0 Then
            offsetY = 0

            If StartY = -1 Then
                If MyData.YOffSet > 0 Then
                    offsetY = MyData.YOffSet
                End If
            End If

            StartY = 0
        End If

        EndX = StartX + (Options.MAX_X + 1) + 1
        EndY = StartY + (Options.MAX_Y + 1) + 1

        If EndX > Map.MaxX Then
            offsetX = 32

            If EndX = Map.MaxX + 1 Then
                If MyData.XOffSet < 0 Then
                    offsetX = MyData.XOffSet + 32
                End If
            End If

            EndX = Map.MaxX
            StartX = EndX - Options.MAX_X - 1
        End If

        If EndY > Map.MaxY Then
            offsetY = 32
            If EndY = Map.MaxY + 1 Then
                If MyData.YOffSet < 0 Then
                    offsetY = MyData.YOffSet + 32
                End If
            End If

            EndY = Map.MaxY
            StartY = EndY - Options.MAX_Y - 1
        End If

        With TileView
            .Top = StartY
            .Height = EndY
            .Left = StartX
            .Width = EndX
        End With

        With Camera
            .Top = offsetY
            .Height = DeviceGame.Size.Y + 32
            .Left = offsetX
            .Width = DeviceGame.Size.X + 32
        End With
#End Region


    End Sub

    Public Sub ProcessAnimation(ByVal Index As Integer)
        If GetTickCount > AnimationBuffer(Index).Timer Then
            AnimationBuffer(Index).Frame += 1
            If AnimationBuffer(Index).Frame > AnimationBuffer(Index).Dados.FrameCount Then
                AnimationBuffer.Remove(Index)
                Return
            End If
            AnimationBuffer(Index).Timer = GetTickCount + AnimationBuffer(Index).Dados.SpeedMS
        End If
    End Sub
#End Region

    Public Overrides Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If frmEditor_Map.Visible Then MouseEditor_Down(e)
        MyBase.MouseDown(e)
    End Sub

    Public Overrides Sub MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        Cursor = New Vector2f(e.X, e.Y)
        If frmEditor_Map.Visible Then MouseEditor_Down(e)
        MyBase.MouseMove(e)
    End Sub

    Public Overrides Function KeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        If SelectChangedAtalho > 0 Then Return True
        If MyBase.KeyPress(e) Then Return True
        Return False
    End Function

    Public Overrides Function KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        ' PADRAO / NO HOVER
        If SelectChangedAtalho > 0 Then
            If e.KeyCode = Keys.Escape Then
                SelectChangedAtalho = 0
                Return True
            End If
            Options.Atalhos(SelectChangedAtalho) = e.KeyCode
            Options.SavePlayerConfig()
            SelectChangedAtalho = 0
            Return True
        End If

        Select Case e.KeyCode
            Case Keys.W, Keys.Up : pressKeyUp = False
            Case Keys.S, Keys.Down : pressKeyDown = False
            Case Keys.A, Keys.Left : pressKeyLeft = False
            Case Keys.D, Keys.Right : pressKeyRight = False
            Case Keys.ShiftKey : pressShift = False
        End Select


        If MyBase.KeyUp(e) Then Return True

        ' POS HOVER
        Select Case e.KeyCode
            Case Keys.Insert
                If Player(MyIndex).Access > 0 Then
                    frmAdmin.Show()
                End If
                Return True

            Case Keys.Enter
                txtChat.SetFocus()

            Case Keys.Space
                SendCheckItem()

            Case Keys.F1
                AnimationBuffer.Add(1, Player(MyIndex).X, Player(MyIndex).Y)

        End Select

        Return False
    End Function

    Public Overrides Function KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        If SelectChangedAtalho > 0 Then Return True
        If MyBase.KeyDown(e) Then Return True

        Select Case e.KeyCode
            Case Keys.W, Keys.Up : pressKeyUp = True
            Case Keys.S, Keys.Down : pressKeyDown = True
            Case Keys.A, Keys.Left : pressKeyLeft = True
            Case Keys.D, Keys.Right : pressKeyRight = True
            Case Keys.ShiftKey : pressShift = True
            Case Keys.ControlKey : CheckAttack()
        End Select
        Return False
    End Function

    Private Sub cmbMochila_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbMochila.MouseUp
        winMochila.Toggle()
    End Sub

    Private Sub cmbOption_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbOption.MouseUp
        winOption.Toggle()
    End Sub

    Private Sub winOption_MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) Handles winOption.MouseMove
        For i As Short = 1 To 12
            If e.X >= winOption.X + 40 And e.X <= winOption.X + 40 + 50 Then
                If e.Y >= winOption.Y + 25 + 20 + (20 * (i - 1)) And e.Y <= winOption.Y + 25 + 20 + (20 * (i - 1)) + 18 Then
                    frmMain.Cursor = Cursors.Hand
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub winOption_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles winOption.MouseUp
        For i As Short = 1 To 12
            If e.X >= winOption.X + 40 And e.X <= winOption.X + 40 + 50 Then
                If e.Y >= winOption.Y + 25 + 20 + (20 * (i - 1)) And e.Y <= winOption.Y + 25 + 20 + (20 * (i - 1)) + 18 Then
                    SelectChangedAtalho = i
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub winMochila_MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs) Handles winMochila.MouseDown
        For i As Short = 1 To Options.MAX_INV
            If Inventory(i - 1).Num > 0 Then
                If Colisao(New Vector2f(e.X, e.Y), New IntRect(winMochila.X + 16 + (34 * ((i - 1) Mod 5)), winMochila.Y + 32 + (34 * ((i - 1) \ 5)), 32, 32)) Then
                    If e.Button = MouseButtons.Left Then
                        Drag.Num = i
                        Drag.Tipo = DragType.Inventory
                        Drag.X = e.X
                        Drag.Y = e.Y
                        Return
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub winMochila_MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) Handles winMochila.MouseMove
        InventoryDesc = -1
        For i As Short = 1 To Options.MAX_INV
            If Inventory(i - 1).Num > 0 Then
                If Colisao(New Vector2f(e.X, e.Y), New IntRect(winMochila.X + 16 + (34 * ((i - 1) Mod 5)), winMochila.Y + 32 + (34 * ((i - 1) \ 5)), 32, 32)) Then
                    InventoryDesc = i - 1
                    Return
                End If
            End If
        Next
    End Sub

    Private Sub winMochila_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles winMochila.MouseUp
        For i As Short = 1 To Options.MAX_INV

            If Colisao(New Vector2f(e.X, e.Y), New IntRect(winMochila.X + 16 + (34 * ((i - 1) Mod 5)), winMochila.Y + 32 + (34 * ((i - 1) \ 5)), 32, 32)) Then
                If Inventory(i - 1).Num > 0 Then
                    If e.Button = MouseButtons.Right Then
                        SendDropInv(i - 1)
                        Return
                    End If
                End If
                If e.Button = MouseButtons.Left Then
                    If Drag.Tipo = DragType.Inventory Then
                        SendChangeInv(Drag.Num - 1, i - 1)
                    End If
                    Return
                End If
            End If

        Next
    End Sub

    Private Sub txtChat_OnEnter() Handles txtChat.OnEnter
        If Len(txtChat.Text.Trim) = 0 Then TextBox.Focus = Nothing : Return
        Dim text As String = txtChat.Text
        txtChat.Text = ""

        Dim command() As String = Split(text, " ")
        Select Case LCase(command(0))
            Case "@setsprite"
                If Player(MyIndex).Access > 0 Then
                    If UBound(command) < 1 Then
                        Chat.Add("@setsprite <numerodasprite>", Color.Red)
                        Return
                    End If

                    If Not IsNumeric(command(1)) Then
                        Chat.Add("@setsprite <NUMEROdasprite>", Color.Red)
                        Return
                    End If
                    SendChangeSprite(Int(command(1)))
                End If
                Return
            Case "@drop"
                If Player(MyIndex).Access > 0 Then
                    If UBound(command) < 1 Then
                        Chat.Add("@drop <numerodoitem> [quantia]", Color.Red)
                        Return
                    End If

                    If Not IsNumeric(command(1)) Then
                        Chat.Add("@drop <numerodoitem> [quantia]", Color.Red)
                        Return
                    End If

                    Dim count As Integer = 1
                    If UBound(command) > 1 Then
                        If IsNumeric(command(2)) Then count = command(2)
                    End If

                    SendDropAdmin(command(1), count)
                End If
                Return
        End Select

        ' Teste chat
        Chat.Add(text)

    End Sub
End Class

Public Class Drag
    Public Shared X As Short
    Public Shared Y As Short
    Public Shared Tipo As Byte
    Public Shared Num As Integer

    Public Shared Sub Draw()
        Dim mousePos As Vector2f = New Vector2f(frmMain.mouseLoc.X, frmMain.mouseLoc.Y)
        If mousePos.X <> X Or mousePos.Y <> Y Then
            Select Case Tipo
                Case DragType.Inventory
                    Dim icon As Short = Inventory(Num - 1).Dados.Icon
                    RenderBox(New Color(60, 60, 60), New IntRect(mousePos.X - 16, mousePos.Y - 16, 32, 32))
                    If icon > 0 Then
                        Dim size As Vector2f = GetTextureSize(texItem(icon))
                        RenderTexture(texItem(icon), New IntRect(mousePos.X - 16 + ((32 - size.X) / 2), mousePos.Y - 16 + ((32 - size.Y) / 2), size.X, size.Y), New IntRect(0, 0, size.X, size.Y))
                    End If
            End Select
        End If
    End Sub

    Public Shared Sub Clear()
        X = 0
        Y = 0
        Tipo = DragType.None
        Num = 0
    End Sub
End Class