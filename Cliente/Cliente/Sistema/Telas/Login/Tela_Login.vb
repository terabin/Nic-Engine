Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Class Tela_Login
    Inherits clsTelas

#Region "Objetos"
    Public WithEvents Login As Window
    Public WithEvents txtLogin As TextBox
    Public WithEvents txtSenha As TextBox
    Public WithEvents cmbLogin As Button
    Public WithEvents cmbCriarConta As Button
    Public WithEvents cmbOption As Button
    Public WithEvents cmbWebSite As Button
    Public WithEvents cmbExit As Button
    Public WithEvents chkMusica As CheckBox
    Public WithEvents chkSom As CheckBox
    Public WithEvents [Option] As Window
    Public WithEvents chkSaveSenha As CheckBox
#End Region

    Public Overrides Sub Open()
        Login = New Window(Me)
        Login.Height = 200
        Login.Width = 350
        Login.X = (DeviceGame.Size.X - Login.Width) / 2
        Login.Y = (DeviceGame.Size.Y - Login.Height) / 2
        Login.Caption = "Login"
        Login.ButtonExit = False
        Login.Show()

        txtLogin = New TextBox(Login)
        txtLogin.Height = 16
        txtLogin.Width = 150
        txtLogin.MaxLenght = 100
        txtLogin.SetFocus()

        txtSenha = New TextBox(Login)
        txtSenha.Height = 16
        txtSenha.Width = 150
        txtSenha.MaxLenght = 100
        txtSenha.Password = True

        cmbLogin = New Button(Login)
        cmbLogin.Height = 18
        cmbLogin.Width = 100
        cmbLogin.Caption = "Logar"

        cmbCriarConta = New Button(Login)
        cmbCriarConta.Height = 18
        cmbCriarConta.Width = 100
        cmbCriarConta.Caption = "Nova Conta"
        cmbCriarConta.X = 232
        cmbCriarConta.Y = 28
        cmbCriarConta.Colour = New Color(255, 128, 0)

        cmbOption = New Button(Login)
        cmbOption.Height = 18
        cmbOption.Width = 100
        cmbOption.Caption = "Opções"
        cmbOption.X = 232
        cmbOption.Y = 50
        cmbOption.Colour = New Color(255, 128, 0)

        cmbWebSite = New Button(Login)
        cmbWebSite.Height = 18
        cmbWebSite.Width = 100
        cmbWebSite.Caption = "WebSite"
        cmbWebSite.X = 232
        cmbWebSite.Y = 72
        cmbWebSite.Colour = New Color(255, 128, 0)

        cmbExit = New Button(Login)
        cmbExit.Height = 18
        cmbExit.Width = 100
        cmbExit.Caption = "Sair"
        cmbExit.X = 232
        cmbExit.Y = 94
        cmbExit.Colour = New Color(255, 128, 0)

        [Option] = New Window(Me)
        [Option].Width = 200
        [Option].Height = 150
        [Option].X = (DeviceGame.Size.X - [Option].Width) / 2
        [Option].Y = (DeviceGame.Size.Y - [Option].Height) / 2
        [Option].ButtonExit = True
        [Option].Caption = "Opções"
        [Option].ColorTint = New Color(100, 155, 100)

        chkMusica = New CheckBox([Option])
        chkMusica.Caption = "Músicas"
        chkMusica.X = 30
        chkMusica.Checked = Options.Music

        chkSom = New CheckBox([Option])
        chkSom.Caption = "Sons"
        chkSom.X = 30
        chkSom.Checked = Options.Sound

        chkSaveSenha = New CheckBox(Login)
        chkSaveSenha.Caption = "Salvar Dados?"
        chkSaveSenha.X = 50
        chkSaveSenha.Y = 106
        chkSaveSenha.Checked = Options.SalvarDados

        If Options.SalvarDados Then
            txtLogin.Text = Options.SalvarDados_Nome
            txtSenha.Text = Options.SalvarDados_Senha
        End If
        If Options.Music Then Musica.Play("Main.ogg")

        ' Configurar Tab
        txtLogin.TabNext = txtSenha
        txtSenha.TabNext = txtLogin

        ' Conexão
        Connect()
    End Sub

    Public Overrides Sub Draw()
        ' Background
        RenderTexture(texGUI(4), New IntRect(New Vector2f(), DeviceGame.Size), New IntRect(New Vector2f(), GetTextureSize(texGUI(4))))
    End Sub

    Public Sub Login_Closed() Handles Login.Closed
        GameRun = False
    End Sub

    Public Sub Login_Draw() Handles Login.OnDraw
        txtLogin.X = 50
        txtLogin.Y = 45
        txtSenha.X = 50
        txtSenha.Y = 45 + 24
        cmbLogin.X = 100
        cmbLogin.Y = 174
        RenderText("ID:", Login.X + 44 - GetTextWidth("ID:"), Login.Y + 44, New Color(60, 60, 60))
        RenderText("PWD:", Login.X + 44 - GetTextWidth("PWD:"), Login.Y + 68, New Color(60, 60, 60))

        RenderBox(New Color(40, 40, 40, 245), New IntRect(Login.X + 220, Login.Y + 22, 124, Login.Height - 29))

        ' Status
        RenderText("Status:", Login.X + 224, Login.Y + Login.Height - 21, New Color(230, 230, 230), 10)
        If IsConnected Then
            RenderText("Online", Login.X + 224 + GetTextWidth("Status:_", 10), Login.Y + Login.Height - 21, Color.Green, 10)
        Else
            RenderText("Offline", Login.X + 224 + GetTextWidth("Status:_", 10), Login.Y + Login.Height - 21, Color.Red, 10)
        End If
    End Sub

    Public Sub cmbExit_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbExit.MouseUp
        GameRun = False
    End Sub

    Private Sub cmbOption_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbOption.MouseUp
        [Option].Show()
    End Sub

    Private Sub cmbWebSite_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbWebSite.MouseUp
        Process.Start("http://www.tabernarpg.com/forum")
    End Sub

    Private Sub Option_OnDraw() Handles [Option].OnDraw
        chkMusica.Y = 50
        chkSom.Y = 64
        RenderText("Efeitos Sonôros", [Option].X + 18, [Option].Y + 30, Color.White, 13)

        RenderText("Volume da Música:", [Option].X + 18, [Option].Y + 88, Color.White, 13)
    End Sub

    Private Sub chkMusica_ChangeValue() Handles chkMusica.ChangeValue
        Options.Music = chkMusica.Checked
        Options.Save()
        If chkMusica.Checked Then
            Musica.Play("Main.ogg")
        Else
            Musica.Stop()
        End If
    End Sub

    Private Sub chkSom_ChangeValue() Handles chkSom.ChangeValue
        Options.Sound = chkSom.Checked
        Options.Save()
        If Not chkSom.Checked Then Som.StopAll()
    End Sub

    Private Sub cmbCriarConta_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbCriarConta.MouseUp
        Tela_General.Open(Telas.Registro)
    End Sub

    Private Sub cmbLogin_MouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) Handles cmbLogin.MouseUp
        If txtLogin.Text.Length = 0 Or txtSenha.Text.Length = 0 Then
            Tela_General.MessageString = "Caixa de digitar Conta ou Senha está vazia!"
            Return
        End If

        If Options.SalvarDados Then
            Options.SalvarDados_Nome = txtLogin.Text
            Options.SalvarDados_Senha = txtSenha.Text
            Options.Save()
        End If

        If IsConnected Then
            SendLogin(txtLogin.Text, txtSenha.Text)
        Else
            Tela_General.MessageString = "Servidor Offline, tente mais tarde!"
        End If
    End Sub

    Private Sub chkSaveSenha_ChangeValue() Handles chkSaveSenha.ChangeValue
        Options.SalvarDados = chkSaveSenha.Checked
    End Sub
End Class
