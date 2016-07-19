Imports System.IO
Imports System.IO.Compression
Module General
    Public Sortear As New System.Random

    Public Sub Main()

        Randomize()

        ' Iniciando Conexão
        InitNetwork()

        ' Redimensionando Array
        ReDim TempPlayer(Options.MAX_PLAYERS + 1)
        ReDim Player(Options.MAX_PLAYERS + 1)
        ReDim Conta(Options.MAX_PLAYERS + 1)
        ReDim Clients(Options.MAX_PLAYERS + 1)
        ReDim MapNpc(Options.MAX_MAPS + 1)
        ReDim Drop(Options.MAX_MAPS + 1)
        ReDim Npc(Options.MAX_NPC + 1)
        ReDim Item(Options.MAX_ITEM + 1)
        ReDim Animation(Options.MAX_ANIMATION + 1)
        Mapa.Clear()
        Mapa.Add(Nothing)

        ' Preparando Mapas
        For i As Short = 1 To Options.MAX_MAPS
            Mapa.Add(New MapData())
            MapNpc(i) = New SpawnData
            Drop(i) = New DropData
        Next

        ' Preparar NPC
        For i As Short = 1 To Options.MAX_NPC
            Npc(i) = New NpcData
        Next

        ' Preparar Item
        For i As Short = 1 To Options.MAX_ITEM
            Item(i) = New ItemData
        Next

        ' Preparar Animação
        For i As Short = 1 To Options.MAX_ANIMATION
            Animation(i) = New AnimData
        Next

        ' Carregar Banco de Dados
        LoadDatabase()

        ' Spawn
        CacheSpawnAllMap()

        ' Preparando Socket
        'frmServer.lstPlayers.Items.Clear()
        For i As Short = 1 To Options.MAX_PLAYERS
            TempPlayer(i) = New clsTempPlayer
            Player(i) = New clsPlayer
            Conta(i) = New clsConta
            Clients(i) = New Client
            frmServer.lstView.Items.Add(i)
            frmServer.lstView.Items(i - 1).SubItems.Add("")
            frmServer.lstView.Items(i - 1).SubItems.Add("")
            frmServer.lstView.Items(i - 1).SubItems.Add("")
        Next

        ' Iniciando Servidor
        frmServer.Text = "Servidor: " & Options.GAME_NAME
        frmServer.Show()
        ServerRun = True
        ServerLoop()
    End Sub

    Public Sub LoadDatabase()
        LoadClasses()
        LoadAnimations()
        LoadNpcs()
        LoadItems()
        LoadMapas()
    End Sub

    Public ReadOnly Property GetTickCount As Integer
        Get
            Return Environment.TickCount
        End Get
    End Property

    Public Sub DoEvents()
        Application.DoEvents()
    End Sub

    Public Sub Sleep(ByVal miliseconds As Long)
        Threading.Thread.Sleep(miliseconds)
    End Sub

    ''' <summary>
    ''' Calcula o Módulo de um Vetor (X,Y)
    ''' </summary>
    Public Function Modulo(ByVal X As Integer, ByVal Y As Integer) As Integer
        Return Math.Sqrt((X ^ 2) + (Y ^ 2))
    End Function

    Public Function Compress(ByVal b() As Byte) As Byte()
        Dim ms As New System.IO.MemoryStream()
        Dim gzipstream As New Compression.GZipStream(ms, CompressionMode.Compress)
        gzipstream.Write(b, 0, b.Length)
        gzipstream.Flush()
        gzipstream.Close()
        Dim ret() As Byte = ms.ToArray()
        gzipstream.Close()
        gzipstream.Dispose()
        ms.Close()
        ms.Dispose()
        Return ret
    End Function

    Public Function Decompress(ByVal bytes() As Byte) As Byte()
        Dim ms As New MemoryStream(bytes)
        Dim gz As New GZipStream(ms, CompressionMode.Decompress)
        Dim bt(3) As Byte
        ms.Position = ms.Length - 4
        ms.Read(bt, 0, 4)
        ms.Position = 0
        Dim size As Integer = BitConverter.ToInt32(bt, 0)
        Dim buffer(size + 100) As Byte
        Dim offset As Integer = 0
        Dim total As Integer = 0
        While (True)
            Dim j As Integer = gz.Read(buffer, offset, 100)
            If j = 0 Then Exit While
            offset += j
            total += j
        End While
        gz.Close()
        gz.Dispose()
        ms.Close()
        ms.Dispose()
        Dim ra(total - 1) As Byte
        Array.ConstrainedCopy(buffer, 0, ra, 0, total)
        Return ra
    End Function

    Public Function Rand(ByVal Min As Integer, ByVal Max As Integer) As Integer
        Randomize()
        Return Int((Max - Min + 1) * Rnd()) + Min
    End Function
End Module
