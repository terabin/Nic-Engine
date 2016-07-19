
Module modSpawn
    Public MapNpc() As SpawnData
    Public Const HIBERN_TIMER As Integer = 6000 ' 6 segundos

    Public Sub CacheSpawnAllMap()
        For i As Short = 1 To Options.MAX_MAPS
            MapNpc(i).ClearAll()
            For x As Short = 0 To Options.MAX_MAP_NPCS - 1
                If Mapa(i).Spawn(x) > 0 Then
                    MapNpc(i).Add(Mapa(i).Spawn(x), Rand(0, Mapa(i).MaxX), Rand(0, Mapa(i).MaxY))
                End If
            Next
        Next
    End Sub

    Public Sub ProcessSpawnIA()
        For i As Short = 1 To Options.MAX_MAPS
            MapNpc(i).Update()
        Next
    End Sub
End Module

Public Class SpawnData
    Public Item(0 To Options.MAX_MAP_NPCS - 1) As SpawnItemData
    Public Class SpawnItemData
        Public Num As Integer
        Public Dead As Boolean
        Public Hibernar As Boolean
        Public timerHibernar As Integer
        Public timerRespawn As Integer
        Public X As Short
        Public Y As Short
        Public DestinoX As Short
        Public DestinoY As Short
        Public HP As Long
        Public Alvo As Integer
        Public Dir As Byte
        Public timerAttack As Integer

        Public Sub New()
            Num = 0
            Dead = False
            Hibernar = False
            timerHibernar = 0
            timerRespawn = 0
            DestinoX = 0
            DestinoY = 0
            Alvo = 0
        End Sub

        Public ReadOnly Property Dados As NpcData
            Get
                If Num >= 0 Then
                    Return Npc(Num)
                Else
                    Return Nothing
                End If
            End Get
        End Property

        Public Sub Update(ByVal senderMapID As Integer)
#Region "Dead"
            If Dead Then
                ' Função de Respawn
                If GetTickCount > timerRespawn Then
                    Dead = False
                    HP = Dados.HP
                    X = Rand(0, Mapa(senderMapID).MaxX)
                    Y = Rand(0, Mapa(senderMapID).MaxY)
                    DestinoX = X
                    DestinoY = Y
                    timerRespawn = 0
                    timerHibernar = GetTickCount + HIBERN_TIMER
                    Alvo = 0
                    Dir = Rand(0, 3)
                    SendNpcSpawnAll(senderMapID, MyIndex(senderMapID))
                End If
                Return
            End If
#End Region

#Region "Determinar Movimento"
            If Alvo > 0 Then
                ' Seguir o alvo
                If IsPlaying(Alvo) Then
                    If Player(Alvo).Map = senderMapID Then
                        DestinoX = Player(Alvo).X
                        DestinoY = Player(Alvo).Y
                    Else
                        DestinoX = X
                        DestinoY = Y
                        timerHibernar = GetTickCount + HIBERN_TIMER
                    End If
                Else
                    DestinoX = X
                    DestinoY = Y
                    timerHibernar = GetTickCount + HIBERN_TIMER
                End If
            Else
                ' Tipo agressivo
                If Dados.Tipo = NpcType.AttackAll Then
                    For i As Integer = 1 To Options.MAX_PLAYERS
                        If IsPlaying(i) Then
                            If Player(i).Map = senderMapID Then
                                If Player(i).Access = 0 Then
                                    If Modulo(X - Player(i).X, Y - Player(i).Y) <= 6 Then ' 6 de Range
                                        Alvo = i
                                        Return
                                    End If
                                End If
                            End If
                        End If
                    Next
                ElseIf Dados.Tipo = NpcType.AttackSafe Then
                    For o As Short = 0 To Options.MAX_MAP_NPCS - 1
                        If MapNpc(senderMapID)(o).Num > 0 And X <> MyIndex(senderMapID) Then
                            If MapNpc(senderMapID)(o).Num = Num Then
                                If Modulo(X - MapNpc(senderMapID)(o).X, Y - MapNpc(senderMapID)(o).Y) <= 6 Then
                                    If MapNpc(senderMapID)(o).Alvo > 0 Then
                                        Alvo = MapNpc(senderMapID)(o).Alvo
                                        Return
                                    End If
                                End If
                            End If
                        End If
                    Next
                End If

                ' Pós Hibern, se mover pra outro lugar
                If timerHibernar > 0 Then
                        If GetTickCount > timerHibernar Then
debugX:
                            DestinoX = X + Rand(-6, 6)
                            If DestinoX < 0 Or DestinoX > Mapa(senderMapID).MaxX Then GoTo debugX
