Public Class LSysInt
    Public ReadOnly Property LDefs As New List(Of LDef)

    Public Sub New(code As String)
        For i As Integer = 0 To code.Length - 1
            If code(i) = "{" Then
                Dim defName As String = FindPreviousWord(code, i)
                For j As Integer = i + 1 To code.Length - 1
                    If Char.IsLetterOrDigit(code(j)) Then
                        For k = j + 1 To code.Length - 1
                            If code(k) = "}" Then
                                LDefs.Add(New LDef(defName, code.Substring(j, k - j)))
                                j = code.Length
                                i = k
                                Exit For
                            End If
                        Next
                    ElseIf code(j) = "}" Then
                        LDefs.Add(New LDef(defName, code.Substring(j, j - i - 1)))
                        Exit For
                    End If
                Next
            End If
        Next
    End Sub

    Private Function FindPreviousWord(code As String, i As Integer) As String
        For i = i - 1 To 0 Step -1
            If Char.IsLetterOrDigit(code(i)) Then
                For j As Integer = i - 1 To 0 Step -1
                    If Not Char.IsLetterOrDigit(code(j)) Then
                        Return code.Substring(j + 1, i - j)
                    End If
                Next
                Return code.Substring(0, i + 1)
            End If
        Next

        Return ""
    End Function
End Class
