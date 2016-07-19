Module modMap
    Public Map As New MapData
    Public PATH_MAPA As String = Application.StartupPath & "/data/mapas/"

    Public Sub SalvarMapa()
        Dim fileName As String = PATH_MAPA & Player(MyIndex).Map & ".map"
        If Map Is Nothing Then Return
        Dim filestream As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate, IO.FileAccess.ReadWrite)
        Dim w As IO.BinaryWriter = New IO.BinaryWriter(filestream)

        w.Write(Map.Nome)
        w.Write(Map.Revision)
        w.Write(Map.Zona)
        w.Write(Map.Musica)
        w.Write(Map.Top)
        w.Write(Map.Left)
        w.Write(Map.Bottom)
        w.Write(Map.Right)
        w.Write(Map.MaxX)
        w.Write(Map.MaxY)

        For X As Short = 0 To Map.MaxX
            For Y As Short = 0 To Map.MaxY
                w.Write(Map.Tile(X, Y).Type)
                w.Write(Map.Tile(X, Y).Value1)
                For i As Short = 1 To Layers.Count - 1
                    w.Write(Map.Tile(X, Y).Layer(i).TileSet)
                    w.Write(Map.Tile(X, Y).Layer(i).BufferX)
                    w.Write(Map.Tile(X, Y).Layer(i).BufferY)
                Next
            Next
        Next

        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            w.Write(Map.Spawn(i))
        Next

        w.Close()
        filestream.Close()

    End Sub

    Public Sub LoadMapa()
        Dim fileName As String = PATH_MAPA & Player(MyIndex).Map & ".map"
        If Not IO.File.Exists(fileName) Then SalvarMapa()

        Dim filestream As IO.FileStream = New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)
        Dim r As IO.BinaryReader = New IO.BinaryReader(filestream)

        Map.Nome = r.ReadString
        Map.Revision = r.ReadInt32
        Map.Zona = r.ReadByte
        Map.Musica = r.ReadString
        Map.Top = r.ReadInt32
        Map.Left = r.ReadInt32
        Map.Bottom = r.ReadInt32
        Map.Right = r.ReadInt32
        Map.MaxX = r.ReadInt16
        Map.MaxY = r.ReadInt16
        ReDim Map.Tile(Map.MaxX + 1, Map.MaxY + 1)

        For X As Short = 0 To Map.MaxX
            For Y As Short = 0 To Map.MaxY
                Map.Tile(X, Y) = New TileData
                Map.Tile(X, Y).Type = r.ReadByte
                Map.Tile(X, Y).Value1 = r.ReadString

                For i As Short = 1 To Layers.Count - 1
                    Map.Tile(X, Y).Layer(i).TileSet = r.ReadInt16
                    Map.Tile(X, Y).Layer(i).BufferX = r.ReadInt16
                    Map.Tile(X, Y).Layer(i).BufferY = r.ReadInt16
                Next
            Next
        Next

        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            Map.Spawn(i) = r.ReadInt32
        Next

        r.Close()
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

    Public ReadOnly Property Num As Short
        Get
            Return Player(MyIndex).Map
        End Get
    End Property
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

    Public ReadOnly Property Bound As SFML.System.Vector2i
        Get
            Return New SFML.System.Vector2i(BufferX, BufferY)
        End Get
    End Property
End Class