Public Class Rule
    Public ReadOnly Property Variable As String
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
        Me.Variable = GetFcnPrm(fcn)
        If Me.Variable = fcn Then
            Me.FunctionName = ""
        Else
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
                        ev.Formula = GetFcnPrm(token)
                        Return New ApplyResult(Transform.Replace(Variable, ev.Evaluate()), ApplyResult.ResultStatus.OK)
                    Else
                        Return New ApplyResult(token, ApplyResult.ResultStatus.Ignore)
                    End If
                ElseIf token = Variable Then
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
