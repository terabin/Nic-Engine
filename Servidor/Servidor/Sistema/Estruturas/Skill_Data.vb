Module modSkill_Data
    Public Skill As List(Of SkillData) = New List(Of SkillData)
    Public Const MAX_EFEITOS As Byte = 10

    Public Sub LoadSkills()
        For i As Short = 1 To Options.MAX_SKILL
            Skill(i).Load()
        Next
    End Sub
End Module

Public Class SkillData
    Public Effect() As EffectData

    ''' <summary>
    ''' Nome da Skill
    ''' </summary>
    Public Nome As String = ""

    ''' <summary>
    ''' Quantia de MP a se gasto
    ''' </summary>
    Public CustoMP As Integer

    ''' <summary>
    ''' Tempo de re-uso da skill
    ''' </summary>
    Public CoolDown As Short

    ''' <summary>
    ''' Limite de Efeitos
    ''' </summary>
    Public MaxEffect As Short = 1

    ''' <summary>
    ''' Gráfico do Icone
    ''' </summary>
    Public Icon As Short

#Region "Method"
    Public Sub New()
        ReDim Effect(MAX_EFEITOS + 1)

        For i As Byte = 1 To MAX_EFEITOS
            Effect(i) = New EffectData
        Next
    End Sub

    Public Sub Load()
        Dim fileName As String = Application.StartupPath & "/Data/Skill/" & ID & ".bin"
        If IO.File.Exists(fileName) Then Save()
        Dim s As New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using r As New IO.BinaryReader(s)
            Nome = r.ReadString
            CustoMP = r.ReadInt32
            CoolDown = r.ReadInt16
            MaxEffect = r.ReadInt16
            Icon = r.ReadInt16

            For i As Short = 1 To MAX_EFEITOS
                Effect(i).Tipo = r.ReadByte
                Effect(i).CastAnimation = r.ReadInt16
                Effect(i).CastTimer = r.ReadInt16
                Effect(i).Anim = r.ReadInt16
                Effect(i).Vital = r.ReadInt32
                Effect(i).isAOE = r.ReadByte
                Effect(i).Range = r.ReadInt16
                Effect(i).Roubo = r.ReadByte
            Next
        End Using
    End Sub

    Public Sub Save()
        Dim fileName As String = Application.StartupPath & "/Data/Skill/" & ID & ".bin"
        Dim s As New IO.FileStream(fileName, IO.FileMode.OpenOrCreate)

        Using w As New IO.BinaryWriter(s)
            w.Write(Nome)
            w.Write(CustoMP)
            w.Write(CoolDown)
            w.Write(MaxEffect)
            w.Write(Icon)

            For i As Short = 1 To MAX_EFEITOS
                w.Write(Effect(i).Tipo)
                w.Write(Effect(i).CastAnimation)
                w.Write(Effect(i).CastTimer)
                w.Write(Effect(i).Anim)
                w.Write(Effect(i).Vital)
                w.Write(Effect(i).isAOE)
                w.Write(Effect(i).Range)
                w.Write(Effect(i).Roubo)
            Next
        End Using
    End Sub

    Public ReadOnly Property ID As Integer
        Get
            Return Skill.IndexOf(Me)
        End Get
    End Property
#End Region

    Public Class EffectData
        ''' <summary>
        ''' Tipo do Efeito
        ''' </summary>
        Public Tipo As Byte

        ''' <summary>
        ''' Animação ao usar a Skill no personagem
        ''' </summary>
        Public CastAnimation As Short

        ''' <summary>
        ''' Dados da animação de uso da skill
        ''' </summary>
        Public ReadOnly Property CastData As AnimData
            Get
                If CastAnimation > 0 Then Return Animation(CastAnimation)
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Tempo que leva para usar a Skill
        ''' </summary>
        Public CastTimer As Short

        ''' <summary>
        ''' Animação do Efeito
        ''' </summary>
        Public Anim As Short

        ''' <summary>
        ''' Dados da animação do Efeito
        ''' </summary>
        Public ReadOnly Property AnimData As AnimData
            Get
                If Anim > 0 Then Return Animation(Anim)
                Return Nothing
            End Get
        End Property

        ''' <summary>
        ''' Valor de Cura ou Dano do efeito
        ''' </summary>
        Public Vital As Integer

        ''' <summary>
        ''' Determina se o efeito é de Área
        ''' </summary>
        Public isAOE As Byte

        ''' <summary>
        ''' Valor de Área ou Distância do alvo, máximo!
        ''' </summary>
        Public Range As Short

        ''' <summary>
        ''' <para>Valor de roubo de Vida ou Mana.</para> 
        ''' <para>Valores: 0 - 100%</para>
        ''' </summary>
        Public Roubo As Byte
    End Class
End Class