debugY:
                            DestinoY = Y + Rand(-6, 6)
                            If DestinoY < 0 Or DestinoY > Mapa(senderMapID).MaxY Then GoTo debugY
                            If Not CanNewDestiny(senderMapID) Then GoTo debugX
                            timerHibernar = 0
                        End If
                    End If
                End If
#End Region

#Region "Direção"
                Dim direction As Byte = 0
            If DestinoX - 1 > X Then direction = 1 ' RIGHT
            If DestinoX + 1 < X Then direction = 2 ' LEFT
            If DestinoY - 1 > Y Then direction = 3 ' DOWN
            If DestinoY + 1 < Y Then direction = 4 ' UP
            If DestinoX - 1 > X And DestinoY - 1 > Y Then 'direction = 5 ' RIGHT + DOWN
                direction = 1
                If Rand(0, 1) = 0 Then direction = 3
            End If
            If DestinoX - 1 > X And DestinoY + 1 < Y Then 'direction = 6 ' RIGHT + UP
                direction = 1
                If Rand(0, 1) = 0 Then direction = 4
            End If
            If DestinoX + 1 < X And DestinoY - 1 > Y Then ' direction = 7 ' LEFT + DOWN
                direction = 2
                If Rand(0, 1) = 0 Then direction = 3
            End If
            If DestinoX + 1 < X And DestinoY + 1 < Y Then 'direction = 8 ' LEFT + UP
                direction = 2
                If Rand(0, 1) = 0 Then direction = 4
            End If
            If X - 1 = DestinoX And Y - 1 = DestinoY Then
                direction = 2 + (2 * Rand(0, 1))
            End If
            If X + 1 = DestinoX And Y - 1 = DestinoY Then
                direction = 1 + (3 * Rand(0, 1))
            End If
            If X + 1 = DestinoX And Y + 1 = DestinoY Then
                direction = 1 + (2 * Rand(0, 1))
            End If
            If X - 1 = DestinoX And Y + 1 = DestinoY Then
                direction = 2 + (1 * Rand(0, 1))
            End If
#End Region

#Region "Processar Movimento"
debugPross:
            Select Case direction
                Case 0 ' Atacar ou Hibernar
                    If Alvo > 0 Then
                        ' Atacar
                        Dim d As Byte = Dir
                        If DestinoX > X Then d = Dirs.RIGHT
                        If DestinoX < X Then d = Dirs.LEFT
                        If DestinoY > Y Then d = Dirs.DOWN
                        If DestinoY < Y Then d = Dirs.UP

                        If d <> Dir Then
                            Dir = d
                            SendNpcDir(senderMapID, MyIndex(senderMapID))
                        End If
                        If GetTickCount > timerAttack Then
                            Dim damage As Integer = Dados.STR - Player(Alvo).Defesa
                            If damage > 0 Then Combat.NpcToPlayer(senderMapID, MyIndex(senderMapID), damage)
                            timerAttack = GetTickCount + 1000
                        End If
                    Else
                        ' Hibernar
                        If timerHibernar = 0 Then
                            DestinoX = X
                            DestinoY = Y
                            timerHibernar = GetTickCount + HIBERN_TIMER
                        End If
                    End If
                Case 1 ' Right
                    If CanMove(Dirs.RIGHT, senderMapID) Then
                        ProcessMove(Dirs.RIGHT, senderMapID)
                    Else
                        direction = Rand(2, 4)
                        GoTo debugPross
                    End If
                Case 2 ' Left
                    If CanMove(Dirs.LEFT, senderMapID) Then
                        ProcessMove(Dirs.LEFT, senderMapID)
                    Else
                        direction = Rand(1, 3)
                        If direction = 2 Then direction = 4
                        GoTo debugPross
                    End If
                Case 3 ' Down
                    If CanMove(Dirs.DOWN, senderMapID) Then
                        ProcessMove(Dirs.DOWN, senderMapID)
                    Else
                        direction = Rand(1, 3)
                        If direction = 3 Then direction = 4
                        GoTo debugPross
                    End If
                Case 4 ' UP
                    If CanMove(Dirs.UP, senderMapID) Then
                        ProcessMove(Dirs.UP, senderMapID)
                    Else
                        direction = Rand(1, 3)
                        GoTo debugPross
                    End If
            End Select
