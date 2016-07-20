Module Mapa_Data
    Public Mapa As List(Of MapData) = New List(Of MapData)
    Public PATH_MAPA As String = Application.StartupPath & "/data/mapas/"

    Public Sub LoadMapas()
        For i As Short = 1 To Options.MAX_MAPS
            LoadMapa(i)
        Next
    End Sub

    Public Sub LoadMapa(ByVal Index As Short)
        Dim fileName As String = PATH_MAPA & Index & ".map"
        If Not IO.File.Exists(fileName) Then SaveMapa(Index)

        Dim filestream As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)
        Dim r As IO.BinaryReader = New IO.BinaryReader(filestream)

        Mapa(Index).Nome = r.ReadString
        Mapa(Index).Revision = r.ReadInt32
        Mapa(Index).Zona = r.ReadByte
        Mapa(Index).Musica = r.ReadString
        Mapa(Index).Top = r.ReadInt32
        Mapa(Index).Left = r.ReadInt32
        Mapa(Index).Bottom = r.ReadInt32
        Mapa(Index).Right = r.ReadInt32
        Mapa(Index).MaxX = r.ReadInt16
        Mapa(Index).MaxY = r.ReadInt16
        ReDim Mapa(Index).Tile(Mapa(Index).MaxX + 1, Mapa(Index).MaxY + 1)

        For X As Short = 0 To Mapa(Index).MaxX
            For Y As Short = 0 To Mapa(Index).MaxY
                Mapa(Index).Tile(X, Y) = New TileData
                Mapa(Index).Tile(X, Y).Type = r.ReadByte
                Mapa(Index).Tile(X, Y).Value1 = r.ReadString

                For i As Short = 1 To Layers.Count - 1
                    Mapa(Index).Tile(X, Y).Layer(i).TileSet = r.ReadInt16
                    Mapa(Index).Tile(X, Y).Layer(i).BufferX = r.ReadInt16
                    Mapa(Index).Tile(X, Y).Layer(i).BufferY = r.ReadInt16
                Next
            Next
        Next

        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            Mapa(Index).Spawn(i) = r.ReadInt32
        Next

        r.Close()
        filestream.Close()
        DoEvents()
    End Sub

    Public Sub SaveMapa(ByVal Index As Short)
        Dim fileName As String = PATH_MAPA & Index & ".map"

        Dim filestream As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        Dim w As IO.BinaryWriter = New IO.BinaryWriter(filestream)

        w.Write(Mapa(Index).Nome)
        w.Write(Mapa(Index).Revision)
        w.Write(Mapa(Index).Zona)
        w.Write(Mapa(Index).Musica)
        w.Write(Mapa(Index).Top)
        w.Write(Mapa(Index).Left)
        w.Write(Mapa(Index).Bottom)
        w.Write(Mapa(Index).Right)
        w.Write(Mapa(Index).MaxX)
        w.Write(Mapa(Index).MaxY)

        For X As Short = 0 To Mapa(Index).MaxX
            For Y As Short = 0 To Mapa(Index).MaxY
                w.Write(Mapa(Index).Tile(X, Y).Type)
                w.Write(Mapa(Index).Tile(X, Y).Value1)
                For i As Short = 1 To Layers.Count - 1
                    w.Write(Mapa(Index).Tile(X, Y).Layer(i).TileSet)
                    w.Write(Mapa(Index).Tile(X, Y).Layer(i).BufferX)
                    w.Write(Mapa(Index).Tile(X, Y).Layer(i).BufferY)
                Next
            Next
        Next

        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            w.Write(Mapa(Index).Spawn(i))
        Next

        w.Close()
        filestream.Close()
        DoEvents()
    End Sub

End Module


Public Class MapData
    Public Anim As Boolean
    Public AnimTimer As Integer
    Public Tile(,) As TileData
    Public Spawn() As Integer
    Public MaxX As Short = Options.MAX_X
    Public MaxY As Short = Options.MAX_Y
    Public Top As Integer
    Public Left As Integer
    Public Bottom As Integer
    Public Right As Integer
    Public Revision As Integer


#Region "Properties"
    Public Nome As String = New String("", 20)
    Public Zona As Byte
    Public Musica As String = New String("", 40)
#End Region

    Public Sub New()
        ReDim Spawn(Options.MAX_MAP_NPCS)
        ReDim Tile(MaxX + 1, MaxY + 1)

        For X As Short = 0 To MaxX
            For Y As Short = 0 To MaxY
                Tile(X, Y) = New TileData
            Next
        Next
    End Sub

End Class

Public Class TileData
    Public Layer(Layers.Count) As LayerData
    Public Type As Byte
    Public Value1 As String = ""

    Public Sub New()
        Value1 = ""
        For i As Short = 1 To Layers.Count - 1
            Layer(i) = New LayerData
        Next
    End Sub

    Default Property Base(ByVal LayerNum As Short) As LayerData
        Get
            Return Layer(LayerNum)
        End Get
        Set(ByVal value As LayerData)
            Layer(LayerNum) = value
        End Set
    End Property
End Class

Public Class LayerData
    Public TileSet As Short
    Public BufferX As Short
    Public BufferY As Short

    Public Sub New()
        TileSet = 0
        BufferX = 0
        BufferY = 0
    End Sub

    Public Sub Insert(ByVal tileNum As Short, ByVal X As Short, ByVal Y As Short)
        TileSet = tileNum
        BufferX = X
        BufferY = Y
    End Sub
End Class
