Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Module Editor_Item

    Public Sub RenderEditor_ItemIcon()
        DeviceItemIcon.DispatchEvents()
        DeviceItemIcon.Clear(Color.Black)

        If frmEditor_Item.scrlIcon.Value > 0 Then
            Dim icon As Integer = frmEditor_Item.scrlIcon.Value

            If icon > 0 And icon < texItem.Count Then
                Dim shape As Shape = New RectangleShape(New Vector2f(32, 32))
                GlobalTexture(texItem(icon)).Check()
                shape.Texture = GlobalTexture(texItem(icon)).Textura
                shape.TextureRect = New IntRect(0, 0, 32, 32)
                DeviceItemIcon.Draw(shape)
                GlobalTexture(texItem(icon)).Timer = GetTickCount + 5000
            End If
        End If

        DeviceItemIcon.Display()
    End Sub
End Module
