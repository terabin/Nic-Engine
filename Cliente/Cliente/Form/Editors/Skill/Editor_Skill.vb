Imports SFML.Graphics
Imports SFML.System

Module Editor_Skill
    Public Sub RenderEditor_Skill()

        DeviceSkill.DispatchEvents()
        DeviceSkill.Clear(Color.Black)

        If frmEditor_Skill.scrlIcon.Value > 0 Then
            Dim idIcon As Short = frmEditor_Skill.scrlIcon.Value

            GlobalTexture(texSkill(idIcon)).Check()
            Dim size As Vector2f = GetTextureSize(texSkill(idIcon))

            Dim s As Shape = New RectangleShape(New Vector2f(32, 32))
            s.Texture = GlobalTexture(texSkill(idIcon)).Textura
            s.TextureRect = New IntRect(New Vector2f(0, 0), size)
            DeviceSkill.Draw(s)
            GlobalTexture(texSkill(idIcon)).Timer = GetTickCount + 5000
        End If

        DeviceSkill.Display()
    End Sub
End Module
