Imports System.Threading
Imports System.IO.Ports
Imports System.Threading.Tasks

Public Class ArduinoController
    'The following SendOrPostCallback objects are typed event handlers whose return value is void
    'and single input parameter is of type object
    '(i.e.
    '   int myDefaultGestureHandler(object state) {
    '       Console.WriteLine("The default event handler was triggered!");
    '   }
    '
    '   DefaultGesture += myDefaultGestureHandler;
    ')

    Public Event AllEvents(ByVal commsCode As String)
    Public DefaultGesture As SendOrPostCallback
    Public MiddleFingerGesture As SendOrPostCallback
    Public ThumbsUpGesture As SendOrPostCallback
    Public PinkysOutGesture As SendOrPostCallback
    Public RingOnItGesture As SendOrPostCallback
    Public RudePointingGesture As SendOrPostCallback
    Public PowerFistGesture As SendOrPostCallback
    Public SpiderManGesture As SendOrPostCallback
    Public TheStinkGesture As SendOrPostCallback

    Private receiveCommsDictionary As System.Collections.Generic.Dictionary(Of String, SendOrPostCallback)

    Private arduinoSerialPort As SerialPort

    Private originalSynchronizationContext As SynchronizationContext

    Public Sub New(ByVal baudRate As Integer, ByVal portName As String)
        receiveCommsDictionary = New System.Collections.Generic.Dictionary(Of String, SendOrPostCallback)()
        receiveCommsDictionary.Add("100", DefaultGesture)
        receiveCommsDictionary.Add("101", MiddleFingerGesture)
        receiveCommsDictionary.Add("102", ThumbsUpGesture)
        receiveCommsDictionary.Add("103", PinkysOutGesture)
        receiveCommsDictionary.Add("104", RingOnItGesture)
        receiveCommsDictionary.Add("105", RudePointingGesture)
        receiveCommsDictionary.Add("106", PowerFistGesture)
        receiveCommsDictionary.Add("107", SpiderManGesture)
        receiveCommsDictionary.Add("108", TheStinkGesture)
        arduinoSerialPort = New SerialPort()
        arduinoSerialPort.BaudRate = baudRate
        arduinoSerialPort.PortName = portName
        Try
            arduinoSerialPort.Open()
        Catch e As System.UnauthorizedAccessException
            Console.WriteLine("We were unable to open the arudino serial port on: " & portName & ". Ensure nothing else is reading from the same port such as the Arduino Serial Monitor.")
        Catch e As System.IO.IOException
            Console.WriteLine("Is the hand connected to the computer? And on port: " & portName)
        End Try

        originalSynchronizationContext = SynchronizationContext.Current
        Task.Factory.StartNew(AddressOf listenToArduinoCallbacks, TaskCreationOptions.LongRunning)
    End Sub


    'This function runs in a separate thread which is started in the constructor of this object.
    'It listens for messages sent from the Arduino and triggers subscribed event handlers.
    Private Sub listenToArduinoCallbacks()
        While arduinoSerialPort.IsOpen
            Dim commsCode As String = arduinoSerialPort.ReadLine().Trim()
            originalSynchronizationContext.Post(Sub(state) RaiseEvent AllEvents(commsCode), Nothing)

            'For more specific events we have this map that we're not currently using
            If receiveCommsDictionary.ContainsKey(commsCode) AndAlso receiveCommsDictionary(commsCode) IsNot Nothing Then
                originalSynchronizationContext.Post(receiveCommsDictionary(commsCode), Nothing)
            End If
        End While
    End Sub

    Public Sub SendRawCommunications(ByVal comms As String)
        arduinoSerialPort.WriteLine(comms)
    End Sub

    Public Sub PinkyPlus()
        arduinoSerialPort.WriteLine("1")
    End Sub

    Public Sub PinkyMinus()
        arduinoSerialPort.WriteLine("2")
    End Sub

    Public Sub RingPlus()
        arduinoSerialPort.WriteLine("3")
    End Sub

    Public Sub RingMinus()
        arduinoSerialPort.WriteLine("4")
    End Sub

    Public Sub MiddlePlus()
        arduinoSerialPort.WriteLine("5")
    End Sub

    Public Sub MiddleMinus()
        arduinoSerialPort.WriteLine("6")
    End Sub

    Public Sub IndexPlus()
        arduinoSerialPort.WriteLine("7")
    End Sub

    Public Sub IndexMinus()
        arduinoSerialPort.WriteLine("8")
    End Sub

    Public Sub ThumbPlus()
        arduinoSerialPort.WriteLine("9")
    End Sub

    Public Sub ThumbMinus()
        arduinoSerialPort.WriteLine("10")
    End Sub
End Class
