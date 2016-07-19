
Module modDrop
    Public Const DROP_TIMER As Integer = (1000 * 60) * 3 ' 3 Minutos
    Public Drop() As DropData
End Module

Public Class DropData
    Public Item() As DropItemData
    Public Class DropItemData
        Inherits clsInv

        Public X As Short
        Public Y As Short
        Public Timer As Integer
    End Class

    Public Sub New()
        ReDim Item(Options.MAX_MAP_ITEM + 1)

        For i As Short = 1 To Options.MAX_MAP_ITEM
            Item(i) = New DropItemData
        Next
    End Sub

    Default Public Property Base(ByVal id As Integer) As DropItemData
        Get
            Return Item(id)
        End Get
        Set(ByVal value As DropItemData)
            Item(id) = value
        End Set
    End Property

    Public Function Add(ByVal Id As Integer, ByVal Valor As Integer, ByVal X As Short, ByVal Y As Short) As Short
        For i As Short = 1 To Options.MAX_MAP_ITEM
            If Item(i).Num = 0 Then
                Item(i).Num = Id
                Item(i).Value = Valor
                Item(i).X = X
                Item(i).Y = Y
                Item(i).Timer = GetTickCount + DROP_TIMER

                For o As Short = 0 To Options.MAX_INV_GEMA - 1
                    Item(i).GemaSlot(o) = 0
                Next
                Return i
            End If
        Next
        Return 0
    End Function

    Public Function Add(ByVal Id As Integer, ByVal Valor As Integer, ByVal X As Short, ByVal Y As Short, ByVal Gemas() As Integer) As Short
        For i As Short = 1 To Options.MAX_MAP_ITEM
            If Item(i).Num = 0 Then
                Item(i).Num = Id
                Item(i).Value = Valor
                Item(i).X = X
                Item(i).Y = Y
                Item(i).Timer = GetTickCount + DROP_TIMER

                For o As Short = 0 To Options.MAX_INV_GEMA - 1
                    Item(i).GemaSlot(o) = Gemas(o)
                Next
                Return i
            End If
        Next
        Return 0
    End Function

    Public Function Remove(ByVal itemIndex As Integer) As DropData
        With Item(itemIndex)
            .Num = 0
            .Value = 0
            .X = 0
            .Y = 0
            .Timer = 0
            For o As Short = 0 To Options.MAX_INV_GEMA - 1
                .GemaSlot(o) = 0
            Next
        End With
        Return Me
    End Function
End Class
