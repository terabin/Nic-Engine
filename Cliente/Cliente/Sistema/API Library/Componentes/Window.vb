Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Public Class Window
    Inherits BaseComponent

    ' Global
    Public Shared Focus As Object

#Region "Dados"
    Private sCaption As String
    Private sVisible As Boolean
    Private sX As Short
    Private sY As Short
    Private sWidth As Short
    Private sHeight As Short
    Private sButtonExit As Boolean
    Private sButtonMinimize As Boolean
    Private sDragged As Boolean
    Private sColorTint As Color = Color.White
#End Region

#Region "System"
    Private Vinculado As Object
    Public _Hover As Boolean
    Private _HoverButtonExit As Boolean
    Private _HoverButtonMinimize As Boolean
    Private [Object]() As Object
#End Region

#Region "Events"
    Public Event MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event MouseDbClick(ByVal e As System.Windows.Forms.MouseEventArgs)
    Public Event OnDraw()
    Public Event Open()
    Public Event Closed()
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

    Public Property Visible As Boolean
        Get
            Return sVisible
        End Get
        Set(ByVal value As Boolean)
            sVisible = value
            If sVisible Then
                SetFocus()
                RaiseEvent Open()
            Else
                Vinculado.SetFocusOtherWindow(Me)
                RaiseEvent Closed()
            End If
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

    Public Property ButtonExit As Boolean
        Get
            Return sButtonExit
        End Get
        Set(ByVal value As Boolean)
            sButtonExit = value
        End Set
    End Property

    Public Property ButtonMinimize As Boolean
        Get
            Return sButtonMinimize
        End Get
        Set(ByVal value As Boolean)
            sButtonMinimize = value
        End Set
    End Property

    Public Property Dragged As Boolean
        Get
            Return sDragged
        End Get
        Set(ByVal value As Boolean)
            sDragged = value
        End Set
    End Property

    Public ReadOnly Property Hover As Boolean
        Get
            Return _Hover
        End Get
    End Property

    Public Property ColorTint As Color
        Get
            Return sColorTint
        End Get
        Set(ByVal value As Color)
            sColorTint = value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Sub New(ByVal otherObject As Object)
        otherObject.AddObject(Me)
        Vinculado = otherObject

        ' Default
        ReDim [Object](0)
    End Sub

    Public Overrides Function OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        _Hover = False
        _HoverButtonExit = False
        If [Object].Count > 1 Then
            For i As Integer = 1 To [Object].Count - 1
                [Object](i)._Hover = False
            Next
        End If
        If Not Visible Then Return False
        If e.X >= X And e.X <= X + Width Then
            If e.Y >= Y And e.Y <= Y + Height Then
                _Hover = True

                Dim offSet As Short
                If ButtonExit Then
                    If e.X >= X + Width - 18 And e.X <= X + Width - 2 Then
                        If e.Y >= Y + 2 And e.Y <= Y + 18 Then
                            _HoverButtonExit = True
                            frmMain.Cursor = Cursors.Hand
                        End If
                    End If
                    offSet = 1
                End If

                RaiseEvent MouseMove(e)
                If [Object].Count > 1 Then
                    For i As Integer = 1 To [Object].Count - 1
                        If [Object](i).OnMouseMove(e) Then Return True
                    Next
                End If

                Return True
            End If
        End If
        MyBase.OnMouseMove(e)
        Return False
    End Function

    Public Overrides Function OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        If Not Visible Then Return False
        If Hover Then
            SetFocus()
            If Dragged Then
                If e.Y >= Y And e.Y <= Y + 18 Then
                    Vinculado.DragObject = Me
                    Vinculado.DragObject_X = e.X - X
                    Vinculado.DragObject_Y = e.Y - Y
                    Return True
                End If
            End If

            RaiseEvent MouseDown(e)
            If [Object].Count > 1 Then
                For i As Integer = 1 To [Object].Count - 1
                    If [Object](i).OnMouseDown(e) Then Return True
                Next
            End If
            Return True
        End If
        MyBase.OnMouseDown(e)
        Return False
    End Function

    Public Overrides Function OnMouseDbClick(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        If Not Visible Then Return False
        If Hover Then
            RaiseEvent MouseDbClick(e)
            If [Object].Count > 1 Then
                For i As Integer = 1 To [Object].Count - 1
                    If [Object](i).OnMouseDbClick(e) Then Return True
                Next
            End If
            Return True
        End If
        MyBase.OnMouseDbClick(e)
        Return False
    End Function

    Public Overrides Function OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        If Not Visible Then Return False
        If ButtonExit Then
            If _HoverButtonExit Then
                Hide()
            End If
        End If

        If Hover Then

            RaiseEvent MouseUp(e)
            If [Object].Count > 1 Then
                For i As Integer = 1 To [Object].Count - 1
                    If [Object](i).OnMouseUp(e) Then Return True
                Next
            End If
            Return True
        End If
        MyBase.OnMouseUp(e)
        Return False
    End Function

    Public Overrides Sub Draw()

        If Not Visible Then Return
        ' Bar
        RenderBox(New Color(40, 40, 40, 240), New IntRect(X, Y, Width, 18))

        Dim vertex As VertexArray
        vertex = New VertexArray
        vertex.Append(New Vertex(New Vector2f(X, Y), Color.Transparent))
        vertex.Append(New Vertex(New Vector2f(X + (Width / 2), Y), New Color(255, 255, 255, 100)))
        vertex.Append(New Vertex(New Vector2f(X, Y + 18), Color.Transparent))
        vertex.Append(New Vertex(New Vector2f(X + (Width / 2), Y + 18), New Color(255, 255, 255, 100)))
        vertex.PrimitiveType = PrimitiveType.TrianglesStrip
        DeviceGame.Draw(vertex)

        vertex = New VertexArray
        vertex.Append(New Vertex(New Vector2f(X + (Width / 2), Y), New Color(255, 255, 255, 100)))
        vertex.Append(New Vertex(New Vector2f(X + Width, Y), Color.Transparent))
        vertex.Append(New Vertex(New Vector2f(X + (Width / 2), Y + 18), New Color(255, 255, 255, 100)))
        vertex.Append(New Vertex(New Vector2f(X + Width, Y + 18), Color.Transparent))
        vertex.PrimitiveType = PrimitiveType.TrianglesStrip
        DeviceGame.Draw(vertex)

        If Caption.Length > 0 Then RenderText(Caption, X + 4, Y + 1, New Color(240, 240, 240), , False)

        Dim offSet As Short = 0
        If ButtonExit Then
            If Not _HoverButtonExit Then
                RenderBox(New Color(204, 0, 0), New IntRect(X + Width - 2 - 16, Y + 2, 14, 14))
                RenderText("x", X + Width - 15, Y + 1, New Color(200, 200, 200), , True)
            Else
                RenderBox(New Color(204, 80, 80), New IntRect(X + Width - 2 - 16, Y + 2, 14, 14))
                RenderText("x", X + Width - 15, Y + 1, New Color(240, 240, 240), , True)
            End If
            offSet = 1
        End If

        ' Background
        RenderBox(New Color(40, 40, 40, 240), New IntRect(X, Y + 16, Width, Height - 18))
        RenderTexture(texTex, New IntRect(X + 4, Y + 20, Width - 8, Height - 25), New IntRect(0, 0, GetTextureSize(texTex).X, GetTextureSize(texTex).Y), ColorTint)
        'RenderBox(New Color(255, 153, 51, 255), New IntRect(X + 4, Y + 20, Width - 8, Height - 23))

        'RenderBox(New Color(255, 255, 204, 255), New IntRect(X + 4, Y + 20, Width - 8, Height - 23))

        RaiseEvent OnDraw()

        If [Object].Count > 1 Then
            For i As Integer = 1 To [Object].Count - 1
                [Object](i).Draw()
            Next
        End If

        MyBase.Draw()
    End Sub

    Public Sub SetFocus()
        Vinculado.SetFocusWindow(Me)
    End Sub

    Public Sub Show()
        Focus = Me
        Visible = True
    End Sub

    Public Sub Hide()
        Focus = Nothing
        Visible = False
    End Sub

    Public Sub Toggle()
        Visible = Not Visible
    End Sub

    Public Sub AddObject(ByVal Obj As Object)
        ReDim Preserve [Object]([Object].Count)
        Dim idCount As Integer = [Object].Count - 1
        [Object](idCount) = Obj
    End Sub
#End Region

End Class
