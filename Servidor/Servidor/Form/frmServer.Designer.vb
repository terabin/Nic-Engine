<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServer
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
        Me.lstView = New System.Windows.Forms.ListView()
        Me.colIndex = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colIPAddress = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colAccount = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.colCharacter = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(9, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(59, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Jogadores:"
        '
        'lstView
        '
        Me.lstView.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.colIndex, Me.colIPAddress, Me.colAccount, Me.colCharacter})
        Me.lstView.FullRowSelect = True
        Me.lstView.GridLines = True
        Me.lstView.HideSelection = False
        Me.lstView.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lstView.LabelEdit = True
        Me.lstView.Location = New System.Drawing.Point(12, 46)
        Me.lstView.MultiSelect = False
        Me.lstView.Name = "lstView"
        Me.lstView.ShowItemToolTips = True
        Me.lstView.Size = New System.Drawing.Size(487, 219)
        Me.lstView.TabIndex = 2
        Me.lstView.UseCompatibleStateImageBehavior = False
        Me.lstView.View = System.Windows.Forms.View.Details
        '
        'colIndex
        '
        Me.colIndex.Text = "ID"
        Me.colIndex.Width = 50
        '
        'colIPAddress
        '
        Me.colIPAddress.Text = "Endereço de IP"
        Me.colIPAddress.Width = 150
        '
        'colAccount
        '
        Me.colAccount.Text = "Conta"
        Me.colAccount.Width = 140
        '
        'colCharacter
        '
        Me.colCharacter.Text = "Personagem"
        Me.colCharacter.Width = 140
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(507, 46)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(141, 19)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Acesso"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'frmServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(660, 297)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.lstView)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.ImeMode = System.Windows.Forms.ImeMode.[On]
        Me.MaximizeBox = False
        Me.Name = "frmServer"
        Me.Text = "Servidor"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents lstView As System.Windows.Forms.ListView
    Public WithEvents colIndex As System.Windows.Forms.ColumnHeader
    Public WithEvents colIPAddress As System.Windows.Forms.ColumnHeader
    Public WithEvents colAccount As System.Windows.Forms.ColumnHeader
    Public WithEvents colCharacter As System.Windows.Forms.ColumnHeader
    Friend WithEvents Button1 As System.Windows.Forms.Button

End Class
