Imports SFML
Imports SFML.Window
Imports SFML.System
Imports SFML.Graphics

Module SFML_Text
    Public fontString As Text

    Public Sub InitFontText()
        fontString = New Text("", GameFont, 12)
    End Sub

    Public Sub RenderText(ByVal Text As String, ByVal X As Short, ByVal Y As Short, ByVal Color As Color, Optional ByVal FontSize As Single = 12, Optional ByVal FontBold As Boolean = False, Optional ByVal Shadow As Boolean = False)
        fontString.DisplayedString = Text
        fontString.CharacterSize = FontSize
        fontString.Style = Graphics.Text.Styles.Regular

        If FontBold Then fontString.Style = Graphics.Text.Styles.Bold

        If Shadow Then
            fontString.Color = New Color(40, 40, 40)
            fontString.Position = New Vector2f(X + 0.5, Y + 0.5)
            DeviceGame.Draw(fontString)
        End If

        ' Normal
        fontString.Color = Color
        fontString.Position = New Vector2f(X, Y)
        DeviceGame.Draw(fontString)
    End Sub

    Public Function GetTextWidth(ByVal text As String, Optional ByVal FontSize As Single = 12, Optional ByVal FontBold As Boolean = False) As Short
        fontString.DisplayedString = text
        fontString.Position = New Vector2f(0, 0)
        fontString.CharacterSize = FontSize

        fontString.Style = Graphics.Text.Styles.Regular
        If FontBold Then fontString.Style = Graphics.Text.Styles.Bold
        Return fontString.GetGlobalBounds().Width + fontString.GetGlobalBounds.Left
    End Function

    Public Function WordWrap_Array(ByVal Text As String, ByVal MaxLineLen As Long, Optional ByVal SizeFont As Single = 12) As String()
        Dim lineCount As Long, i As Long, Size As Long, lastSpace As Long, B As Long
        Dim theArray() As String
        ReDim theArray(0)
        'Too small of text
        If Len(Text) < 2 Then
            ReDim theArray(0 To 1)
            theArray(1) = Text
            Return theArray
        End If

        ' default values
        B = 1
        lastSpace = 1
        Size = 0
        For i = 1 To Len(Text)
            ' if it's a space, store it
            Select Case Mid(Text, i, 1)
                Case " " : lastSpace = i
                Case "_" : lastSpace = i
                Case "-" : lastSpace = i
            End Select

            'Add up the size
            Size = Size + GetTextWidth(Mid(Text, i, 1), SizeFont)


            'Check for too large of a size
            If Size > MaxLineLen Then
                'Check if the last space was too far back
                If i - lastSpace > 12 Then
                    'Too far away to the last space, so break at the last character
                    lineCount = lineCount + 1
                    ReDim Preserve theArray(0 To lineCount)

                    theArray(lineCount) = Trim(Mid(Text, B, (i - 1) - B))
                    B = i - 1
                    Size = 0
                Else
                    'Break at the last space to preserve the word
                    lineCount = lineCount + 1
                    ReDim Preserve theArray(0 To lineCount)
                    theArray(lineCount) = Trim(Mid(Text, B, lastSpace - B))
                    B = lastSpace + 1

                    'Count all the words we ignored (the ones that weren't printed, but are before "i")
                    Size = GetTextWidth(Mid(Text, lastSpace, i - lastSpace), SizeFont)
                End If
            End If

            ' Remainder
            If i = Len(Text) Then
                If B <> i Then
                    lineCount = lineCount + 1
                    ReDim Preserve theArray(0 To lineCount)
                    theArray(lineCount) = theArray(lineCount) & Mid(Text, B, i)
                End If
            End If
nextI:
        Next
        Return theArray
    End Function

End Module
