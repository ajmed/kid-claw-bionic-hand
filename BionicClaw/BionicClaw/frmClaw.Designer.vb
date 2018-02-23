<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmClaw
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmClaw))
        Me.txtTwitchChat = New System.Windows.Forms.RichTextBox()
        Me.lblBTC = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.grpTwitch = New System.Windows.Forms.GroupBox()
        Me.cmdTwitchBack = New System.Windows.Forms.Button()
        Me.cmdSaveTwitch = New System.Windows.Forms.Button()
        Me.txtTwitchOAuth = New System.Windows.Forms.TextBox()
        Me.txtTwitchUsername = New System.Windows.Forms.TextBox()
        Me.lblUaP = New System.Windows.Forms.Label()
        Me.lblTwitchChat = New System.Windows.Forms.Label()
        Me.grpOBS = New System.Windows.Forms.GroupBox()
        Me.Button9 = New System.Windows.Forms.Button()
        Me.Button10 = New System.Windows.Forms.Button()
        Me.Button5 = New System.Windows.Forms.Button()
        Me.Button6 = New System.Windows.Forms.Button()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Button4 = New System.Windows.Forms.Button()
        Me.Button3 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.cmdOBSBack = New System.Windows.Forms.Button()
        Me.txtOBSPass = New System.Windows.Forms.TextBox()
        Me.cmdSaveOBS = New System.Windows.Forms.Button()
        Me.txtOBSPort = New System.Windows.Forms.TextBox()
        Me.txtOBSHost = New System.Windows.Forms.TextBox()
        Me.lblHPP = New System.Windows.Forms.Label()
        Me.cmdTwitchSettings = New System.Windows.Forms.Button()
        Me.cmdOBSSettings = New System.Windows.Forms.Button()
        Me.cmdConnect = New System.Windows.Forms.Button()
        Me.grpMedia = New System.Windows.Forms.GroupBox()
        Me.cmdRemoveMedia = New System.Windows.Forms.Button()
        Me.txtMedia = New System.Windows.Forms.TextBox()
        Me.cmdAddMedia = New System.Windows.Forms.Button()
        Me.lstMedia = New System.Windows.Forms.ListBox()
        Me.cmdMediaBack = New System.Windows.Forms.Button()
        Me.cmdMediaSettings = New System.Windows.Forms.Button()
        Me.grpCooldown = New System.Windows.Forms.GroupBox()
        Me.txtInterval = New System.Windows.Forms.TextBox()
        Me.lblInterval = New System.Windows.Forms.Label()
        Me.txtCooldown = New System.Windows.Forms.TextBox()
        Me.lblCooldown = New System.Windows.Forms.Label()
        Me.lstCooldownUser = New System.Windows.Forms.ListBox()
        Me.lstCooldownTime = New System.Windows.Forms.ListBox()
        Me.cmdCooldownBack = New System.Windows.Forms.Button()
        Me.cmdCooldownSettings = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tmrQueue = New System.Windows.Forms.Timer(Me.components)
        Me.lstQueue = New System.Windows.Forms.ListBox()
        Me.tmrCooldown = New System.Windows.Forms.Timer(Me.components)
        Me.tmrMedia = New System.Windows.Forms.Timer(Me.components)
        Me.cmdResetTimerSettings = New System.Windows.Forms.Button()
        Me.grpResetTimer = New System.Windows.Forms.GroupBox()
        Me.lblTotalSeconds = New System.Windows.Forms.Label()
        Me.lblMinutes = New System.Windows.Forms.Label()
        Me.lblHours = New System.Windows.Forms.Label()
        Me.lblSeconds = New System.Windows.Forms.Label()
        Me.txtReset = New System.Windows.Forms.TextBox()
        Me.lblReset = New System.Windows.Forms.Label()
        Me.picReset = New System.Windows.Forms.PictureBox()
        Me.cmdResetTimerBack = New System.Windows.Forms.Button()
        Me.tmrReset = New System.Windows.Forms.Timer(Me.components)
        Me.picOBSReady = New System.Windows.Forms.PictureBox()
        Me.picTwitchReady = New System.Windows.Forms.PictureBox()
        Me.picClaw = New System.Windows.Forms.PictureBox()
        Me.tmrText = New System.Windows.Forms.Timer(Me.components)
        Me.tmrClock = New System.Windows.Forms.Timer(Me.components)
        Me.tmrTopCountdown = New System.Windows.Forms.Timer(Me.components)
        Me.tmrIntro = New System.Windows.Forms.Timer(Me.components)
        Me.lblTopTotalSeconds = New System.Windows.Forms.Label()
        Me.lblClock = New System.Windows.Forms.Label()
        Me.Button11 = New System.Windows.Forms.Button()
        Me.grpTwitch.SuspendLayout()
        Me.grpOBS.SuspendLayout()
        Me.grpMedia.SuspendLayout()
        Me.grpCooldown.SuspendLayout()
        Me.grpResetTimer.SuspendLayout()
        CType(Me.picReset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picOBSReady, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTwitchReady, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picClaw, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTwitchChat
        '
        Me.txtTwitchChat.Enabled = False
        Me.txtTwitchChat.Location = New System.Drawing.Point(20, 137)
        Me.txtTwitchChat.Name = "txtTwitchChat"
        Me.txtTwitchChat.Size = New System.Drawing.Size(160, 51)
        Me.txtTwitchChat.TabIndex = 429
        Me.txtTwitchChat.Text = ""
        '
        'lblBTC
        '
        Me.lblBTC.AutoSize = True
        Me.lblBTC.Font = New System.Drawing.Font("Tahoma", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBTC.Location = New System.Drawing.Point(21, 9)
        Me.lblBTC.Name = "lblBTC"
        Me.lblBTC.Size = New System.Drawing.Size(142, 19)
        Me.lblBTC.TabIndex = 430
        Me.lblBTC.Text = "Bionic Twitch Claw"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Black
        Me.Label2.Location = New System.Drawing.Point(22, 28)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(140, 2)
        Me.Label2.TabIndex = 431
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(200, 8)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(5, 345)
        Me.Label3.TabIndex = 432
        '
        'grpTwitch
        '
        Me.grpTwitch.Controls.Add(Me.cmdTwitchBack)
        Me.grpTwitch.Controls.Add(Me.cmdSaveTwitch)
        Me.grpTwitch.Controls.Add(Me.txtTwitchOAuth)
        Me.grpTwitch.Controls.Add(Me.txtTwitchUsername)
        Me.grpTwitch.Controls.Add(Me.lblUaP)
        Me.grpTwitch.Controls.Add(Me.lblTwitchChat)
        Me.grpTwitch.Controls.Add(Me.txtTwitchChat)
        Me.grpTwitch.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grpTwitch.Location = New System.Drawing.Point(214, 142)
        Me.grpTwitch.Name = "grpTwitch"
        Me.grpTwitch.Size = New System.Drawing.Size(190, 212)
        Me.grpTwitch.TabIndex = 433
        Me.grpTwitch.TabStop = False
        Me.grpTwitch.Text = "Twitch"
        '
        'cmdTwitchBack
        '
        Me.cmdTwitchBack.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdTwitchBack.Location = New System.Drawing.Point(171, 192)
        Me.cmdTwitchBack.Name = "cmdTwitchBack"
        Me.cmdTwitchBack.Size = New System.Drawing.Size(20, 20)
        Me.cmdTwitchBack.TabIndex = 436
        Me.cmdTwitchBack.Text = "X"
        Me.cmdTwitchBack.UseVisualStyleBackColor = True
        '
        'cmdSaveTwitch
        '
        Me.cmdSaveTwitch.Location = New System.Drawing.Point(36, 73)
        Me.cmdSaveTwitch.Name = "cmdSaveTwitch"
        Me.cmdSaveTwitch.Size = New System.Drawing.Size(129, 23)
        Me.cmdSaveTwitch.TabIndex = 435
        Me.cmdSaveTwitch.Text = "Save Credentials"
        Me.cmdSaveTwitch.UseVisualStyleBackColor = True
        '
        'txtTwitchOAuth
        '
        Me.txtTwitchOAuth.Location = New System.Drawing.Point(55, 44)
        Me.txtTwitchOAuth.Name = "txtTwitchOAuth"
        Me.txtTwitchOAuth.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtTwitchOAuth.Size = New System.Drawing.Size(129, 20)
        Me.txtTwitchOAuth.TabIndex = 433
        '
        'txtTwitchUsername
        '
        Me.txtTwitchUsername.Location = New System.Drawing.Point(55, 20)
        Me.txtTwitchUsername.Name = "txtTwitchUsername"
        Me.txtTwitchUsername.Size = New System.Drawing.Size(129, 20)
        Me.txtTwitchUsername.TabIndex = 432
        '
        'lblUaP
        '
        Me.lblUaP.Location = New System.Drawing.Point(16, 21)
        Me.lblUaP.Name = "lblUaP"
        Me.lblUaP.Size = New System.Drawing.Size(42, 41)
        Me.lblUaP.TabIndex = 434
        Me.lblUaP.Text = "USER:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "OAuth:"
        Me.lblUaP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblTwitchChat
        '
        Me.lblTwitchChat.AutoSize = True
        Me.lblTwitchChat.Font = New System.Drawing.Font("Tahoma", 10.0!)
        Me.lblTwitchChat.Location = New System.Drawing.Point(17, 117)
        Me.lblTwitchChat.Name = "lblTwitchChat"
        Me.lblTwitchChat.Size = New System.Drawing.Size(69, 17)
        Me.lblTwitchChat.TabIndex = 431
        Me.lblTwitchChat.Text = "Chat Text"
        '
        'grpOBS
        '
        Me.grpOBS.Controls.Add(Me.Button11)
        Me.grpOBS.Controls.Add(Me.Button9)
        Me.grpOBS.Controls.Add(Me.Button10)
        Me.grpOBS.Controls.Add(Me.Button5)
        Me.grpOBS.Controls.Add(Me.Button6)
        Me.grpOBS.Controls.Add(Me.Button7)
        Me.grpOBS.Controls.Add(Me.Button8)
        Me.grpOBS.Controls.Add(Me.Button4)
        Me.grpOBS.Controls.Add(Me.Button3)
        Me.grpOBS.Controls.Add(Me.Button2)
        Me.grpOBS.Controls.Add(Me.Button1)
        Me.grpOBS.Controls.Add(Me.cmdOBSBack)
        Me.grpOBS.Controls.Add(Me.txtOBSPass)
        Me.grpOBS.Controls.Add(Me.cmdSaveOBS)
        Me.grpOBS.Controls.Add(Me.txtOBSPort)
        Me.grpOBS.Controls.Add(Me.txtOBSHost)
        Me.grpOBS.Controls.Add(Me.lblHPP)
        Me.grpOBS.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grpOBS.Location = New System.Drawing.Point(433, 142)
        Me.grpOBS.Name = "grpOBS"
        Me.grpOBS.Size = New System.Drawing.Size(190, 212)
        Me.grpOBS.TabIndex = 436
        Me.grpOBS.TabStop = False
        Me.grpOBS.Text = "OBS"
        '
        'Button9
        '
        Me.Button9.Location = New System.Drawing.Point(161, 134)
        Me.Button9.Name = "Button9"
        Me.Button9.Size = New System.Drawing.Size(25, 23)
        Me.Button9.TabIndex = 451
        Me.Button9.UseVisualStyleBackColor = True
        '
        'Button10
        '
        Me.Button10.Location = New System.Drawing.Point(161, 165)
        Me.Button10.Name = "Button10"
        Me.Button10.Size = New System.Drawing.Size(25, 23)
        Me.Button10.TabIndex = 450
        Me.Button10.UseVisualStyleBackColor = True
        '
        'Button5
        '
        Me.Button5.Location = New System.Drawing.Point(99, 134)
        Me.Button5.Name = "Button5"
        Me.Button5.Size = New System.Drawing.Size(25, 23)
        Me.Button5.TabIndex = 449
        Me.Button5.UseVisualStyleBackColor = True
        '
        'Button6
        '
        Me.Button6.Location = New System.Drawing.Point(99, 165)
        Me.Button6.Name = "Button6"
        Me.Button6.Size = New System.Drawing.Size(25, 23)
        Me.Button6.TabIndex = 448
        Me.Button6.UseVisualStyleBackColor = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(130, 134)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(25, 23)
        Me.Button7.TabIndex = 447
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(130, 165)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(25, 23)
        Me.Button8.TabIndex = 446
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(68, 165)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(25, 23)
        Me.Button4.TabIndex = 445
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(68, 134)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(25, 23)
        Me.Button3.TabIndex = 444
        Me.Button3.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(37, 165)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(25, 23)
        Me.Button2.TabIndex = 443
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(37, 134)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(25, 23)
        Me.Button1.TabIndex = 442
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cmdOBSBack
        '
        Me.cmdOBSBack.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdOBSBack.Location = New System.Drawing.Point(171, 192)
        Me.cmdOBSBack.Name = "cmdOBSBack"
        Me.cmdOBSBack.Size = New System.Drawing.Size(20, 20)
        Me.cmdOBSBack.TabIndex = 441
        Me.cmdOBSBack.Text = "X"
        Me.cmdOBSBack.UseVisualStyleBackColor = True
        '
        'txtOBSPass
        '
        Me.txtOBSPass.Location = New System.Drawing.Point(52, 67)
        Me.txtOBSPass.Name = "txtOBSPass"
        Me.txtOBSPass.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtOBSPass.Size = New System.Drawing.Size(129, 20)
        Me.txtOBSPass.TabIndex = 440
        '
        'cmdSaveOBS
        '
        Me.cmdSaveOBS.Location = New System.Drawing.Point(36, 97)
        Me.cmdSaveOBS.Name = "cmdSaveOBS"
        Me.cmdSaveOBS.Size = New System.Drawing.Size(129, 23)
        Me.cmdSaveOBS.TabIndex = 439
        Me.cmdSaveOBS.Text = "Save Credentials"
        Me.cmdSaveOBS.UseVisualStyleBackColor = True
        '
        'txtOBSPort
        '
        Me.txtOBSPort.Location = New System.Drawing.Point(52, 43)
        Me.txtOBSPort.Name = "txtOBSPort"
        Me.txtOBSPort.Size = New System.Drawing.Size(129, 20)
        Me.txtOBSPort.TabIndex = 437
        '
        'txtOBSHost
        '
        Me.txtOBSHost.Location = New System.Drawing.Point(52, 19)
        Me.txtOBSHost.Name = "txtOBSHost"
        Me.txtOBSHost.Size = New System.Drawing.Size(129, 20)
        Me.txtOBSHost.TabIndex = 436
        '
        'lblHPP
        '
        Me.lblHPP.Location = New System.Drawing.Point(20, 20)
        Me.lblHPP.Name = "lblHPP"
        Me.lblHPP.Size = New System.Drawing.Size(35, 67)
        Me.lblHPP.TabIndex = 438
        Me.lblHPP.Text = "Host:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Port:" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "Pass:"
        Me.lblHPP.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cmdTwitchSettings
        '
        Me.cmdTwitchSettings.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cmdTwitchSettings.Location = New System.Drawing.Point(12, 156)
        Me.cmdTwitchSettings.Name = "cmdTwitchSettings"
        Me.cmdTwitchSettings.Size = New System.Drawing.Size(138, 23)
        Me.cmdTwitchSettings.TabIndex = 437
        Me.cmdTwitchSettings.Text = "Twitch Settings"
        Me.cmdTwitchSettings.UseVisualStyleBackColor = True
        '
        'cmdOBSSettings
        '
        Me.cmdOBSSettings.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cmdOBSSettings.Location = New System.Drawing.Point(12, 185)
        Me.cmdOBSSettings.Name = "cmdOBSSettings"
        Me.cmdOBSSettings.Size = New System.Drawing.Size(138, 23)
        Me.cmdOBSSettings.TabIndex = 438
        Me.cmdOBSSettings.Text = "OBS Settings"
        Me.cmdOBSSettings.UseVisualStyleBackColor = True
        '
        'cmdConnect
        '
        Me.cmdConnect.Font = New System.Drawing.Font("Tahoma", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdConnect.Location = New System.Drawing.Point(12, 302)
        Me.cmdConnect.Name = "cmdConnect"
        Me.cmdConnect.Size = New System.Drawing.Size(167, 51)
        Me.cmdConnect.TabIndex = 441
        Me.cmdConnect.Text = "Connect"
        Me.cmdConnect.UseVisualStyleBackColor = True
        '
        'grpMedia
        '
        Me.grpMedia.Controls.Add(Me.cmdRemoveMedia)
        Me.grpMedia.Controls.Add(Me.txtMedia)
        Me.grpMedia.Controls.Add(Me.cmdAddMedia)
        Me.grpMedia.Controls.Add(Me.lstMedia)
        Me.grpMedia.Controls.Add(Me.cmdMediaBack)
        Me.grpMedia.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grpMedia.Location = New System.Drawing.Point(651, 142)
        Me.grpMedia.Name = "grpMedia"
        Me.grpMedia.Size = New System.Drawing.Size(190, 212)
        Me.grpMedia.TabIndex = 442
        Me.grpMedia.TabStop = False
        Me.grpMedia.Text = "Media"
        '
        'cmdRemoveMedia
        '
        Me.cmdRemoveMedia.Location = New System.Drawing.Point(100, 145)
        Me.cmdRemoveMedia.Name = "cmdRemoveMedia"
        Me.cmdRemoveMedia.Size = New System.Drawing.Size(79, 23)
        Me.cmdRemoveMedia.TabIndex = 446
        Me.cmdRemoveMedia.Text = "Remove"
        Me.cmdRemoveMedia.UseVisualStyleBackColor = True
        '
        'txtMedia
        '
        Me.txtMedia.Location = New System.Drawing.Point(15, 122)
        Me.txtMedia.Name = "txtMedia"
        Me.txtMedia.Size = New System.Drawing.Size(164, 20)
        Me.txtMedia.TabIndex = 445
        '
        'cmdAddMedia
        '
        Me.cmdAddMedia.Location = New System.Drawing.Point(15, 145)
        Me.cmdAddMedia.Name = "cmdAddMedia"
        Me.cmdAddMedia.Size = New System.Drawing.Size(79, 23)
        Me.cmdAddMedia.TabIndex = 444
        Me.cmdAddMedia.Text = "Add"
        Me.cmdAddMedia.UseVisualStyleBackColor = True
        '
        'lstMedia
        '
        Me.lstMedia.FormattingEnabled = True
        Me.lstMedia.Location = New System.Drawing.Point(15, 21)
        Me.lstMedia.Name = "lstMedia"
        Me.lstMedia.Size = New System.Drawing.Size(164, 95)
        Me.lstMedia.TabIndex = 443
        '
        'cmdMediaBack
        '
        Me.cmdMediaBack.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdMediaBack.Location = New System.Drawing.Point(171, 192)
        Me.cmdMediaBack.Name = "cmdMediaBack"
        Me.cmdMediaBack.Size = New System.Drawing.Size(20, 20)
        Me.cmdMediaBack.TabIndex = 441
        Me.cmdMediaBack.Text = "X"
        Me.cmdMediaBack.UseVisualStyleBackColor = True
        '
        'cmdMediaSettings
        '
        Me.cmdMediaSettings.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cmdMediaSettings.Location = New System.Drawing.Point(12, 214)
        Me.cmdMediaSettings.Name = "cmdMediaSettings"
        Me.cmdMediaSettings.Size = New System.Drawing.Size(167, 23)
        Me.cmdMediaSettings.TabIndex = 443
        Me.cmdMediaSettings.Text = "Media Settings"
        Me.cmdMediaSettings.UseVisualStyleBackColor = True
        '
        'grpCooldown
        '
        Me.grpCooldown.Controls.Add(Me.txtInterval)
        Me.grpCooldown.Controls.Add(Me.lblInterval)
        Me.grpCooldown.Controls.Add(Me.txtCooldown)
        Me.grpCooldown.Controls.Add(Me.lblCooldown)
        Me.grpCooldown.Controls.Add(Me.lstCooldownUser)
        Me.grpCooldown.Controls.Add(Me.lstCooldownTime)
        Me.grpCooldown.Controls.Add(Me.cmdCooldownBack)
        Me.grpCooldown.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grpCooldown.Location = New System.Drawing.Point(868, 142)
        Me.grpCooldown.Name = "grpCooldown"
        Me.grpCooldown.Size = New System.Drawing.Size(190, 212)
        Me.grpCooldown.TabIndex = 443
        Me.grpCooldown.TabStop = False
        Me.grpCooldown.Text = "Cooldown and Interval"
        '
        'txtInterval
        '
        Me.txtInterval.Location = New System.Drawing.Point(66, 29)
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(109, 20)
        Me.txtInterval.TabIndex = 448
        '
        'lblInterval
        '
        Me.lblInterval.Location = New System.Drawing.Point(6, 28)
        Me.lblInterval.Name = "lblInterval"
        Me.lblInterval.Size = New System.Drawing.Size(62, 19)
        Me.lblInterval.TabIndex = 447
        Me.lblInterval.Text = "Interval:"
        Me.lblInterval.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCooldown
        '
        Me.txtCooldown.Location = New System.Drawing.Point(66, 67)
        Me.txtCooldown.Name = "txtCooldown"
        Me.txtCooldown.Size = New System.Drawing.Size(109, 20)
        Me.txtCooldown.TabIndex = 446
        '
        'lblCooldown
        '
        Me.lblCooldown.Location = New System.Drawing.Point(6, 66)
        Me.lblCooldown.Name = "lblCooldown"
        Me.lblCooldown.Size = New System.Drawing.Size(62, 19)
        Me.lblCooldown.TabIndex = 444
        Me.lblCooldown.Text = "Cooldown:"
        Me.lblCooldown.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lstCooldownUser
        '
        Me.lstCooldownUser.FormattingEnabled = True
        Me.lstCooldownUser.Location = New System.Drawing.Point(19, 91)
        Me.lstCooldownUser.Name = "lstCooldownUser"
        Me.lstCooldownUser.Size = New System.Drawing.Size(115, 95)
        Me.lstCooldownUser.TabIndex = 442
        '
        'lstCooldownTime
        '
        Me.lstCooldownTime.FormattingEnabled = True
        Me.lstCooldownTime.Location = New System.Drawing.Point(140, 91)
        Me.lstCooldownTime.Name = "lstCooldownTime"
        Me.lstCooldownTime.Size = New System.Drawing.Size(35, 95)
        Me.lstCooldownTime.TabIndex = 443
        '
        'cmdCooldownBack
        '
        Me.cmdCooldownBack.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdCooldownBack.Location = New System.Drawing.Point(171, 192)
        Me.cmdCooldownBack.Name = "cmdCooldownBack"
        Me.cmdCooldownBack.Size = New System.Drawing.Size(20, 20)
        Me.cmdCooldownBack.TabIndex = 441
        Me.cmdCooldownBack.Text = "X"
        Me.cmdCooldownBack.UseVisualStyleBackColor = True
        '
        'cmdCooldownSettings
        '
        Me.cmdCooldownSettings.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cmdCooldownSettings.Location = New System.Drawing.Point(12, 243)
        Me.cmdCooldownSettings.Name = "cmdCooldownSettings"
        Me.cmdCooldownSettings.Size = New System.Drawing.Size(167, 23)
        Me.cmdCooldownSettings.TabIndex = 449
        Me.cmdCooldownSettings.Text = "Cooldown Settings"
        Me.cmdCooldownSettings.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Black
        Me.Label1.Location = New System.Drawing.Point(22, 297)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(140, 2)
        Me.Label1.TabIndex = 450
        '
        'tmrQueue
        '
        Me.tmrQueue.Interval = 1000
        '
        'lstQueue
        '
        Me.lstQueue.FormattingEnabled = True
        Me.lstQueue.Location = New System.Drawing.Point(1285, 9)
        Me.lstQueue.Name = "lstQueue"
        Me.lstQueue.Size = New System.Drawing.Size(128, 342)
        Me.lstQueue.TabIndex = 444
        '
        'tmrCooldown
        '
        Me.tmrCooldown.Interval = 1000
        '
        'tmrMedia
        '
        Me.tmrMedia.Interval = 1000
        '
        'cmdResetTimerSettings
        '
        Me.cmdResetTimerSettings.Font = New System.Drawing.Font("Tahoma", 8.25!)
        Me.cmdResetTimerSettings.Location = New System.Drawing.Point(12, 271)
        Me.cmdResetTimerSettings.Name = "cmdResetTimerSettings"
        Me.cmdResetTimerSettings.Size = New System.Drawing.Size(167, 23)
        Me.cmdResetTimerSettings.TabIndex = 451
        Me.cmdResetTimerSettings.Text = "Reset Timer Settings"
        Me.cmdResetTimerSettings.UseVisualStyleBackColor = True
        '
        'grpResetTimer
        '
        Me.grpResetTimer.Controls.Add(Me.lblTotalSeconds)
        Me.grpResetTimer.Controls.Add(Me.lblMinutes)
        Me.grpResetTimer.Controls.Add(Me.lblHours)
        Me.grpResetTimer.Controls.Add(Me.lblSeconds)
        Me.grpResetTimer.Controls.Add(Me.txtReset)
        Me.grpResetTimer.Controls.Add(Me.lblReset)
        Me.grpResetTimer.Controls.Add(Me.picReset)
        Me.grpResetTimer.Controls.Add(Me.cmdResetTimerBack)
        Me.grpResetTimer.Font = New System.Drawing.Font("Tahoma", 8.0!)
        Me.grpResetTimer.Location = New System.Drawing.Point(1075, 142)
        Me.grpResetTimer.Name = "grpResetTimer"
        Me.grpResetTimer.Size = New System.Drawing.Size(190, 212)
        Me.grpResetTimer.TabIndex = 447
        Me.grpResetTimer.TabStop = False
        Me.grpResetTimer.Text = "Reset Timer"
        '
        'lblTotalSeconds
        '
        Me.lblTotalSeconds.AutoSize = True
        Me.lblTotalSeconds.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTotalSeconds.Location = New System.Drawing.Point(40, 80)
        Me.lblTotalSeconds.Name = "lblTotalSeconds"
        Me.lblTotalSeconds.Size = New System.Drawing.Size(18, 20)
        Me.lblTotalSeconds.TabIndex = 452
        Me.lblTotalSeconds.Text = "0"
        Me.lblTotalSeconds.Visible = False
        '
        'lblMinutes
        '
        Me.lblMinutes.AutoSize = True
        Me.lblMinutes.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinutes.Location = New System.Drawing.Point(98, 80)
        Me.lblMinutes.Name = "lblMinutes"
        Me.lblMinutes.Size = New System.Drawing.Size(18, 20)
        Me.lblMinutes.TabIndex = 451
        Me.lblMinutes.Text = "0"
        '
        'lblHours
        '
        Me.lblHours.AutoSize = True
        Me.lblHours.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHours.Location = New System.Drawing.Point(64, 80)
        Me.lblHours.Name = "lblHours"
        Me.lblHours.Size = New System.Drawing.Size(18, 20)
        Me.lblHours.TabIndex = 450
        Me.lblHours.Text = "0"
        '
        'lblSeconds
        '
        Me.lblSeconds.AutoSize = True
        Me.lblSeconds.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSeconds.Location = New System.Drawing.Point(132, 80)
        Me.lblSeconds.Name = "lblSeconds"
        Me.lblSeconds.Size = New System.Drawing.Size(18, 20)
        Me.lblSeconds.TabIndex = 449
        Me.lblSeconds.Text = "0"
        '
        'txtReset
        '
        Me.txtReset.Location = New System.Drawing.Point(63, 120)
        Me.txtReset.Name = "txtReset"
        Me.txtReset.Size = New System.Drawing.Size(109, 20)
        Me.txtReset.TabIndex = 448
        '
        'lblReset
        '
        Me.lblReset.Location = New System.Drawing.Point(19, 119)
        Me.lblReset.Name = "lblReset"
        Me.lblReset.Size = New System.Drawing.Size(46, 19)
        Me.lblReset.TabIndex = 447
        Me.lblReset.Text = "Timer:"
        Me.lblReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picReset
        '
        Me.picReset.Image = Global.BionicClaw.My.Resources.Resources.reset
        Me.picReset.Location = New System.Drawing.Point(37, 14)
        Me.picReset.Name = "picReset"
        Me.picReset.Size = New System.Drawing.Size(116, 59)
        Me.picReset.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picReset.TabIndex = 442
        Me.picReset.TabStop = False
        '
        'cmdResetTimerBack
        '
        Me.cmdResetTimerBack.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmdResetTimerBack.Location = New System.Drawing.Point(171, 192)
        Me.cmdResetTimerBack.Name = "cmdResetTimerBack"
        Me.cmdResetTimerBack.Size = New System.Drawing.Size(20, 20)
        Me.cmdResetTimerBack.TabIndex = 441
        Me.cmdResetTimerBack.Text = "X"
        Me.cmdResetTimerBack.UseVisualStyleBackColor = True
        '
        'tmrReset
        '
        Me.tmrReset.Interval = 1000
        '
        'picOBSReady
        '
        Me.picOBSReady.Image = Global.BionicClaw.My.Resources.Resources.cross
        Me.picOBSReady.Location = New System.Drawing.Point(156, 185)
        Me.picOBSReady.Name = "picOBSReady"
        Me.picOBSReady.Size = New System.Drawing.Size(23, 23)
        Me.picOBSReady.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picOBSReady.TabIndex = 440
        Me.picOBSReady.TabStop = False
        '
        'picTwitchReady
        '
        Me.picTwitchReady.Image = Global.BionicClaw.My.Resources.Resources.cross
        Me.picTwitchReady.Location = New System.Drawing.Point(156, 156)
        Me.picTwitchReady.Name = "picTwitchReady"
        Me.picTwitchReady.Size = New System.Drawing.Size(23, 23)
        Me.picTwitchReady.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picTwitchReady.TabIndex = 439
        Me.picTwitchReady.TabStop = False
        '
        'picClaw
        '
        Me.picClaw.Image = Global.BionicClaw.My.Resources.Resources.hand
        Me.picClaw.Location = New System.Drawing.Point(31, 29)
        Me.picClaw.Name = "picClaw"
        Me.picClaw.Size = New System.Drawing.Size(124, 124)
        Me.picClaw.TabIndex = 434
        Me.picClaw.TabStop = False
        '
        'tmrText
        '
        Me.tmrText.Interval = 5000
        '
        'tmrClock
        '
        Me.tmrClock.Interval = 10
        '
        'tmrTopCountdown
        '
        Me.tmrTopCountdown.Interval = 1000
        '
        'tmrIntro
        '
        Me.tmrIntro.Interval = 167000
        '
        'lblTopTotalSeconds
        '
        Me.lblTopTotalSeconds.AutoSize = True
        Me.lblTopTotalSeconds.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTopTotalSeconds.Location = New System.Drawing.Point(282, 66)
        Me.lblTopTotalSeconds.Name = "lblTopTotalSeconds"
        Me.lblTopTotalSeconds.Size = New System.Drawing.Size(18, 20)
        Me.lblTopTotalSeconds.TabIndex = 453
        Me.lblTopTotalSeconds.Text = "0"
        '
        'lblClock
        '
        Me.lblClock.AutoSize = True
        Me.lblClock.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblClock.Location = New System.Drawing.Point(257, 29)
        Me.lblClock.Name = "lblClock"
        Me.lblClock.Size = New System.Drawing.Size(43, 20)
        Me.lblClock.TabIndex = 454
        Me.lblClock.Text = "Time"
        '
        'Button11
        '
        Me.Button11.Location = New System.Drawing.Point(7, 134)
        Me.Button11.Name = "Button11"
        Me.Button11.Size = New System.Drawing.Size(24, 23)
        Me.Button11.TabIndex = 452
        Me.Button11.Text = "C"
        Me.Button11.UseVisualStyleBackColor = True
        '
        'frmClaw
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1339, 361)
        Me.Controls.Add(Me.lblClock)
        Me.Controls.Add(Me.lblTopTotalSeconds)
        Me.Controls.Add(Me.grpResetTimer)
        Me.Controls.Add(Me.grpCooldown)
        Me.Controls.Add(Me.grpMedia)
        Me.Controls.Add(Me.grpOBS)
        Me.Controls.Add(Me.grpTwitch)
        Me.Controls.Add(Me.lstQueue)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblBTC)
        Me.Controls.Add(Me.picClaw)
        Me.Controls.Add(Me.cmdResetTimerSettings)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdCooldownSettings)
        Me.Controls.Add(Me.cmdMediaSettings)
        Me.Controls.Add(Me.cmdConnect)
        Me.Controls.Add(Me.picOBSReady)
        Me.Controls.Add(Me.picTwitchReady)
        Me.Controls.Add(Me.cmdOBSSettings)
        Me.Controls.Add(Me.cmdTwitchSettings)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmClaw"
        Me.Text = "Bionic Claw"
        Me.grpTwitch.ResumeLayout(False)
        Me.grpTwitch.PerformLayout()
        Me.grpOBS.ResumeLayout(False)
        Me.grpOBS.PerformLayout()
        Me.grpMedia.ResumeLayout(False)
        Me.grpMedia.PerformLayout()
        Me.grpCooldown.ResumeLayout(False)
        Me.grpCooldown.PerformLayout()
        Me.grpResetTimer.ResumeLayout(False)
        Me.grpResetTimer.PerformLayout()
        CType(Me.picReset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picOBSReady, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTwitchReady, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picClaw, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtTwitchChat As RichTextBox
    Friend WithEvents lblBTC As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents grpTwitch As GroupBox
    Friend WithEvents lblTwitchChat As Label
    Friend WithEvents picClaw As PictureBox
    Friend WithEvents cmdSaveTwitch As Button
    Friend WithEvents txtTwitchOAuth As TextBox
    Friend WithEvents txtTwitchUsername As TextBox
    Friend WithEvents lblUaP As Label
    Friend WithEvents grpOBS As GroupBox
    Friend WithEvents txtOBSPass As TextBox
    Friend WithEvents cmdSaveOBS As Button
    Friend WithEvents txtOBSPort As TextBox
    Friend WithEvents txtOBSHost As TextBox
    Friend WithEvents lblHPP As Label
    Friend WithEvents cmdTwitchSettings As Button
    Friend WithEvents cmdOBSSettings As Button
    Friend WithEvents picTwitchReady As PictureBox
    Friend WithEvents picOBSReady As PictureBox
    Friend WithEvents cmdConnect As Button
    Friend WithEvents cmdTwitchBack As Button
    Friend WithEvents cmdOBSBack As Button
    Friend WithEvents grpMedia As GroupBox
    Friend WithEvents cmdMediaBack As Button
    Friend WithEvents cmdMediaSettings As Button
    Friend WithEvents grpCooldown As GroupBox
    Friend WithEvents lstCooldownUser As ListBox
    Friend WithEvents lstCooldownTime As ListBox
    Friend WithEvents cmdCooldownBack As Button
    Friend WithEvents cmdCooldownSettings As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents tmrQueue As Timer
    Friend WithEvents lstQueue As ListBox
    Friend WithEvents tmrCooldown As Timer
    Friend WithEvents tmrMedia As Timer
    Friend WithEvents lstMedia As ListBox
    Friend WithEvents cmdRemoveMedia As Button
    Friend WithEvents txtMedia As TextBox
    Friend WithEvents cmdAddMedia As Button
    Friend WithEvents txtCooldown As TextBox
    Friend WithEvents lblCooldown As Label
    Friend WithEvents txtInterval As TextBox
    Friend WithEvents lblInterval As Label
    Friend WithEvents cmdResetTimerSettings As Button
    Friend WithEvents grpResetTimer As GroupBox
    Friend WithEvents cmdResetTimerBack As Button
    Friend WithEvents tmrReset As Timer
    Friend WithEvents picReset As PictureBox
    Friend WithEvents txtReset As TextBox
    Friend WithEvents lblReset As Label
    Friend WithEvents lblTotalSeconds As Label
    Friend WithEvents lblMinutes As Label
    Friend WithEvents lblHours As Label
    Friend WithEvents lblSeconds As Label
    Friend WithEvents Button9 As Button
    Friend WithEvents Button10 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents Button6 As Button
    Friend WithEvents Button7 As Button
    Friend WithEvents Button8 As Button
    Friend WithEvents Button4 As Button
    Friend WithEvents Button3 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents tmrText As Timer
    Friend WithEvents tmrClock As Timer
    Friend WithEvents tmrTopCountdown As Timer
    Friend WithEvents tmrIntro As Timer
    Friend WithEvents lblTopTotalSeconds As Label
    Friend WithEvents lblClock As Label
    Friend WithEvents Button11 As Button
End Class
