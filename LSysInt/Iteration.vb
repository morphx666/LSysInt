Imports System.Collections.Concurrent
Imports System.Threading
Imports LSysInt

Public Class Iteration
    Public ReadOnly Property Steps As New List(Of String)
    Public ReadOnly Property Instructions As String
    Public ReadOnly Property InitialVector As Vector

    Private mIsDone As Boolean = False
    Public ReadOnly Property Vectors As New ConcurrentBag(Of Vector)

    Private execThread As Thread
    Private vStack As New Stack(Of Vector)
    Private ReadOnly defaultAngle As Double = 90.0
    Private ReadOnly defaultLength As Double = 1.0
    Private ev As New Evaluator()

    Public Event [Error](sender As Object, e As EventArgs)
    Public Event Done(sender As Object, e As EventArgs)

    Public Sub New(instructions As String, initialVector As Vector, defaultLength As Double, defaultAngle As Double)
        Me.Instructions = instructions
        Me.Steps = instructions.Split(" ").ToList()
        Me.InitialVector = initialVector
        Me.defaultLength = defaultLength
        Me.defaultAngle = defaultAngle

        Start()
    End Sub

    Public Sub New(instructions As List(Of String), initialVector As Vector, defaultLength As Double, defaultAngle As Double)
        Me.Instructions = Join(instructions.ToArray(), " ")
        Me.Steps = instructions.ToList()
        Me.InitialVector = initialVector
        Me.defaultLength = defaultLength
        Me.defaultAngle = defaultAngle

        Start()
    End Sub

    Private Sub Start()
        execThread = New Thread(AddressOf Execute) With {
            .IsBackground = True
        }
        execThread.Start()
    End Sub

    Public ReadOnly Property IsDone As Boolean
        Get
            Return mIsDone
        End Get
    End Property

    Public Sub Abort()
        execThread.Abort()
    End Sub

    Private Sub Execute()
        Dim pen As Boolean = True
        Dim v1 As New Vector(InitialVector)
        Dim v2 As New Vector(v1)
        Dim val As Double
        Dim isValid As Boolean
        Dim vs() As String
        Dim v() As Double

        For Each s In Steps
            ev.Formula = Rule.GetFcnPrm(s)
            Try
                isValid = True
                val = ev.Evaluate()
            Catch
                isValid = False
                'RaiseEvent [Error](Me, New EventArgs())
            End Try
            pen = False

            Try
                Select Case Rule.GetFcnName(s)
                    Case "F" : v2.Move(If(isValid, val * v1.Magnitude, defaultLength * v1.Magnitude)) : pen = True
                    Case "B" : v2.Move(If(isValid, -val * v1.Magnitude, -defaultLength * v1.Magnitude)) : pen = True
                    Case "f" : v2.Move(If(isValid, val, defaultLength) * v1.Magnitude)
                    Case "+" : v2.Angle += If(isValid, val, defaultAngle)
                    Case "-" : v2.Angle -= If(isValid, val, defaultAngle)
                    Case "[" : vStack.Push(New Vector(v2))
                    Case "]" : v2 = vStack.Pop() : Continue For
                    Case "%"
                        vs = Rule.GetFcnPrm(s).Split(","c)
                        ReDim v(vs.Length - 1)
                        For i As Integer = 0 To vs.Length - 1
                            ev.Formula = vs(i)
                            v(i) = ev.Evaluate() * 255
                        Next
                        v2.Color = Drawing.Color.FromArgb(v(3), v(0), v(1), v(2))
                    Case Else ' Ignore unprocessed statement
                        'Thread.Sleep(1)
                End Select

                If pen Then Vectors.Add(New Vector(v1.Origin, v2.Origin, v2.Color))

                v1 = New Vector(v2)
            Catch ex As Exception
                RaiseEvent [Error](Me, New EventArgs)
            End Try
        Next

        mIsDone = True
        RaiseEvent Done(Me, New EventArgs)
    End Sub
End Class
