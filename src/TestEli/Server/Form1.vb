﻿Imports System.Net.Sockets
Imports System.ComponentModel
Imports System.Net
Imports System.IO

Public Class Form1
    Dim serverStatus As Boolean = False
    Dim serverTrying As Boolean = False
    Dim Server As TcpListener
    Dim Clients As New List(Of TcpClient)
    'Private Server As TCPController

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
        ' Server = New TCPController
        'ChatTextBox.Text = "::  Server Starting  ::" & Environment.NewLine

        '   AddHandler Server.MessageRecieved, AddressOf OnlineRecieved

    End Sub

    'Private Delegate Sub UpdateTextDelegate(TB As TextBox, txt As String)

    'Update textbox
    ' Private Sub UpdateText(TB As TextBox, txt As String)
    'If TB.InvokeRequired Then
    '    TB.Invoke(New UpdateTextDelegate(AddressOf UpdateText), New Object() {TB, txt})
    ' Else
    '      If txt IsNot Nothing Then TB.AppendText(txt & Environment.NewLine)
    '   End If
    'End Sub


    'Private Sub OnlineRecieved(sender As TCPController, data As String)
    '   UpdateText(ChatTextBox, data)
    'End Sub

    ' Private Sub Form1_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
    '    Server.IsListening = False
    'End Sub

    Private Sub StartButton_Click(sender As Object, e As EventArgs) Handles StartButton.Click
        startserver()
    End Sub
    Function startserver()
        If serverStatus = False Then
            serverTrying = True
            Try
                Server = New TcpListener(IPAddress.Any, 64555)
                Server.Start()
                serverStatus = True
            Catch ex As Exception
                serverStatus = False
            End Try
            serverTrying = False

        End If
        Return True
    End Function

    Private Sub StopButton_Click(sender As Object, e As EventArgs) Handles StopButton.Click
        stopserver()

    End Sub
    Function stopserver()
        If serverStatus = True Then
            serverTrying = True
            Try
                For Each client As TcpClient In Clients
                    client.Close()
                Next
                Server.Stop()
                serverStatus = False
            Catch ex As Exception
                stopserver()
            End Try
        End If
        Return True
    End Function
    Function Handler_Client(ByVal state As Object)
        Try
            Using Client As TcpClient = Server.AcceptTcpClient
                If serverTrying = False Then
                    Threading.ThreadPool.QueueUserWorkItem(AddressOf Handler_Client)
                End If
                Clients.Add(Client)
                Dim TX As New StreamWriter(Client.GetStream)
                Dim RX As New StreamReader(Client.GetStream)
                If RX.BaseStream.CanRead = True Then

                End If
            End Using
        Catch ex As Exception

        End Try
        Return True
    End Function
End Class