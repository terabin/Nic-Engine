Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Class Tela_General
    Public Shared Tela As Telas

#Region "Telas"
    Public Shared Login As Tela_Login
    Public Shared Registro As Tela_Registro
    Public Shared SelectChar As Tela_SelectChar
    Public Shared CriarChar As Tela_CriarChar
    Public Shared InGame As Tela_InGame
#End Region

    Public Shared [Object]() As Object

    Private Shared tmrAlpha As Long
    Private Shared Alpha As Byte
    Private Shared initAlpha As Boolean

    ' Message Alert
    Public Shared MessageString As String = ""

    Public Shared Sub Init()
        ' Default
        ReDim [Object](0)

        ' Carregar Telas
        Login = New Tela_Login
        Registro = New Tela_Registro
        SelectChar = New Tela_SelectChar
        CriarChar = New Tela_CriarChar
        InGame = New Tela_InGame

        ' Timer
        tmrAlpha = GetTickCount
        Alpha = 0
        initAlpha = False
        TextBox.Focus = Nothing
    End Sub

    Public Shared Sub Core()
        If Tela = Telas.None Then
            If GetTickCount > tmrAlpha Then
                If Not initAlpha Then
                    Alpha += 1
                    If Alpha = 255 Then initAlpha = True
                Else
                    If Alpha > 0 Then
                        Alpha -= 1
                    Else
                        Open(Telas.Login)
                    End If
                End If
                tmrAlpha = GetTickCount + 2
            End If
        Else
            [Object](Tela).Core()
        End If
    End Sub

    Public Shared Sub Draw()
        'DeviceGame.DispatchEvents()
        DeviceGame.Clear(Color.Black)

        If Tela = Telas.None Then
            RenderTexture(texGUI(3), New IntRect((DeviceGame.Size.X - 500) / 2, (DeviceGame.Size.Y - 500) / 2, 500, 500), New IntRect(New Vector2f(), GetTextureSize(texGUI(3))), New Color(255, 255, 255, Alpha))
        Else
            [Object](Tela).DrawAll()
        End If

        If MessageString.Length > 0 Then DrawMessage()

        RenderText(GameFPS, 40, 40, Color.White)

        DeviceGame.Display()
    End Sub

    Public Shared Sub DrawMessage()
        Dim vertex As New VertexArray(PrimitiveType.TrianglesStrip)
        vertex.Append(New Vertex(New Vector2f(), New Color(0, 0, 0, 150)))
        vertex.Append(New Vertex(New Vector2f(DeviceGame.Size.X, 0), New Color(0, 0, 0, 150)))
        vertex.Append(New Vertex(New Vector2f(0, DeviceGame.Size.Y), New Color(0, 0, 0, 150)))
        vertex.Append(New Vertex(New Vector2f(DeviceGame.Size.X, DeviceGame.Size.Y), New Color(0, 0, 0, 150)))
        DeviceGame.Draw(vertex)

        RenderText(MessageString, (DeviceGame.Size.X - GetTextWidth(MessageString, 13)) / 2, DeviceGame.Size.Y / 2 - 10, Color.White, 14)
    End Sub

    Public Shared Sub Open(ByVal telaNum As Telas)
        ' Fechar a tela anterior
        TextBox.Focus = Nothing
        If Tela > 0 Then [Object](Tela).Close()

        ' Abrir nova tela
        Tela = telaNum
        [Object](Tela).Open()
    End Sub

    Public Shared Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If MessageString.Length > 0 Then Return
        If Tela = Telas.None Then
        Else
            [Object](Tela).GeneralMouseDown(e)
        End If
    End Sub

    Public Shared Sub MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If MessageString.Length > 0 Then Return
        If Tela = Telas.None Then
        Else
            [Object](Tela).GeneralMouseMove(e)
        End If
    End Sub

    Public Shared Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If MessageString.Length > 0 Then MessageString = "" : Return
        If Tela = Telas.None Then
            Open(Telas.Login)
        Else
            [Object](Tela).GeneralMouseUp(e)
        End If
    End Sub

    Public Shared Sub MouseDbClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        If Tela = Telas.None Then
        Else
            [Object](Tela).GeneralMouseDbClick(e)
        End If
    End Sub

    Public Shared Sub KeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If Tela <> Telas.None Then
            [Object](Tela).KeyPress(e)
        End If
    End Sub

    Public Shared Sub KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs)
        If Tela <> Telas.None Then
            [Object](Tela).KeyUp(e)
        End If
    End Sub

    Public Shared Sub KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If Tela <> Telas.None Then
            [Object](Tela).KeyDown(e)
        End If
    End Sub
