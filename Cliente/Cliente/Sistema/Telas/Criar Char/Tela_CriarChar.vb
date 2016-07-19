Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Public Class Tela_CriarChar
    Inherits clsTelas

#Region "Componentes"
    Public WithEvents CriarChar As Window
    Public WithEvents cmbBack As Button
    Public WithEvents cmbCriar As Button
    Public WithEvents txtNome As TextBox
#End Region

    Private selectClasse As Short

    Public Overrides Sub Open()
        CriarChar = New Window(Me)
        CriarChar.Caption = "Criar Personagem"
        CriarChar.Width = 290
        CriarChar.Height = 200
        CriarChar.X = (DeviceGame.Size.X - CriarChar.Width) / 2
        CriarChar.Y = (DeviceGame.Size.Y - CriarChar.Height) / 2
        CriarChar.Show()

        cmbBack = New Button(CriarChar)
        cmbBack.Caption = "Voltar"
        cmbBack.Width = 100
        cmbBack.Height = 18
        cmbBack.X = CriarChar.Width - 6 - cmbBack.Width
        cmbBack.Y = CriarChar.Height - 7 - cmbBack.Height

        cmbCriar = New Button(CriarChar)
        cmbCriar.Caption = "Criar"
        cmbCriar.Width = 100
        cmbCriar.Height = 18
        cmbCriar.X = CriarChar.Width - 6 - cmbCriar.Width
        cmbCriar.Y = CriarChar.Height - 7 - cmbBack.Height - 2 - cmbCriar.Height

        txtNome = New TextBox(CriarChar)
        txtNome.Width = 140
        txtNome.Height = 16
        txtNome.MaxLenght = 80
        txtNome.X = 10
        txtNome.Y = 39
        txtNome.ColorBG = New Color(255, 255, 255)
        txtNome.ColorText = New Color(60, 60, 60)
        txtNome.SetFocus()

        selectClasse = 1
        MyBase.Open()
    End Sub

    Public Overrides Sub Draw()
        ' Background
        RenderTexture(texGUI(4), New IntRect(New Vector2f(), DeviceGame.Size), New IntRect(New Vector2f(), GetTextureSize(texGUI(4))))
        MyBase.Draw()
    End Sub

    Private Sub cmbBack_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbBack.MouseUp
        Tela_General.Open(Telas.SelectChar)
    End Sub

    Private Sub CriarChar_MouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) Handles CriarChar.MouseMove
        For i As Short = 1 To UBound(Classe)
            If e.X >= CriarChar.X + 10 + (32 * ((i - 1) Mod 4)) And e.X <= CriarChar.X + 10 + 26 + (32 * ((i - 1) Mod 4)) Then
                If e.Y >= CriarChar.Y + 76 + (32 * ((i - 1) \ 4)) And e.Y <= CriarChar.Y + 76 + 26 + (32 * ((i - 1) \ 4)) Then
                    frmMain.Cursor = Cursors.Hand
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub CriarChar_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles CriarChar.MouseUp
        For i As Short = 1 To UBound(Classe)
            If e.X >= CriarChar.X + 10 + (32 * ((i - 1) Mod 4)) And e.X <= CriarChar.X + 10 + 26 + (32 * ((i - 1) Mod 4)) Then
                If e.Y >= CriarChar.Y + 76 + (32 * ((i - 1) \ 4)) And e.Y <= CriarChar.Y + 76 + 26 + (32 * ((i - 1) \ 4)) Then
                    selectClasse = i
                    Exit For
                End If
            End If
        Next
    End Sub

    Private Sub CriarChar_OnDraw() Handles CriarChar.OnDraw
        RenderBox(New Color(40, 40, 40, 245), New IntRect(CriarChar.X + 6, CriarChar.Y + 22, 150, CriarChar.Height - 29))

        RenderText("Digite o Nome:", CriarChar.X + 10, CriarChar.Y + 23, New Color(230, 230, 230))
        RenderText("Escolha o personagem:", CriarChar.X + 10, CriarChar.Y + 60, New Color(230, 230, 230))

        Dim size As Vector2f
        For i As Short = 1 To UBound(Classe)
            size = GetTextureSize(texChar(Classe(i).Sprite))
            RenderBox(New Color(255, 128, 0), New IntRect(CriarChar.X + 10 + (32 * ((i - 1) Mod 4)), CriarChar.Y + 76 + (32 * ((i - 1) \ 4)), 26, 26))
            RenderTexture(texChar(Classe(i).Sprite), New IntRect(CriarChar.X + 10 + ((26 - (size.X / 4)) / 2) + (32 * ((i - 1) Mod 4)), CriarChar.Y + 76 - 1 + (32 * ((i - 1) \ 4)), 26, 26), New IntRect(0, 0, 26, 26))
        Next

        size = GetTextureSize(texChar(Classe(selectClasse).Sprite))
        RenderText("Aparência", CriarChar.X + 156 + (((CriarChar.Width - 156) - GetTextWidth("Aparência", 13)) / 2), CriarChar.Y + 23, New Color(230, 230, 230), 13, , True)
        RenderTexture(texChar(Classe(selectClasse).Sprite), New IntRect(CriarChar.X + 156 + (((CriarChar.Width - 156) - 32) / 2), CriarChar.Y + 40, size.X / 4, size.Y / 4), New IntRect(0, 0, size.X / 4, size.Y / 4))
    End Sub

    Private Sub cmbCriar_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbCriar.MouseUp
        If txtNome.Text.Length < 3 Then
            Tela_General.MessageString = "O nome deve ser com 3 ou mais letras!"
            Return
        End If
        SendCriarChar(txtNome.Text, selectClasse, Tela_General.SelectChar.SelectSlot)
    End Sub
End Class
