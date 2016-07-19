Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Public Class Tela_SelectChar
    Inherits clsTelas

#Region "Componentes"
    Public WithEvents SelectChar As Window
    Public WithEvents cmbUseorCreate As Button
    Public WithEvents cmbDelete As Button
    Public WithEvents cmbBack As Button
#End Region

    Public SelectSlot As Short = 0

    Public Overrides Sub Open()
        SelectChar = New Window(Me)
        SelectChar.Width = 340
        SelectChar.Height = 150
        SelectChar.X = (DeviceGame.Size.X - SelectChar.Width) / 2
        SelectChar.Y = (DeviceGame.Size.Y - SelectChar.Height) / 2
        SelectChar.Caption = "Selecione o Personagem"
        SelectChar.Show()

        cmbUseorCreate = New Button(SelectChar)
        cmbUseorCreate.Width = 100
        cmbUseorCreate.Height = 16
        cmbUseorCreate.X = (SelectChar.Width - cmbUseorCreate.Width) / 2
        cmbUseorCreate.Y = SelectChar.Height - 8 - 18 * 3
        cmbUseorCreate.Colour = New Color(255, 128, 0)
        cmbUseorCreate.Caption = "Criar"

        cmbDelete = New Button(SelectChar)
        cmbDelete.Width = 100
        cmbDelete.Height = 16
        cmbDelete.X = (SelectChar.Width - cmbDelete.Width) / 2
        cmbDelete.Y = SelectChar.Height - 8 - 18 * 2
        cmbDelete.Caption = "Deletar"

        cmbBack = New Button(SelectChar)
        cmbBack.Width = 100
        cmbBack.Height = 16
        cmbBack.X = (SelectChar.Width - cmbBack.Width) / 2
        cmbBack.Y = SelectChar.Height - 8 - 18
        cmbBack.Caption = "Voltar"

        SelectSlot = 1
        MyBase.Open()
    End Sub

    Public Overrides Sub Draw()
        ' Background
        RenderTexture(texGUI(4), New IntRect(New Vector2f(), DeviceGame.Size), New IntRect(New Vector2f(), GetTextureSize(texGUI(4))))
        MyBase.Draw()
    End Sub

    Private Sub SelectChar_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles SelectChar.MouseUp
        For i As Short = 1 To 5
            If e.X >= SelectChar.X + 6 + (66 * (i - 1)) And e.X <= SelectChar.X + 6 + (66 * (i - 1)) + 64 Then
                If e.Y >= SelectChar.Y + 22 And e.Y <= SelectChar.Y + 22 + 64 Then
                    SelectSlot = i
                End If
            End If
        Next
    End Sub

    Private Sub SelectChar_OnDraw() Handles SelectChar.OnDraw
        Dim text As String = "", w As Short = 0
        For i As Short = 1 To 5
            If Not SelectSlot = i Then
                RenderBox(New Color(70, 70, 70), New IntRect(SelectChar.X + 6 + (66 * (i - 1)), SelectChar.Y + 22, 64, 64))
            Else
                RenderBox(New Color(120, 120, 120), New IntRect(SelectChar.X + 6 + (66 * (i - 1)), SelectChar.Y + 22, 64, 64))
            End If

            If CharData(i - 1).ID >= 0 Then
                If CharData(i - 1).Sprite > 0 And CharData(i - 1).Sprite < texChar.Length Then
                    RenderTexture(texChar(CharData(i - 1).Sprite), New IntRect(SelectChar.X + 6 + (66 * (i - 1) + ((64 - (GetTextureSize(texChar(CharData(i - 1).Sprite)).X / 4)) / 2)), SelectChar.Y + 22 + 7, GetTextureSize(texChar(CharData(i - 1).Sprite)).X / 4, GetTextureSize(texChar(CharData(i - 1).Sprite)).Y / 4), New IntRect(0 * (GetTextureSize(texChar(CharData(i - 1).Sprite)).X / 4), 0, GetTextureSize(texChar(CharData(i - 1).Sprite)).X / 4, GetTextureSize(texChar(CharData(i - 1).Sprite)).Y / 4))
                End If
                text = ""
                w = 0
                For o As Short = 1 To CharData(i - 1).Nome.Length - 1
                    w = w + GetTextWidth(Mid(CharData(i - 1).Nome, o, 1))
                    If w < 56 Then
                        text &= Mid(CharData(i - 1).Nome, o, 1)
                    Else
                        Exit For
                    End If
                Next
                If text.Length > 0 Then RenderText(text, SelectChar.X + 6 + (66 * (i - 1)) + ((64 - GetTextWidth(text)) / 2), SelectChar.Y + 22 + 1, Color.White)
            End If
        Next

        If CharData(SelectSlot - 1).ID >= 0 Then
            cmbUseorCreate.Caption = "Usar"
        Else
            cmbUseorCreate.Caption = "Criar"
        End If
    End Sub

    Private Sub cmbBack_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbBack.MouseUp
        PlayerSocket.Close()
        PlayerSocket = Nothing
        Connect()
        Tela_General.Open(Telas.Login)
    End Sub

    Private Sub cmbUseorCreate_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbUseorCreate.MouseUp
        If cmbUseorCreate.Caption = "Criar" Then
            Tela_General.Open(Telas.CriarChar)
        Else
            SendUseChar(SelectSlot - 1)
        End If
    End Sub
End Class

Public Class clsCharData
    Public item(5) As clsCharDataDetail
    Shadows Class clsCharDataDetail
        Public Nome As String = ""
        Public Sprite As Short = 1
        Public ID As Short = -1
    End Class

    Public Sub New()
        For i As Byte = 0 To 4
            item(i) = New clsCharDataDetail
        Next
    End Sub

    Default Public Property Base(ByVal Index As Integer) As clsCharDataDetail
        Get
            Return item(Index)
        End Get
        Set(ByVal value As clsCharDataDetail)
            item(Index) = value
        End Set
    End Property
End Class