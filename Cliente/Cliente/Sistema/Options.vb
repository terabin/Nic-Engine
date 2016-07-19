Public Class Options
    Public Shared Music As Boolean
    Public Shared Sound As Boolean
    Public Shared IP As String = "localhost"
    Public Shared PORT As String = "5000"
    Public Shared SalvarDados As Boolean
    Public Shared SalvarDados_Nome As String = ""
    Public Shared SalvarDados_Senha As String = ""
    Public Shared GAMENAME As String = "NIC Engine"

#Region "Máximo"
    Public Shared MAX_PLAYERS As Short = 100
    Public Shared MAX_X As Short = Int(1024 / 32)
    Public Shared MAX_Y As Short = Int(768 / 32)
    Public Shared MAX_NPC As Short = 100
    Public Shared MAX_MAP_NPCS As Short = 30
    Public Shared MAX_MAPS As Short = 100
    Public Shared MAX_INV As Short = 30
    Public Shared MAX_INV_GEMA As Short = 3
    Public Shared MAX_ITEM As Short = 100
    Public Shared MAX_MAP_ITEM As Short = 30
    Public Shared MAX_MSGANIM As Short = 20
    Public Shared MAX_ANIMATION_BUFFER As Short = 50
    Public Shared MAX_ANIMATION As Short = 100
    Public Shared MAX_SKILL As Short = 100
#End Region


#Region "Player/System Config"
    Public Shared Atalhos(13) As Integer

    Public Shared Sub LoadPlayerConfig()
        If Not IO.File.Exists(Application.StartupPath & "/Data/Config/" & Player(MyIndex).Nome & "_config.dat") Then
            DefaulPlayerConfig()
            SavePlayerConfig()
            Return
        End If

        Dim fileStream As IO.FileStream = New IO.FileStream(Application.StartupPath & "/Data/Config/" & Player(MyIndex).Nome & "_config.dat", IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        Dim r As IO.BinaryReader = New IO.BinaryReader(fileStream)
        For i As Short = 1 To 12
            Atalhos(i) = r.ReadInt32
        Next
        r.Close()
        fileStream.Close()
    End Sub

    Public Shared Sub DefaulPlayerConfig()
        For i As Short = 1 To 12
            Atalhos(i) = 49 + (i - 1)
            Select Case i
                Case 10 : Atalhos(i) = Keys.D0 ' 0
                Case 11 : Atalhos(i) = 189 ' -
                Case 12 : Atalhos(i) = 187 ' =
            End Select
        Next
    End Sub

    Public Shared Sub SavePlayerConfig()
        Dim fileStream As IO.FileStream = New IO.FileStream(Application.StartupPath & "/Data/Config/" & Player(MyIndex).Nome & "_config.dat", IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        Dim w As IO.BinaryWriter = New IO.BinaryWriter(fileStream)
        For i As Short = 1 To 12
            w.Write(Atalhos(i))
        Next
        w.Close()
        fileStream.Close()
    End Sub

    Public Shared Sub Save()
        Dim fileStream As IO.FileStream = New IO.FileStream(Application.StartupPath & "/option.dat", IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        Dim w As IO.BinaryWriter = New IO.BinaryWriter(fileStream)
        w.Write(Music)
        w.Write(Sound)
        w.Write(SalvarDados)
        w.Write(SalvarDados_Nome)
        w.Write(SalvarDados_Senha)
        w.Close()
        fileStream.Close()
    End Sub

    Public Shared Sub Load()
        If Not IO.File.Exists(Application.StartupPath & "/option.dat") Then Return
        Dim fileStream As IO.FileStream = New IO.FileStream(Application.StartupPath & "/option.dat", IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        Dim r As IO.BinaryReader = New IO.BinaryReader(fileStream)
        Music = r.ReadBoolean
        Sound = r.ReadBoolean
        SalvarDados = r.ReadBoolean
        SalvarDados_Nome = r.ReadString
        SalvarDados_Senha = r.ReadString
        r.Close()
        fileStream.Close()
    End Sub
#End Region
End Class
