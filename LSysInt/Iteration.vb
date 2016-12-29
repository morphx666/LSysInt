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

    Public Event [Error](sender As Object, e As EventArgs)
    Public Event Done(sender As Object, e As EventArgs)

    Public Sub New(instructions As String, initialVector As Vector)
        Me.Instructions = instructions
        Me.Steps = instructions.Split(" ").ToList()
        Me.InitialVector = initialVector

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

        'Dim v As Vector = Nothing
        'While Not mVectors.IsEmpty
        '    mVectors.TryTake(v)
        'End While

        Try
            For Each s In Steps
                ev.Formula = Rule.GetFcnPrm(s)
                pen = False

                Select Case s(0)
                    Case "F" : v2.Move(ev.Evaluate() * v1.Magnitude) : pen = True
                    Case "B" : v2.Move(-ev.Evaluate() * v1.Magnitude) : pen = True
                    Case "f" : v2.Move(ev.Evaluate() * v1.Magnitude)
                    Case "+" : v2.Angle += ev.Evaluate()
                    Case "-" : v2.Angle -= ev.Evaluate()
                    Case "[" : vStack.Push(New Vector(v2))
                    Case "]" : v2 = vStack.Pop() : Continue For
                    Case Else ' Ignore unprocessed statement
                        Thread.Sleep(1)
                End Select

                If pen Then mVectors.Add(New Vector(v1.Origin, v2.Origin))

                v1 = New Vector(v2)
            Next
        Catch ex As Exception
            RaiseEvent [Error](Me, New EventArgs)
        Finally
            mIsDone = True
            RaiseEvent Done(Me, New EventArgs)
        End Try
    End Sub
End Class
