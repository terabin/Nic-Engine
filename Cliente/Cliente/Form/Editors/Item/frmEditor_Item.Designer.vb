<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor_Item
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.txtIndex = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.txtPeso = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.scrlIcon = New System.Windows.Forms.HScrollBar()
        Me.picIcon = New System.Windows.Forms.PictureBox()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.txtDescrição = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkStack = New System.Windows.Forms.CheckBox()
        Me.chkDrop = New System.Windows.Forms.CheckBox()
        Me.cmbTipo = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNome = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.gpEquip = New System.Windows.Forms.GroupBox()
        Me.cmbEquip = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtCon = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtFor = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtMp = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtHP = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.scrlPaper = New System.Windows.Forms.HScrollBar()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.gpPocao = New System.Windows.Forms.GroupBox()
        Me.cmbEffect = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCD = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtVital = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbCura = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpEquip.SuspendLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpPocao.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(611, 22)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(60, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Salvar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(577, 22)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(28, 23)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'txtIndex
        '
        Me.txtIndex.Location = New System.Drawing.Point(78, 23)
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.ReadOnly = True
        Me.txtIndex.Size = New System.Drawing.Size(491, 20)
        Me.txtIndex.TabIndex = 5
        Me.txtIndex.Text = "..."
        Me.txtIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Selecionar: "
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Location = New System.Drawing.Point(18, 90)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(651, 322)
        Me.TabControl1.TabIndex = 8
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.txtPeso)
        Me.TabPage1.Controls.Add(Me.Label14)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.Button3)
        Me.TabPage1.Controls.Add(Me.txtDescrição)
        Me.TabPage1.Controls.Add(Me.Label4)
        Me.TabPage1.Controls.Add(Me.chkStack)
        Me.TabPage1.Controls.Add(Me.chkDrop)
        Me.TabPage1.Controls.Add(Me.cmbTipo)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.txtNome)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.gpEquip)
        Me.TabPage1.Controls.Add(Me.gpPocao)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(643, 296)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'txtPeso
        '
        Me.txtPeso.Location = New System.Drawing.Point(272, 72)
        Me.txtPeso.Name = "txtPeso"
        Me.txtPeso.Size = New System.Drawing.Size(38, 20)
        Me.txtPeso.TabIndex = 13
        Me.txtPeso.Text = "0"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(213, 71)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(53, 20)
        Me.Label14.TabIndex = 12
        Me.Label14.Text = "Peso:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.scrlIcon)
        Me.GroupBox1.Controls.Add(Me.picIcon)
        Me.GroupBox1.Location = New System.Drawing.Point(359, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(278, 60)
        Me.GroupBox1.TabIndex = 9
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Icone"
        '
        'scrlIcon
        '
        Me.scrlIcon.LargeChange = 1
        Me.scrlIcon.Location = New System.Drawing.Point(48, 38)
        Me.scrlIcon.Maximum = 0
        Me.scrlIcon.Name = "scrlIcon"
        Me.scrlIcon.Size = New System.Drawing.Size(220, 17)
        Me.scrlIcon.TabIndex = 1
        '
        'picIcon
        '
        Me.picIcon.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.picIcon.Location = New System.Drawing.Point(13, 21)
        Me.picIcon.Name = "picIcon"
        Me.picIcon.Size = New System.Drawing.Size(32, 32)
        Me.picIcon.TabIndex = 0
        Me.picIcon.TabStop = False
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(316, 137)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(22, 20)
        Me.Button3.TabIndex = 8
        Me.Button3.Text = "?"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txtDescrição
        '
        Me.txtDescrição.Location = New System.Drawing.Point(94, 138)
        Me.txtDescrição.Multiline = True
        Me.txtDescrição.Name = "txtDescrição"
        Me.txtDescrição.Size = New System.Drawing.Size(216, 152)
        Me.txtDescrição.TabIndex = 7
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(11, 138)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(77, 20)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Descrição:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkStack
        '
        Me.chkStack.AutoSize = True
        Me.chkStack.Location = New System.Drawing.Point(94, 95)
        Me.chkStack.Name = "chkStack"
        Me.chkStack.Size = New System.Drawing.Size(80, 17)
        Me.chkStack.TabIndex = 5
        Me.chkStack.Text = "Empilhavel "
        Me.chkStack.UseVisualStyleBackColor = True
        '
        'chkDrop
        '
        Me.chkDrop.AutoSize = True
        Me.chkDrop.Location = New System.Drawing.Point(94, 72)
        Me.chkDrop.Name = "chkDrop"
        Me.chkDrop.Size = New System.Drawing.Size(69, 17)
        Me.chkDrop.TabIndex = 4
        Me.chkDrop.Text = "Dropavel"
        Me.chkDrop.UseVisualStyleBackColor = True
        '
        'cmbTipo
        '
        Me.cmbTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbTipo.FormattingEnabled = True
        Me.cmbTipo.Items.AddRange(New Object() {"Nenhum", "Equipamento", "Poção"})
        Me.cmbTipo.Location = New System.Drawing.Point(94, 45)
        Me.cmbTipo.Name = "cmbTipo"
        Me.cmbTipo.Size = New System.Drawing.Size(216, 21)
        Me.cmbTipo.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(11, 44)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 20)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Tipo:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNome
        '
        Me.txtNome.Location = New System.Drawing.Point(94, 19)
        Me.txtNome.Name = "txtNome"
        Me.txtNome.Size = New System.Drawing.Size(216, 20)
        Me.txtNome.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(11, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(77, 20)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Nome:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gpEquip
        '
        Me.gpEquip.Controls.Add(Me.cmbEquip)
        Me.gpEquip.Controls.Add(Me.Label15)
        Me.gpEquip.Controls.Add(Me.txtCon)
        Me.gpEquip.Controls.Add(Me.Label13)
        Me.gpEquip.Controls.Add(Me.txtFor)
        Me.gpEquip.Controls.Add(Me.Label12)
        Me.gpEquip.Controls.Add(Me.txtMp)
        Me.gpEquip.Controls.Add(Me.Label11)
        Me.gpEquip.Controls.Add(Me.txtHP)
        Me.gpEquip.Controls.Add(Me.Label10)
        Me.gpEquip.Controls.Add(Me.Label9)
        Me.gpEquip.Controls.Add(Me.scrlPaper)
        Me.gpEquip.Controls.Add(Me.PictureBox1)
        Me.gpEquip.Location = New System.Drawing.Point(359, 72)
        Me.gpEquip.Name = "gpEquip"
        Me.gpEquip.Size = New System.Drawing.Size(278, 218)
        Me.gpEquip.TabIndex = 11
        Me.gpEquip.TabStop = False
        Me.gpEquip.Text = "Equipamentos"
        Me.gpEquip.Visible = False
        '
        'cmbEquip
        '
        Me.cmbEquip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEquip.FormattingEnabled = True
        Me.cmbEquip.Items.AddRange(New Object() {"Arma", "Capacete", "Armadura", "Luvas", "Botas", "Anel", "Colar"})
        Me.cmbEquip.Location = New System.Drawing.Point(48, 119)
        Me.cmbEquip.Name = "cmbEquip"
        Me.cmbEquip.Size = New System.Drawing.Size(222, 21)
        Me.cmbEquip.TabIndex = 13
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(11, 120)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(34, 20)
        Me.Label15.TabIndex = 12
        Me.Label15.Text = "Tipo:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCon
        '
        Me.txtCon.Location = New System.Drawing.Point(191, 189)
        Me.txtCon.Name = "txtCon"
        Me.txtCon.Size = New System.Drawing.Size(38, 20)
        Me.txtCon.TabIndex = 11
        Me.txtCon.Text = "0"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(132, 188)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(53, 20)
        Me.Label13.TabIndex = 10
        Me.Label13.Text = "CON:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFor
        '
        Me.txtFor.Location = New System.Drawing.Point(191, 162)
        Me.txtFor.Name = "txtFor"
        Me.txtFor.Size = New System.Drawing.Size(38, 20)
        Me.txtFor.TabIndex = 9
        Me.txtFor.Text = "0"
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(132, 161)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(53, 20)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "FOR:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMp
        '
        Me.txtMp.Location = New System.Drawing.Point(74, 188)
        Me.txtMp.Name = "txtMp"
        Me.txtMp.Size = New System.Drawing.Size(38, 20)
        Me.txtMp.TabIndex = 7
        Me.txtMp.Text = "0"
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(15, 187)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(53, 20)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "MP:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHP
        '
        Me.txtHP.Location = New System.Drawing.Point(74, 162)
        Me.txtHP.Name = "txtHP"
        Me.txtHP.Size = New System.Drawing.Size(38, 20)
        Me.txtHP.TabIndex = 5
        Me.txtHP.Text = "0"
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(15, 161)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(53, 20)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "HP:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(50, 50)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(63, 13)
        Me.Label9.TabIndex = 3
        Me.Label9.Text = "Paperdoll: 0"
        '
        'scrlPaper
        '
        Me.scrlPaper.LargeChange = 1
        Me.scrlPaper.Location = New System.Drawing.Point(50, 65)
        Me.scrlPaper.Maximum = 0
        Me.scrlPaper.Name = "scrlPaper"
        Me.scrlPaper.Size = New System.Drawing.Size(220, 17)
        Me.scrlPaper.TabIndex = 2
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.PictureBox1.Location = New System.Drawing.Point(13, 19)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(32, 64)
        Me.PictureBox1.TabIndex = 1
        Me.PictureBox1.TabStop = False
        '
        'gpPocao
        '
        Me.gpPocao.Controls.Add(Me.cmbEffect)
        Me.gpPocao.Controls.Add(Me.Label8)
        Me.gpPocao.Controls.Add(Me.txtCD)
        Me.gpPocao.Controls.Add(Me.Label7)
        Me.gpPocao.Controls.Add(Me.txtVital)
        Me.gpPocao.Controls.Add(Me.Label6)
        Me.gpPocao.Controls.Add(Me.cmbCura)
        Me.gpPocao.Controls.Add(Me.Label5)
        Me.gpPocao.Location = New System.Drawing.Point(359, 72)
        Me.gpPocao.Name = "gpPocao"
        Me.gpPocao.Size = New System.Drawing.Size(278, 218)
        Me.gpPocao.TabIndex = 10
        Me.gpPocao.TabStop = False
        Me.gpPocao.Text = "Poção"
        Me.gpPocao.Visible = False
        '
        'cmbEffect
        '
        Me.cmbEffect.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbEffect.FormattingEnabled = True
        Me.cmbEffect.Items.AddRange(New Object() {"HP", "MP"})
        Me.cmbEffect.Location = New System.Drawing.Point(54, 127)
        Me.cmbEffect.Name = "cmbEffect"
        Me.cmbEffect.Size = New System.Drawing.Size(212, 21)
        Me.cmbEffect.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(7, 126)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 20)
        Me.Label8.TabIndex = 10
        Me.Label8.Text = "Efeito:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCD
        '
        Me.txtCD.Location = New System.Drawing.Point(54, 98)
        Me.txtCD.Name = "txtCD"
        Me.txtCD.Size = New System.Drawing.Size(212, 20)
        Me.txtCD.TabIndex = 9
        Me.txtCD.Text = "0"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(7, 96)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(41, 20)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "CD:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVital
        '
        Me.txtVital.Location = New System.Drawing.Point(54, 67)
        Me.txtVital.Name = "txtVital"
        Me.txtVital.Size = New System.Drawing.Size(212, 20)
        Me.txtVital.TabIndex = 7
        Me.txtVital.Text = "0"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(7, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 20)
        Me.Label6.TabIndex = 6
        Me.Label6.Text = "Vital:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbCura
        '
        Me.cmbCura.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbCura.FormattingEnabled = True
        Me.cmbCura.Items.AddRange(New Object() {"HP", "MP"})
        Me.cmbCura.Location = New System.Drawing.Point(54, 36)
        Me.cmbCura.Name = "cmbCura"
        Me.cmbCura.Size = New System.Drawing.Size(212, 21)
        Me.cmbCura.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(7, 35)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(41, 20)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Cura:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmEditor_Item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(684, 420)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtIndex)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditor_Item"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Editor de Item"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.picIcon, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpEquip.ResumeLayout(False)
        Me.gpEquip.PerformLayout()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpPocao.ResumeLayout(False)
        Me.gpPocao.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents txtIndex As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents txtDescrição As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkStack As System.Windows.Forms.CheckBox
    Friend WithEvents chkDrop As System.Windows.Forms.CheckBox
    Friend WithEvents cmbTipo As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtNome As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents scrlIcon As System.Windows.Forms.HScrollBar
    Friend WithEvents picIcon As System.Windows.Forms.PictureBox
    Friend WithEvents gpPocao As System.Windows.Forms.GroupBox
    Friend WithEvents cmbCura As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtVital As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtCD As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cmbEffect As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPeso As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents gpEquip As System.Windows.Forms.GroupBox
    Friend WithEvents txtCon As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtFor As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtMp As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtHP As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents scrlPaper As System.Windows.Forms.HScrollBar
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents cmbEquip As System.Windows.Forms.ComboBox
    Friend WithEvents Label15 As System.Windows.Forms.Label
End Class
