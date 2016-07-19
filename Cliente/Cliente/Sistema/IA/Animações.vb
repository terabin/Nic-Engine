Imports SFML.System
Imports SFML.Graphics

Module Animações
    Public Const MSGANIM_VELO As Single = 0.7
    Public Const MSGANIM_TIMER As Integer = 2000 ' 3 segundos
    Public MsgAnim As MsgAnimBuffer
End Module

Public Class MsgAnimBuffer
    Public Item() As MsgAnimBufferData
    Public Class MsgAnimBufferData
        Public X As Single
        Public Y As Single
        Public Text As String
        Public Color As Color
        Public TimerEnd As Integer
    End Class

    Public Sub New()
        ReDim Item(Options.MAX_MSGANIM + 1)

        For i As Short = 1 To Item.Count - 1
            Item(i) = New MsgAnimBufferData
        Next
    End Sub

    Default Public Property Base(ByVal Index As Short) As MsgAnimBufferData
        Get
            Return Item(Index)
        End Get
        Set(ByVal value As MsgAnimBufferData)
            Item(Index) = value
        End Set
    End Property

    Public Sub Add(ByVal Text As String, ByVal X As Single, ByVal Y As Single, ByVal Colour As Color)
        If Len(Trim(Text)) = 0 Then Return
        For i As Short = 1 To Item.Count - 1
            If Item(i).TimerEnd = 0 Then
                Item(i).Text = Text
                Item(i).X = X
                Item(i).Y = Y
                Item(i).Color = Colour
                Item(i).TimerEnd = GetTickCount + MSGANIM_TIMER
                Return
            End If
        Next
    End Sub

    Public Sub Remove(ByVal Index As Short)
        Item(Index).Text = ""
        Item(Index).X = 0
        Item(Index).Y = 0
        Item(Index).TimerEnd = 0
    End Sub

    Public Sub ClearAll()
        For i As Short = 1 To Item.Count - 1
            Remove(i)
        Next
    End Sub
End Class

Public Class clsAnimationBufferBase
    Public Frame As Integer
    Public AnimationID As Integer
    Public X As Short
    Public Y As Short
    Public Timer As Integer

    Public ReadOnly Property Dados As AnimData
        Get
            Return Animation(AnimationID)
        End Get
    End Property

    Public Property Position As Vector2f
        Get
            Return New Vector2f(X, Y)
        End Get
        Set(value As Vector2f)
            X = value.X
            Y = value.Y
        End Set
    End Property
End Class