Public Class frmEditor_MapProp

    Public tempSpawn(Options.MAX_MAP_NPCS) As Integer
    Public isOpened As Boolean
    Private Sub frmEditor_MapProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        isOpened = False
        ' Listar Musica
        lstMusic.Items.Clear()

        Dim di As New IO.DirectoryInfo(Musica.PATH)
        Dim aryFi As IO.FileInfo() = di.GetFiles("*.*")
        Dim fi As IO.FileInfo
        lstMusic.Items.Add("None")

        For Each fi In aryFi
            lstMusic.Items.Add(fi.Name)
        Next

        If Map.Musica.Trim.Length = 0 Then
            lstMusic.SelectedIndex = 0
        Else
            For i As Short = 0 To lstMusic.Items.Count - 1
                If lstMusic.Items(i) = Map.Musica Then
                    lstMusic.SelectedIndex = i
                End If
            Next
        End If

        ' Spawn
        lstNpc.Items.Clear()
        For i As Short = 0 To Options.MAX_MAP_NPCS - 1
            tempSpawn(i) = Map.Spawn(i)
            If Map.Spawn(i) > 0 Then
                ' // NO
                lstNpc.Items.Add(Trim(Npc(Map.Spawn(i)).Nome))
            Else
                lstNpc.Items.Add("...")
            End If
        Next
        lstNpc.SelectedIndex = 0

        ' Default
        cmbMoral.SelectedIndex = 0
        cmbNPC.Items.Clear()
        cmbNPC.Items.Add("Nenhum")

        For i As Short = 1 To Options.MAX_NPC
            cmbNPC.Items.Add(Trim(Npc(i).Nome))
        Next

        cmbNPC.SelectedIndex = 0



        ' Size map
        txtMaxX.Text = Map.MaxX
        txtMaxY.Text = Map.MaxY

        ' Warp map
        txtTop.Text = Map.Top
        txtLeft.Text = Map.Left
        txtBottom.Text = Map.Bottom
        txtRight.Text = Map.Right
        isOpened = True
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.DialogResult = DialogResult.OK

        ' ReSize
        If txtMaxX.Text <> Map.MaxX Or txtMaxY.Text <> Map.MaxY Then
            Dim tmpTile(Map.MaxX + 1, Map.MaxY + 1) As TileData
            tmpTile = Map.Tile.Clone

            Dim tmpX As Short = Map.MaxX
            Dim tmpY As Short = Map.MaxY
            Map.MaxX = txtMaxX.Text
            Map.MaxY = txtMaxY.Text
            ReDim Map.Tile(Map.MaxX + 1, Map.MaxY + 1)

            If Map.MaxX > tmpX Or Map.MaxY > tmpY Then
                For X As Short = 0 To Map.MaxX
                    For Y As Short = 0 To Map.MaxY
                        Map.Tile(X, Y) = New TileData()
                    Next
                Next
            End If

            If Map.MaxX < tmpX Then tmpX = Map.MaxX
            If Map.MaxY < tmpY Then tmpY = Map.MaxY

            For X As Short = 0 To tmpX
                For Y As Short = 0 To tmpY
                    Map.Tile(X, Y) = tmpTile(X, Y)
                Next
            Next

            Erase tmpTile
        End If

        ' Outros
        Map.Nome = txtNome.Text
        Map.Zona = cmbMoral.SelectedIndex
        Map.Musica = lstMusic.SelectedItem
        Map.Top = txtTop.Text
        Map.Left = txtLeft.Text
        Map.Bottom = txtBottom.Text
        Map.Right = txtRight.Text

        ' Spawn
        For i As Short = 0 To Options.MAX_MAP_NPCS
            Map.Spawn(i) = tempSpawn(i)
        Next
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Play" Then
            If lstMusic.SelectedIndex > 0 Then Musica.Play(lstMusic.SelectedItem)
            Button1.Text = "Stop"
        ElseIf Button1.Text = "Stop" Then
            Musica.Stop()
            Button1.Text = "Play"
        End If
    End Sub

    Private Sub txtMaxX_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaxX.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_X
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < Options.MAX_X Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_X
    End Sub

    Private Sub txtMaxY_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtMaxY.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_Y
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < Options.MAX_Y Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_Y
    End Sub

    Private Sub cmbMoral_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbMoral.SelectedIndexChanged

    End Sub

    Private Sub txtMaxX_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMaxX.TextChanged

    End Sub

    Private Sub txtTop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtTop.TextChanged
        
    End Sub

    Private Sub txtTop_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtTop.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text > Options.MAX_MAPS Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_MAPS
    End Sub

    Private Sub txtRight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRight.TextChanged

    End Sub

    Private Sub txtRight_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRight.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text > Options.MAX_MAPS Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_MAPS
    End Sub

    Private Sub txtLeft_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLeft.TextChanged

    End Sub

    Private Sub txtLeft_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLeft.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text > Options.MAX_MAPS Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_MAPS
    End Sub

    Private Sub txtBottom_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtBottom.TextChanged

    End Sub

    Private Sub txtBottom_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtBottom.Validated
        If Not IsNumeric(DirectCast(sender, System.Windows.Forms.TextBox).Text) Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text < 0 Then DirectCast(sender, System.Windows.Forms.TextBox).Text = 0
        If DirectCast(sender, System.Windows.Forms.TextBox).Text > Options.MAX_MAPS Then DirectCast(sender, System.Windows.Forms.TextBox).Text = Options.MAX_MAPS
    End Sub

    Private Sub cmbNPC_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbNPC.SelectedIndexChanged
        'Dim i As Integer = lstNpc.SelectedIndex
        If Not isOpened Then Return
        'tempSpawn(i) = cmbNPC.SelectedIndex
        Dim c As ListBox.SelectedIndexCollection = lstNpc.SelectedIndices
        Dim tmp() As Integer
        ReDim tmp(c.Count)

        ' Copy Index
        For i As Short = 0 To c.Count - 1
            tmp(i) = c(i)
        Next

        ' Att dados
        For i As Short = 0 To tmp.Count - 1
            tempSpawn(tmp(i)) = cmbNPC.SelectedIndex
            lstNpc.Items(tmp(i)) = cmbNPC.SelectedItem
        Next

    End Sub
End Class