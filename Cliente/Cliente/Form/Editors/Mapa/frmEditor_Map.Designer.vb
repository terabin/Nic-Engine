<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditor_Map
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEditor_Map))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.picTile = New System.Windows.Forms.PictureBox()
        Me.scrlLeft = New System.Windows.Forms.HScrollBar()
        Me.scrlTop = New System.Windows.Forms.VScrollBar()
        Me.gpLayer = New System.Windows.Forms.GroupBox()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.RadioButton8 = New System.Windows.Forms.RadioButton()
        Me.RadioButton7 = New System.Windows.Forms.RadioButton()
        Me.RadioButton6 = New System.Windows.Forms.RadioButton()
        Me.RadioButton5 = New System.Windows.Forms.RadioButton()
        Me.RadioButton4 = New System.Windows.Forms.RadioButton()
        Me.RadioButton3 = New System.Windows.Forms.RadioButton()
        Me.RadioButton2 = New System.Windows.Forms.RadioButton()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton()
        Me.rbGround = New System.Windows.Forms.RadioButton()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.scrlMapTile = New System.Windows.Forms.HScrollBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.RadioButton9 = New System.Windows.Forms.RadioButton()
        Me.RadioButton10 = New System.Windows.Forms.RadioButton()
        Me.gpAtr = New System.Windows.Forms.GroupBox()
        Me.RadioButton11 = New System.Windows.Forms.RadioButton()
        Me.chkGrid = New System.Windows.Forms.CheckBox()
        Me.Button5 = New System.Windows.Forms.Button()
        CType(Me.picTile, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gpLayer.SuspendLayout()
        Me.gpAtr.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.AutoSize = True
        Me.Button1.FlatAppearance.BorderSize = 0
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.Location = New System.Drawing.Point(2, 2)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(22, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.AutoSize = True
        Me.Button2.FlatAppearance.BorderSize = 0
        Me.Button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button2.Image = CType(resources.GetObject("Button2.Image"), System.Drawing.Image)
        Me.Button2.Location = New System.Drawing.Point(29, 2)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(22, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.UseVisualStyleBackColor = True
        '
        'picTile
        '
        Me.picTile.BackColor = System.Drawing.Color.YellowGreen
        Me.picTile.Location = New System.Drawing.Point(12, 40)
        Me.picTile.Name = "picTile"
        Me.picTile.Size = New System.Drawing.Size(444, 423)
        Me.picTile.TabIndex = 2
        Me.picTile.TabStop = False
        '
        'scrlLeft
        '
        Me.scrlLeft.LargeChange = 1
        Me.scrlLeft.Location = New System.Drawing.Point(12, 466)
        Me.scrlLeft.Maximum = 0
        Me.scrlLeft.Name = "scrlLeft"
        Me.scrlLeft.Size = New System.Drawing.Size(444, 16)
        Me.scrlLeft.TabIndex = 3
        '
        'scrlTop
        '
        Me.scrlTop.LargeChange = 1
        Me.scrlTop.Location = New System.Drawing.Point(459, 40)
        Me.scrlTop.Maximum = 0
        Me.scrlTop.Name = "scrlTop"
        Me.scrlTop.Size = New System.Drawing.Size(16, 423)
        Me.scrlTop.TabIndex = 4
        '
        'gpLayer
        '
        Me.gpLayer.Controls.Add(Me.Button4)
        Me.gpLayer.Controls.Add(Me.Button3)
        Me.gpLayer.Controls.Add(Me.RadioButton8)
        Me.gpLayer.Controls.Add(Me.RadioButton7)
        Me.gpLayer.Controls.Add(Me.RadioButton6)
        Me.gpLayer.Controls.Add(Me.RadioButton5)
        Me.gpLayer.Controls.Add(Me.RadioButton4)
        Me.gpLayer.Controls.Add(Me.RadioButton3)
        Me.gpLayer.Controls.Add(Me.RadioButton2)
        Me.gpLayer.Controls.Add(Me.RadioButton1)
        Me.gpLayer.Controls.Add(Me.rbGround)
        Me.gpLayer.Controls.Add(Me.Label2)
        Me.gpLayer.Controls.Add(Me.scrlMapTile)
        Me.gpLayer.Controls.Add(Me.Label1)
        Me.gpLayer.Location = New System.Drawing.Point(478, 40)
        Me.gpLayer.Name = "gpLayer"
        Me.gpLayer.Size = New System.Drawing.Size(226, 423)
        Me.gpLayer.TabIndex = 5
        Me.gpLayer.TabStop = False
        Me.gpLayer.Text = "TileSet"
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(34, 328)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(75, 22)
        Me.Button4.TabIndex = 13
        Me.Button4.Text = "Fill"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(115, 327)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 12
        Me.Button3.Text = "Clear"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'RadioButton8
        '
        Me.RadioButton8.AutoSize = True
        Me.RadioButton8.Location = New System.Drawing.Point(122, 157)
        Me.RadioButton8.Name = "RadioButton8"
        Me.RadioButton8.Size = New System.Drawing.Size(89, 17)
        Me.RadioButton8.TabIndex = 11
        Me.RadioButton8.Text = "Fringe 2 Anim"
        Me.RadioButton8.UseVisualStyleBackColor = True
        '
        'RadioButton7
        '
        Me.RadioButton7.AutoSize = True
        Me.RadioButton7.Location = New System.Drawing.Point(12, 157)
        Me.RadioButton7.Name = "RadioButton7"
        Me.RadioButton7.Size = New System.Drawing.Size(63, 17)
        Me.RadioButton7.TabIndex = 10
        Me.RadioButton7.Text = "Fringe 2"
        Me.RadioButton7.UseVisualStyleBackColor = True
        '
        'RadioButton6
        '
        Me.RadioButton6.AutoSize = True
        Me.RadioButton6.Location = New System.Drawing.Point(122, 134)
        Me.RadioButton6.Name = "RadioButton6"
        Me.RadioButton6.Size = New System.Drawing.Size(80, 17)
        Me.RadioButton6.TabIndex = 9
        Me.RadioButton6.Text = "Fringe Anim"
        Me.RadioButton6.UseVisualStyleBackColor = True
        '
        'RadioButton5
        '
        Me.RadioButton5.AutoSize = True
        Me.RadioButton5.Location = New System.Drawing.Point(12, 134)
        Me.RadioButton5.Name = "RadioButton5"
        Me.RadioButton5.Size = New System.Drawing.Size(54, 17)
        Me.RadioButton5.TabIndex = 8
        Me.RadioButton5.Text = "Fringe"
        Me.RadioButton5.UseVisualStyleBackColor = True
        '
        'RadioButton4
        '
        Me.RadioButton4.AutoSize = True
        Me.RadioButton4.Location = New System.Drawing.Point(122, 111)
        Me.RadioButton4.Name = "RadioButton4"
        Me.RadioButton4.Size = New System.Drawing.Size(86, 17)
        Me.RadioButton4.TabIndex = 7
        Me.RadioButton4.Text = "Mask 2 Anim"
        Me.RadioButton4.UseVisualStyleBackColor = True
        '
        'RadioButton3
        '
        Me.RadioButton3.AutoSize = True
        Me.RadioButton3.Location = New System.Drawing.Point(12, 111)
        Me.RadioButton3.Name = "RadioButton3"
        Me.RadioButton3.Size = New System.Drawing.Size(60, 17)
        Me.RadioButton3.TabIndex = 6
        Me.RadioButton3.Text = "Mask 2"
        Me.RadioButton3.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(122, 88)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(77, 17)
        Me.RadioButton2.TabIndex = 5
        Me.RadioButton2.Text = "Mask Anim"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(12, 88)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(51, 17)
        Me.RadioButton1.TabIndex = 4
        Me.RadioButton1.Text = "Mask"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'rbGround
        '
        Me.rbGround.AutoSize = True
        Me.rbGround.Checked = True
        Me.rbGround.Location = New System.Drawing.Point(12, 65)
        Me.rbGround.Name = "rbGround"
        Me.rbGround.Size = New System.Drawing.Size(60, 17)
        Me.rbGround.TabIndex = 3
        Me.rbGround.TabStop = True
        Me.rbGround.Text = "Ground"
        Me.rbGround.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(9, 49)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(38, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Layers"
        '
        'scrlMapTile
        '
        Me.scrlMapTile.LargeChange = 1
        Me.scrlMapTile.Location = New System.Drawing.Point(9, 391)
        Me.scrlMapTile.Maximum = 1
        Me.scrlMapTile.Minimum = 1
        Me.scrlMapTile.Name = "scrlMapTile"
        Me.scrlMapTile.Size = New System.Drawing.Size(203, 16)
        Me.scrlMapTile.TabIndex = 1
        Me.scrlMapTile.Value = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 378)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(52, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "TileSet: 1"
        '
        'RadioButton9
        '
        Me.RadioButton9.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton9.AutoSize = True
        Me.RadioButton9.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton9.Checked = True
        Me.RadioButton9.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.RadioButton9.FlatAppearance.BorderSize = 0
        Me.RadioButton9.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray
        Me.RadioButton9.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton9.Image = CType(resources.GetObject("RadioButton9.Image"), System.Drawing.Image)
        Me.RadioButton9.Location = New System.Drawing.Point(478, 3)
        Me.RadioButton9.Name = "RadioButton9"
        Me.RadioButton9.Size = New System.Drawing.Size(22, 22)
        Me.RadioButton9.TabIndex = 6
        Me.RadioButton9.TabStop = True
        Me.RadioButton9.UseVisualStyleBackColor = False
        '
        'RadioButton10
        '
        Me.RadioButton10.Appearance = System.Windows.Forms.Appearance.Button
        Me.RadioButton10.AutoSize = True
        Me.RadioButton10.BackColor = System.Drawing.Color.Transparent
        Me.RadioButton10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.RadioButton10.FlatAppearance.BorderSize = 0
        Me.RadioButton10.FlatAppearance.CheckedBackColor = System.Drawing.Color.Gray
        Me.RadioButton10.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.RadioButton10.Image = CType(resources.GetObject("RadioButton10.Image"), System.Drawing.Image)
        Me.RadioButton10.Location = New System.Drawing.Point(506, 2)
        Me.RadioButton10.Name = "RadioButton10"
        Me.RadioButton10.Size = New System.Drawing.Size(22, 22)
        Me.RadioButton10.TabIndex = 7
        Me.RadioButton10.UseVisualStyleBackColor = False
        '
        'gpAtr
        '
        Me.gpAtr.Controls.Add(Me.RadioButton11)
        Me.gpAtr.Location = New System.Drawing.Point(478, 40)
        Me.gpAtr.Name = "gpAtr"
        Me.gpAtr.Size = New System.Drawing.Size(226, 423)
        Me.gpAtr.TabIndex = 8
        Me.gpAtr.TabStop = False
        Me.gpAtr.Text = "Atributos"
        Me.gpAtr.Visible = False
        '
        'RadioButton11
        '
        Me.RadioButton11.AutoSize = True
        Me.RadioButton11.Checked = True
        Me.RadioButton11.Location = New System.Drawing.Point(13, 19)
        Me.RadioButton11.Name = "RadioButton11"
        Me.RadioButton11.Size = New System.Drawing.Size(52, 17)
        Me.RadioButton11.TabIndex = 0
        Me.RadioButton11.TabStop = True
        Me.RadioButton11.Text = "Block"
        Me.RadioButton11.UseVisualStyleBackColor = True
        '
        'chkGrid
        '
        Me.chkGrid.Appearance = System.Windows.Forms.Appearance.Button
        Me.chkGrid.AutoSize = True
        Me.chkGrid.FlatAppearance.BorderSize = 0
        Me.chkGrid.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.chkGrid.Image = CType(resources.GetObject("chkGrid.Image"), System.Drawing.Image)
        Me.chkGrid.Location = New System.Drawing.Point(372, 2)
        Me.chkGrid.Name = "chkGrid"
        Me.chkGrid.Size = New System.Drawing.Size(22, 22)
        Me.chkGrid.TabIndex = 9
        Me.chkGrid.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.AutoSize = True
        Me.Button5.FlatAppearance.BorderSize = 0
        Me.Button5.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button5.Image = CType(resources.GetObject("Button5.Image"), System.Drawing.Image)
        Me.Button5.Location = New System.Drawing.Point(57, 1)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(22, 23)
        Me.Button5.TabIndex = 10
        Me.Button5.UseVisualStyleBackColor = True
        '
        'frmEditor_Map
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(707, 537)
        Me.ControlBox = False
        Me.Controls.Add(Me.Button5)
        Me.Controls.Add(Me.chkGrid)
        Me.Controls.Add(Me.RadioButton10)
        Me.Controls.Add(Me.RadioButton9)
        Me.Controls.Add(Me.gpLayer)
        Me.Controls.Add(Me.scrlTop)
        Me.Controls.Add(Me.scrlLeft)
        Me.Controls.Add(Me.picTile)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.gpAtr)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmEditor_Map"
        Me.Text = "Editor de Mapas"
        CType(Me.picTile, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gpLayer.ResumeLayout(False)
        Me.gpLayer.PerformLayout()
        Me.gpAtr.ResumeLayout(False)
        Me.gpAtr.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents scrlLeft As System.Windows.Forms.HScrollBar
    Friend WithEvents scrlTop As System.Windows.Forms.VScrollBar
    Public WithEvents picTile As System.Windows.Forms.PictureBox
    Friend WithEvents gpLayer As System.Windows.Forms.GroupBox
    Friend WithEvents scrlMapTile As System.Windows.Forms.HScrollBar
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents RadioButton8 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton7 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton6 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton5 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton4 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton3 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Friend WithEvents rbGround As System.Windows.Forms.RadioButton
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents RadioButton9 As System.Windows.Forms.RadioButton
    Friend WithEvents RadioButton10 As System.Windows.Forms.RadioButton
    Friend WithEvents gpAtr As System.Windows.Forms.GroupBox
    Friend WithEvents RadioButton11 As System.Windows.Forms.RadioButton
    Friend WithEvents chkGrid As System.Windows.Forms.CheckBox
    Friend WithEvents Button5 As System.Windows.Forms.Button
End Class
