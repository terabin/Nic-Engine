Module modSpawn
    Public Spawn As cSpawn

    Public Sub ProcessNpcMove(ByVal spawnID As Short)
        If Spawn(spawnID).move = 0 Then Return

        Dim speed As Single = 2
        If Spawn(spawnID).move = 2 Then speed = 3
        Select Case Spawn(spawnID).Dir
            Case Dirs.UP
                Spawn(spawnID).yOffSet -= speed
                If Spawn(spawnID).yOffSet < 0 Then Spawn(spawnID).yOffSet = 0
            Case Dirs.DOWN
                Spawn(spawnID).yOffSet += speed
                If Spawn(spawnID).yOffSet > 0 Then Spawn(spawnID).yOffSet = 0
            Case Dirs.LEFT
                Spawn(spawnID).xOffSet -= speed
                If Spawn(spawnID).xOffSet < 0 Then Spawn(spawnID).xOffSet = 0
            Case Dirs.RIGHT
                Spawn(spawnID).xOffSet += speed
                If Spawn(spawnID).xOffSet > 0 Then Spawn(spawnID).xOffSet = 0
        End Select

        If Spawn(spawnID).Dir = Dirs.UP Or Spawn(spawnID).Dir = Dirs.LEFT Then
            If Spawn(spawnID).xOffSet <= 0 And Spawn(spawnID).yOffSet <= 0 Then
                Spawn(spawnID).move = 0
                Spawn(spawnID).MoveStep += 1
                If Spawn(spawnID).MoveStep > 1 Then Spawn(spawnID).MoveStep = 0
            End If
        ElseIf Spawn(spawnID).Dir = Dirs.DOWN Or Spawn(spawnID).Dir = Dirs.RIGHT Then
            If Spawn(spawnID).xOffSet >= 0 And Spawn(spawnID).yOffSet >= 0 Then
                Spawn(spawnID).move = 0
                Spawn(spawnID).MoveStep += 1
                If Spawn(spawnID).MoveStep > 1 Then Spawn(spawnID).MoveStep = 0
            End If
        End If
    End Sub
End Module

Public Class cSpawn
    Public Item(Options.MAX_MAP_NPCS) As SpawnData

    Public Class SpawnData
        Public Num As Integer = -1
        Public HP As Long
        Public X As Short
        Public Y As Short
        Public Dead As Boolean
        Public Dir As Short

        ' Move
        Public xOffSet As Single
        Public yOffSet As Single
        Public move As Byte
        Public MoveStep As Short

        Public Sub New()
            Num = -1
            HP = 0
            X = 0
            Y = 0
            Dead = True
            xOffSet = 0
            yOffSet = 0
        End Sub

        Public ReadOnly Property Dados As NpcData
            Get
                If Num = -1 Then Return Nothing
                Return Npc(Num)
            End Get
        End Property

    End Class

    Public Sub New()
        For i As Short = 0 To Item.Count - 1
            Item(i) = New SpawnData
        Next
    End Sub

    Public Function ClearAll() As cSpawn
        For i As Short = 0 To Item.Count - 1
            Change(i, -1, 0, 0, 0, False, 0)
        Next
        Return Me
    End Function

    Public Function Change(ByVal IDSpawn As Short, ByVal Num As Integer, ByVal HP As Long, ByVal X As Short, ByVal Y As Short, ByVal dead As Boolean, ByVal Dir As Byte) As cSpawn
        With Item(IDSpawn)
            .Num = Num
            .HP = HP
            .X = X
            .Y = Y
            .Dead = dead
            .Dir = Dir
            .move = 0
        End With
        Return Me
    End Function

    Default Public Property Base(ByVal Index As Integer) As SpawnData
        Get
            Return Item(Index)
        End Get
        Set(ByVal value As SpawnData)
            Item(Index) = value
        End Set
    End Property
End Class
