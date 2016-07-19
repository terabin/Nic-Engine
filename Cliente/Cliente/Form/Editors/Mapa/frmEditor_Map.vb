Public Class frmEditor_Map

    Public MousePos As New SFML.Graphics.IntRect(0, 0, 1, 1)
    Public LayerNum As Short
    Public TileTypeSet As Short

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        CancelEditor_Map()
        Me.Hide()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        SaveEditor_Map()
        Me.Hide()
    End Sub

    Private Sub frmEditor_Map_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub picTile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picTile.Click

    End Sub

    Private Sub picTile_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTile.MouseDown
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            MousePos = New SFML.Graphics.IntRect(Int(e.X / 32), (Int(e.Y / 32)), 1, 1)
        End If
    End Sub

    Private Sub picTile_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles picTile.MouseHover

    End Sub

    Private Sub picTile_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTile.MouseMove
        If e.Button = System.Windows.Forms.MouseButtons.Left Then
            Dim X As Short = 1
            Dim Y As Short = 1

            X = Int(e.X / 32) - MousePos.Left + 1
            Y = Int(e.Y / 32) - MousePos.Top + 1
            If X < 1 Then X = 1
            If Y < 1 Then Y = 1
            MousePos.Width = X
            MousePos.Height = Y

        End If
    End Sub

    Private Sub picTile_MouseUp(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles picTile.MouseUp

    End Sub

    Private Sub rbGround_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rbGround.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 1
    End Sub

    Private Sub RadioButton1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 2
    End Sub

    Private Sub RadioButton2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton2.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 3
    End Sub

    Private Sub RadioButton3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton3.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 4
    End Sub

    Private Sub RadioButton4_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton4.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 5
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles gpLayer.Enter

    End Sub

    Private Sub RadioButton5_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton5.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 6
    End Sub

    Private Sub RadioButton6_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton6.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 7
    End Sub

    Private Sub RadioButton7_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton7.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 8
    End Sub

    Private Sub RadioButton8_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton8.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then LayerNum = 9
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        For X As Short = 0 To Map.MaxX
            For Y As Short = 0 To Map.MaxY
                Map.Tile(X, Y)(LayerNum).Insert(scrlMapTile.Value, MousePos.Left * 32, MousePos.Top * 32)
            Next
        Next
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        For X As Short = 0 To Map.MaxX
            For Y As Short = 0 To Map.MaxY
                Map.Tile(X, Y)(LayerNum).Insert(0, 0, 0)
            Next
        Next
    End Sub

    Private Sub RadioButton10_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton10.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then
            gpLayer.Hide()
            gpAtr.Show()
        End If
    End Sub

    Private Sub RadioButton9_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton9.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then
            gpLayer.Show()
            gpAtr.Hide()
        End If
    End Sub

    Private Sub RadioButton11_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles RadioButton11.CheckedChanged
        If DirectCast(sender, RadioButton).Checked Then TileTypeSet = TileType.Block
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        frmEditor_MapProp.ShowDialog()
    End Sub
End Class