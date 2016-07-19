Public Class frmEditor_Npc

    Public Index As Integer = -1

    Private Sub frmEditor_Npc_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub frmEditor_Npc_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        cmbType.SelectedIndex = 0
        cmbDropSlot.SelectedIndex = 0
        cmbSpellSlot.SelectedIndex = 0
        Me.Height = 84
    End Sub

    Private Sub scrlSprite_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles scrlSprite.Scroll
        Dim o As String = "Nothing"
        If scrlSprite.Value > 0 Then o = scrlSprite.Value
        Label3.Text = "Sprite: " & o
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        frmEditor_Index.lstIndex.Items.Clear()
        For i As Short = 1 To Options.MAX_NPC
            frmEditor_Index.lstIndex.Items.Add(Trim(Npc(i).Nome))
        Next
        frmEditor_Index.lstIndex.SelectedIndex = 0

        If frmEditor_Index.ShowDialog() = DialogResult.Yes Then
            Index = frmEditor_Index.lstIndex.SelectedIndex + 1
            txtIndex.Text = ""
            If Index > -1 Then
                Me.Height = 449
                Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - 449) / 2
                UpdateEditor()
                txtIndex.Text = frmEditor_Index.lstIndex.SelectedItem
            End If
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        frmEditor_Index.lstIndex.Items.Clear()
        frmEditor_Index.lstIndex.Items.Add("Nenhum")
        For i As Short = 1 To Options.MAX_ITEM
            frmEditor_Index.lstIndex.Items.Add(Trim(Item(i).Nome))
        Next
        frmEditor_Index.lstIndex.SelectedIndex = 0
        If frmEditor_Index.ShowDialog() = DialogResult.Yes Then
            Dim Index2 As Integer = frmEditor_Index.lstIndex.SelectedIndex
            txtDropItem.Text = ""
            If Index2 > 0 Then
                Dim i As Short = cmbDropSlot.SelectedIndex
                editorNpc_Drop(i).Num = Index2
                txtDropItem.Text = frmEditor_Index.lstIndex.SelectedItem
            End If
        End If
    End Sub

    Private Sub frmEditor_Npc_VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.VisibleChanged
        If Me.Visible Then txtNome.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        frmEditor_Index.lstIndex.Items.Clear()
        frmEditor_Index.lstIndex.Items.Add("Nenhum")
        If frmEditor_Index.ShowDialog() = DialogResult.Yes Then
            Dim Index2 As Integer = frmEditor_Index.lstIndex.SelectedIndex + 1
            txtSpellName.Text = ""
            If Index2 > 0 Then
                Dim i As Short = cmbSpellSlot.SelectedIndex
                editorNpc_Spell(i).Num = Index2
                txtSpellName.Text = frmEditor_Index.lstIndex.SelectedItem
            End If
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Npc(Index).Nome = txtNome.Text
        Npc(Index).Tipo = cmbType.SelectedIndex
        Npc(Index).Level = txtLevel.Text
        Npc(Index).EXP = txtEXP.Text
        Npc(Index).HP = txtHP.Text
        Npc(Index).STR = txtFOR.Text
        Npc(Index).CON = txtRES.Text
        Npc(Index).Sprite = scrlSprite.Value
        Npc(Index).Paperdoll = scrlPaper.Value
        Npc(Index).NpcSpellIntervalo = txtSpellInt.Text
        Npc(Index).SpawnTime = txtSpawn.Text

        For i As Short = 0 To 3
            ' Drops
            Npc(Index).Drop(i).Num = editorNpc_Drop(i).Num
            Npc(Index).Drop(i).Value = editorNpc_Drop(i).Value
            Npc(Index).Drop(i).Chance = editorNpc_Drop(i).Chance

            ' Spells
            Npc(Index).NpcSpell(i).Num = editorNpc_Spell(i).Num
            Npc(Index).NpcSpell(i).CD = editorNpc_Spell(i).CD
        Next

        SendNpcUpdate(Index)

        MsgBox("Salvo")
        txtIndex.Text = "..."
        Me.Height = 84
        Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - 84) / 2
    End Sub

    Private Sub txtSpellInt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSpellInt.TextChanged

    End Sub

    Private Sub txtSpellInt_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSpellInt.Validated
        If Not IsNumeric(txtSpellInt.Text) Then txtSpellInt.Text = 1
        If txtSpellInt.Text < 1 Then txtSpellInt.Text = 1
    End Sub

    Private Sub txtRES_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtRES.Validating
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtDropChance_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDropChance.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 1
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 1 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 1

        Dim i As Short = cmbDropSlot.SelectedIndex
        editorNpc_Drop(i).Chance = DirectCast(sender, System.Windows.Forms.TextBox).Text
    End Sub

    Private Sub txtDropValue_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtDropValue.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 1
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 1 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 1

        Dim i As Short = cmbDropSlot.SelectedIndex
        editorNpc_Drop(i).Value = DirectCast(sender, System.Windows.Forms.TextBox).Text
    End Sub

    Private Sub txtSpellCD_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSpellCD.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 1
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 1 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 1

        Dim i As Short = cmbSpellSlot.SelectedIndex
        editorNpc_Spell(i).CD = DirectCast(sender, System.Windows.Forms.TextBox).Text
    End Sub

    Private Sub txtSpawn_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSpawn.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtFOR_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFOR.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtHP_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtHP.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtEXP_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtEXP.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub txtLevel_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLevel.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
    End Sub

    Private Sub UpdateEditor()
        ReDim editorNpc_Drop(4)
        ReDim editorNpc_Spell(4)

        For i As Short = 0 To 3
            editorNpc_Drop(i) = New NpcData.DropData
            editorNpc_Spell(i) = New NpcData.NpcSpellData
        Next

        txtNome.Text = Npc(Index).Nome
        cmbType.SelectedIndex = Npc(Index).Tipo
        txtLevel.Text = Npc(Index).Level
        txtHP.Text = Npc(Index).HP
        txtEXP.Text = Npc(Index).EXP
        txtFOR.Text = Npc(Index).STR
        txtRES.Text = Npc(Index).CON
        txtSpawn.Text = Npc(Index).SpawnTime
        scrlSprite.Value = Npc(Index).Sprite
        scrlPaper.Value = Npc(Index).Paperdoll
        txtSpellInt.Text = Npc(Index).NpcSpellIntervalo

        For i As Short = 0 To 3
            ' Drop
            editorNpc_Drop(i).Num = Npc(Index).Drop(i).Num
            editorNpc_Drop(i).Value = Npc(Index).Drop(i).Value
            editorNpc_Drop(i).Chance = Npc(Index).Drop(i).Chance

            ' Spell
            editorNpc_Spell(i).Num = Npc(Index).NpcSpell(i).Num
            editorNpc_Spell(i).CD = Npc(Index).NpcSpell(i).CD
        Next

        cmbDropSlot.SelectedIndex = 0
        cmbSpellSlot.SelectedIndex = 0
        UpdateDrop()
        UpdateSpell()
    End Sub

    Private Sub UpdateDrop()
        Dim i As Short = cmbDropSlot.SelectedIndex

        txtDropItem.Text = "Nenhum"
        txtDropValue.Text = editorNpc_Drop(i).Value
        txtDropChance.Text = editorNpc_Drop(i).Chance
        If editorNpc_Drop(i).Num > 0 Then txtDropItem.Text = Trim(Item(editorNpc_Drop(i).Num).Nome)
    End Sub

    Private Sub UpdateSpell()
        Dim i As Short = cmbSpellSlot.SelectedIndex

        txtSpellName.Text = "Nenhum"
        txtSpellCD.Text = editorNpc_Spell(i).CD
    End Sub

    Private Sub cmbDropSlot_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDropSlot.SelectedIndexChanged
        UpdateDrop()
    End Sub

    Private Sub cmbSpellSlot_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSpellSlot.SelectedIndexChanged
        UpdateSpell()
    End Sub
End Class