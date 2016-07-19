Imports SFML
Imports SFML.System
Imports SFML.Graphics

Module modChat
    Public Const MAX_CHAT As Byte = 8
End Module

Public Class Chat
    Public Shared Buffer() As ChatText

    Class ChatText
        Public Colour As Color
        Public Text As String

        Public Sub New()
            Text = ""
            Colour = Color.White
        End Sub
    End Class

    Public Shared Sub Load()
        ReDim Buffer(MAX_CHAT + 1)

        For i As Byte = 1 To MAX_CHAT
            Buffer(i) = New ChatText
        Next
    End Sub

    Public Shared Sub Draw()
        Dim X As Short, Y As Short

        X = 8
        Y = DeviceGame.Size.Y - 40

        For i As Byte = 1 To MAX_CHAT
            If Not Buffer(i) Is Nothing Then
                If Len(Buffer(i).Text.Trim) > 0 Then
                    RenderText(Buffer(i).Text, X, Y - (14 * (i - 1)), Buffer(i).Colour, , , True)
                End If
            End If
        Next
    End Sub

    Public Shared Sub Add(ByVal Text As String)
        If Buffer.Count = 0 Then Return
        For i As Short = MAX_CHAT To 2 Step -1
            Buffer(i).Text = Buffer(i - 1).Text
            Buffer(i).Colour = Buffer(i - 1).Colour
        Next
        Buffer(1).Text = Text
        Buffer(1).Colour = Color.White
    End Sub

    Public Shared Sub Add(ByVal Text As String, ByVal Colour As Color)
        If Buffer.Count = 0 Then Return
        For i As Short = MAX_CHAT To 2 Step -1
            Buffer(i).Text = Buffer(i - 1).Text
            Buffer(i).Colour = Buffer(i - 1).Colour
        Next
        Buffer(1).Text = Text
        Buffer(1).Colour = Colour
    End Sub
End Class
