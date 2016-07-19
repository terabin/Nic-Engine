Public Class frmEditor_Anim

    Private Sub txtNome_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNome.TextChanged

    End Sub

    Private Sub frmEditor_Anim_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub

    Private Sub frmEditor_Anim_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        scrlAnim.Maximum = texAnimation.Count - 1
        cmbIndex.Items.Clear()
        For i As Short = 1 To Options.MAX_ANIMATION
            cmbIndex.Items.Add(Trim(Animation(i).Nome))
        Next
        cmbIndex.SelectedIndex = 0
        UpdateEditor()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim cDialog As ColorDialog = New ColorDialog()
        cDialog.Color = Color.White

        If cDialog.ShowDialog <> DialogResult.Cancel Then
            picColor.BackColor = cDialog.Color
        End If

    End Sub

    Private Sub Label9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles lblAnim.Click

    End Sub

    Private Sub scrlAnim_Scroll(sender As Object, e As ScrollEventArgs) Handles scrlAnim.Scroll

    End Sub

    Private Sub scrlAnim_ValueChanged(sender As Object, e As EventArgs) Handles scrlAnim.ValueChanged
        lblAnim.Text = scrlAnim.Value
    End Sub

    Private Sub UpdateEditor()
        Dim index As Integer = cmbIndex.SelectedIndex + 1

        txtNome.Text = Animation(index).Nome
        cmbLayer.SelectedIndex = Animation(index).Layer
        scrlAnim.Value = Animation(index).AnimID
        txtFrameCount.Text = Animation(index).FrameCount
        txtFX.Text = Animation(index).FrameX
        txtFY.Text = Animation(index).FrameY
        txtMS.Text = Animation(index).SpeedMS
        picColor.BackColor = Animation(index).ColourGDI
        chkBlend.Checked = Animation(index).BlendMode
    End Sub

    Private Sub cmbIndex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbIndex.SelectedIndexChanged
        UpdateEditor()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim index As Integer = cmbIndex.SelectedIndex + 1
        Animation(index).Nome = txtNome.Text
        Animation(index).Layer = cmbLayer.SelectedIndex
        Animation(index).AnimID = scrlAnim.Value
        Animation(index).FrameCount = txtFrameCount.Text
        Animation(index).FrameX = txtFX.Text
        Animation(index).FrameY = txtFY.Text
        Animation(index).SpeedMS = txtMS.Text
        Animation(index).ColourGDI = picColor.BackColor
        Animation(index).BlendMode = chkBlend.Checked

        cmbIndex.Items.Item(index - 1) = Animation(index).Nome

        SendUpdateAnim(index)
        MsgBox("Salvo")
    End Sub
End Class