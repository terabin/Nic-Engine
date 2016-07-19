Module BaseComponentes
    ' Nothing for where
End Module

Public Class BaseComponent
    Public Overridable Sub Draw()

    End Sub

    Public Overridable Sub Core()

    End Sub

    Public Overridable Function OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        Return False
    End Function

    Public Overridable Function OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        Return False
    End Function

    Public Overridable Function OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        Return False
    End Function

    Public Overridable Function OnMouseDbClick(ByVal e As System.Windows.Forms.MouseEventArgs) As Boolean
        Return False
    End Function
End Class