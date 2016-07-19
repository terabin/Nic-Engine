Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Module Editor_Anim
    Private editorAnim_Timer As Integer
    Private editorAnim_Frame As Integer
    Public Sub RenderEditor_Animation()
        DeviceAnim.DispatchEvents()
        DeviceAnim.Clear(New Color(70, 70, 70))

        If frmEditor_Anim.scrlAnim.Value > 0 Then
            Dim anim As Short = frmEditor_Anim.scrlAnim.Value

            GlobalTexture(texAnimation(anim)).Check()
            Dim s As Shape = New RectangleShape(New Vector2f(frmEditor_Anim.picAnim.Width, frmEditor_Anim.picAnim.Height))
            Dim sizeT As Vector2f = GetTextureSize(texAnimation(anim))
            s.Texture = GlobalTexture(texAnimation(anim)).Textura
            s.TextureRect = New IntRect((editorAnim_Frame Mod CInt(frmEditor_Anim.txtFX.Text)) * (sizeT.X / CInt(frmEditor_Anim.txtFX.Text)), (editorAnim_Frame \ CInt(frmEditor_Anim.txtFX.Text)) * (sizeT.Y / CInt(frmEditor_Anim.txtFY.Text)), sizeT.X / CInt(frmEditor_Anim.txtFX.Text), sizeT.Y / CInt(frmEditor_Anim.txtFY.Text))
            s.FillColor = New Color(frmEditor_Anim.picColor.BackColor.R, frmEditor_Anim.picColor.BackColor.G, frmEditor_Anim.picColor.BackColor.B, frmEditor_Anim.picColor.BackColor.A)
            If GetTickCount > editorAnim_Timer Then
                editorAnim_Frame += 1
                If editorAnim_Frame > CInt(frmEditor_Anim.txtFrameCount.Text) Then editorAnim_Frame = 0
                editorAnim_Timer = GetTickCount + CInt(frmEditor_Anim.txtMS.Text)
            End If
            Dim render As RenderStates = RenderStates.Default
            If CBool(frmEditor_Anim.chkBlend.Checked) Then render.BlendMode = BlendMode.Add
            DeviceAnim.Draw(s, render)
            GlobalTexture(texAnimation(anim)).Timer = GetTickCount + 5000
        End If

        DeviceAnim.Display()
    End Sub
End Module
