Imports System.Threading
Imports System.IO.Ports
Imports System.Threading.Tasks

Public Class ArduinoController

    Enum Gestures
        DefaultGesture = 100
        MiddleFinger = 101
        ThumbsUp = 102
        PinkysOut = 103
        RingOnIt = 104
        RudePointing = 105
        PowerFist = 106
        SpiderMan = 107
        TheStink = 108
        HangLoose = 109
        RockOut = 110
        Peace = 111
        Ok = 112
        Loser = 113
        ScoutsHonor = 114
    End Enum

    Public Event AllEvents(ByVal commsCode As String)

    Private arduinoSerialPort As SerialPort

    Private originalSynchronizationContext As SynchronizationContext

    Public Sub New(ByVal baudRate As Integer, ByVal portName As String)
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
