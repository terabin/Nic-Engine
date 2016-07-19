Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Public Class TextBox
    Inherits BaseComponent

    ' Global
    Public Shared Animation As Boolean
    Public Shared AnimationTimer As Long
    Public Shared Focus As Object


#Region "Dados"
    Private sText As String
    Private sX As Short
    Private sY As Short
    Private sWidth As Short
    Private sHeight As Short
    Private sPass As Boolean
    Private sMaxLen As Integer
    Private sColorBG As Color
    Private sColorText As Color
#End Region

#Region "System"
    Public _Hover As Boolean
    Public TabNext As Object
    Private vinculo As Object
#End Region

#Region "Events"
    Public Event TextChanged()
    Public Event OnEnter()
#End Region

#Region "Property"
    Public Property Text As String
        Get
            Return sText
        End Get
        Set(ByVal value As String)
            sText = value
            RaiseEvent TextChanged()
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

    Public Property Password As Boolean
        Get
            Return sPass
        End Get
        Set(ByVal value As Boolean)
            sPass = value
        End Set
    End Property

    Public Property MaxLenght As Integer
        Get
            Return sMaxLen
        End Get
        Set(ByVal value As Integer)
            sMaxLen = value
        End Set
    End Property

    Public ReadOnly Property Hover As Boolean
        Get
            Return _Hover
        End Get
    End Property

    Public Property ColorBG As Color
        Get
            Return sColorBG
        End Get
        Set(ByVal value As Color)
            sColorBG = value
        End Set
    End Property

    Public Property ColorText As Color
        Get
            Return sColorText
        End Get
        Set(ByVal value As Color)
            sColorText = value
        End Set
    End Property
#End Region

#Region "Method"
    Public Sub New(ByVal otherObject As Object)
        otherObject.AddObject(Me)
        vinculo = otherObject
        TabNext = Nothing
        sText = ""
        sColorBG = New Color(60, 60, 60)
        sColorText = New Color(230, 230, 230)
    End Sub

    Public Overrides Sub Draw()
        Dim s As String, Text2 As String
        Dim i As Long
        ' Background
        RenderBox(sColorBG, New IntRect(vinculo.X + X, vinculo.Y + Y, Width, Height))

        Text2 = Text
        If Password Then
            Text2 = ""
            For i = 1 To Len(Text)
                Text2 = Text2 & "*"
            Next
        End If

        Dim w As Long = 0
        s = Text2
        Dim xx As Integer = 0, pp As String = ""
        If GetTextWidth(Text2, 12) > Width - 36 Then
            For i = Text2.Length - 1 To 1 Step -1
                xx += GetTextWidth(Text2.Substring(i, 1), 12)
                If xx > Width - 36 Then
                    pp = Right(Text2, Text2.Length - i)
                    Exit For
                End If

            Next
        End If
        If Not pp = String.Empty Then s = pp

        If Focus Is Me Then
            If Animation Then s = s & "|"
        End If
        If Len(s) > 0 Then RenderText(s, vinculo.X + X + 4, vinculo.Y + Y + 2, sColorText, 12)
        MyBase.Draw()
    End Sub

    Public Overrides Function OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        _Hover = False
        If e.X >= vinculo.X + X And e.X <= vinculo.X + X + Width Then
            If e.Y >= vinculo.Y + Y And e.Y <= vinculo.Y + Y + Height Then
                _Hover = True
                Return True
            End If
        End If
        MyBase.OnMouseMove(e)
        Return False
    End Function

    Public Overrides Function OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        If Hover Then
            SetFocus()
            Return True
        End If
        MyBase.OnMouseUp(e)
        Return False
    End Function

    Public Sub Tab()
        If Not TabNext Is Nothing Then TabNext.SetFocus()
    End Sub

    Public Sub Enter()
        RaiseEvent OnEnter()
    End Sub

    Public Sub SetFocus()
        Focus = Me
    End Sub
#End Region

End Class
