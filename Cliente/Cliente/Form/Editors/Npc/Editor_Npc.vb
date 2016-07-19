Imports System.ComponentModel
Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Module Editor_Npc
    Public editorNpc_Drop() As NpcData.DropData
    Public editorNpc_Spell() As NpcData.NpcSpellData

    Public Sub OpenEditor_NPC()
        ReDim editorNpc_Drop(4)
        ReDim editorNpc_Spell(4)

        For i As Short = 0 To 3
            editorNpc_Drop(i) = New NpcData.DropData
            editorNpc_Spell(i) = New NpcData.NpcSpellData
        Next

        frmEditor_Npc.scrlSprite.Maximum = texChar.Length - 1
        frmEditor_Npc.Show()
    End Sub

    Public Sub RenderEditor_NPCSprite()
        DeviceNpcSprite.DispatchEvents()
        DeviceNpcSprite.Clear(SFML.Graphics.Color.Magenta)

        Dim idSprite As Integer = frmEditor_Npc.scrlSprite.Value
        If idSprite > 0 And idSprite < texChar.Length Then
            Dim s As Shape = New RectangleShape(New Vector2f(50, 50))
            GlobalTexture(texChar(idSprite)).Check()
            s.Texture = GlobalTexture(texChar(idSprite)).Textura
            s.TextureRect = New IntRect(0, 0, 50, 50)
            DeviceNpcSprite.Draw(s)
            GlobalTexture(texChar(idSprite)).Timer = GetTickCount + 5000
        End If
        DeviceNpcSprite.Display()
    End Sub
End Module


