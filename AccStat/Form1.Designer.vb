<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
		Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
		Me.Button1 = New System.Windows.Forms.Button()
		Me.TextBox1 = New System.Windows.Forms.TextBox()
		Me.ListBox1 = New System.Windows.Forms.ListBox()
		Me.Button2 = New System.Windows.Forms.Button()
		Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
		Me.FolderBrowserDialog1 = New System.Windows.Forms.FolderBrowserDialog()
		Me.Label2 = New System.Windows.Forms.Label()
		Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
		Me.Label1 = New System.Windows.Forms.Label()
		Me.CBCreateGrafs = New System.Windows.Forms.CheckBox()
		Me.CBSpeed = New System.Windows.Forms.CheckBox()
		Me.CBDist = New System.Windows.Forms.CheckBox()
		Me.CBForce = New System.Windows.Forms.CheckBox()
		Me.CBTransparent = New System.Windows.Forms.CheckBox()
		Me.SuspendLayout()
		'
		'Button1
		'
		Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Button1.Location = New System.Drawing.Point(320, 301)
		Me.Button1.Name = "Button1"
		Me.Button1.Size = New System.Drawing.Size(68, 28)
		Me.Button1.TabIndex = 11
		Me.Button1.Text = "&Commit"
		Me.Button1.UseVisualStyleBackColor = True
		'
		'TextBox1
		'
		Me.TextBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.TextBox1.Location = New System.Drawing.Point(77, 10)
		Me.TextBox1.Name = "TextBox1"
		Me.TextBox1.Size = New System.Drawing.Size(281, 20)
		Me.TextBox1.TabIndex = 1
		'
		'ListBox1
		'
		Me.ListBox1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
			Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ListBox1.FormattingEnabled = True
		Me.ListBox1.Location = New System.Drawing.Point(12, 85)
		Me.ListBox1.Name = "ListBox1"
		Me.ListBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended
		Me.ListBox1.Size = New System.Drawing.Size(375, 186)
		Me.ListBox1.TabIndex = 8
		'
		'Button2
		'
		Me.Button2.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.Button2.Location = New System.Drawing.Point(364, 10)
		Me.Button2.Name = "Button2"
		Me.Button2.Size = New System.Drawing.Size(24, 20)
		Me.Button2.TabIndex = 2
		Me.Button2.Text = "..."
		Me.Button2.UseVisualStyleBackColor = True
		'
		'ProgressBar1
		'
		Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
			Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
		Me.ProgressBar1.Location = New System.Drawing.Point(12, 277)
		Me.ProgressBar1.Name = "ProgressBar1"
		Me.ProgressBar1.Size = New System.Drawing.Size(375, 18)
		Me.ProgressBar1.TabIndex = 9
		'
		'Label2
		'
		Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
		Me.Label2.AutoSize = True
		Me.Label2.Location = New System.Drawing.Point(9, 309)
		Me.Label2.Name = "Label2"
		Me.Label2.Size = New System.Drawing.Size(41, 13)
		Me.Label2.TabIndex = 10
		Me.Label2.Text = "Ready."
		'
		'Label1
		'
		Me.Label1.AutoSize = True
		Me.Label1.Location = New System.Drawing.Point(9, 14)
		Me.Label1.Name = "Label1"
		Me.Label1.Size = New System.Drawing.Size(59, 13)
		Me.Label1.TabIndex = 0
		Me.Label1.Text = "Data folder"
		'
		'CBCreateGrafs
		'
		Me.CBCreateGrafs.AutoSize = True
		Me.CBCreateGrafs.Checked = True
		Me.CBCreateGrafs.CheckState = System.Windows.Forms.CheckState.Checked
		Me.CBCreateGrafs.Location = New System.Drawing.Point(12, 36)
		Me.CBCreateGrafs.Name = "CBCreateGrafs"
		Me.CBCreateGrafs.Size = New System.Drawing.Size(126, 17)
		Me.CBCreateGrafs.TabIndex = 3
		Me.CBCreateGrafs.Text = "Create &Graph Images"
		Me.CBCreateGrafs.UseVisualStyleBackColor = True
		'
		'CBSpeed
		'
		Me.CBSpeed.AutoSize = True
		Me.CBSpeed.Checked = True
		Me.CBSpeed.CheckState = System.Windows.Forms.CheckState.Checked
		Me.CBSpeed.Location = New System.Drawing.Point(32, 59)
		Me.CBSpeed.Name = "CBSpeed"
		Me.CBSpeed.Size = New System.Drawing.Size(57, 17)
		Me.CBSpeed.TabIndex = 4
		Me.CBSpeed.Text = "&Speed"
		Me.CBSpeed.UseVisualStyleBackColor = True
		'
		'CBDist
		'
		Me.CBDist.AutoSize = True
		Me.CBDist.Checked = True
		Me.CBDist.CheckState = System.Windows.Forms.CheckState.Checked
		Me.CBDist.Location = New System.Drawing.Point(95, 59)
		Me.CBDist.Name = "CBDist"
		Me.CBDist.Size = New System.Drawing.Size(68, 17)
		Me.CBDist.TabIndex = 5
		Me.CBDist.Text = "&Distance"
		Me.CBDist.UseVisualStyleBackColor = True
		'
		'CBForce
		'
		Me.CBForce.AutoSize = True
		Me.CBForce.Checked = True
		Me.CBForce.CheckState = System.Windows.Forms.CheckState.Checked
		Me.CBForce.Location = New System.Drawing.Point(169, 59)
		Me.CBForce.Name = "CBForce"
		Me.CBForce.Size = New System.Drawing.Size(53, 17)
		Me.CBForce.TabIndex = 6
		Me.CBForce.Text = "&Force"
		Me.CBForce.UseVisualStyleBackColor = True
		'
		'CBTransparent
		'
		Me.CBTransparent.AutoSize = True
		Me.CBTransparent.Checked = True
		Me.CBTransparent.CheckState = System.Windows.Forms.CheckState.Checked
		Me.CBTransparent.Location = New System.Drawing.Point(228, 59)
		Me.CBTransparent.Name = "CBTransparent"
		Me.CBTransparent.Size = New System.Drawing.Size(83, 17)
		Me.CBTransparent.TabIndex = 7
		Me.CBTransparent.Text = "&Transparent"
		Me.CBTransparent.UseVisualStyleBackColor = True
		'
		'Form1
		'
		Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
		Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
		Me.ClientSize = New System.Drawing.Size(400, 341)
		Me.Controls.Add(Me.CBTransparent)
		Me.Controls.Add(Me.CBForce)
		Me.Controls.Add(Me.CBDist)
		Me.Controls.Add(Me.CBSpeed)
		Me.Controls.Add(Me.CBCreateGrafs)
		Me.Controls.Add(Me.Label1)
		Me.Controls.Add(Me.Label2)
		Me.Controls.Add(Me.ProgressBar1)
		Me.Controls.Add(Me.Button2)
		Me.Controls.Add(Me.ListBox1)
		Me.Controls.Add(Me.TextBox1)
		Me.Controls.Add(Me.Button1)
		Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
		Me.MinimumSize = New System.Drawing.Size(360, 240)
		Me.Name = "Form1"
		Me.Text = "Acc Stat"
		Me.ResumeLayout(False)
		Me.PerformLayout()

End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents ListBox1 As System.Windows.Forms.ListBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents FolderBrowserDialog1 As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents SaveFileDialog1 As System.Windows.Forms.SaveFileDialog
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CBCreateGrafs As System.Windows.Forms.CheckBox
    Friend WithEvents CBSpeed As System.Windows.Forms.CheckBox
    Friend WithEvents CBDist As System.Windows.Forms.CheckBox
	Friend WithEvents CBForce As System.Windows.Forms.CheckBox
	Friend WithEvents CBTransparent As System.Windows.Forms.CheckBox

End Class
