﻿Imports System.Threading

Public Class LDef
    Public ReadOnly Property Name As String
    Public ReadOnly Property Variables As New List(Of String)
    Public ReadOnly Property Constants As New List(Of String)
    Public ReadOnly Property Axiom As String
    Public ReadOnly Property Rules As New List(Of Rule)

    Private mMaxLevel As Integer = 1
    Private mIterations As New List(Of Iteration)

    Private evalThread As Thread
    Private internals() As String = {"axiom", "rule", "level"}

    Public Sub New(name As String, code As String)
        name = name

        Dim data As String = ""

        Dim lines() As String = code.Split(Environment.NewLine)
        For i As Integer = 0 To lines.Length - 1
            For Each internal In internals
                internal += ":"

                Dim p As Integer = lines(i).IndexOf(internal)
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
                        Case "axiom:" : Axiom = data
                        Case "rule:"
                            Dim tokens() As String = data.Split("=")
                            Rules.Add(New Rule(tokens(0).Trim(), tokens(1).Trim()))
                        Case "level:" : If Not Integer.TryParse(data, mMaxLevel) OrElse mMaxLevel < 1 Then mMaxLevel = 1
                    End Select
                End If
            Next
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

    Public Sub Evaluate(initialVector As Vector)
        AbortIterations()

        evalThread = New Thread(Sub()
                                    Dim iter As String = Axiom
                                    Dim newIter As String = ""
                                    Dim ar As Rule.ApplyResult

                                    ' FIXME: This code is horrendously slow!
                                    For i As Integer = 1 To mMaxLevel
                                        newIter = ""

                                        For Each token In iter.Split(" ")
                                            newIter += token + " "
                                            For Each rule In Rules
                                                ar = rule.Apply(token)
                                                If ar.Status = Rule.ApplyResult.ResultStatus.OK Then newIter = newIter.Replace(token, ar.Result)
                                            Next
                                        Next

                                        newIter = newIter.Trim()
                                        mIterations.Add(New Iteration(newIter, initialVector))
                                        Thread.Sleep(1)

                                        iter = newIter
                                    Next
                                End Sub)
        evalThread.IsBackground = True
        evalThread.Start()
    End Sub
End Class
