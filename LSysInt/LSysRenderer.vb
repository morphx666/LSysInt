Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms

Public Class LSysRenderer
    Private mLDef As LDef
    Private mCurrentIter As Integer = 0

    Private renderThread As Thread

    Private Sub Renderer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.ResizeRedraw, True)
        Me.SetStyle(ControlStyles.UserPaint, True)

        renderThread = New Thread(Sub()
                                      Dim delay As Integer = 60
                                      Dim counter As Integer = 0

                                      Do
                                          Thread.Sleep(delay)
                                          Me.Invalidate()

                                          counter += 1
                                          If counter = 5 Then
                                              If mLDef?.Iterations.Count > mCurrentIter AndAlso mLDef.Iterations(mCurrentIter).IsDone Then
                                                  If mCurrentIter = mLDef.Iterations.Count - 1 Then Thread.Sleep(1000)

                                                  mCurrentIter += 1
                                                  mCurrentIter = mCurrentIter Mod mLDef.Iterations.Count
                                              End If
                                              counter = 0
                                          End If
                                      Loop
                                  End Sub)
        renderThread.IsBackground = True
        renderThread.Start()
    End Sub

    Public Property LDef As LDef
        Get
            Return mLDef
        End Get
        Set(value As LDef)
            If mLDef IsNot Nothing Then mLDef.Abort()
            mLDef = value
            Start()
        End Set
    End Property

    Private Sub Start()
        mCurrentIter = 0
        If mLDef IsNot Nothing Then
            Dim len As Double = 800.0
            mLDef.Evaluate(New Vector(len, 0, New PointF(-len / 2, len / 2)))
        End If
    End Sub

    Private Sub Renderer_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics

        g.Clear(Color.Black)

        ' Make (0,0) the center of the screen (instead of the usual top/left)
        g.ScaleTransform(1.0, -1.0)
        g.TranslateTransform(Me.DisplayRectangle.Width / 2, -Me.DisplayRectangle.Height / 2)

        If mLDef IsNot Nothing Then
            If mCurrentIter < mLDef.Iterations.Count Then
                For Each v In mLDef.Iterations(mCurrentIter).Vectors
                    g.DrawLine(Pens.White, v.Origin, v.Destination)
                Next
            End If

            g.ResetTransform()
            g.DrawString($"Iteration: {mCurrentIter + 1}/{mLDef.MaxLevel}", Me.Font, Brushes.White, 10, 10)
        End If
    End Sub
End Class
