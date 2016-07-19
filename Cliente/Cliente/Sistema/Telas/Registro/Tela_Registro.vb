Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Public Class Tela_Registro
    Inherits clsTelas

#Region "Janelas"
    Public WithEvents Registro As Window
    Public WithEvents cmbRegistrar As Button
    Public WithEvents cmbVoltar As Button
    Public WithEvents txtNome As TextBox
    Public WithEvents txtSenha As TextBox
    Public WithEvents txtSenha2 As TextBox
#End Region

    Public Overrides Sub Open()

        Registro = New Window(Me)
        Registro.Width = 230
        Registro.Height = 150
        Registro.X = (DeviceGame.Size.X - Registro.Width) / 2
        Registro.Y = (DeviceGame.Size.Y - Registro.Height) / 2
        Registro.Caption = "Nova Conta"
        Registro.Show()

        cmbRegistrar = New Button(Registro)
        cmbRegistrar.Caption = "Registrar"
        cmbRegistrar.Width = 100
        cmbRegistrar.Height = 18
        cmbRegistrar.X = 10
        cmbRegistrar.Y = Registro.Height - 28

        cmbVoltar = New Button(Registro)
        cmbVoltar.Caption = "Voltar"
        cmbVoltar.Width = 100
        cmbVoltar.Height = 18
        cmbVoltar.X = Registro.Width - 110
        cmbVoltar.Y = Registro.Height - 28

        txtNome = New TextBox(Registro)
        txtNome.Width = 150
        txtNome.Height = 16
        txtNome.MaxLenght = 100
        txtNome.SetFocus()

        txtSenha = New TextBox(Registro)
        txtSenha.Width = 150
        txtSenha.Height = 16
        txtSenha.Password = True
        txtSenha.MaxLenght = 100

        txtSenha2 = New TextBox(Registro)
        txtSenha2.Width = 150
        txtSenha2.Height = 16
        txtSenha2.Password = True
        txtSenha2.MaxLenght = 100

        MyBase.Open()
    End Sub

    Public Overrides Sub Draw()
        ' Background
        RenderTexture(texGUI(4), New IntRect(New Vector2f(), DeviceGame.Size), New IntRect(New Vector2f(), GetTextureSize(texGUI(4))))
        MyBase.Draw()
    End Sub

    Private Sub cmbVoltar_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbVoltar.MouseUp
        Tela_General.Open(Telas.Login)
    End Sub

    Private Sub Registro_OnDraw() Handles Registro.OnDraw
        txtNome.X = 50
        txtNome.Y = 45
        txtSenha.X = 50
        txtSenha.Y = 45 + 24
        txtSenha2.X = 50
        txtSenha2.Y = 45 + 48

        RenderText("ID:", Registro.X + 44 - GetTextWidth("ID:"), Registro.Y + 44, New Color(230, 230, 230), , , True)
        RenderText("PWD:", Registro.X + 44 - GetTextWidth("PWD:"), Registro.Y + 44 + 24, New Color(230, 230, 230), , , True)
        RenderText("PWD2:", Registro.X + 44 - GetTextWidth("PWD2:"), Registro.Y + 44 + 48, New Color(230, 230, 230), , , True)
    End Sub

    Private Sub cmbRegistrar_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbRegistrar.MouseUp
        If IsConnected Then
            If txtNome.Text.Length < 3 Or txtSenha.Text.Length < 3 Then
                MsgBox("Somente pode Nome ou Senha com 3 ou mais Caracteres!", , Options.GAMENAME)
                Return
            End If

            If txtSenha.Text.ToLower <> txtSenha2.Text.ToLower Then
                MsgBox("As senham não estão iguais!", , Options.GAMENAME)
                Return
            End If

            SendRegistro(txtNome.Text, txtSenha.Text)
        End If
    End Sub
End Class
