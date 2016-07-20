Module Player_Data
    Public Const ATTACK_TIMER_DEFAULT As Integer = 1000 ' 1 s

    Public Property Inventory(ByVal ID As Short) As clsInv
        Get
            Return Player(MyIndex).Inv(ID)
        End Get
        Set(ByVal value As clsInv)
            Player(MyIndex).Inv(ID) = value
        End Set
    End Property

    Public Property MyData As clsPlayer
        Get
            Return Player(MyIndex)
        End Get
        Set(ByVal value As clsPlayer)
            Player(MyIndex) = value
        End Set
    End Property
End Module

Public Class clsPlayer
#Region "Dados"
    Public Id As Integer
    Public Nome As String
    Public Classe As Short
    Public Sprite As Short
    Public Level As Short
    Public Map As Short
    Public X As Short
    Public Y As Short
    Public Dir As Byte
    Private sSTR As Integer
    Private sCON As Integer
    Private sHP As Integer
    Private sMP As Integer
    Public Points As Integer
    Public EXP As Long
    Public Access As Byte
    Public PK As Byte
    Public PKCount As Short
    Public PVPCount As Short
    Public Inv() As clsInv
    Public MaxHP As Integer

    ' Efeitos
    Public Hero As clsAnimationBufferBase

    ' Movimento
    Public Move As Byte
    Public MoveStep As Short
    Public MoveTimer As Integer
    Public MoveAnimTimer As Integer
    Public XOffSet As Single
    Public YOffSet As Single

    ' Ataque
    Public Attack As Byte
    Public AttackTimer As Integer
#End Region

#Region "Property"
    Public Property STR(Optional ByVal RAW As Boolean = False) As Integer
        Get
            If RAW Then
                Return sSTR
            Else
                Return sSTR
            End If
        End Get
        Set(ByVal value As Integer)
            sSTR = value
        End Set
    End Property

    Public Property CON(Optional ByVal RAW As Boolean = False) As Integer
        Get
            If RAW Then
                Return sCON
            Else
                Return sCON
            End If
        End Get
        Set(ByVal value As Integer)
            sCON = value
        End Set
    End Property

    Public Property HP(Optional ByVal RAW As Boolean = False) As Integer
        Get
            If RAW Then
                Return sHP
            Else
                Return sHP
            End If
        End Get
        Set(ByVal value As Integer)
            sHP = value
        End Set
    End Property

    Public Property MP(Optional ByVal RAW As Boolean = False) As Integer
        Get
            If RAW Then
                Return sMP
            Else
                Return sMP
            End If
        End Get
        Set(ByVal value As Integer)
            sMP = value
        End Set
    End Property

    Public ReadOnly Property GetInvCount As Short
        Get
            Return Inv.Length - 1
        End Get
    End Property

    Public ReadOnly Property GetInvMax As Short
        Get
            Return 30
        End Get
    End Property
#End Region

#Region "Method"
    Public Sub New()
        Clear()
    End Sub

    Public Sub Clear()
        Nome = ""
        Classe = 1
        Sprite = 1
        Level = 1
        Map = 1
        X = 0
        Y = 0
        Dir = 0
        sSTR = 0
        sCON = 0
        sHP = 100
        sMP = 50
        Points = 0
        EXP = 0
        Access = 0
        PK = 0
        PKCount = 0
        PVPCount = 0
        Hero = New clsAnimationBufferBase

        ReDim Inv(Options.MAX_INV)
        For i As Short = 0 To Options.MAX_INV - 1
            Inv(i) = New clsInv
        Next
    End Sub

    Public Sub AddInvSlot(ByVal newInv As clsInv)
        ReDim Preserve Inv(Inv.Length)
        Inv(Inv.Length - 1) = New clsInv()
    End Sub
#End Region

End Class

Public Class clsInv
    Public Num As Short
    Public Value As Integer
    Public GemaSlot() As Short

#Region "Method"
    Public Sub New()
        Num = 0
        Value = 0
        ReDim GemaSlot(Options.MAX_INV_GEMA)

        For i As Short = 0 To Options.MAX_INV_GEMA - 1
            GemaSlot(i) = 0
        Next
    End Sub

     Public ReadOnly Property Dados As ItemData
        Get
            If Num > 0 Then
                Return Item(Num)
            End If
            Return Nothing
        End Get
    End Property
#End Region
End Class