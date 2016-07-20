Module modItemData
    Public Item() As ItemData

    Public Sub LoadItems()
        For i As Short = 1 To Options.MAX_ITEM
            Item(i).Load()
        Next
    End Sub
End Module

Public Class ItemData
    Public Nome As String = ""
    Public Icon As Short
    Public Tipo As Byte
    Public isDrop As Byte
    Public isStack As Byte
    Public Peso As Integer
    Public Desc As String = ""
    Public PotionMode As Byte
    Public PotionVital As Integer
    Public PotionCD As Short
    Public PotionEffect As Short
    Public EquipTipo As Byte
    Public EquipPaper As Short
    Public EquipHP As Integer
    Public EquipMP As Integer
    Public EquipFOR As Integer
    Public EquipCON As Integer

    Public Sub Save()
        Dim fileName As String = Application.StartupPath & "\Data\Item\" & ID & ".bin"
        Dim s As New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using w As New IO.BinaryWriter(s)
            w.Write(Nome)
            w.Write(Icon)
            w.Write(Tipo)
            w.Write(isDrop)
            w.Write(isStack)
            w.Write(Peso)
            w.Write(Desc)
            w.Write(PotionMode)
            w.Write(PotionVital)
            w.Write(PotionCD)
            w.Write(PotionEffect)
            w.Write(EquipTipo)
            w.Write(EquipPaper)
            w.Write(EquipHP)
            w.Write(EquipMP)
            w.Write(EquipFOR)
            w.Write(EquipCON)
            w.Close()
        End Using
    End Sub

    Public Sub Load()
        Dim fileName As String = Application.StartupPath & "\Data\Item\" & ID & ".bin"
        If Not IO.File.Exists(fileName) Then Save()

        Dim s As New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)
        Using r As New IO.BinaryReader(s)
            Nome = r.ReadString
            Icon = r.ReadInt16
            Tipo = r.ReadByte
            isDrop = r.ReadByte
            isStack = r.ReadByte
            Peso = r.ReadInt32
            Desc = r.ReadString
            PotionMode = r.ReadByte
            PotionVital = r.ReadInt32
            PotionCD = r.ReadInt16
            PotionEffect = r.ReadInt16
            EquipTipo = r.ReadByte
            EquipPaper = r.ReadInt16
            EquipHP = r.ReadInt32
            EquipMP = r.ReadInt32
            EquipFOR = r.ReadInt32
            EquipCON = r.ReadInt32
            r.Close()
        End Using
    End Sub

    Public ReadOnly Property ID As Integer
        Get
            Return Array.IndexOf(Item, Me)
        End Get
    End Property
End Class