Public Class frmServer

    Private Sub frmServer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        ServerRun = False
    End Sub

    Private Sub frmServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Application.EnableVisualStyles()
    End Sub

    Private Sub lstView_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lstView.SelectedIndexChanged

    End Sub

    Public Sub UpdateListView()

    End Sub

    Private Sub Button1_Click_(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If lstView.SelectedIndices.Count <= 0 Then Return
        If lstView.SelectedIndices(0) < 0 Then Return
        Dim index As Integer = lstView.SelectedIndices(0) + 1
        If Not IsPlaying(index) Then Return
        Dim s As String = InputBox("Digite o acesso:" & vbNewLine & "0 - Normal" & vbNewLine & "1 - Monitor" & vbNewLine & "2 - Mapper" & vbNewLine & "3 - Databaser" & vbNewLine & "4 - Administrador GG", "Dar Acesso")

        If Not IsNumeric(s) Then MsgBox("Digite um valor válido!")
        If s < 0 Or s > 4 Then MsgBox("Digite um valor dentro de 0 a 4")

        Player(index).Access = s
        Player(index).Save()
        SendPlayerData(index)
    End Sub
End Class