#End Region
        End Sub

        Public Sub ProcessMove(ByVal Dir2 As Byte, ByVal senderMapID As Integer)
            Dir = Dir2
            Select Case Dir
                Case Dirs.UP : Y -= 1
                Case Dirs.DOWN : Y += 1
                Case Dirs.LEFT : X -= 1
                Case Dirs.RIGHT : X += 1
            End Select

            ' Enviar para os jogadores
            SendNpcMove(senderMapID, MyIndex(senderMapID))
        End Sub

        Public Function CanMove(ByVal Dir2 As Byte, ByVal senderMapID As Integer) As Boolean
            Dim tmpX As Short = X
            Dim tmpY As Short = Y

            Select Case Dir2
                Case Dirs.UP : tmpY -= 1
                Case Dirs.DOWN : tmpY += 1
                Case Dirs.LEFT : tmpX -= 1
                Case Dirs.RIGHT : tmpX += 1
            End Select

            ' Fora do mapa
            If tmpX < 0 Then Return False
            If tmpX > Mapa(senderMapID).MaxX Then Return False
            If tmpY < 0 Then Return False
            If tmpY > Mapa(senderMapID).MaxY Then Return False

            ' Bloqueio ou teleporte
            If Mapa(senderMapID).Tile(tmpX, tmpY).Type = (TileType.Block Or TileType.Warp) Then Return False

            ' Checar se tem algum jogador no caminho
            For i As Short = 1 To Options.MAX_PLAYERS
                If IsPlaying(i) Then
                    If Player(i).Map = senderMapID Then
                        If Player(i).X = tmpX And Player(i).Y = tmpY Then Return False
                    End If
                End If
            Next

            ' NPC
            For i As Short = 0 To Options.MAX_MAP_NPCS - 1
                If MapNpc(senderMapID)(i).Num > 0 And i <> MyIndex(senderMapID) Then
                    If Not MapNpc(senderMapID)(i).Dead Then
                        If MapNpc(senderMapID)(i).X = tmpX And MapNpc(senderMapID)(i).Y = tmpY Then Return False
                    End If
                End If
            Next

            Return True
        End Function

        Public Function CanNewDestiny(ByVal senderMapID As Integer) As Boolean
            ' Block ou Warp
            If Mapa(senderMapID).Tile(DestinoX, DestinoY).Type = (TileType.Block Or TileType.Warp) Then Return False

            ' Jogador
            If Alvo = 0 Then
                For i As Integer = 1 To Options.MAX_PLAYERS
                    If IsPlaying(i) Then
                        If Player(i).Map = senderMapID And Player(i).X = DestinoX And Player(i).Y = DestinoY Then
                            Return False
                        End If
                    End If
                Next
            End If

            ' NPC
            For i As Short = 0 To Options.MAX_MAP_NPCS - 1
                If MapNpc(senderMapID)(i).Num > 0 And i <> MyIndex(senderMapID) Then
                    If Not MapNpc(senderMapID)(i).Dead Then
                        If MapNpc(senderMapID)(i).X = DestinoX And MapNpc(senderMapID)(i).Y = DestinoY Then Return False
                    End If
                End If
            Next

            Return True
        End Function

        Public ReadOnly Property MyIndex(ByVal senderMapID As Integer) As Integer
            Get
                Return Array.IndexOf(MapNpc(senderMapID).Item, Me)
            End Get
        End Property
    End Class

    Public Sub New()
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            Item(i) = New SpawnItemData
        Next
    End Sub

    Default Public Property Base(ByVal Index As Integer) As SpawnItemData
        Get
            Return Item(Index)
        End Get
        Set(ByVal value As SpawnItemData)
            Item(Index) = value
        End Set
    End Property

    Public Function Add(ByVal Num As Integer, ByVal X As Short, ByVal Y As Short) As SpawnData
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            If Item(i).Num = 0 Then
                Item(i).Num = Num
                Item(i).X = X
                Item(i).Y = Y
                Item(i).DestinoX = X
                Item(i).DestinoY = Y
                Item(i).Alvo = 0
                Item(i).HP = Item(i).Dados.HP
                Item(i).Hibernar = True
                Item(i).timerHibernar = GetTickCount + HIBERN_TIMER
                Item(i).Dead = False
                Item(i).Dir = Rand(0, 3)
                Return Me
            End If
        Next
        Return Me
    End Function

    Public Function ClearAll() As SpawnData
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            Item(i).Num = 0
            Item(i).X = 0
            Item(i).Y = 0
            Item(i).HP = 0
            Item(i).Hibernar = False
            Item(i).timerHibernar = 0
            Item(i).Dead = False
            Item(i).DestinoX = 0
            Item(i).DestinoY = 0
            Item(i).timerRespawn = 0
            Item(i).Alvo = 0
        Next
        Return Me
    End Function

    Public Sub Update()
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            If Item(i).Num > 0 Then Item(i).Update(myMap)
        Next
        DoEvents()
    End Sub

    Public ReadOnly Property myMap As Integer
        Get
            Return Array.IndexOf(MapNpc, Me)
        End Get
    End Property

End Class