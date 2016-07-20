Public Class frmEditor_Skill
    Public tabConfig As Boolean
    Public EffectIndex As Byte
    Public tempEffect() As SkillData.EffectData

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

    End Sub

    Private Sub TabControl2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabControl2.SelectedIndexChanged
        'If Not tabConfig Then TabControl2.SelectedIndex = 0
    End Sub

    Private Sub frmEditor_Skill_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        scrlIcon.Maximum = texSkill.Count - 1
        cmbAnim.Items.Clear()
        cmbAnim.Items.Add("Nothing")
        cmbCAnim.Items.Clear()
        cmbCAnim.Items.Add("Nothing")

        For i As Short = 1 To Options.MAX_ANIMATION
            cmbAnim.Items.Add(Animation(i).Nome)
            cmbCAnim.Items.Add(Animation(i).Nome)
        Next

        cmbIndex.Items.Clear()
        For i As Short = 1 To Options.MAX_SKILL
            cmbIndex.Items.Add(Skill(i).Nome)
        Next
        cmbIndex.SelectedIndex = 0
    End Sub

    Private Sub TabControl2_Selecting(sender As Object, e As TabControlCancelEventArgs) Handles TabControl2.Selecting
        If Not tabConfig Then e.Cancel = True
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles cmbEffect1.Click
        TabChange(1)
    End Sub

    Public Sub UpdateEditor()
        Dim index As Short = cmbIndex.SelectedIndex + 1
        ReDim tempEffect(MAX_EFEITOS + 1)
        Array.Copy(Skill(index).Effect, tempEffect, tempEffect.Count - 1)
        cmbMEffect.SelectedIndex = Skill(index).MaxEffect - 1
        txtNome.Text = Skill(index).Nome
        txtMP.Text = Skill(index).CustoMP
        txtCD.Text = Skill(index).CoolDown
        scrlIcon.Value = Skill(index).icon

        tabConfig = True
        TabControl2.SelectedIndex = 0
        tabConfig = False
    End Sub

    Private Sub cmbMEffect_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMEffect.SelectedIndexChanged
        UpdateButton()
    End Sub

    Private Sub UpdateButton()
        cmbEffect1.Enabled = False
        cmbEffect2.Enabled = False
        cmbEffect3.Enabled = False
        cmbEffect4.Enabled = False
        cmbEffect5.Enabled = False
        cmbEffect6.Enabled = False
        cmbEffect7.Enabled = False
        cmbEffect8.Enabled = False
        cmbEffect9.Enabled = False
        cmbEffect10.Enabled = False

        Dim i As Short = cmbMEffect.SelectedIndex
        If i >= 0 Then cmbEffect1.Enabled = True
        If i >= 1 Then cmbEffect2.Enabled = True
        If i >= 2 Then cmbEffect3.Enabled = True
        If i >= 3 Then cmbEffect4.Enabled = True
        If i >= 4 Then cmbEffect5.Enabled = True
        If i >= 5 Then cmbEffect6.Enabled = True
        If i >= 6 Then cmbEffect7.Enabled = True
        If i >= 7 Then cmbEffect8.Enabled = True
        If i >= 8 Then cmbEffect9.Enabled = True
        If i >= 9 Then cmbEffect10.Enabled = True
    End Sub

    Private Sub TabChange(ByVal IDeffect As Byte)
        EffectIndex = IDeffect
        tabConfig = True
        TabControl2.SelectedIndex = 1
        tabConfig = False
        cmbTipo.SelectedIndex = 0
        UpdateEffect()
    End Sub

    Private Sub cmbEffect2_Click(sender As Object, e As EventArgs) Handles cmbEffect2.Click
        TabChange(2)
    End Sub

    Private Sub cmbEffect3_Click(sender As Object, e As EventArgs) Handles cmbEffect3.Click
        TabChange(3)
    End Sub

    Private Sub cmbEffect4_Click(sender As Object, e As EventArgs) Handles cmbEffect4.Click
        TabChange(4)
    End Sub

    Private Sub cmbEffect5_Click(sender As Object, e As EventArgs) Handles cmbEffect5.Click
        TabChange(5)
    End Sub

    Private Sub cmbEffect6_Click(sender As Object, e As EventArgs) Handles cmbEffect6.Click
        TabChange(6)
    End Sub

    Private Sub cmbEffect7_Click(sender As Object, e As EventArgs) Handles cmbEffect7.Click
        TabChange(7)
    End Sub

    Private Sub cmbEffect8_Click(sender As Object, e As EventArgs) Handles cmbEffect8.Click
        TabChange(8)
    End Sub

    Private Sub cmbEffect9_Click(sender As Object, e As EventArgs) Handles cmbEffect9.Click
        TabChange(9)
    End Sub

    Private Sub cmbEffect10_Click(sender As Object, e As EventArgs) Handles cmbEffect10.Click
        TabChange(10)
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs) Handles Button2.Click
        tempEffect(EffectIndex).Anim = cmbAnim.SelectedIndex
        tempEffect(EffectIndex).CastAnimation = cmbCAnim.SelectedIndex
        tempEffect(EffectIndex).CastTimer = txtCTimer.Text
        tempEffect(EffectIndex).isAOE = chkAoe.Checked
        tempEffect(EffectIndex).Range = txtRange.Text
        tempEffect(EffectIndex).Tipo = cmbTipo.SelectedIndex
        tempEffect(EffectIndex).Vital = txtVital.Text
        tempEffect(EffectIndex).Roubo = txtRouba.Text

        tabConfig = True
        TabControl2.SelectedIndex = 0
        tabConfig = False
    End Sub

    Private Sub chkEA_CheckedChanged(sender As Object, e As EventArgs) Handles chkEA.CheckedChanged

    End Sub

    Private Sub txtRouba_TextChanged(sender As Object, e As EventArgs) Handles txtRouba.TextChanged

    End Sub

    Private Sub txtRouba_Validated(sender As Object, e As EventArgs) Handles txtRouba.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text > 100 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 100
    End Sub

    Private Sub cmbTipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbTipo.SelectedIndexChanged
        gpRouba.Hide()
        gpVital.Hide()

        Select Case cmbTipo.SelectedIndex
            Case 0, 1 : gpRouba.Show() ' Rouba HP/MP
            Case 2, 3 : gpVital.Show() ' Cura ou Dano
        End Select
    End Sub

    Public Sub UpdateEffect()
        cmbCAnim.SelectedIndex = tempEffect(EffectIndex).CastAnimation
        txtCTimer.Text = tempEffect(EffectIndex).CastTimer
        cmbAnim.SelectedIndex = tempEffect(EffectIndex).Anim
        cmbTipo.SelectedIndex = tempEffect(EffectIndex).Tipo
        txtRange.Text = tempEffect(EffectIndex).Range
        chkAoe.Checked = tempEffect(EffectIndex).isAOE
        txtVital.Text = tempEffect(EffectIndex).Vital
        txtRouba.Text = tempEffect(EffectIndex).Roubo
    End Sub

    Private Sub cmbIndex_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbIndex.SelectedIndexChanged
        UpdateEditor()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim Index As Short = cmbIndex.SelectedIndex + 1

        With Skill(Index)
            .Nome = txtNome.Text
            .CustoMP = txtMP.Text
            .CoolDown = txtCD.Text
            .Icon = scrlIcon.Value
            .MaxEffect = cmbMEffect.SelectedIndex + 1
            Array.Copy(tempEffect, .Effect, tempEffect.Count - 1)

            cmbIndex.Items.Item(cmbIndex.SelectedIndex) = .Nome
            SendUpdateSkill(Index)
            MsgBox("Salvo!")
        End With
    End Sub
End Class