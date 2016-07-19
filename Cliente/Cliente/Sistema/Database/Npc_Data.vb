Module modNpcData
    Public Npc() As NpcData
End Module

Public Class NpcData
    Public Nome As String = ""
    Public Tipo As Short
    Public Sprite As Short
    Public Paperdoll As Short
    Public HP As Long
    Public Level As Short
    Public EXP As Long
    Public STR As Integer
    Public CON As Integer
    Public Drop() As DropData
    Public NpcSpell() As NpcSpellData
    Public NpcSpellIntervalo As Short
    Public SpawnTime As Short

    Public Sub New()
        ReDim Drop(4)
        ReDim NpcSpell(4)

        For i As Short = 0 To UBound(Drop)
            Drop(i) = New DropData
        Next

        For i As Short = 0 To UBound(NpcSpell)
            NpcSpell(i) = New NpcSpellData
        Next
    End Sub

    Class DropData
        Public Num As Integer
        Public Value As Integer
        Public Chance As Short

        Public Sub New()
            Num = 0
            Value = 0
            Chance = 1
        End Sub
    End Class

    Class NpcSpellData
        Public Num As Integer
        Public CD As Short

        Public Sub New()
            Num = 0
            CD = 0
        End Sub
    End Class
End Class

