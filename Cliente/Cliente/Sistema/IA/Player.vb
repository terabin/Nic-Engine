Module modPlayer
    Public Player() As clsPlayer

#Region "Keys"
    Public pressKeyUp As Boolean
    Public pressKeyDown As Boolean
    Public pressKeyLeft As Boolean
    Public pressKeyRight As Boolean
    Public pressShift As Boolean
#End Region

    Public Sub PlayerMove(ByVal Dir As Dirs)
        If Player(MyIndex).Move > 0 Then Return
        If Player(MyIndex).Dir <> Dir Then
            Player(MyIndex).Dir = Dir
            SendChangeDir(Dir)
        End If

        If CanMove Then
            Select Case Dir
                Case Dirs.UP
                    Player(MyIndex).Y -= 1
                    Player(MyIndex).YOffSet = 32

                Case Dirs.DOWN
                    Player(MyIndex).Y += 1
                    Player(MyIndex).YOffSet = -32
                Case Dirs.LEFT
                    Player(MyIndex).X -= 1
                    Player(MyIndex).XOffSet = 32
                Case Dirs.RIGHT
                    Player(MyIndex).X += 1
                    Player(MyIndex).XOffSet = -32
            End Select
            Player(MyIndex).Move = 1
            If pressShift Then Player(MyIndex).Move = 2
            SendPlayerMove()
        End If
    End Sub

    Public Sub ProcessPlayerMove(ByVal Index As Integer)
        If Player(Index).Move = 0 Then Return
        Dim speed As Single = 2
        If Player(Index).Move = 2 Then speed = 3
        Select Case Player(Index).Dir
            Case Dirs.UP
                Player(Index).YOffSet -= speed
                If Player(Index).YOffSet < 0 Then Player(Index).YOffSet = 0
            Case Dirs.DOWN
                Player(Index).YOffSet += speed
                If Player(Index).YOffSet > 0 Then Player(Index).YOffSet = 0
            Case Dirs.LEFT
                Player(Index).XOffSet -= speed
                If Player(Index).XOffSet < 0 Then Player(Index).XOffSet = 0
            Case Dirs.RIGHT
                Player(Index).XOffSet += speed
                If Player(Index).XOffSet > 0 Then Player(Index).XOffSet = 0
        End Select

        If Player(Index).Dir = Dirs.UP Or Player(Index).Dir = Dirs.LEFT Then
            If Player(Index).XOffSet <= 0 And Player(Index).YOffSet <= 0 Then
                Player(Index).Move = 0
                Player(Index).MoveStep += 1
                If Player(Index).MoveStep > 1 Then Player(Index).MoveStep = 0
                ' Player(Index).MoveTimer = GetTickCount + 150
            End If
        ElseIf Player(Index).Dir = Dirs.DOWN Or Player(Index).Dir = Dirs.RIGHT Then
            If Player(Index).XOffSet >= 0 And Player(Index).YOffSet >= 0 Then
                Player(Index).Move = 0
                Player(Index).MoveStep += 1
                If Player(Index).MoveStep > 1 Then Player(Index).MoveStep = 0
                ' Player(Index).MoveTimer = GetTickCount + 150
            End If
        End If
    End Sub

    Public ReadOnly Property CanMove As Boolean
        Get
            If Player(MyIndex).Move > 0 Then Return False

            Dim X As Short = Player(MyIndex).X
            Dim Y As Short = Player(MyIndex).Y

            Select Case Player(MyIndex).Dir
                Case Dirs.UP : Y -= 1
                Case Dirs.DOWN : Y += 1
                Case Dirs.LEFT : X -= 1
                Case Dirs.RIGHT : X += 1
            End Select

            For i As Short = 0 To Options.MAX_MAP_NPCS - 1
                If Spawn(i).Num > -1 And Not Spawn(i).Dead Then
                    If X = Spawn(i).X And Y = Spawn(i).Y Then Return False
                End If
            Next

            If X >= 0 And X < Map.MaxX And Y >= 0 And Y < Map.MaxY Then
                Select Case Map.Tile(X, Y).Type
                    Case TileType.None : Return True
                    Case TileType.Block : Return False
                End Select
                Return True
            Else
                SendPlayerWarpMap()
                Return False
            End If
        End Get
    End Property

    Public Sub CheckAttack()
        If MyData.Attack = 0 Then
            MyData.Attack = 1
            MyData.AttackTimer = GetTickCount + ATTACK_TIMER_DEFAULT
            SendAttack()
        End If
    End Sub
End Module
