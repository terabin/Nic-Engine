<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor_Npc
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
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIndex = New System.Windows.Forms.TextBox()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.txtDropChance = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtDropValue = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.txtDropItem = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cmbDropSlot = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.txtSpawn = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtHP = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtRES = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtFOR = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtEXP = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtLevel = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cmbType = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.scrlPaper = New System.Windows.Forms.HScrollBar()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.scrlSprite = New System.Windows.Forms.HScrollBar()
        Me.picSprite = New System.Windows.Forms.PictureBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNome = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtSpellCD = New System.Windows.Forms.TextBox()
        Me.txtSpellInt = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtSpellName = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.cmbSpellSlot = New System.Windows.Forms.ComboBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.picSprite, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Selecionar: "
        '
        'txtIndex
        '
        Me.txtIndex.Location = New System.Drawing.Point(78, 23)
        Me.txtIndex.Name = "txtIndex"
        Me.txtIndex.ReadOnly = True
        Me.txtIndex.Size = New System.Drawing.Size(491, 20)
        Me.txtIndex.TabIndex = 1
        Me.txtIndex.Text = "..."
        Me.txtIndex.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(577, 20)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(28, 23)
        Me.Button1.TabIndex = 2
        Me.Button1.Text = "..."
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(611, 20)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(60, 23)
        Me.Button2.TabIndex = 3
        Me.Button2.Text = "Salvar"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 58)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(659, 348)
        Me.TabControl1.TabIndex = 4
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox3)
        Me.TabPage1.Controls.Add(Me.GroupBox2)
        Me.TabPage1.Controls.Add(Me.cmbType)
        Me.TabPage1.Controls.Add(Me.Label5)
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.txtNome)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(651, 322)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "General"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txtDropChance)
        Me.GroupBox3.Controls.Add(Me.Label13)
        Me.GroupBox3.Controls.Add(Me.txtDropValue)
        Me.GroupBox3.Controls.Add(Me.Label12)
        Me.GroupBox3.Controls.Add(Me.Button3)
        Me.GroupBox3.Controls.Add(Me.txtDropItem)
        Me.GroupBox3.Controls.Add(Me.Label11)
        Me.GroupBox3.Controls.Add(Me.cmbDropSlot)
        Me.GroupBox3.Controls.Add(Me.Label10)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 174)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(351, 138)
        Me.GroupBox3.TabIndex = 6
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Drops"
        '
        'txtDropChance
        '
        Me.txtDropChance.Location = New System.Drawing.Point(231, 98)
        Me.txtDropChance.Name = "txtDropChance"
        Me.txtDropChance.Size = New System.Drawing.Size(63, 20)
        Me.txtDropChance.TabIndex = 13
        Me.txtDropChance.Text = "1"
        '
        'Label13
        '
        Me.Label13.Location = New System.Drawing.Point(150, 99)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(81, 17)
        Me.Label13.TabIndex = 12
        Me.Label13.Text = "Chance: 1/"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDropValue
        '
        Me.txtDropValue.Location = New System.Drawing.Point(90, 72)
        Me.txtDropValue.Name = "txtDropValue"
        Me.txtDropValue.Size = New System.Drawing.Size(204, 20)
        Me.txtDropValue.TabIndex = 11
        Me.txtDropValue.Text = "1"
        Me.txtDropValue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label12
        '
        Me.Label12.Location = New System.Drawing.Point(3, 72)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 17)
        Me.Label12.TabIndex = 10
        Me.Label12.Text = "Quantia:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(300, 45)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(28, 23)
        Me.Button3.TabIndex = 9
        Me.Button3.Text = "..."
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txtDropItem
        '
        Me.txtDropItem.Location = New System.Drawing.Point(90, 46)
        Me.txtDropItem.Name = "txtDropItem"
        Me.txtDropItem.ReadOnly = True
        Me.txtDropItem.Size = New System.Drawing.Size(204, 20)
        Me.txtDropItem.TabIndex = 8
        Me.txtDropItem.Text = "Nenhum"
        Me.txtDropItem.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(3, 46)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(81, 17)
        Me.Label11.TabIndex = 7
        Me.Label11.Text = "Item:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbDropSlot
        '
        Me.cmbDropSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbDropSlot.FormattingEnabled = True
        Me.cmbDropSlot.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cmbDropSlot.Location = New System.Drawing.Point(90, 19)
        Me.cmbDropSlot.Name = "cmbDropSlot"
        Me.cmbDropSlot.Size = New System.Drawing.Size(204, 21)
        Me.cmbDropSlot.TabIndex = 6
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(3, 20)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(81, 17)
        Me.Label10.TabIndex = 5
        Me.Label10.Text = "Slot:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txtSpawn)
        Me.GroupBox2.Controls.Add(Me.Label21)
        Me.GroupBox2.Controls.Add(Me.txtHP)
        Me.GroupBox2.Controls.Add(Me.Label14)
        Me.GroupBox2.Controls.Add(Me.txtRES)
        Me.GroupBox2.Controls.Add(Me.Label8)
        Me.GroupBox2.Controls.Add(Me.txtFOR)
        Me.GroupBox2.Controls.Add(Me.Label9)
        Me.GroupBox2.Controls.Add(Me.txtEXP)
        Me.GroupBox2.Controls.Add(Me.Label7)
        Me.GroupBox2.Controls.Add(Me.txtLevel)
        Me.GroupBox2.Controls.Add(Me.Label6)
        Me.GroupBox2.Location = New System.Drawing.Point(6, 91)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(351, 77)
        Me.GroupBox2.TabIndex = 5
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Atributos"
        '
        'txtSpawn
        '
        Me.txtSpawn.Location = New System.Drawing.Point(287, 44)
        Me.txtSpawn.Name = "txtSpawn"
        Me.txtSpawn.Size = New System.Drawing.Size(58, 20)
        Me.txtSpawn.TabIndex = 12
        Me.txtSpawn.Text = "0"
        '
        'Label21
        '
        Me.Label21.Location = New System.Drawing.Point(232, 47)
        Me.Label21.Name = "Label21"
        Me.Label21.Size = New System.Drawing.Size(50, 17)
        Me.Label21.TabIndex = 11
        Me.Label21.Text = "SPAWN:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtHP
        '
        Me.txtHP.Location = New System.Drawing.Point(287, 18)
        Me.txtHP.Name = "txtHP"
        Me.txtHP.Size = New System.Drawing.Size(58, 20)
        Me.txtHP.TabIndex = 10
        Me.txtHP.Text = "0"
        '
        'Label14
        '
        Me.Label14.Location = New System.Drawing.Point(237, 21)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(44, 17)
        Me.Label14.TabIndex = 9
        Me.Label14.Text = "HP:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRES
        '
        Me.txtRES.Location = New System.Drawing.Point(174, 44)
        Me.txtRES.Name = "txtRES"
        Me.txtRES.Size = New System.Drawing.Size(58, 20)
        Me.txtRES.TabIndex = 8
        Me.txtRES.Text = "0"
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(127, 47)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(41, 17)
        Me.Label8.TabIndex = 7
        Me.Label8.Text = "RES:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFOR
        '
        Me.txtFOR.Location = New System.Drawing.Point(56, 44)
        Me.txtFOR.Name = "txtFOR"
        Me.txtFOR.Size = New System.Drawing.Size(58, 20)
        Me.txtFOR.TabIndex = 6
        Me.txtFOR.Text = "0"
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(12, 47)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(38, 17)
        Me.Label9.TabIndex = 5
        Me.Label9.Text = "FOR:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtEXP
        '
        Me.txtEXP.Location = New System.Drawing.Point(174, 18)
        Me.txtEXP.Name = "txtEXP"
        Me.txtEXP.Size = New System.Drawing.Size(58, 20)
        Me.txtEXP.TabIndex = 4
        Me.txtEXP.Text = "0"
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(124, 21)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(44, 17)
        Me.Label7.TabIndex = 3
        Me.Label7.Text = "EXP:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLevel
        '
        Me.txtLevel.Location = New System.Drawing.Point(56, 18)
        Me.txtLevel.Name = "txtLevel"
        Me.txtLevel.Size = New System.Drawing.Size(58, 20)
        Me.txtLevel.TabIndex = 2
        Me.txtLevel.Text = "1"
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(9, 21)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 17)
        Me.Label6.TabIndex = 1
        Me.Label6.Text = "Level:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbType
        '
        Me.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbType.FormattingEnabled = True
        Me.cmbType.Items.AddRange(New Object() {"Ataca todos", "Ataca se for Atacado", "Amigavel", "Event"})
        Me.cmbType.Location = New System.Drawing.Point(96, 64)
        Me.cmbType.Name = "cmbType"
        Me.cmbType.Size = New System.Drawing.Size(204, 21)
        Me.cmbType.TabIndex = 4
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(9, 65)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 17)
        Me.Label5.TabIndex = 3
        Me.Label5.Text = "Tipo:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox1.Controls.Add(Me.scrlPaper)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.scrlSprite)
        Me.GroupBox1.Controls.Add(Me.picSprite)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Location = New System.Drawing.Point(363, 13)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(269, 299)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Gráficos"
        '
        'scrlPaper
        '
        Me.scrlPaper.LargeChange = 1
        Me.scrlPaper.Location = New System.Drawing.Point(23, 140)
        Me.scrlPaper.Maximum = 0
        Me.scrlPaper.Name = "scrlPaper"
        Me.scrlPaper.Size = New System.Drawing.Size(220, 17)
        Me.scrlPaper.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 127)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 5
        Me.Label4.Text = "Paperdoll: Nothing"
        '
        'scrlSprite
        '
        Me.scrlSprite.LargeChange = 1
        Me.scrlSprite.Location = New System.Drawing.Point(23, 91)
        Me.scrlSprite.Maximum = 0
        Me.scrlSprite.Name = "scrlSprite"
        Me.scrlSprite.Size = New System.Drawing.Size(220, 17)
        Me.scrlSprite.TabIndex = 4
        '
        'picSprite
        '
        Me.picSprite.BackColor = System.Drawing.Color.FromArgb(CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer), CType(CType(64, Byte), Integer))
        Me.picSprite.Location = New System.Drawing.Point(112, 19)
        Me.picSprite.Name = "picSprite"
        Me.picSprite.Size = New System.Drawing.Size(50, 50)
        Me.picSprite.TabIndex = 3
        Me.picSprite.TabStop = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(20, 78)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Sprite: Nothing"
        '
        'txtNome
        '
        Me.txtNome.Location = New System.Drawing.Point(96, 32)
        Me.txtNome.MaxLength = 20
        Me.txtNome.Name = "txtNome"
        Me.txtNome.Size = New System.Drawing.Size(204, 20)
        Me.txtNome.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(9, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(81, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Nome:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox4)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(651, 322)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Outros"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.Label20)
        Me.GroupBox4.Controls.Add(Me.Label19)
        Me.GroupBox4.Controls.Add(Me.txtSpellCD)
        Me.GroupBox4.Controls.Add(Me.txtSpellInt)
        Me.GroupBox4.Controls.Add(Me.Label18)
        Me.GroupBox4.Controls.Add(Me.txtSpellName)
        Me.GroupBox4.Controls.Add(Me.Label16)
        Me.GroupBox4.Controls.Add(Me.Label15)
        Me.GroupBox4.Controls.Add(Me.Label17)
        Me.GroupBox4.Controls.Add(Me.cmbSpellSlot)
        Me.GroupBox4.Controls.Add(Me.Button4)
        Me.GroupBox4.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(339, 131)
        Me.GroupBox4.TabIndex = 0
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Spells"
        '
        'Label20
        '
        Me.Label20.Location = New System.Drawing.Point(300, 98)
        Me.Label20.Name = "Label20"
        Me.Label20.Size = New System.Drawing.Size(32, 17)
        Me.Label20.TabIndex = 22
        Me.Label20.Text = "s"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label19
        '
        Me.Label19.Location = New System.Drawing.Point(303, 19)
        Me.Label19.Name = "Label19"
        Me.Label19.Size = New System.Drawing.Size(32, 17)
        Me.Label19.TabIndex = 21
        Me.Label19.Text = "s"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSpellCD
        '
        Me.txtSpellCD.Location = New System.Drawing.Point(93, 97)
        Me.txtSpellCD.Name = "txtSpellCD"
        Me.txtSpellCD.Size = New System.Drawing.Size(204, 20)
        Me.txtSpellCD.TabIndex = 18
        Me.txtSpellCD.Text = "1"
        Me.txtSpellCD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtSpellInt
        '
        Me.txtSpellInt.Location = New System.Drawing.Point(93, 18)
        Me.txtSpellInt.Name = "txtSpellInt"
        Me.txtSpellInt.Size = New System.Drawing.Size(204, 20)
        Me.txtSpellInt.TabIndex = 20
        Me.txtSpellInt.Text = "1"
        Me.txtSpellInt.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label18
        '
        Me.Label18.Location = New System.Drawing.Point(6, 18)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(81, 17)
        Me.Label18.TabIndex = 19
        Me.Label18.Text = "Intervalo:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSpellName
        '
        Me.txtSpellName.Location = New System.Drawing.Point(93, 71)
        Me.txtSpellName.Name = "txtSpellName"
        Me.txtSpellName.ReadOnly = True
        Me.txtSpellName.Size = New System.Drawing.Size(204, 20)
        Me.txtSpellName.TabIndex = 15
        Me.txtSpellName.Text = "Nenhum"
        Me.txtSpellName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label16
        '
        Me.Label16.Location = New System.Drawing.Point(6, 71)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(81, 17)
        Me.Label16.TabIndex = 14
        Me.Label16.Text = "Spell:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.Location = New System.Drawing.Point(6, 97)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(81, 17)
        Me.Label15.TabIndex = 17
        Me.Label15.Text = "Cooldown:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label17
        '
        Me.Label17.Location = New System.Drawing.Point(6, 45)
        Me.Label17.Name = "Label17"
        Me.Label17.Size = New System.Drawing.Size(81, 17)
        Me.Label17.TabIndex = 12
        Me.Label17.Text = "Slot:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmbSpellSlot
        '
        Me.cmbSpellSlot.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbSpellSlot.FormattingEnabled = True
        Me.cmbSpellSlot.Items.AddRange(New Object() {"1", "2", "3", "4"})
        Me.cmbSpellSlot.Location = New System.Drawing.Point(93, 44)
        Me.cmbSpellSlot.Name = "cmbSpellSlot"
        Me.cmbSpellSlot.Size = New System.Drawing.Size(204, 21)
        Me.cmbSpellSlot.TabIndex = 13
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(303, 70)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(28, 23)
        Me.Button4.TabIndex = 16
        Me.Button4.Text = "..."
        Me.Button4.UseVisualStyleBackColor = True
        '
        'frmEditor_Npc
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
        Me.Name = "frmEditor_Npc"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Editor de Npc"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.picSprite, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtIndex As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents txtNome As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents picSprite As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents scrlSprite As System.Windows.Forms.HScrollBar
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents txtDropChance As System.Windows.Forms.TextBox
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtDropValue As System.Windows.Forms.TextBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents txtDropItem As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents cmbDropSlot As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents txtRES As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtFOR As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtEXP As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtLevel As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbType As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents scrlPaper As System.Windows.Forms.HScrollBar
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtHP As System.Windows.Forms.TextBox
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents Label20 As System.Windows.Forms.Label
    Friend WithEvents Label19 As System.Windows.Forms.Label
    Friend WithEvents txtSpellInt As System.Windows.Forms.TextBox
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents txtSpellCD As System.Windows.Forms.TextBox
    Friend WithEvents txtSpellName As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label17 As System.Windows.Forms.Label
    Friend WithEvents cmbSpellSlot As System.Windows.Forms.ComboBox
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents txtSpawn As System.Windows.Forms.TextBox
    Friend WithEvents Label21 As System.Windows.Forms.Label
End Class
