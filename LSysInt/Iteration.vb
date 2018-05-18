Imports System.Collections.Concurrent
Imports System.Threading

Public Class Iteration
    Public ReadOnly Property Steps As New List(Of String)
    Public ReadOnly Property Instructions As String
    Public ReadOnly Property InitialVector As Vector

    Private mIsDone As Boolean = False
    Private mVectors As New ConcurrentBag(Of Vector)

    Private execThread As Thread
    Private vStack As New Stack(Of Vector)
    Private defaultAngle As Double = 90.0
    Private defaultLength As Double = 1.0

    Public Event [Error](sender As Object, e As EventArgs)
    Public Event Done(sender As Object, e As EventArgs)

    Public Sub New(instructions As String, initialVector As Vector, defaultLength As Double, defaultAngle As Double)
        Me.Instructions = instructions
        Me.Steps = instructions.Split(" ").ToList()
        Me.InitialVector = initialVector
        Me.defaultLength = defaultLength
        Me.defaultAngle = defaultAngle

        execThread = New Thread(AddressOf Execute)
        execThread.IsBackground = True
        execThread.Start()
    End Sub

    Public Sub New(instructions As List(Of String), initialVector As Vector, defaultLength As Double, defaultAngle As Double)
        Me.Instructions = Join(instructions.ToArray(), " ")
        Me.Steps = instructions.ToList()
        Me.InitialVector = initialVector
        Me.defaultLength = defaultLength
        Me.defaultAngle = defaultAngle

        execThread = New Thread(AddressOf Execute)
        execThread.IsBackground = True
        execThread.Start()
    End Sub

    Public ReadOnly Property Vectors As ConcurrentBag(Of Vector)
        Get
            Return mVectors
        End Get
    End Property

    Public ReadOnly Property IsDone As Boolean
        Get
            Return mIsDone
        End Get
    End Property

    Public Sub Abort()
        execThread.Abort()
    End Sub

    Private Sub Execute()
        Dim ev As New Evaluator()
        Dim pen As Boolean = True
        Dim v1 As New Vector(InitialVector)
        Dim v2 As New Vector(v1)
        Dim val As Double
        Dim valIsValid As Boolean

        'Dim v As Vector = Nothing
        'While Not mVectors.IsEmpty
        '    mVectors.TryTake(v)
        'End While

        For Each s In Steps
            ev.Formula = Rule.GetFcnPrm(s)
            Try
                valIsValid = True
                val = ev.Evaluate()
            Catch
                valIsValid = False
                RaiseEvent [Error](Me, New EventArgs())
            End Try
            pen = False

            Try
                Select Case Rule.GetFcnName(s)
                    Case "F"
                        If valIsValid Then
                            v2.Move(val * v1.Magnitude)
                        Else
                            v2.Move(defaultLength * v1.Magnitude)
                        End If
                        pen = True
                    Case "B"
                        If valIsValid Then
                            v2.Move(-val * v1.Magnitude)
                        Else
                            v2.Move(-defaultLength * v1.Magnitude)
                        End If
                        pen = True
                    Case "f" : v2.Move(If(valIsValid, val, defaultLength) * v1.Magnitude)
                    Case "+" : v2.Angle += If(valIsValid, val, defaultAngle)
                    Case "-" : v2.Angle -= If(valIsValid, val, defaultAngle)
                    Case "[" : vStack.Push(New Vector(v2))
                    Case "]" : v2 = vStack.Pop() : Continue For
                    Case "%"
                        Dim vs() As String = Rule.GetFcnPrm(s).Split(",")
                        Dim v(vs.Length - 1) As Double
                        For i As Integer = 0 To vs.Length - 1
                            ev.Formula = vs(i)
                            v(i) = ev.Evaluate() * 255
                        Next
                        v2.Color = Drawing.Color.FromArgb(v(3), v(0), v(1), v(2))
                    Case Else ' Ignore unprocessed statement
                        'Thread.Sleep(1)
                End Select

                If pen Then mVectors.Add(New Vector(v1.Origin, v2.Origin, v2.Color))

                v1 = New Vector(v2)
            Catch ex As Exception
                RaiseEvent [Error](Me, New EventArgs)
            End Try
        Next

        mIsDone = True
        RaiseEvent Done(Me, New EventArgs)
    End Sub
End Class
