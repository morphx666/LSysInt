Imports System.Drawing
Imports System.Threading
Imports System.Windows.Forms

Public Class LSysRenderer
    Public Enum RenderModes
        [Default] = 0
        Centered = 1
    End Enum

    Private mLDef As LDef
    Private mCurrentIter As Integer = 0
    Private mRenderMode As RenderModes = RenderModes.Centered

    Private renderThread As Thread

    Public Property Multiplier As Integer = 800

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

                                          Try
                                              counter += 1
                                              If counter = 5 Then
                                                  counter = 0
                                                  If mLDef?.Iterations.Count > mCurrentIter AndAlso mLDef.Iterations(mCurrentIter).IsDone Then
                                                      If mCurrentIter = mLDef?.Iterations.Count - 1 Then Thread.Sleep(1000)

                                                      mCurrentIter += 1
                                                      mCurrentIter = mCurrentIter Mod mLDef?.Iterations.Count
                                                  End If
                                              End If
                                          Catch
                                          End Try
                                      Loop
                                  End Sub) With {
            .IsBackground = True
                                  }
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

    Public Property RenderMode As RenderModes
        Get
            Return mRenderMode
        End Get
        Set(value As RenderModes)
            mRenderMode = value
        End Set
    End Property

    Private Sub Start()
        mCurrentIter = 0
        If mLDef IsNot Nothing Then
            If mRenderMode = RenderModes.Default Then
                mLDef.Evaluate(New Vector(Multiplier, 0, New PointF(-Multiplier / 2, Multiplier / 2)))
            Else
                mLDef.Evaluate(New Vector(Multiplier, 0, New PointF(0, 0)))
            End If
        End If
    End Sub

    Private Sub Renderer_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim g As Graphics = e.Graphics

        g.Clear(Color.Black)

        ' Make (0,0) the center of the screen (instead of the usual top/left)
        g.ScaleTransform(1.0, -1.0)
        g.TranslateTransform(Me.DisplayRectangle.Width / 2, -Me.DisplayRectangle.Height / 2)

        Try
            If mRenderMode = RenderModes.Centered Then
                If mLDef IsNot Nothing Then
                    If mCurrentIter < mLDef.Iterations.Count AndAlso mLDef.Iterations(mCurrentIter).Vectors.Any() Then
                        Dim leftMost As Double = mLDef.Iterations(mCurrentIter).Vectors.Min(Function(v) v.Origin.X)
                        Dim rightMost As Double = mLDef.Iterations(mCurrentIter).Vectors.Max(Function(v) v.Origin.X)
                        Dim topMost As Double = mLDef.Iterations(mCurrentIter).Vectors.Min(Function(v) v.Origin.Y)
                        Dim bottomMost As Double = mLDef.Iterations(mCurrentIter).Vectors.Max(Function(v) v.Origin.Y)

                        Dim width As Integer = (rightMost - leftMost)
                        Dim height As Integer = (bottomMost - topMost)

                        'Dim offsetLeft As Double = -(rightMost - leftMost) / 2
                        'Dim offsetTop As Double = (topMost - bottomMost) / 2

                        Dim vc As Vector

                        For Each v In mLDef.Iterations(mCurrentIter).Vectors
                            vc = New Vector(v)
                            vc.Translate(-leftMost - width / 2, -topMost - height / 2)

                            Using p As New Pen(vc.Color)
                                g.DrawLine(p, vc.Origin, vc.Destination)
                            End Using
                        Next

                        'g.DrawRectangle(Pens.Red, CSng(offsetLeft), CSng(offsetTop), CSng(width), CSng(height))
                    End If
                End If
            Else
                If mLDef IsNot Nothing Then
                    If mCurrentIter < mLDef.Iterations.Count Then
                        For Each v As Vector In mLDef.Iterations(mCurrentIter).Vectors
                            Using p As New Pen(v.Color)
                                g.DrawLine(p, v.Origin, v.Destination)
                            End Using
                        Next
                    End If
                End If
            End If
        Catch ex As Exception
        End Try

        g.ResetTransform()

        Try
            g.DrawString($"Iteration: {mCurrentIter + 1}/{mLDef?.MaxLevel}", Me.Font, Brushes.White, 10, 10 + 12 * 0)
            g.DrawString($"Vectors: {mLDef?.Iterations.Sum(Function(i) i.Vectors.Count):N0}", Me.Font, Brushes.White, 10, 10 + 12 * 1)
        Catch
        End Try
    End Sub
End Class