End Class

Public MustInherit Class clsTelas
    ' Methods
    Public ReadOnly Property X As Short
        Get
            Return 0
        End Get
    End Property
    Public ReadOnly Property Y As Short
        Get
            Return 0
        End Get
    End Property
    Public Overridable Sub Open()

    End Sub
    Public Overridable Sub Draw()

    End Sub
    Public Overridable Sub Core()

    End Sub
    Public Overridable Sub MouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub
    Public Overridable Sub MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub
    Public Overridable Sub MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub
    Public Overridable Sub MouseDbClick(ByVal e As System.Windows.Forms.MouseEventArgs)

    End Sub
    Public Overridable Function KeyPress(ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        If Not TextBox.Focus Is Nothing Then
            If (AscW(e.KeyChar) <> 28) And (AscW(e.KeyChar) <> Keys.Back) Then
                If (AscW(e.KeyChar) >= 32 And AscW(e.KeyChar) <= 126) Or AscW(e.KeyChar) = 231 Or AscW(e.KeyChar) = 199 Or (AscW(e.KeyChar) >= 225 And AscW(e.KeyChar) <= 250) Then
                    If TextBox.Focus.Text.Length < TextBox.Focus.MaxLenght Then
                        TextBox.Focus.Text += Chr(AscW(e.KeyChar))
                    End If
                End If
                Return True
            End If

            If (AscW(e.KeyChar) = Keys.Back) Then
                If TextBox.Focus.Text.Length > 0 Then
                    TextBox.Focus.Text = Mid(TextBox.Focus.Text, 1, TextBox.Focus.Text.Length - 1)
                End If
                Return True
            End If
        End If
        Return False
    End Function
    Public Overridable Function KeyUp(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        If Not TextBox.Focus Is Nothing Then
            If e.KeyCode = Keys.Tab Then
                TextBox.Focus.Tab()
                Return True
            End If
            If e.KeyCode = Keys.Enter Then
                TextBox.Focus.Enter()
                Return True
            End If
            Return True
        End If
        Return False
    End Function
    Public Overridable Function KeyDown(ByVal e As System.Windows.Forms.KeyEventArgs) As Boolean
        If Not TextBox.Focus Is Nothing Then Return True
        Return False
    End Function
    Public Overridable Sub DrawFront()

    End Sub

    ' Outros
    Public [Object]() As Object
    Public listFocus() As Object
    Public DragObject As Object
    Public DragObject_X As Short
    Public DragObject_Y As Short

    Public Sub New()
        ReDim Preserve Tela_General.Object(Tela_General.Object.Count)
        Dim idCount As Integer = Tela_General.Object.Count - 1
        Tela_General.Object(idCount) = Me

        ' Default
        ReDim [Object](0)
        ReDim listFocus(0)
    End Sub

    Public Sub AddObject(ByVal Obj As Object)
        ReDim Preserve [Object]([Object].Count)
        Dim idCount As Integer = [Object].Count - 1
        [Object](idCount) = Obj

        If TypeOf Obj Is Window Then
            ReDim Preserve listFocus(listFocus.Count)
            idCount = listFocus.Count - 1
            listFocus(idCount) = Obj
        End If
    End Sub

    Public Sub DrawAll()
        Draw()

        If TypeOf Me Is Tela_InGame Then
            If frmEditor_Map.Visible Then Return
        End If
        If [Object].Count > 1 Then
            For i As Integer = 1 To [Object].Count - 1
                If Not TypeOf [Object](i) Is Window Then [Object](i).Draw()
            Next
        End If

        If listFocus.Count > 1 Then
            For i As Integer = listFocus.Count - 1 To 1 Step -1
                listFocus(i).Draw()
            Next
        End If

        DrawFront()
    End Sub

    Public Sub GeneralMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If TypeOf Me Is Tela_InGame Then
            If frmEditor_Map.Visible Then GoTo lineMouse
        End If
        If [Object].Count > 1 Then
            If Not DragObject Is Nothing Then
                If e.X <> DragObject.X + DragObject_X Or e.Y <> DragObject.Y + DragObject_Y Then
                    Return
                End If
            End If

            For i As Integer = [Object].Count - 1 To 1 Step -1
                If Not TypeOf [Object](i) Is Window Then
                    If [Object](i).OnMouseDown(e) Then Return
                End If
            Next
        End If

        If listFocus.Count > 1 Then
            For i As Integer = 1 To listFocus.Count - 1
                If listFocus(i).OnMouseDown(e) Then Return
            Next
        End If

lineMouse:
        MouseDown(e)
    End Sub

    Public Sub GeneralMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        frmMain.Cursor = Cursors.Default

        If TypeOf Me Is Tela_InGame Then
            If frmEditor_Map.Visible Then GoTo lineMouse
        End If

        If [Object].Count > 1 Then
            If Not DragObject Is Nothing Then
                DragObject.X = e.X - DragObject_X
                DragObject.Y = e.Y - DragObject_Y

                If DragObject.X < 0 Then DragObject.X = 0
                If DragObject.Y < 0 Then DragObject.Y = 0
                If DragObject.X + DragObject.Width > DeviceGame.Size.X Then DragObject.X = DeviceGame.Size.X - DragObject.Width
                If DragObject.Y + DragObject.Height > DeviceGame.Size.Y Then DragObject.Y = DeviceGame.Size.Y - DragObject.Height
                Return
            End If
            For i As Integer = [Object].Count - 1 To 1 Step -1
                [Object](i)._Hover = False
            Next


            For i As Integer = [Object].Count - 1 To 1 Step -1
                If Not TypeOf [Object](i) Is Window Then
                    If [Object](i).OnMouseMove(e) Then Return
                End If
            Next
        End If

        If listFocus.Count > 1 Then
            For i As Integer = 1 To listFocus.Count - 1
                If listFocus(i).OnMouseMove(e) Then Return
            Next
        End If

lineMouse:
        MouseMove(e)
    End Sub

    Public Sub GeneralMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If TypeOf Me Is Tela_InGame Then
            If frmEditor_Map.Visible Then GoTo lineMouse
        End If

        If [Object].Count > 1 Then
            If Not DragObject Is Nothing Then
                DragObject = Nothing
                DragObject_X = 0
                DragObject_Y = 0
            End If

            For i As Integer = [Object].Count - 1 To 1 Step -1
                If Not TypeOf [Object](i) Is Window Then
                    If [Object](i).OnMouseUp(e) Then GoTo resetValue
                End If
            Next
        End If

        If listFocus.Count > 1 Then
            For i As Integer = 1 To listFocus.Count - 1
                If listFocus(i).OnMouseUp(e) Then GoTo resetValue
            Next
        End If

lineMouse:
        MouseUp(e)

resetValue:
        Drag.Clear()
    End Sub

    Public Sub GeneralMouseDbClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        If TypeOf Me Is Tela_InGame Then
            If frmEditor_Map.Visible Then GoTo lineMouse
        End If
        If [Object].Count > 1 Then
            For i As Integer = [Object].Count - 1 To 1 Step -1
                If Not TypeOf [Object](i) Is Window Then
                    If [Object](i).OnMouseDbClick(e) Then Return
                End If
            Next
        End If

        If listFocus.Count > 1 Then
            For i As Integer = 1 To listFocus.Count - 1
                If listFocus(i).OnMouseUp(e) Then Return
            Next
        End If

lineMouse:
        MouseDbClick(e)
    End Sub

    Public Sub SetFocusWindow(ByVal obj As Object)
        Dim id As Integer

        id = 0
        For i As Integer = 1 To listFocus.Count - 1
            If listFocus(i) Is obj Then
                id = i
                Exit For
            End If
        Next

        ' Reorganized
        If id > 1 Then
            For i As Integer = id To 2 Step -1
                listFocus(i) = listFocus(i - 1)
            Next

            listFocus(1) = obj
        End If
    End Sub

    Public Sub SetFocusOtherWindow(ByVal obj As Object)
        Dim id As Integer

        id = 0
        For i As Integer = 1 To listFocus.Count - 1
            If listFocus(i) Is obj Then
                id = i
                Exit For
            End If
        Next

        ' Reorganized
        If id < listFocus.Count - 1 Then
            For i As Integer = id To listFocus.Count - 2
                listFocus(i) = listFocus(i + 1)
            Next
            listFocus(listFocus.Count - 1) = obj
        End If
    End Sub

    Public Sub Close()
        ReDim [Object](0)
        ReDim listFocus(0)
    End Sub
End Class