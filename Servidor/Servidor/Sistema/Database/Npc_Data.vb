
Module modNpcData
    Public Npc() As NpcData

    Public Sub LoadNpcs()
        For i As Short = 1 To Options.MAX_NPC
            Npc(i).Load()
        Next
    End Sub
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

        Clear()
    End Sub

    Public Sub Clear()
        Nome = ""
        Tipo = 0
        Sprite = 0
        Paperdoll = 0
        HP = 0
        Level = 0
        EXP = 0
        STR = 0
        CON = 0

        For i As Short = 0 To UBound(Drop)
            Drop(i).Num = 0
            Drop(i).Value = 0
            Drop(i).Chance = 1
        Next

        For i As Short = 0 To UBound(NpcSpell)
            NpcSpell(i).Num = 0
            NpcSpell(i).CD = 0
        Next
    End Sub

    Public Sub Load()
        Dim fileName As String = Application.StartupPath & "\Data\Npc\" & ID & ".bin"
        If Not IO.File.Exists(fileName) Then Save()

        Dim s As New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)
        Using r As New IO.BinaryReader(s)
            Nome = r.ReadString
            Tipo = r.ReadInt16
            Sprite = r.ReadInt16
            Paperdoll = r.ReadInt16
            HP = r.ReadInt64
            Level = r.ReadInt16
            EXP = r.ReadInt64
            STR = r.ReadInt32
            CON = r.ReadInt32
            SpawnTime = r.ReadInt16

            For i As Short = 0 To UBound(Drop)
                Drop(i).Num = r.ReadInt32
                Drop(i).Value = r.ReadInt32
                Drop(i).Chance = r.ReadInt16
            Next

            For i As Short = 0 To UBound(NpcSpell)
                NpcSpell(i).Num = r.ReadInt32
                NpcSpell(i).CD = r.ReadInt16
            Next
            r.Close()
        End Using
    End Sub

    Public Sub Save()
        Dim fileName As String = Application.StartupPath & "\Data\Npc\" & ID & ".bin"
        Dim s As New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using w As New IO.BinaryWriter(s)
            w.Write(Nome)
            w.Write(Tipo)
            w.Write(Sprite)
            w.Write(Paperdoll)
            w.Write(HP)
            w.Write(Level)
            w.Write(EXP)
            w.Write(STR)
            w.Write(CON)
            w.Write(SpawnTime)

            For i As Short = 0 To UBound(Drop)
                w.Write(Drop(i).Num)
                w.Write(Drop(i).Value)
                w.Write(Drop(i).Chance)
            Next

            For i As Short = 0 To UBound(NpcSpell)
                w.Write(NpcSpell(i).Num)
                w.Write(NpcSpell(i).CD)
            Next
            w.Close()
        End Using

    End Sub

    Public ReadOnly Property ID As Integer
        Get
            Return Array.IndexOf(Npc, Me)
        End Get
    End Property

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