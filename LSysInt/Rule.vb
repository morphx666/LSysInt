Public Class Rule
    Public ReadOnly Property Variables As New List(Of String)
    Public ReadOnly Property FunctionName As String
    Public ReadOnly Property Transform As String

    Public Structure ApplyResult
        Public Enum ResultStatus
            OK
            Ignore
            [Error]
        End Enum

        Public ReadOnly Property Result As String
        Public ReadOnly Property Status As ResultStatus

        Public Sub New(result As String, status As ResultStatus)
            Me.Result = result
            Me.Status = status
        End Sub
    End Structure

    Dim ev As New Evaluator()

    Public Sub New(fcn As String, transform As String)
        Dim vars As String = GetFcnPrm(fcn)
        If vars = fcn Then
            Me.FunctionName = ""
            Me.Variables.Add(vars)
        Else
            Me.Variables = vars.Split(",").ToList()
            Me.FunctionName = fcn.Substring(0, fcn.IndexOf("("))
        End If

        Me.Transform = transform
    End Sub

    Public Function Apply(token As String) As ApplyResult
        If token.Contains(" ") Then
            Dim subTokens() As String = token.Split(" ")
            For Each subToken In subTokens
                token = token.Replace(subToken, Apply(subToken).Result)
            Next
            Return New ApplyResult(token, True)
        Else
            Try
                If token.Contains("(") Then
                    If token.StartsWith($"{FunctionName}(") Then
                        Dim pr As String() = GetFcnPrm(token).Split(",")
                        If pr.Length = Variables.Count Then
                            Dim tx As String = Transform
                            For i As Integer = 0 To Variables.Count - 1
                                If i = pr.Length Then Exit For
                                Dim var As String = Variables(i)
                                ev.Formula = pr(i)
                                tx = tx.Replace(var, ev.Evaluate())
                            Next
                            Return New ApplyResult(tx, ApplyResult.ResultStatus.OK)
                        Else
                            Return New ApplyResult(token, ApplyResult.ResultStatus.OK)
                        End If
                    Else
                        Return New ApplyResult(token, ApplyResult.ResultStatus.Ignore)
                    End If
                ElseIf Variables.Contains(token) Then
                    Return New ApplyResult(Transform, ApplyResult.ResultStatus.OK)
                Else
                    Return New ApplyResult(token, ApplyResult.ResultStatus.Ignore)
                End If
            Catch ex As Exception
                Return New ApplyResult(token, ApplyResult.ResultStatus.Error)
            End Try
        End If
    End Function

    Public Shared Function GetFcnPrm(code As String) As String
        Dim p1 As Integer = code.IndexOf("(")
        Dim p2 As Integer = code.LastIndexOf(")")
        If p1 <> -1 AndAlso p2 <> -1 Then
            Return code.Substring(p1 + 1, p2 - p1 - 1)
        Else
            Return code
        End If
    End Function
End Class
