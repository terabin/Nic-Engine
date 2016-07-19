Public Class frmEditor_Item
    Private helpDesc As ToolTip
    Private Index As Integer

    Private Sub frmEditor_Item_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        e.Cancel = True
        Me.Hide()
    End Sub
    Private Sub frmEditor_Item_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Height = 84
        scrlIcon.Maximum = texItem.Count - 1

        helpDesc = New ToolTip()
        Dim text As String
        text = "[vital] = Retorna o valor de quanto vital configurada para a poção" & vbNewLine & "[cd] = Retorna o tempo de re-uso do item"

        helpDesc.SetToolTip(Button3, text)
        cmbTipo.SelectedIndex = 0
        cmbEquip.SelectedIndex = 0
        Index = -1
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmEditor_Index.lstIndex.Items.Clear()
        For i As Short = 1 To Options.MAX_ITEM
            frmEditor_Index.lstIndex.Items.Add(Trim(Item(i).Nome))
        Next
        frmEditor_Index.lstIndex.SelectedIndex = 0

        If frmEditor_Index.ShowDialog = DialogResult.Yes Then
            Index = frmEditor_Index.lstIndex.SelectedIndex + 1
            txtIndex.Text = frmEditor_Index.lstIndex.SelectedItem
            Me.Height = 449
            Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - 449) / 2
            UpdateEditor()
        End If
    End Sub

    Private Sub Button3_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.MouseLeave
        helpDesc.ShowAlways = False
    End Sub

    Private Sub Button3_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button3.MouseMove
        helpDesc.ShowAlways = True
    End Sub

    Private Sub txtVital_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtVital.Validated
        If Not IsNumeric(DirectCast(sender, Windows.Forms.TextBox).Text) Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtCD_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCD.Validated
        If Not IsNumeric(DirectCast(sender, Windows.Forms.TextBox).Text) Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbTipo.SelectedIndexChanged
        gpPocao.Hide()
        gpEquip.Hide()
        Select Case cmbTipo.SelectedIndex
            Case ItemType.Poção : gpPocao.Show()
            Case ItemType.Equipamento : gpEquip.Show()
        End Select
    End Sub

    Public Sub UpdateEditor()
        cmbTipo.SelectedIndex = Item(Index).Tipo
        cmbCura.SelectedIndex = Item(Index).PotionMode
        cmbEquip.SelectedIndex = Item(Index).EquipTipo
        cmbEffect.Items.Clear()
        cmbEffect.Items.Add("Nenhum")
        cmbEffect.SelectedIndex = Item(Index).PotionEffect
        scrlIcon.Value = Item(Index).Icon
        txtNome.Text = Item(Index).Nome
        txtDescrição.Text = Item(Index).Desc
        chkDrop.Checked = Item(Index).isDrop
        chkStack.Checked = Item(Index).isStack
        txtHP.Text = Item(Index).EquipHP
        txtMp.Text = Item(Index).EquipMP
        txtFor.Text = Item(Index).EquipFOR
        txtCon.Text = Item(Index).EquipCON
        scrlPaper.Value = Item(Index).EquipPaper
        txtCD.Text = Item(Index).PotionCD
        txtVital.Text = Item(Index).PotionVital
    End Sub

    Private Sub txtHP_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHP.Validated
        If Not IsNumeric(DirectCast(sender, Windows.Forms.TextBox).Text) Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtMp_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMp.Validated
        If Not IsNumeric(DirectCast(sender, Windows.Forms.TextBox).Text) Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtFor_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFor.Validated
        If Not IsNumeric(DirectCast(sender, Windows.Forms.TextBox).Text) Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtCon_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCon.Validated
        If Not IsNumeric(DirectCast(sender, Windows.Forms.TextBox).Text) Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If Index = -1 Then Return

        Item(Index).Nome = txtNome.Text
        Item(Index).Tipo = cmbTipo.SelectedIndex
        Item(Index).Desc = txtDescrição.Text
        Item(Index).Icon = scrlIcon.Value
        Item(Index).isDrop = chkDrop.Checked
        Item(Index).isStack = chkStack.Checked
        Item(Index).Peso = txtPeso.Text
        Item(Index).PotionMode = cmbCura.SelectedIndex
        Item(Index).PotionVital = txtVital.Text
        Item(Index).PotionCD = txtCD.Text
        Item(Index).PotionEffect = cmbEffect.SelectedIndex
        Item(Index).EquipTipo = cmbEquip.SelectedIndex
        Item(Index).EquipPaper = scrlPaper.Value
        Item(Index).EquipHP = txtHP.Text
        Item(Index).EquipMP = txtMp.Text
        Item(Index).EquipFOR = txtFor.Text
        Item(Index).EquipCON = txtCon.Text

        SendItemUpdate(Index)
        MsgBox("Salvo!")
        Me.Height = 84
        Index = -1

    End Sub

    Private Sub txtPeso_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPeso.Validated
        If Not IsNumeric(DirectCast(sender, Windows.Forms.TextBox).Text) Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, Windows.Forms.TextBox).Text = 0
    End Sub
End Class