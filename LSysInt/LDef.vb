Imports System.Drawing
Imports System.Threading

Public Class LDef
    Public Structure Constant
        Public ReadOnly Property Name As String
        Public ReadOnly Property Value As String

        Public Sub New(name As String, value As String)
            Me.Name = name
            Me.Value = value
        End Sub
    End Structure

    Public ReadOnly Property Name As String
    Public ReadOnly Property Constants As New List(Of Constant)
    Public ReadOnly Property Axiom As String
    Public ReadOnly Property Rules As New List(Of Rule)

    Private mMaxLevel As Integer = 1
    Private mIterations As New List(Of Iteration)

    Private defaultAngle As Double = 90.0
    Private offsetX As Double = 0
    Private offsetY As Double = 0
    Private defaultLength As Double = 1.0

    Private evalThread As Thread
    Private internals() As String = {"axiom", "rule", "level", "angle", "offsetX", "offsetY", "length", "constant"}

    Public Sub New(name As String, code As String)
        Me.Name = name
        Me.Axiom = ""

        Dim data As String = ""
        Dim ruleData As New List(Of Tuple(Of String, String))
        Dim p As Integer
        Dim tokens() As String

        Dim lines() As String = code.Split(Environment.NewLine)
        For i As Integer = 0 To lines.Length - 1
            For Each internal In internals
                internal += ":"

                p = lines(i).IndexOf(internal)
                If p <> -1 Then
                    data = lines(i).Substring(p + internal.Length + 1).Trim() + " "

                    For j As Integer = i + 1 To lines.Length - 1
                        If lines(j).Contains(":") Then
                            i = j - 1
                            Exit For
                        Else
                            data += lines(j).Trim() + " "
                        End If
                    Next

                    Select Case internal
                        Case "axiom:" : Axiom = data.Trim()
                        Case "rule:"
                            tokens = data.Split("=")
                            ruleData.Add(New Tuple(Of String, String)(tokens(0).Trim(), tokens(1).Trim()))
                        Case "level:" : If Not Integer.TryParse(data, mMaxLevel) OrElse mMaxLevel < 1 Then mMaxLevel = 1
                        Case "angle:" : Double.TryParse(data, defaultAngle)
                        Case "offsetX:" : Double.TryParse(data, offsetX)
                        Case "offsetY:" : Double.TryParse(data, offsetY)
                        Case "length:" : Double.TryParse(data, defaultLength)
                        Case "constant:"
                            tokens = data.Split("=")
                            Constants.Add(New Constant(tokens(0).Trim(), tokens(1).Trim()))
                    End Select
                End If
            Next
        Next

        For Each rule In ruleData
            Rules.Add(New Rule(rule.Item1, ApplyConstants(rule.Item2)))
        Next
    End Sub

    Public Sub Abort()
        evalThread.Abort()
        AbortIterations()
    End Sub

    Public ReadOnly Property Iterations As List(Of Iteration)
        Get
            Return mIterations
        End Get
    End Property

    Private Sub AbortIterations()
        mIterations.ForEach(Sub(i) i.Abort())
        mIterations.Clear()
    End Sub

    Public Property MaxLevel As Integer
        Get
            Return mMaxLevel
        End Get
        Set(value As Integer)
            mMaxLevel = value
        End Set
    End Property

    Private Sub ApplyConstants(data As List(Of String))
        For Each c In Constants
            For n As Integer = 0 To data.Count - 1
                data(n) = data(n).Replace(c.Name, c.Value)
            Next
        Next
    End Sub

    Private Function ApplyConstants(data As String) As String
        For Each c In Constants
            data = data.Replace(c.Name, c.Value)
        Next
        Return data
    End Function

    Public Sub Evaluate(initialVector As Vector)
        AbortIterations()

        initialVector.Origin = New PointF(initialVector.Origin.X + offsetX, initialVector.Origin.Y + offsetY)
        initialVector.Magnitude += defaultLength

        evalThread = New Thread(Sub()
                                    Dim iter As List(Of String) = Axiom.Split(" "c).ToList()
                                    Dim newIter As New List(Of String)
                                    Dim ar As Rule.ApplyResult
                                    Dim tmp As String = ""

                                    For i As Integer = 1 To mMaxLevel
                                        newIter.Clear()

                                        For Each token In iter
                                            If token = "" Then Continue For
                                            newIter.Add(token)
                                            For Each rule In Rules
                                                ar = rule.Apply(token)
                                                If ar.Status = Rule.ApplyResult.ResultStatus.OK Then
                                                    newIter.RemoveAt(newIter.Count - 1)
                                                    newIter.AddRange(ar.Result.Split(" "c))
                                                End If
                                            Next
                                        Next

                                        ApplyConstants(newIter)
                                        iter = newIter.ToList() ' Clone

                                        ' FIXME: Isn't this redundant?
                                        'For Each c In Constants
                                        '    For n As Integer = 0 To newIter.Count - 1
                                        '        newIter(n) = newIter(n).Replace(c.Name, c.Value)
                                        '    Next
                                        'Next

                                        mIterations.Add(New Iteration(newIter, initialVector, defaultLength, defaultAngle))
                                        Thread.Sleep(100)
                                    Next
                                End Sub) With {
                                    .IsBackground = True
                                }
        evalThread.Start()
    End Sub
End Class
