Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Public Class Button
    Inherits BaseComponent

#Region "Dados"
    Private sCaption As String
    Private sX As Short
    Private sY As Short
    Private sWidth As Short
    Private sHeight As Short
    Private sColour As Color
#End Region

#Region "System"
    Public _Hover As Boolean
    Private _DebugHover As Boolean
    Private _Down As Boolean
    Private Vinculo As Object
#End Region

#Region "Events"
    Public Event MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event OnDraw(ByVal X As Short, ByVal Y As Short)
#End Region

#Region "Property"
    Public Property Caption As String
        Get
            Caption = sCaption
        End Get
        Set(ByVal value As String)
            sCaption = value
        End Set
    End Property

    Public Property X As Short
        Get
            Return sX
        End Get
        Set(ByVal value As Short)
            sX = value
        End Set
    End Property

    Public Property Y As Short
        Get
            Return sY
        End Get
        Set(ByVal value As Short)
            sY = value
        End Set
    End Property

    Public Property Width As Short
        Get
            Return sWidth
        End Get
        Set(ByVal value As Short)
            sWidth = value
        End Set
    End Property

    Public Property Height As Short
        Get
            Return sHeight
        End Get
        Set(ByVal value As Short)
            sHeight = value
        End Set
    End Property

    Public Property Colour As Color
        Get
            Return sColour
        End Get
        Set(ByVal value As Color)
            sColour = value
        End Set
    End Property

    Public ReadOnly Property Hover As Boolean
        Get
            Return _Hover
        End Get
    End Property
#End Region

#Region "Method"
    Public Sub New(ByVal otherObject As Object)
        otherObject.AddObject(Me)
        Vinculo = otherObject
        sCaption = ""
        Colour = New Color(70, 70, 70)
    End Sub

    Public Overrides Function OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        If e.X >= Vinculo.X + X And e.X <= Vinculo.X + X + Width Then
            If e.Y >= Vinculo.Y + Y And e.Y <= Vinculo.Y + Y + Height Then
                If Not _DebugHover Then Som.Play("cursor1.wav")
                _DebugHover = True
                _Hover = True
                frmMain.Cursor = Cursors.Hand
                RaiseEvent MouseMove(e)
                Return True
            Else
                _DebugHover = False
            End If
        Else
            _DebugHover = False
        End If
        MyBase.OnMouseMove(e)
        Return False
    End Function

    Public Overrides Function OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        If Hover Then
            _Down = True
            RaiseEvent MouseDown(e)
            Return True
        End If
        MyBase.OnMouseDown(e)
        Return False
    End Function

    Public Overrides Function OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        _Down = False
        If Hover Then
            Som.Play("Cursor2.wav")
            RaiseEvent MouseUp(e)
            Return True
        End If
        MyBase.OnMouseUp(e)
        Return False
    End Function

    Public Overrides Sub Draw()
        ' Normal
        RenderBox(New Color(Colour.R, Colour.G, Colour.B, Colour.A), New IntRect(Vinculo.X + X, Vinculo.Y + Y, Width, Height))

        ' Event
        RaiseEvent OnDraw(Vinculo.X + X, Vinculo.Y + Y)

        Dim vertex As VertexArray, Colour2 As Color
        vertex = New VertexArray
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X, Vinculo.Y + Y), Color.Transparent))
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + (Width / 2), Vinculo.Y + Y), New Color(255, 255, 255, 70)))
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X, Vinculo.Y + Y + Height), Color.Transparent))
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + (Width / 2), Vinculo.Y + Y + Height), New Color(255, 255, 255, 70)))
        vertex.PrimitiveType = PrimitiveType.TrianglesStrip
        DeviceGame.Draw(vertex)

        vertex = New VertexArray
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + (Width / 2), Vinculo.Y + Y), New Color(255, 255, 255, 70)))
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + Width, Vinculo.Y + Y), Color.Transparent))
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + (Width / 2), Vinculo.Y + Y + Height), New Color(255, 255, 255, 70)))
        vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + Width, Vinculo.Y + Y + Height), Color.Transparent))
        vertex.PrimitiveType = PrimitiveType.TrianglesStrip
        DeviceGame.Draw(vertex)


        If Hover Then
            vertex = New VertexArray
            Colour2 = New Color(255, 255, 255, 50)
            If _Down Then Colour2 = New Color(0, 0, 0, 50)
            vertex.Append(New Vertex(New Vector2f(Vinculo.X + X, Vinculo.Y + Y), Colour2))
            vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + Width, Vinculo.Y + Y), Colour2))
            vertex.Append(New Vertex(New Vector2f(Vinculo.X + X, Vinculo.Y + Y + Height), Colour2))
            vertex.Append(New Vertex(New Vector2f(Vinculo.X + X + Width, Vinculo.Y + Y + Height), Colour2))
            vertex.PrimitiveType = PrimitiveType.TrianglesStrip
            DeviceGame.Draw(vertex)
        End If
        ' Caption
        If Caption.Length > 0 Then RenderText(Caption, Vinculo.X + X + ((Width - GetTextWidth(Caption)) / 2), Vinculo.Y + Y + ((Height - 16) / 2), New Color(220, 220, 220), , , True)

        MyBase.Draw()
    End Sub
#End Region
End Class
