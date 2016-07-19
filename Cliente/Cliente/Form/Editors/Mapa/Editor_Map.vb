Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Module Editor_Map

    Public Sub SaveEditor_Map()
        SendSaveMap()
    End Sub

    Public Sub CancelEditor_Map()
        SendRequestMap(1)
    End Sub

    Public Sub OpenEditor_Map()
        frmEditor_Map.scrlMapTile.Value = 1
        frmEditor_Map.scrlMapTile.Maximum = texTile.Length - 1
        frmEditor_Map.LayerNum = 1
        frmEditor_Map.rbGround.Checked = True
        frmEditor_Map.Show()
    End Sub

    Public Sub RenderEditor_MapTile()
        DeviceMap.DispatchEvents()
        DeviceMap.Clear(New Color(100, 200, 100))

        Dim idTexture As Integer = texTile(frmEditor_Map.scrlMapTile.Value)
        GlobalTexture(idTexture).Check()

        Dim vertex As New VertexArray(PrimitiveType.TrianglesStrip)
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.scrlLeft.Value * -32, frmEditor_Map.scrlTop.Value * -32), Color.White, New Vector2f(0, 0)))
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.scrlLeft.Value * -32 + GetTextureSize(idTexture).X, frmEditor_Map.scrlTop.Value * -32), Color.White, New Vector2f(GetTextureSize(idTexture).X, 0)))
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.scrlLeft.Value * -32, frmEditor_Map.scrlTop.Value * -32 + GetTextureSize(idTexture).Y), Color.White, New Vector2f(0, GetTextureSize(idTexture).Y)))
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.scrlLeft.Value * -32 + GetTextureSize(idTexture).X, frmEditor_Map.scrlTop.Value * -32 + GetTextureSize(idTexture).Y), Color.White, GetTextureSize(idTexture)))
        Dim render As RenderStates = RenderStates.Default
        render.Texture = GlobalTexture(idTexture).Textura
        DeviceMap.Draw(vertex, render)

        ' Mouse Position
        vertex = New VertexArray(PrimitiveType.LinesStrip)
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.MousePos.Left * 32, frmEditor_Map.MousePos.Top * 32), Color.Red))
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.MousePos.Left * 32 + frmEditor_Map.MousePos.Width * 32, frmEditor_Map.MousePos.Top * 32), Color.Red))
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.MousePos.Left * 32 + frmEditor_Map.MousePos.Width * 32, frmEditor_Map.MousePos.Top * 32 + frmEditor_Map.MousePos.Height * 32), Color.Red))
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.MousePos.Left * 32, frmEditor_Map.MousePos.Top * 32 + frmEditor_Map.MousePos.Height * 32), Color.Red))
        vertex.Append(New Vertex(New Vector2f(frmEditor_Map.MousePos.Left * 32, frmEditor_Map.MousePos.Top * 32), Color.Red))
        DeviceMap.Draw(vertex)
        DeviceMap.Display()
    End Sub

    Public Sub MouseEditor_Down(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim X As Short = TileView.Left + ((e.Location.X + Camera.Left) \ 32)
        Dim Y As Short = TileView.Top + ((e.Location.Y + Camera.Top) \ 32)
        Dim LayerNum As Byte = frmEditor_Map.LayerNum

        If e.Button = MouseButtons.Left Then
            If frmEditor_Map.gpLayer.Visible Then
                If frmEditor_Map.MousePos.Width = 1 And frmEditor_Map.MousePos.Height = 1 Then
                    If X < 0 Or Y < 0 Or X > Map.MaxX Or Y > Map.MaxY Then Return
                    Map.Tile(X, Y)(LayerNum).Insert(frmEditor_Map.scrlMapTile.Value, frmEditor_Map.MousePos.Left * 32, frmEditor_Map.MousePos.Top * 32)
                Else
                    For x2 As Short = 0 To frmEditor_Map.MousePos.Width - 1
                        For y2 As Short = 0 To frmEditor_Map.MousePos.Height - 1
                            Map.Tile(X + x2, Y + y2)(LayerNum).Insert(frmEditor_Map.scrlMapTile.Value, frmEditor_Map.MousePos.Left * 32 + 32 * x2, frmEditor_Map.MousePos.Top * 32 + 32 * y2)
                        Next
                    Next
                End If
            Else
                Map.Tile(X, Y).Type = frmEditor_Map.TileTypeSet
            End If

        ElseIf e.Button = MouseButtons.Right Then
            If frmEditor_Map.gpLayer.Visible Then
                Map.Tile(X, Y)(LayerNum).Insert(0, 0, 0)
            Else
                Map.Tile(X, Y).Type = TileType.None
            End If
        End If
    End Sub
End Module
