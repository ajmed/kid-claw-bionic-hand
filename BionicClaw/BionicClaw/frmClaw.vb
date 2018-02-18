'OBS Control
Imports System
Imports WebSocketSharp
Imports Newtonsoft.Json.Linq

'Twitch Chat Control
Imports TwitchLib
Imports TwitchLib.Events.Client


Public Class frmClaw
    'Individual Declarations

    'Connection Approval
    Dim OBSReady As Boolean = False
    Dim TwitchReady As Boolean = False

    'Media Control
    Dim MediaReady As Boolean = False
    Dim MediaScene As String
    Dim MediaSource As String
    Dim MediaDuration As Integer
    Dim TextSource As String
    Dim TextScene As String

    Dim Muted As Boolean

    'Claw Control
    Private arduinoController As ArduinoController

    'OBS Control
    Dim _ws As WebSocket
    Dim _responseHandlers As Dictionary(Of String, TaskCompletionSource(Of JObject))

    'Reset Timer Control
    Dim Hours, Minutes, Seconds As Integer
    Dim Timer As String = Nothing

    'Top of the Hour Control
    Dim Clock As String
    Dim TopHours, TopMinutes, TopSeconds As Integer
    Dim TopTimer As String = Nothing


#Region "OBS Structure"
    Public Structure OBSAuthInfo
        Public ReadOnly AuthRequired As Boolean
        Public ReadOnly Challenge As String
        Public ReadOnly PasswordSalt As String
        ''' <param name="data">JSON response body as a <see cref="JObject"/></param>
        Public Sub New(data As JObject)
            AuthRequired = data("authRequired").Value(Of Boolean)
            Challenge = data("challenge").Value(Of String)
            PasswordSalt = data("salt").Value(Of String)
        End Sub
    End Structure
#End Region

#Region "Load & Close"
    Private Sub frmClaw_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set and Lock Size
        Me.Width = 200
        Me.Height = 400
        Me.MaximumSize = New Size(200, 400)
        Me.MinimumSize = New Size(200, 400)
        MaximizeBox = False
        MinimizeBox = False

        'Assign Settings to txtBoxes
        txtTwitchUsername.Text = My.Settings.TwitchUsername
        txtTwitchOAuth.Text = My.Settings.TwitchOAuth
        txtOBSHost.Text = My.Settings.OBSHost
        txtOBSPort.Text = My.Settings.OBSPort
        txtOBSPass.Text = My.Settings.OBSPass
        txtInterval.Text = My.Settings.Interval
        txtReset.Text = My.Settings.Reset
        tmrQueue.Interval = My.Settings.Interval
        txtCooldown.Text = My.Settings.Cooldown
        tmrCooldown.Interval = My.Settings.Cooldown
        tmrQueue.Enabled = False
        tmrCooldown.Enabled = False
        tmrReset.Enabled = False

        'OBS Control
        _responseHandlers = New Dictionary(Of String, TaskCompletionSource(Of JObject))()

        'OBS and Twitch Ready?
        OBSTwitchReady()

        'Load Media
        LoadMediaString()

    End Sub
    Private Sub frmClaw_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        SaveMediaString()
    End Sub
    Private Sub frmClaw_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed

    End Sub
#End Region

#Region "OBS Control"
    'Command List
    'SetSourceRender(Source Name, True, Scene Name)
    'SetSourceRender(Source Name, False, Scene Name)
    'SetCurrentScene(Scene Name)
    'ToggleStreaming
    'StartStreaming
    'StopStreaming
    'ToggleMute("name")
    'SetSceneItemPosition(Source Name, X, Y, sceneName)
    'SetSceneItemTransform(Source Name, rotation, xScale, yScale, sceneName)

    Private Sub ConnectToOBS()
        Try
            ' ws://127.0.0.1:4444
            ' or
            ' wss://.... - for ssh
            Connect("ws://" & My.Settings.OBSHost & ":" & My.Settings.OBSPort, My.Settings.OBSPass)
        Catch generatedExceptionName As AuthFailureException
            'MessageBox.Show("Authentication failed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        Catch ex As ErrorResponseException
            'MessageBox.Show("Connect failed : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Return
        End Try

    End Sub


    Public Sub Connect(url As String, password As String)
        If _ws IsNot Nothing AndAlso _ws.IsAlive() Then
            Disconnect()
        End If

        _ws = New WebSocket(url)
        AddHandler _ws.OnMessage, AddressOf WebsocketMessageHandler
        _ws.Connect()

        Dim authInfo As OBSAuthInfo = GetAuthInfo()
        If authInfo.AuthRequired Then
            Authenticate(password, authInfo)
        End If
    End Sub

    Public Sub Disconnect()
        If _ws IsNot Nothing Then _ws.Close()
        _ws = Nothing
        For Each cb As Object In _responseHandlers
            Dim tcs = cb.Value
            tcs.TrySetCanceled()
        Next
    End Sub

    Public Function Authenticate(password As String, authInfo As OBSAuthInfo) As Boolean
        Dim secret As String = HashEncode(password & Convert.ToString(authInfo.PasswordSalt))
        Dim authResponse As String = HashEncode(secret & Convert.ToString(authInfo.Challenge))

        Dim requestFields = New JObject()
        requestFields.Add("auth", authResponse)

        Try
            ' Throws ErrorResponseException if auth fails
            SendRequest("Authenticate", requestFields)
        Catch generatedExceptionName As ErrorResponseException
            Throw New AuthFailureException()
        End Try

        Return True
    End Function

    Public Class ErrorResponseException
        Inherits Exception
        ''' <summary>
        ''' Constructor
        ''' </summary>
        ''' <param name="message"></param>
        Public Sub New(message As String)
            MyBase.New(message)
        End Sub
    End Class

    Public Class AuthFailureException
        Inherits Exception
    End Class

    Protected Function HashEncode(input As String) As String
        Dim sha256 = New Security.Cryptography.SHA256Managed()

        Dim textBytes As Byte() = System.Text.Encoding.ASCII.GetBytes(input)
        Dim hash As Byte() = sha256.ComputeHash(textBytes)

        Return System.Convert.ToBase64String(hash)
    End Function

    Public Function GetAuthInfo() As OBSAuthInfo
        Dim response As JObject = SendRequest("GetAuthRequired")
        Return New OBSAuthInfo(response)
    End Function

    Private Sub WebsocketMessageHandler(sender As Object, e As MessageEventArgs)
        If Not e.IsText Then
            Return
        End If

        Dim body As JObject = JObject.Parse(e.Data)

        If body("message-id") IsNot Nothing Then
            ' Handle a request : 
            ' Find the response handler based on 
            ' its associated message ID
            Dim msgID As String = body("message-id").Value(Of String)
            Dim handler = _responseHandlers(msgID)

            If handler IsNot Nothing Then
                ' Set the response body as Result and notify the request sender
                handler.SetResult(body)

                ' The message with the given ID has been processed,
                ' so its handler can be discarded
                _responseHandlers.Remove(msgID)
            End If
        ElseIf body("update-type") IsNot Nothing Then
            ' Handle an event
            Dim eventType As String = body("update-type").ToString()
            ProcessEventType(eventType, body)
        End If
    End Sub

    Public Function SendRequest(requestType As String, Optional additionalFields As JObject = Nothing) As JObject
        Dim messageID As String

        ' Generate a random message id and make sure it is unique within the handlers dictionary
        Do
            messageID = NewMessageID()
        Loop While _responseHandlers.ContainsKey(messageID)

        ' Build the bare-minimum body for a request
        Dim body = New JObject()
        body.Add("request-type", requestType)
        body.Add("message-id", messageID)

        ' Add optional fields if provided
        If additionalFields IsNot Nothing Then
            Dim mergeSettings = New JsonMergeSettings() With {.MergeArrayHandling = MergeArrayHandling.Union}
            body.Merge(additionalFields)
        End If

        ' Prepare the asynchronous response handler
        Dim tcs = New TaskCompletionSource(Of JObject)()
        _responseHandlers.Add(messageID, tcs)

        ' Send the message and wait for a response
        ' (received and notified by the websocket response handler)
        _ws.Send(body.ToString())
        tcs.Task.Wait()

        ' Throw an exception if the server returned an error.
        ' An error occurs if authentication fails or one if the request body is invalid.
        Dim result = tcs.Task.Result

        If result("status").Value(Of String) = "error" Then
            Throw New ErrorResponseException(result("error").Value(Of String))
        End If

        Return result
    End Function

    Protected Function NewMessageID(Optional length As Integer = 16) As String
        Const pool As String = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim random = New Random()

        Dim result As String = ""
        For i As Integer = 0 To length - 1
            Dim index As Integer = random.[Next](0, pool.Length - 1)
            result += pool(index)
        Next

        Return result
    End Function


#Region "Events"
    ' as OBSWebsocket
    Public Delegate Sub SceneChangeCallback(sender As Object, newSceneName As String)
    Public Delegate Sub SourceOrderChangeCallback(sender As Object, sceneName As String)
    Public Delegate Sub SceneItemUpdateCallback(sender As Object, sceneName As String, itemname As String)
    Public Delegate Sub TransitionChangeCallback(sender As Object, newTransitionName As String)
    Public Delegate Sub TransitionDurationChangeCallback(sender As Object, newDuration As Integer)
    Public Delegate Sub OutputStateCallback(sender As Object, type As OutputState)
    Public Delegate Sub StreamStatusCallback(sender As Object, status As OBSStreamStatus)
    'public delegate void StreamStatusCallback(OBSWebsocket sender, OBSStreamStatus status);
    'Public Delegate Sub StreamStatusCallback(sender As Object, status As OBSStreamStatus)

    Public Event OnSceneChange As SceneChangeCallback
    Public Event OnSceneListChange As EventHandler
    Public Event OnSourceOrderChange As SourceOrderChangeCallback
    Public Event OnSceneItemAdded As SceneItemUpdateCallback
    Public Event OnSceneItemRemoved As SceneItemUpdateCallback
    Public Event OnSceneItemVisibilityChange As SceneItemUpdateCallback
    Public Event OnSceneCollectionChange As EventHandler
    Public Event OnSceneCollectionListChange As EventHandler
    Public Event OnTransitionChange As TransitionChangeCallback
    Public Event OnTransitionDurationChange As TransitionDurationChangeCallback
    Public Event OnTransitionListChange As EventHandler
    Public Event OnTransitionBegin As EventHandler
    Public Event OnProfileChange As EventHandler
    Public Event OnProfileListChange As EventHandler
    Public Event OnStreamingStateChange As OutputStateCallback
    Public Event OnRecordingStateChange As OutputStateCallback
    Public Event OnStreamStatus As StreamStatusCallback
    Public Event OnExit As EventHandler
#End Region

    Public Enum OutputState
        Starting
        Started
        Stopping
        Stopped
    End Enum

    Protected Sub ProcessEventType(eventType As String, body As JObject)
        Dim status As OBSStreamStatus
        Select Case eventType
            Case "SwitchScenes"
                RaiseEvent OnSceneChange(Me, body("scene-name").Value(Of String))
            Case "ScenesChanged"
                RaiseEvent OnSceneListChange(Me, EventArgs.Empty)
            Case "SourceOrderChanged"
                RaiseEvent OnSourceOrderChange(Me, body("scene-name").Value(Of String))
            Case "SceneItemAdded"
                RaiseEvent OnSceneItemAdded(Me, body("scene-name").Value(Of String), body("item-name").Value(Of String))
            Case "SceneItemRemoved"
                RaiseEvent OnSceneItemRemoved(Me, body("scene-name").Value(Of String), body("item-name").Value(Of String))
            Case "SceneItemVisibilityChanged"
                RaiseEvent OnSceneItemVisibilityChange(Me, body("scene-name").Value(Of String), body("item-name").Value(Of String))
            Case "SceneCollectionChanged"
                RaiseEvent OnSceneCollectionChange(Me, EventArgs.Empty)
            Case "SceneCollectionListChanged"
                RaiseEvent OnSceneCollectionListChange(Me, EventArgs.Empty)
            Case "SwitchTransition"
                RaiseEvent OnTransitionChange(Me, body("transition-name").Value(Of String))
            Case "TransitionDurationChanged"
                RaiseEvent OnTransitionDurationChange(Me, body("new-duration").Value(Of Integer))
            Case "TransitionListChanged"
                RaiseEvent OnTransitionListChange(Me, EventArgs.Empty)
            Case "TransitionBegin"
                RaiseEvent OnTransitionBegin(Me, EventArgs.Empty)
            Case "ProfileChanged"
                RaiseEvent OnProfileChange(Me, EventArgs.Empty)
            Case "ProfileListChanged"
                RaiseEvent OnProfileListChange(Me, EventArgs.Empty)
            Case "StreamStarting"
                RaiseEvent OnStreamingStateChange(Me, OutputState.Starting)
            Case "StreamStarted"
                RaiseEvent OnStreamingStateChange(Me, OutputState.Started)
            Case "StreamStopping"
                RaiseEvent OnStreamingStateChange(Me, OutputState.Stopping)
            Case "StreamStopped"
                RaiseEvent OnStreamingStateChange(Me, OutputState.Stopped)
            Case "RecordingStarting"
                RaiseEvent OnRecordingStateChange(Me, OutputState.Starting)
            Case "RecordingStarted"
                RaiseEvent OnRecordingStateChange(Me, OutputState.Started)
            Case "RecordingStopping"
                RaiseEvent OnRecordingStateChange(Me, OutputState.Stopping)
            Case "RecordingStopped"
                RaiseEvent OnRecordingStateChange(Me, OutputState.Stopped)
            Case "StreamStatus"
                'If OnStreamStatus IsNot Nothing Then
                status = New OBSStreamStatus(body)
                RaiseEvent OnStreamStatus(Me, status)
      'End If
            Case "Exiting"
                RaiseEvent OnExit(Me, EventArgs.Empty)
        End Select
    End Sub

    Public Structure OBSStreamStatus
        Public ReadOnly Streaming As Boolean ' True if streaming is started and running, false otherwise
        Public ReadOnly Recording As Boolean ' True if recording is started and running, false otherwise
        Public ReadOnly BytesPerSec As Integer ' Stream bitrate in bytes per second
        Public ReadOnly KbitsPerSec As Integer ' Stream bitrate in kilobits per second
        Public ReadOnly Strain As Single ' RTMP output strain
        Public ReadOnly TotalStreamTime As Integer ' Total time since streaming start
        Public ReadOnly TotalFrames As Integer ' Number of frames sent since streaming start
        Public ReadOnly DroppedFrames As Integer ' Overall number of frames dropped since streaming start
        Public ReadOnly FPS As Single ' Current framerate in Frames Per Second
        ' <param name="data">JSON event body as a <see cref="JObject"/></param>
        Public Sub New(data As JObject) ' Builds the object from the JSON event body
            Streaming = data("streaming").Value(Of Boolean)
            Recording = data("recording").Value(Of Boolean)
            BytesPerSec = data("bytes-per-sec").Value(Of Integer)
            KbitsPerSec = data("kbits-per-sec").Value(Of Integer)
            Strain = data("strain").Value(Of Single)
            TotalStreamTime = data("total-stream-time").Value(Of Integer)
            TotalFrames = data("num-total-frames").Value(Of Integer)
            DroppedFrames = data("num-dropped-frames").Value(Of Integer)
            FPS = data("fps").Value(Of Single)
        End Sub
    End Structure

    Public Sub SetSourceRender(itemName As String, visible As Boolean, Optional sceneName As String = Nothing)
        Dim requestFields = New JObject()
        requestFields.Add("source", itemName)
        requestFields.Add("render", visible)
        If sceneName IsNot Nothing Then
            requestFields.Add("scene-name", sceneName)
        End If
        SendRequest("SetSourceRender", requestFields)
    End Sub


    Public Sub SetCurrentScene(sceneName As String)
        Dim requestFields = New JObject()
        requestFields.Add("scene-name", sceneName)
        SendRequest("SetCurrentScene", requestFields)
    End Sub

    Public Sub ToggleStreaming()
        SendRequest("StartStopStreaming")
    End Sub

    Public Sub StartStreaming()
        SendRequest("StartStreaming")
    End Sub

    Public Sub StopStreaming()
        SendRequest("StopStreaming")
    End Sub

    Public Sub ToggleRecording()
        SendRequest("StartStopRecording")
    End Sub

    Public Sub StartRecording()
        SendRequest("StartRecording")
    End Sub

    Public Sub StopRecording()
        SendRequest("StopRecording")
    End Sub

    Public Sub ToggleMute(sourceName As String)
        Dim requestFields = New JObject()
        requestFields.Add("source", sourceName)
        SendRequest("ToggleMute", requestFields)
    End Sub

    Public Sub SetSceneItemPosition(itemName As String, X As Integer, Y As Integer, sceneName As String)
        Dim requestFields = New JObject()
        requestFields.Add("item", itemName)
        requestFields.Add("x", X)
        requestFields.Add("y", Y)
        requestFields.Add("scene-name", sceneName)
        SendRequest("SetSceneItemPosition", requestFields)
    End Sub

    Public Sub SetSceneItemTransform(itemName As String, rotation As Integer, xScale As Integer, yScale As Integer, sceneName As String)
        Dim requestFields = New JObject()
        requestFields.Add("item", itemName)
        requestFields.Add("x-scale", xScale)
        requestFields.Add("y-scale", yScale)
        requestFields.Add("rotation", rotation)
        requestFields.Add("scene-name", sceneName)
        SendRequest("SetSceneItemTransform", requestFields)
    End Sub


#End Region

#Region "TwitchChat"
    'client.SendMessage("Message Here")
    'Public client As New TwitchClient(New TwitchLib.Models.Client.ConnectionCredentials(My.Settings.TwitchUser, My.Settings.TwitchOAuth))
    Public client As TwitchClient

    Private Sub ConnectToTwitchChat()
        client = New TwitchClient(New TwitchLib.Models.Client.ConnectionCredentials(My.Settings.TwitchUsername, My.Settings.TwitchOAuth))
        AddHandler client.OnConnected, AddressOf Me.onConnected
        AddHandler client.OnMessageReceived, AddressOf Me.globalChatMessageRecieved
        AddHandler client.OnDisconnected, AddressOf Me.onDisconnected
        If client.IsConnected = False Then
            client.Connect()
        End If
        txtTwitchChat.Text = "<< Connecting >>"
    End Sub

    Private Sub DisconnectFromTwitchChat()
        client.LeaveChannel(My.Settings.TwitchChannel)
        If client.IsConnected = True Then
            client.Disconnect()
        End If
        txtTwitchChat.Text = "<< Disconnecting . . . >>"
    End Sub
    Public Sub onConnected(sender As Object, e As OnConnectedArgs)
        CheckForIllegalCrossThreadCalls = False
        client.JoinChannel(My.Settings.TwitchChannel)
        txtTwitchChat.Text = "<< Connected >>"
    End Sub
    Public Sub onDisconnected(sender As Object, e As OnDisconnectedArgs)
        CheckForIllegalCrossThreadCalls = False
        txtTwitchChat.Text = "<< Disconnected from chat server >>"
    End Sub

    Public Sub globalChatMessageRecieved(sender As Object, e As OnMessageReceivedArgs)
        CheckForIllegalCrossThreadCalls = False
        'txtTwitchChat.Text = e.ChatMessage.Username + " " + e.ChatMessage.Message
        txtTwitchChat.Text = e.ChatMessage.Message

        Dim UserOnCooldown As Boolean = False
        For Each item In lstCooldownUser.Items
            If item = e.ChatMessage.Username Then UserOnCooldown = True
        Next
        If UserOnCooldown = False And e.ChatMessage.Username <> "nightbot" Then
            Dim RemoveSpace As String = e.ChatMessage.Message
            RemoveSpace = RemoveSpace.Replace(" ", "")
            If UCase(RemoveSpace).Contains("INDEX+") Then
                lstQueue.Items.Add("INDEX+")
                lstQueue.Items.Add("INDEX+")
            ElseIf UCase(RemoveSpace).Contains("INDEX-") Then
                lstQueue.Items.Add("INDEX-")
                lstQueue.Items.Add("INDEX-")
            ElseIf UCase(RemoveSpace).Contains("MIDDLE+") Then
                lstQueue.Items.Add("MIDDLE+")
                lstQueue.Items.Add("MIDDLE+")
            ElseIf UCase(RemoveSpace).Contains("MIDDLE-") Then
                lstQueue.Items.Add("MIDDLE-")
                lstQueue.Items.Add("MIDDLE-")
            ElseIf UCase(RemoveSpace).Contains("RING+") Then
                lstQueue.Items.Add("RING+")
                lstQueue.Items.Add("RING+")
            ElseIf UCase(RemoveSpace).Contains("RING-") Then
                lstQueue.Items.Add("RING-")
                lstQueue.Items.Add("RING-")
            ElseIf UCase(RemoveSpace).Contains("PINKY+") Then
                lstQueue.Items.Add("PINKY+")
                lstQueue.Items.Add("PINKY+")
            ElseIf UCase(RemoveSpace).Contains("PINKY-") Then
                lstQueue.Items.Add("PINKY-")
                lstQueue.Items.Add("PINKY-")
            ElseIf UCase(RemoveSpace).Contains("THUMB+") Then
                lstQueue.Items.Add("THUMB+")
                lstQueue.Items.Add("THUMB+")
            ElseIf UCase(RemoveSpace).Contains("THUMB-") Then
                lstQueue.Items.Add("THUMB-")
                lstQueue.Items.Add("THUMB-")
            End If
        End If

        lstCooldownUser.Items.Add(e.ChatMessage.Username)
        lstCooldownTime.Items.Add(My.Settings.Cooldown)
    End Sub

#End Region

#Region "Arduino Control"
    Private Sub InitializeArduinoControllerAndHandlers()
        arduinoController = New ArduinoController(9600, "COM3")
        AddHandler arduinoController.AllEvents, AddressOf DefaultGestureHandler
    End Sub

    Private Sub DefaultGestureHandler(o As String)
        'Console.WriteLine("We just handled: " & o)
        If CInt(o) > 99 And CInt(o) < 200 Then
            CheckForMedia(o.ToString)
        End If
    End Sub

#End Region

#Region "Buttons"
    Private Sub cmdTwitchSettings_Click(sender As Object, e As EventArgs) Handles cmdTwitchSettings.Click
        grpTwitch.Left = -5
    End Sub

    Private Sub cmdOBSSettings_Click(sender As Object, e As EventArgs) Handles cmdOBSSettings.Click
        grpOBS.Left = -5
    End Sub

    Private Sub cmdMediaSettings_Click(sender As Object, e As EventArgs) Handles cmdMediaSettings.Click
        grpMedia.Left = -5
    End Sub

    Private Sub cmdCooldownSettings_Click(sender As Object, e As EventArgs) Handles cmdCooldownSettings.Click
        grpCooldown.Left = -5
    End Sub

    Private Sub cmdConnect_Click(sender As Object, e As EventArgs) Handles cmdConnect.Click
        If OBSReady And TwitchReady Then
            ConnectToTwitchChat()
            ConnectToOBS()

            'Claw Control
            InitializeArduinoControllerAndHandlers()

            Muted = False

            ResetClaw()
            tmrQueue.Enabled = True
            tmrCooldown.Enabled = True
            cmdConnect.Enabled = False

            tmrClock.Enabled = True
        End If
    End Sub

    Private Sub cmdSaveTwitch_Click(sender As Object, e As EventArgs) Handles cmdSaveTwitch.Click
        My.Settings.TwitchUsername = txtTwitchUsername.Text
        My.Settings.TwitchOAuth = txtTwitchOAuth.Text
        My.Settings.TwitchChannel = txtTwitchUsername.Text
        OBSTwitchReady()
    End Sub

    Private Sub cmdTwitchBack_Click(sender As Object, e As EventArgs) Handles cmdTwitchBack.Click
        grpTwitch.Left = 215
    End Sub

    Private Sub cmdSaveOBS_Click(sender As Object, e As EventArgs) Handles cmdSaveOBS.Click
        My.Settings.OBSHost = txtOBSHost.Text
        My.Settings.OBSPort = txtOBSPort.Text
        My.Settings.OBSPass = txtOBSPass.Text
        OBSTwitchReady()
    End Sub

    Private Sub cmdOBSBack_Click(sender As Object, e As EventArgs) Handles cmdOBSBack.Click
        grpOBS.Left = 215
    End Sub

    Private Sub cmdAddMedia_Click(sender As Object, e As EventArgs) Handles cmdAddMedia.Click
        If txtMedia.Text <> "" Then lstMedia.Items.Add(txtMedia.Text)
        txtMedia.Text = ""
        SaveMediaString()
    End Sub

    Private Sub cmdRemoveMedia_Click(sender As Object, e As EventArgs) Handles cmdRemoveMedia.Click
        lstMedia.Items.Remove(lstMedia.SelectedItem)
        If lstMedia.Items.Count <> 0 Then
            lstMedia.SelectedIndex = 0
        Else
            txtMedia.Text = ""
        End If
        SaveMediaString()
    End Sub

    Private Sub cmdMediaBack_Click(sender As Object, e As EventArgs) Handles cmdMediaBack.Click
        grpMedia.Left = 215
    End Sub

    Private Sub cmdCooldownBack_Click(sender As Object, e As EventArgs) Handles cmdCooldownBack.Click
        grpCooldown.Left = 215
        My.Settings.Interval = txtInterval.Text
        My.Settings.Cooldown = txtCooldown.Text
        tmrQueue.Interval = My.Settings.Interval
        tmrCooldown.Interval = My.Settings.Cooldown
    End Sub

    Private Sub cmdResetTimerSettings_Click(sender As Object, e As EventArgs) Handles cmdResetTimerSettings.Click
        grpResetTimer.Left = -5
    End Sub

    Private Sub cmdResetTimerBack_Click(sender As Object, e As EventArgs) Handles cmdResetTimerBack.Click
        grpResetTimer.Left = 215
        My.Settings.Reset = Integer.Parse(txtReset.Text)
    End Sub

#End Region

#Region "Timers"
    Private Sub tmrQueue_Tick(sender As Object, e As EventArgs) Handles tmrQueue.Tick
        If MediaReady Then
            tmrMedia.Enabled = True
            tmrQueue.Enabled = False
            tmrReset.Enabled = False

            PlayText()
        Else
            If lstQueue.Items.Count <> 0 Then
                lstQueue.SelectedIndex = 0
                Dim RemoveSpace As String = lstQueue.SelectedItem
                RemoveSpace = RemoveSpace.Replace(" ", "")
                If UCase(RemoveSpace).Contains("INDEX+") Then
                    arduinoController.IndexPlus()
                ElseIf UCase(RemoveSpace).Contains("INDEX-") Then
                    arduinoController.IndexMinus()
                ElseIf UCase(RemoveSpace).Contains("MIDDLE+") Then
                    arduinoController.MiddlePlus()
                ElseIf UCase(RemoveSpace).Contains("MIDDLE-") Then
                    arduinoController.MiddleMinus()
                ElseIf UCase(RemoveSpace).Contains("RING+") Then
                    arduinoController.RingPlus()
                ElseIf UCase(RemoveSpace).Contains("RING-") Then
                    arduinoController.RingMinus()
                ElseIf UCase(RemoveSpace).Contains("PINKY+") Then
                    arduinoController.PinkyPlus()
                ElseIf UCase(RemoveSpace).Contains("PINKY-") Then
                    arduinoController.PinkyMinus()
                ElseIf UCase(RemoveSpace).Contains("THUMB+") Then
                    arduinoController.ThumbPlus()
                ElseIf UCase(RemoveSpace).Contains("THUMB-") Then
                    arduinoController.ThumbMinus()
                End If

                lstQueue.Items.Remove(lstQueue.SelectedItem)
            End If
        End If
    End Sub

    Private Sub tmrCooldown_Tick(sender As Object, e As EventArgs) Handles tmrCooldown.Tick
        tmrCooldown.Interval = 1000
        If lstCooldownTime.Items.Count > 0 Then
            For slot As Integer = 0 To lstCooldownTime.Items.Count - 1
                lstCooldownTime.SelectedIndex = slot
                lstCooldownUser.SelectedIndex = slot
                Dim CountdownNum As Integer = CInt(lstCooldownTime.SelectedItem)
                If CountdownNum = 0 Then
                    lstCooldownUser.Items.Remove(lstCooldownUser.SelectedItem)
                    lstCooldownTime.Items.Remove(lstCooldownTime.SelectedItem)
                    tmrCooldown.Interval = 10
                    GoTo AllDone
                Else
                    CountdownNum -= 1
                    lstCooldownTime.Items.Remove(lstCooldownTime.SelectedItem)
                    lstCooldownTime.Items.Insert(slot, CountdownNum)
                End If
            Next
AllDone:
        End If
    End Sub

    Private Sub tmrMedia_Tick(sender As Object, e As EventArgs) Handles tmrMedia.Tick
        SetSourceRender(MediaSource, False, MediaScene)
        tmrMedia.Enabled = False
        MediaReady = False
        tmrQueue.Enabled = True

        ResetClaw()
    End Sub

    Private Sub tmrReset_Tick(sender As Object, e As EventArgs) Handles tmrReset.Tick
        If lblTotalSeconds.Text = 0 Then
            ResetClaw()
        Else
            lblTotalSeconds.Text -= 1
            Seconds = Integer.Parse(lblTotalSeconds.Text)
            Hours = Seconds \ 3600
            Seconds = Seconds Mod 3600
            Minutes = Seconds \ 60
            Seconds = Seconds Mod 60
            lblHours.Text = Hours.ToString.PadLeft(2, "0"c)
            lblMinutes.Text = Minutes.ToString.PadLeft(2, "0"c)
            lblSeconds.Text = Seconds.ToString.PadLeft(2, "0"c)
            'Timer = Hours.ToString.PadLeft(2, "0"c) & ":" & Minutes.ToString.PadLeft(2, "0"c) & ":" & Seconds.ToString.PadLeft(2, "0"c)
            Timer = Minutes.ToString.PadLeft(2, "0"c) & ":" & Seconds.ToString.PadLeft(2, "0"c)

            Dim ALPHAVAL As String = "ClawResetTimer.txt"
            System.IO.File.WriteAllText(ALPHAVAL, "")
            Dim objWriter As New System.IO.StreamWriter(ALPHAVAL, True)
            objWriter.WriteLine(Timer)
            objWriter.Close()
        End If

    End Sub

    Private Sub tmrText_Tick(sender As Object, e As EventArgs) Handles tmrText.Tick
        SetSourceRender(TextSource, False, TextScene)
        tmrText.Enabled = False

        PlayMedia()
    End Sub

    Private Sub tmrClock_Tick(sender As Object, e As EventArgs) Handles tmrClock.Tick
        tmrClock.Interval = 60000
        Clock = DateTime.Now.ToShortTimeString()
        lblClock.Text = Clock
        Clock = Clock.Substring(0, Clock.Length - 3)
        Clock = Clock.Substring(Clock.Length - 2, 2)
        If Clock = "55" Then

            lblTopTotalSeconds.Text = 300
            tmrTopCountdown.Enabled = True
            SetSourceRender("Hour Reset Timer", True, "Claw Camera")
            SetSourceRender("Hour Reset Info", True, "Claw Camera")
        End If
    End Sub

    Private Sub tmrTopCountdown_Tick(sender As Object, e As EventArgs) Handles tmrTopCountdown.Tick
        If lblTopTotalSeconds.Text = 0 Then
            'play intro
            SetCurrentScene("Episode 1 - Opening")
            DesktopAudioOff()
            tmrIntro.Enabled = True
            SetSourceRender("Hour Reset Timer", False, "Claw Camera")
            SetSourceRender("Hour Reset Info", False, "Claw Camera")
            lblTopTotalSeconds.Text = 300
            tmrTopCountdown.Enabled = False
        Else
            lblTopTotalSeconds.Text -= 1
            TopSeconds = Integer.Parse(lblTopTotalSeconds.Text)
            TopHours = TopSeconds \ 3600
            TopSeconds = TopSeconds Mod 3600
            TopMinutes = TopSeconds \ 60
            TopSeconds = TopSeconds Mod 60
            'Timer = Hours.ToString.PadLeft(2, "0"c) & ":" & Minutes.ToString.PadLeft(2, "0"c) & ":" & Seconds.ToString.PadLeft(2, "0"c)
            TopTimer = TopMinutes.ToString.PadLeft(2, "0"c) & ":" & TopSeconds.ToString.PadLeft(2, "0"c)

            Dim ALPHAVAL As String = "TopOfTheHourCountdown.txt"
            System.IO.File.WriteAllText(ALPHAVAL, "")
            Dim objWriter As New System.IO.StreamWriter(ALPHAVAL, True)
            objWriter.WriteLine(TopTimer)
            objWriter.Close()
        End If
    End Sub

    Private Sub tmrIntro_Tick(sender As Object, e As EventArgs) Handles tmrIntro.Tick
        SetCurrentScene("Claw Camera")
        tmrIntro.Enabled = False
        DesktopAudioOn()
        ResetClaw()
    End Sub
#End Region

#Region "Misc"
    Private Sub lstMedia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lstMedia.SelectedIndexChanged
        txtMedia.Text = lstMedia.SelectedItem
    End Sub

#End Region

#Region "Subroutines"
    Private Sub OBSTwitchReady()
        If My.Settings.OBSHost <> Nothing And My.Settings.OBSPort <> Nothing And My.Settings.OBSPass <> Nothing Then
            OBSReady = True
            picOBSReady.Image = My.Resources.check
        Else
            picOBSReady.Image = My.Resources.cross
        End If
        If My.Settings.TwitchUsername <> Nothing And My.Settings.TwitchOAuth <> Nothing And My.Settings.TwitchChannel <> Nothing Then
            TwitchReady = True
            picTwitchReady.Image = My.Resources.check
        Else
            picTwitchReady.Image = My.Resources.cross
        End If
    End Sub

    Private Sub LoadMediaString()
        Dim FirstCharacter As Integer = Nothing
        Do Until My.Settings.Media = ""
            FirstCharacter = My.Settings.Media.IndexOf("~~")
            If My.Settings.Media.Substring(0, FirstCharacter) <> "" Then lstMedia.Items.Add(My.Settings.Media.Substring(0, FirstCharacter))
            My.Settings.Media = My.Settings.Media.Substring(FirstCharacter + 2, My.Settings.Media.Length - FirstCharacter - 2)
        Loop
    End Sub

    Private Sub SaveMediaString()
        My.Settings.Media = ""
        For Each item In lstMedia.Items
            My.Settings.Media = My.Settings.Media & item & "~~"
        Next
    End Sub

    Private Sub CheckForMedia(message As String)
        Dim FirstCharacter As Integer = Nothing
        Dim QueueString As String
        Dim MediaCode As String = Nothing
        For Each item In lstMedia.Items
            QueueString = item
            If QueueString.StartsWith(message) Then
                FirstCharacter = QueueString.IndexOf(",")
                'find Media Code
                MediaCode = QueueString.Substring(0, FirstCharacter)
                QueueString = QueueString.Substring(FirstCharacter + 2, QueueString.Length - FirstCharacter - 2)
                FirstCharacter = QueueString.IndexOf(",")
                'find Media Source
                MediaSource = QueueString.Substring(0, FirstCharacter)
                QueueString = QueueString.Substring(FirstCharacter + 2, QueueString.Length - FirstCharacter - 2)
                FirstCharacter = QueueString.IndexOf(",")
                'find Media Scene
                MediaScene = QueueString.Substring(0, FirstCharacter)
                QueueString = QueueString.Substring(FirstCharacter + 2, QueueString.Length - FirstCharacter - 2)
                FirstCharacter = QueueString.IndexOf(",")
                'find Media Duration
                MediaDuration = QueueString.Substring(0, FirstCharacter)
                QueueString = QueueString.Substring(FirstCharacter + 2, QueueString.Length - FirstCharacter - 2)
                FirstCharacter = QueueString.IndexOf(",")
                'find Text Source
                TextSource = QueueString.Substring(0, FirstCharacter)
                'find Text Scene
                TextScene = QueueString.Substring(FirstCharacter + 2, QueueString.Length - FirstCharacter - 2)

                MediaReady = True
            End If
        Next
    End Sub

    Private Sub PlayText()
        SetSourceRender(TextSource, True, TextScene)
        DesktopAudioOff()
        tmrText.Enabled = True
    End Sub

    Private Sub PlayMedia()
        SetSourceRender(MediaSource, True, MediaScene)
        tmrMedia.Interval = MediaDuration
        tmrMedia.Enabled = True
    End Sub

    Private Sub ResetClaw()
        lstQueue.Items.Clear()
        lstCooldownUser.Items.Clear()
        lstCooldownTime.Items.Clear()

        'Reset Arduino Claw position
        arduinoController.SendRawCommunications("100")

        lblTotalSeconds.Text = My.Settings.Reset
        tmrReset.Enabled = True

        DesktopAudioOn()
    End Sub

    Private Sub DesktopAudioOn()
        If Muted Then
            ToggleMute("Desktop Audio")
            Muted = False
        End If
    End Sub

    Private Sub DesktopAudioOff()
        If Not Muted Then
            ToggleMute("Desktop Audio")
            Muted = True
        End If
    End Sub

#End Region


#Region "Debug"
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        arduinoController.IndexPlus()
        'SetSourceRender("Middle Finger Text", True, "Words")
        'DesktopAudioOn()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        arduinoController.IndexMinus()
        'SetSourceRender("Middle Finger Text", False, "Words")
        'DesktopAudioOff()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        arduinoController.MiddlePlus()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        arduinoController.MiddleMinus()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        arduinoController.RingPlus()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        arduinoController.RingMinus()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        arduinoController.PinkyPlus()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        arduinoController.PinkyMinus()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        arduinoController.ThumbPlus()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        arduinoController.ThumbMinus()
    End Sub
#End Region
End Class
