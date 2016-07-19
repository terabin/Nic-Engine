Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Partial Public Class Tela_InGame
    Private WithEvents cmbMochila As Button
    Private WithEvents winMochila As Window
    Private WithEvents cmbOption As Button
    Private WithEvents winOption As Window
    Private WithEvents winMenu As Window
    Private WithEvents txtChat As TextBox

    Public Overrides Sub Open()
        Musica.Stop()

        winMenu = New Window(Me)
        winMenu.Width = 38
        winMenu.Height = 20 + 35 * 2
        winMenu.X = DeviceGame.Size.X - 40
        winMenu.Y = (DeviceGame.Size.Y - winMenu.Height) / 2
        winMenu.Caption = "Menu"
        winMenu.Dragged = True
        winMenu.Show()

        cmbMochila = New Button(winMenu)
        cmbMochila.Width = 32
        cmbMochila.Height = 32
        cmbMochila.Colour = New Color(255, 104, 0)
        cmbMochila.X = 3
        cmbMochila.Y = 20

        winMochila = New Window(Me)
        winMochila.Width = 200
        winMochila.Height = 250
        winMochila.X = DeviceGame.Size.X - 36 - winMochila.Width
        winMochila.Y = 120
        winMochila.Dragged = True
        winMochila.ButtonExit = True
        winMochila.Caption = "Mochila"

        cmbOption = New Button(winMenu)
        cmbOption.Width = 32
        cmbOption.Height = 32
        cmbOption.Colour = New Color(255, 104, 0)
        cmbOption.X = 3
        cmbOption.Y = 20 + 34

        winOption = New Window(Me)
        winOption.Width = 200
        winOption.Height = 300
        winOption.X = DeviceGame.Size.X - 36 - winMochila.Width
        winOption.Y = 120
        winOption.Dragged = True
        winOption.ButtonExit = True
        winOption.Caption = "Opções"

        txtChat = New TextBox(Me)
        txtChat.MaxLenght = 50
        txtChat.Width = 250
        txtChat.Height = 20
        txtChat.X = 1
        txtChat.Y = DeviceGame.Size.Y - txtChat.Height - 1

        Chat.Load()
        Options.LoadPlayerConfig()
        MyBase.Open()
    End Sub
End Class
