Module modItemData
    Public Item() As ItemData
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
End Class