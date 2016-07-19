Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Public Class CheckBox
    Inherits BaseComponent

#Region "Dados"
    Private sCaption As String
    Private sCheck As Boolean
    Public _Hover As Boolean
    Private Vinculo As Object
    Private sX As Short
    Private sY As Short
#End Region

#Region "Event"
    Public Event ChangeValue()
#End Region

#Region "Property"
    Public Property Caption As String
        Get
            Return sCaption
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

    Public Property Checked As Boolean
        Get
            Return sCheck
        End Get
        Set(ByVal value As Boolean)
            sCheck = value
            RaiseEvent ChangeValue()
        End Set
    End Property

    Public ReadOnly Property Hover As Boolean
        Get
            Return _Hover
        End Get
    End Property

    Public ReadOnly Property Width As Short
        Get
            Return 12 + GetTextWidth("__" & Caption)
        End Get
    End Property
#End Region

#Region "Method"
    Public Sub New(ByVal otherObject As Object)
        otherObject.AddObject(Me)
        Vinculo = otherObject
    End Sub

    Public Overrides Function OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        _Hover = False
        If e.X >= Vinculo.X + X And e.X <= Vinculo.X + X + Width Then
            If e.Y >= Vinculo.Y + Y And e.Y <= Vinculo.Y + Y + 14 Then
                _Hover = True
                frmMain.Cursor = Cursors.Hand
                Return True
            End If
        End If
        MyBase.OnMouseMove(e)
        Return False
    End Function

    Public Overrides Function OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        If Hover Then
            Checked = Not Checked
            Return True
        End If
        Return MyBase.OnMouseUp(e)
        Return False
    End Function

    Public Overrides Sub Draw()
        If Checked Then
            RenderTexture(texCheckBox, New IntRect(Vinculo.X + X, Vinculo.Y + Y, 12, 12), New IntRect(25, 6, 12, 12))
        Else
            RenderTexture(texCheckBox, New IntRect(Vinculo.X + X, Vinculo.Y + Y, 12, 12), New IntRect(50, 6, 12, 12))
        End If
        If Caption.Length > 0 Then RenderText(Caption, Vinculo.X + X + 16, Vinculo.Y + Y, Color.White, , , True)
        MyBase.Draw()
    End Sub
#End Region
End Class
