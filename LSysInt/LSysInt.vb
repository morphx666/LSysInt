' http://algorithmicbotany.org/papers/abop/abop-ch1.pdf

Public Class LSysInt
    Public ReadOnly Property LDefs As New List(Of LDef)

    Public Sub New(code As String)
        ' Remove all spaces from all functions' parameters, otherwise it breaks the parser
        ' Note to self: improve the parser!
        Dim newCode As String = ""
        For i As Integer = 0 To code.Length - 1
            newCode += code(i)

            If code(i) = "("c Then
                For j As Integer = i + 1 To code.Length - 1
                    Select Case code(j)
                        Case " "c
                        Case ")"c : newCode += code(j) : i = j : Exit For
                        Case Else : newCode += code(j)
                    End Select
                Next
            End If
        Next
        While newCode.Contains("  ")
            newCode = newCode.Replace("  ", " "c)
        End While
        code = newCode

        Dim defName As String

        Try
            For i As Integer = 0 To code.Length - 1
                If code(i) = "{"c Then
                    defname = FindPreviousWord(code, i)
                    For j As Integer = i + 1 To code.Length - 1
                        If Char.IsLetterOrDigit(code(j)) Then
                            For k = j + 1 To code.Length - 1
                                If code(k) = "}"c Then
                                    LDefs.Add(New LDef(defName, code.Substring(j, k - j)))
                                    j = code.Length
                                    i = k
                                    Exit For
                                End If
                            Next
                        ElseIf code(j) = "}"c Then
                            LDefs.Add(New LDef(defName, code.Substring(j, j - i - 1)))
                            Exit For
                        End If
                    Next
                End If
            Next
        Catch ex As Exception
        End Try
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
