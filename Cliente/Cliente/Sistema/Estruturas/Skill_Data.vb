Imports SFML.Graphics
Imports SFML.System

Module modSkillData
    Public Skill As HashSet(Of SkillData) = New HashSet(Of SkillData)
    Public Const MAX_EFEITOS As Byte = 10
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

    Public Sub New()
        ReDim Effect(MAX_EFEITOS + 1)

        For i As Byte = 1 To MAX_EFEITOS
            Effect(i) = New EffectData
        Next
    End Sub

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