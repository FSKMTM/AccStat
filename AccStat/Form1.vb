Public Class Form1
    Dim cancelflag As Boolean = False
    Dim running As Boolean = False
    Dim time(20000) As Double
    Dim acc(20000) As Double
    Dim speedacc(20000) As Double
    Dim dist(20000) As Double
    Dim force(20000) As Double
    Dim speedext(20000) As Double
    Dim canvas As System.Drawing.Graphics
    Dim bmp As New Bitmap(1160, 660)
    Dim penacc As New System.Drawing.Pen(Brushes.CornflowerBlue, 2)
    Dim penspeed As New System.Drawing.Pen(Brushes.Chartreuse, 2)
    Dim pendist As New System.Drawing.Pen(Brushes.Salmon, 2)
    Dim penforce As New System.Drawing.Pen(Brushes.Goldenrod, 2)
    Dim penframe As New System.Drawing.Pen(Brushes.LightGray, 1)
    Dim penshadow As New System.Drawing.Pen(Brushes.Black, 1)

    Public Structure AccData
        Dim AvgAcc As Double
        Dim AvgCorAcc As Double
        Dim MFDD As Double
        Dim MaxCorrAcc As Double
        Dim MinAcc As Double
        Dim MinMaxAccRatio As Double
        Dim InitialSpeedAcc As Double
        Dim InitialSpeedExt As Double
        Dim MaxPedalForce As Double
        Dim StoppingDistance As Double
		Dim StoppingTime As Double
		Dim SR_SCRIM_50 As Double
    End Structure

    Public Structure DataSample
        Dim Rank As Integer
        Dim Value As Double
    End Structure

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim lbi As Object
        Dim statfile As System.IO.StreamWriter
        Dim succcount As Integer = 0
        Dim report As String = "Done "
        Dim outdata As AccData

        Dim sumavgacc As Double = 0
        Dim sumavgcoracc As Double = 0
        Dim sumsqavgacc As Double = 0
        Dim sumsqavgcoracc As Double = 0

        Dim avgavgacc As Double = 0
        Dim sdavgacc As Double = 0
        Dim avgavgcoracc As Double = 0
        Dim sdavgcoracc As Double = 0

        Dim summinacc As Double = 0
        Dim sumsqminacc As Double = 0
        Dim avgminacc As Double = 0
        Dim sdminacc As Double = 0

        Dim summaxcoracc As Double = 0
        Dim sumsqmaxcoracc As Double = 0
        Dim avgmaxcoracc As Double = 0
        Dim sdmaxcoracc As Double = 0

        Dim summinmaxaccratio As Double = 0
        Dim sumsqminmaxaccratio As Double = 0
        Dim avgminmaxaccratio As Double = 0
        Dim sdminmaxaccratio As Double = 0

        Dim sumMFDD As Double = 0
        Dim sumsqMFDD As Double = 0
        Dim avgMFDD As Double = 0
        Dim sdMFDD As Double = 0

		Try

		If ListBox1.SelectedItems.Count < 2 Then
			MsgBox("To calculate statistics, you have to select at least two data files.", MsgBoxStyle.Critical)
			Exit Sub
		End If

		SaveFileDialog1.Title = "Output file"
		SaveFileDialog1.Filter = "Dat files|*.dat"
		If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
			Exit Sub
		End If

		If SaveFileDialog1.FileName <> "" Then
			statfile = New System.IO.StreamWriter(SaveFileDialog1.FileName)
		Else
			Exit Sub
		End If

		canvas = Drawing.Graphics.FromImage(bmp)

		Button1.Enabled = False
		TextBox1.Enabled = False
		Button2.Enabled = False
		ListBox1.Enabled = False
		CBCreateGrafs.Enabled = False
		CBSpeed.Enabled = False
		CBDist.Enabled = False
		CBForce.Enabled = False


		cancelflag = False
		running = True

		ProgressBar1.Minimum = 0
		ProgressBar1.Maximum = ListBox1.SelectedItems.Count + 1
		ProgressBar1.Value = 0

		Button1.Enabled = False

		statfile.WriteLine("File name" & vbTab & "AvgAcc" & vbTab & "AvgCorAcc" & vbTab & "MFDD" & vbTab & "MaxCorAcc" & vbTab & "MinAcc" & vbTab & "MinMaxAccRatio" & vbTab & "InitialSpeedAcc" & vbTab & "InitialSpeedExt" & vbTab & "MaxPedalForce" & vbTab & "StoppingDistance" & vbTab & "StoppingTime" & vbTab & "SR_SCRIM_50km/h")

		For Each lbi In ListBox1.SelectedItems
			Label2.Text = "Processing " & lbi.ToString & " (ESC to cancel)"
			Label2.Refresh()
			outdata = CalcFileStats(TextBox1.Text & "\" & lbi.ToString, TextBox1.Text & "\" & lbi.ToString.Replace(".csv", "_stat.dat"))
			ProgressBar1.Value += 1
			If cancelflag Then
				report = "Job cancelled. Done "
				cancelflag = False
				Exit For
			End If
			My.Application.DoEvents()

			sumavgacc += outdata.AvgAcc
			sumsqavgacc += outdata.AvgAcc ^ 2

			sumavgcoracc += outdata.AvgCorAcc
			sumsqavgcoracc += outdata.AvgCorAcc ^ 2

			summinacc += outdata.MinAcc
			sumsqminacc += outdata.MinAcc ^ 2

			summaxcoracc += outdata.MaxCorrAcc
			sumsqmaxcoracc += outdata.MaxCorrAcc ^ 2

			summinmaxaccratio += outdata.MinMaxAccRatio
			sumsqminmaxaccratio += outdata.MinMaxAccRatio ^ 2

			sumMFDD += outdata.MFDD
			sumsqMFDD += outdata.MFDD ^ 2

			statfile.WriteLine(lbi.ToString & vbTab & outdata.AvgAcc & vbTab & outdata.AvgCorAcc & vbTab & outdata.MFDD & vbTab & outdata.MaxCorrAcc & vbTab & outdata.MinAcc & vbTab & outdata.MinMaxAccRatio & vbTab & outdata.InitialSpeedAcc & vbTab & outdata.InitialSpeedExt & vbTab & outdata.MaxPedalForce & vbTab & outdata.StoppingDistance & vbTab & outdata.StoppingTime & vbTab & outdata.SR_SCRIM_50)

			succcount += 1
		Next

		If succcount > 1 Then
			avgavgacc = sumavgacc / succcount
			sdavgacc = Math.Sqrt((sumsqavgacc - succcount * avgavgacc ^ 2) / (succcount - 1))

			avgavgcoracc = sumavgcoracc / succcount
			sdavgcoracc = Math.Sqrt((sumsqavgcoracc - succcount * avgavgcoracc ^ 2) / (succcount - 1))

			avgminacc = summinacc / succcount
			sdminacc = Math.Sqrt((sumsqminacc - succcount * avgminacc ^ 2) / (succcount - 1))

			avgmaxcoracc = summaxcoracc / succcount
			sdmaxcoracc = Math.Sqrt((sumsqmaxcoracc - succcount * avgmaxcoracc ^ 2) / (succcount - 1))

			avgminmaxaccratio = summinmaxaccratio / succcount
			sdminmaxaccratio = Math.Sqrt((sumsqminmaxaccratio - succcount * avgminmaxaccratio ^ 2) / (succcount - 1))

			avgMFDD = sumMFDD / succcount
			sdMFDD = Math.Sqrt((sumsqMFDD - succcount * avgMFDD ^ 2) / (succcount - 1))

			statfile.WriteLine("Mean" & vbTab & avgavgacc & vbTab & avgavgcoracc & vbTab & avgMFDD & vbTab & avgmaxcoracc & vbTab & avgminacc & vbTab & avgminmaxaccratio)
			statfile.WriteLine("Std. dev." & vbTab & sdavgacc & vbTab & sdavgcoracc & vbTab & sdMFDD & vbTab & sdmaxcoracc & vbTab & sdminacc & vbTab & sdminmaxaccratio)
		End If

		If succcount = 1 Then
			Label2.Text = report & succcount.ToString & " file."
		Else
			Label2.Text = report & succcount.ToString & " files."
		End If

		statfile.Close()

		Catch ex As Exception
			MsgBox("Data could not be processed because of the following:" & vbCrLf & ex.Message & vbCrLf & vbCrLf & "(Please ensure that the input files are in correct format and that the output files are writable.)", MsgBoxStyle.Critical)
		End Try

		ProgressBar1.Value = ProgressBar1.Maximum
		Button1.Enabled = True
		TextBox1.Enabled = True
		Button2.Enabled = True
		ListBox1.Enabled = True
		CBCreateGrafs.Enabled = True
		CBSpeed.Enabled = CBCreateGrafs.Checked
		CBDist.Enabled = CBCreateGrafs.Checked
		CBForce.Enabled = CBCreateGrafs.Checked
		running = False

	End Sub

    Function CalcFileStats(ByVal infn As String, ByVal outfn As String) As AccData
        Dim dataFile As System.IO.StreamReader
        Dim outFile As System.IO.StreamWriter
        Dim tmpline As String
        Dim tmparr As String()
        Dim count As Integer = 0
        Dim half As Integer = 0
        Dim OutData As New AccData
        Dim i As Integer
		Dim graftitle As String
		Dim integrate As Boolean
		Dim timeInterval As Double

        Dim forcefactor As Double = 1

		Try
		dataFile = New System.IO.StreamReader(infn)
			outFile = New System.IO.StreamWriter(outfn)
			dataFile.ReadLine()

            'attempt to detect CONSKID, VC2000 and VC4000 CSV and TSV files automagically
            While Not dataFile.EndOfStream
				tmpline = dataFile.ReadLine()

				'check for pedal force in volts in early VC4000 files
				If tmpline.Contains("pedalna dozunanja") Then
					forcefactor = 177.9288646
				End If

                If tmpline.Contains("A") Or tmpline.Contains("G") Then
                    tmpline = ""
                End If
                If tmpline.Contains("#") Or tmpline.Contains("$") Then 'conskid header or GNSS
					tmpline = ""
				End If

				If tmpline <> "" Then
					While tmpline.Contains("  ")
						tmpline = tmpline.Replace("  ", " ")
					End While
					If Asc(tmpline.Substring(0)) < 58 And Asc(tmpline.Substring(0)) > 47 Then
						'handle CSV and TSV
						tmparr = Split(tmpline, ",")
						If tmparr.Length < 2 Then
							tmparr = Split(tmpline, " ")
						End If
						If tmparr.Length > 8 Then
							If tmparr.Length > 12 Then
								'old conskid file
								time(count) = CDbl(tmparr(0)) / 1000
								acc(count) = CDbl(tmparr(1))
								speedacc(count) = 0
								dist(count) = 0
								force(count) = 0
								speedext(count) = 0
							End If
							If tmparr.Length = 12 Then
								'factory VC4000 CSV
								time(count) = CDbl(tmparr(0))
								acc(count) = CDbl(tmparr(1))
								speedacc(count) = CDbl(tmparr(2))
								dist(count) = CDbl(tmparr(3))
								force(count) = CDbl(tmparr(5)) * forcefactor
								speedext(count) = CDbl(tmparr(6))
							End If
							If tmparr.Length < 12 Then
								'old VC2000 file
								time(count) = CDbl(tmparr(0))
								acc(count) = CDbl(tmparr(1))
								speedacc(count) = CDbl(tmparr(2))
								dist(count) = CDbl(tmparr(3))
								force(count) = CDbl(tmparr(7)) * forcefactor
								speedext(count) = CDbl(tmparr(8))
							End If
						Else
							If tmparr.Length = 4 Then
								'new conskid file
								integrate = True
								time(count) = CDbl(tmparr(0)) / 1000
								acc(count) = -CDbl(tmparr(1))
								speedacc(count) = 0
								dist(count) = 0
								force(count) = 0
								speedext(count) = 0
							Else
								'new VC4000 CSV or TSV
								time(count) = CDbl(tmparr(0))
								acc(count) = CDbl(tmparr(1))
								speedacc(count) = CDbl(tmparr(2))
								dist(count) = CDbl(tmparr(3))
								force(count) = CDbl(tmparr(4)) * forcefactor
								speedext(count) = CDbl(tmparr(5))
							End If
						End If
						count += 1
					End If
				End If
			End While
			timeInterval = time(1) - time(0) ' calculate time interval from the first two readings
			If integrate = True Then
				'new conskid file, integrate speed and distance
				integrate = False
				speedacc(count) = 0
				dist(0) = 0
				For i = count - 1 To 0 Step -1
					'speedacc(i) = speedacc(i + 1) - 3.6 * (acc(i) * 9.80665 * timeInterval) 'integrate with fixed time interval
					speedacc(i) = speedacc(i + 1) - 3.6 * (acc(i) * 9.80665 * (time(i + 1) - time(i))) 'integrate with actual time interval
				Next
				For i = 1 To count
					'dist(i) = dist(i - 1) + speedacc(i) * timeInterval / 3.6 'integrate with fixed time interval
					dist(i) = dist(i - 1) + speedacc(i) * (time(i) - time(i - 1)) / 3.6 'integrate with actual time interval
				Next
			End If
			count -= 1
			half = CInt(0.5 * count)
			'calculate values
			With OutData
				.InitialSpeedAcc = speedacc(0)
				For i = 0 To count
					If speedext(i) > 0 Then
						.InitialSpeedExt = speedext(i)
						Exit For
					End If
				Next i
				.MinAcc = FindMin(acc, 0, half).Value
				.MaxCorrAcc = FindMax(acc, CInt(FindMin(acc, 0, half).Rank), CInt(FindMin(acc, half, count).Rank)).Value
				.AvgAcc = FindAvg(acc, 0, count)
				.AvgCorAcc = FindAvg(acc, Math.Min((CInt(FindMin(acc, 0, half).Rank) + 20), count), Math.Max((CInt(FindMin(acc, half, count).Rank) - 20), 0))
				.MFDD = CalcMFDD(speedacc, dist, count)
				.MaxPedalForce = FindMax(force, 0, count).Value
				.MinMaxAccRatio = .MinAcc / .MaxCorrAcc
				.StoppingDistance = FindMax(dist, 0, count).Value
				.StoppingTime = time(count)
				.SR_SCRIM_50 = CalcSFC(.MFDD, 0.0108, 0)
				Debug.Print(CInt(FindMin(acc, 0, half).Rank) & " " & CInt(FindMin(acc, half, count).Rank) & " " & .MaxCorrAcc)
			End With

			outFile.WriteLine("FileName=" & infn & vbCrLf & "AvgAcc=" & OutData.AvgAcc & vbCrLf & "AvgCorrAcc=" & OutData.AvgCorAcc & vbCrLf & "MFDD=" & OutData.MFDD & vbCrLf & "MaxCorrAcc=" & OutData.MaxCorrAcc & vbCrLf & "MinAcc=" & OutData.MinAcc & vbCrLf & "MinMaxAccRatio=" & OutData.MinMaxAccRatio & vbCrLf & "InitialSpeedAcc=" & OutData.InitialSpeedAcc & vbCrLf & "InitialSpeedExt=" & OutData.InitialSpeedExt & vbCrLf & "MaxPedalForce=" & OutData.MaxPedalForce & vbCrLf & "StoppingDistance=" & OutData.StoppingDistance & vbCrLf & "StoppingTime=" & OutData.StoppingTime & vbCrLf & "SR_SCRIM_50km/h=" & OutData.SR_SCRIM_50)

			dataFile.Close()
			outFile.Close()

			'draw graf
			If CBCreateGrafs.Checked = True Then
				If CBTransparent.Checked = True Then
					canvas.Clear(System.Drawing.Color.Transparent)
				Else
					canvas.Clear(System.Drawing.Color.White)
				End If
				Dim gint As Integer = CInt(timeInterval / 0.005)
				DrawGrafData(count, gint)
				graftitle = infn.Substring(infn.LastIndexOf("\") + 1, infn.Length - infn.LastIndexOf("\") - 1) & ", avg. dec. = " & Format(OutData.AvgAcc, "0.000") & " g, MFDD = " & Format(OutData.MFDD, "0.000") & " g, init. speed = " & Format(OutData.InitialSpeedAcc, "0.00") & " km/h, stop. dist. = " & Format(OutData.StoppingDistance, "0.000") & " m, stop time = " & Format(OutData.StoppingTime, "0.000") & " s, SR_SCRIM_50km/h = " & Format(OutData.SR_SCRIM_50, "0.00")
				DrawGrafFrame(graftitle)
				bmp.Save(infn.Replace(".csv", ".png").Replace(".CSV", ".png"), System.Drawing.Imaging.ImageFormat.Png)
			End If

			Catch ex As Exception
				MsgBox("File " & infn & " could not be processed because of the following:" & vbCrLf & ex.Message & vbCrLf & vbCrLf & "(Please ensure that the output files are writable.)", MsgBoxStyle.Critical)
				Exit Function
			End Try

			Return OutData
	End Function
    Function CalcMFDD(ByVal inspeed() As Double, ByVal indist() As Double, ByVal count As Integer) As Double
        Dim v01 As Double
        Dim v08 As Double
        Dim s01 As Double
        Dim s08 As Double
        Dim r01 As Integer
        Dim r08 As Integer
        v01 = 0.1 * FindMax(speedacc, 0, count).Value
        r01 = FindRank(inspeed, v01, count)
        s01 = indist(r01)
        v08 = 0.8 * FindMax(speedacc, 0, count).Value
        r08 = FindRank(inspeed, v08, count)
        s08 = indist(r08)
        If s01 - s08 <> 0 Then
            Return ((v01 / 3.6) ^ 2 - (v08 / 3.6) ^ 2) / 2 / (s01 - s08) / 9.80665 'MFDD [g]
        Else
            Return -1
        End If
	End Function
	Function CalcSFC(inmfdd As Double, k As Double, n As Double) As Double
		Return -(inmfdd - n) / k
	End Function

    Function FindRank(ByVal inarr() As Double, ByVal nxtval As Double, ByVal count As Integer) As Integer
        Dim n As Integer
        Dim epsilon As Double
        Dim mindif As Double = 60
        Dim rtrank As Integer = 0
        For n = 0 To count
            epsilon = Math.Abs(inarr(n) - nxtval)
            If epsilon < mindif Then
                mindif = epsilon
                rtrank = n
            End If
        Next
        Return rtrank
    End Function

    Private Sub DrawGrafFrame(ByVal title As String)
        Dim wdt As System.Drawing.SizeF

        'grid
        For n = 80 To 1080 Step 20
            canvas.DrawLine(penframe, New System.Drawing.Point(n, 600), New System.Drawing.Point(n, 40))
        Next
        For n = 40 To 600 Step 20
            canvas.DrawLine(penframe, New System.Drawing.Point(80, n), New System.Drawing.Point(1080, n))
        Next

        'x axis
        For n = 0 To 1000 Step 40
            canvas.DrawString(Format(n / 200, "0.0"), New System.Drawing.Font("Arial", 8), Brushes.Black, New System.Drawing.Point(n + 70, 600))
        Next

        'left y axis
        For n = 0 To 520 Step 40
            wdt = canvas.MeasureString(Format(-n / 400 + 0.2, "0.0"), New System.Drawing.Font("Arial", 8))
            canvas.DrawString(Format(-n / 400 + 0.2, "0.0"), New System.Drawing.Font("Arial", 8), Brushes.Black, New System.Drawing.Point(CInt(80 - wdt.Width), n + 32))
        Next

        'right y axis
		For n = 0 To 90 Step 10
			canvas.DrawString(Format(n, "0"), New System.Drawing.Font("Arial", 8), Brushes.Black, New System.Drawing.Point(CInt(1086), (90 - n) * 6 + 52))
		Next

        'title
        canvas.DrawString(title, New System.Drawing.Font("Arial", 8), Brushes.Black, New System.Drawing.Point(4, 4))

        'x axis label
        canvas.DrawString("t [s]", New System.Drawing.Font("Arial", 14), Brushes.Black, New System.Drawing.Point(550, 620))

        'y axis labels
        Dim myMatrix As New System.Drawing.Drawing2D.Matrix
        myMatrix.RotateAt(-90, New Point(450, 330))
        canvas.Transform = myMatrix
        canvas.DrawString("a [g]", New System.Drawing.Font("Arial", 14), Brushes.CornflowerBlue, New System.Drawing.Point(450, -100))
        If CBSpeed.Checked Then
            canvas.DrawString("v [km/h]", New System.Drawing.Font("Arial", 14), Brushes.Chartreuse, New System.Drawing.Point(350, 1000))
        End If
        If CBDist.Checked Then
            canvas.DrawString("s [m]", New System.Drawing.Font("Arial", 14), Brushes.Salmon, New System.Drawing.Point(426, 1000))
        End If
        If CBForce.Checked Then
            canvas.DrawString("F [daN]", New System.Drawing.Font("Arial", 14), Brushes.Goldenrod, New System.Drawing.Point(480, 1000))
        End If
        myMatrix.RotateAt(90, New Point(450, 330))
        canvas.Transform = myMatrix
    End Sub

	Private Sub DrawGrafData(ByVal cnt As Integer, gint As Integer)

		Dim tmppts(cnt) As Point

		'acc
		For n = 0 To cnt
			tmppts(n).X = n * gint + 80
			tmppts(n).Y = -CInt(400 * acc(n)) + 120
		Next
		canvas.DrawCurve(penacc, tmppts)

		'speed
		If CBSpeed.Checked Then
			For n = 0 To cnt
				tmppts(n).X = n * gint + 80
				tmppts(n).Y = -CInt(speedacc(n) * 6) + 600
			Next
			canvas.DrawCurve(penspeed, tmppts)
		End If

		'dist
		If CBDist.Checked Then
			For n = 0 To cnt
				tmppts(n).X = n * gint + 80
				tmppts(n).Y = -CInt(dist(n) * 6) + 600
			Next
			canvas.DrawCurve(pendist, tmppts)
		End If

		'force
		If CBForce.Checked Then
			For n = 0 To cnt
				tmppts(n).X = n * gint + 80
				tmppts(n).Y = -CInt(force(n) * 0.6) + 600
			Next
			canvas.DrawCurve(penforce, tmppts)
		End If
	End Sub

    Private Function FindMax(ByVal inarr() As Double, ByVal fromcnt As Integer, ByVal tocnt As Integer) As DataSample
        Dim n As Integer
        Dim tmpt As Integer
        Dim tmpv As Double
        Dim ds As DataSample

        If fromcnt > tocnt Then
            fromcnt = fromcnt Xor tocnt
            tocnt = fromcnt Xor tocnt
            fromcnt = fromcnt Xor tocnt
        End If

        tmpv = inarr(fromcnt)
        tmpt = fromcnt

        For n = fromcnt To tocnt
            If inarr(n) > tmpv Then
                tmpv = inarr(n)
                tmpt = n
            End If
        Next
        With ds
            .Rank = tmpt
            .Value = tmpv
        End With
        Return ds
    End Function

    Private Function FindMin(ByVal inarr() As Double, ByVal fromcnt As Integer, ByVal tocnt As Integer) As DataSample
        Dim n As Integer
        Dim tmpv As Double
        Dim tmpt As Integer
        Dim ds As DataSample

        If fromcnt > tocnt Then
            fromcnt = fromcnt Xor tocnt
            tocnt = fromcnt Xor tocnt
            fromcnt = fromcnt Xor tocnt
        End If

        tmpv = inarr(fromcnt)
        tmpt = fromcnt

        For n = fromcnt To tocnt
            If inarr(n) < tmpv Then
                tmpv = inarr(n)
                tmpt = n
            End If
        Next
        With ds
            .Rank = tmpt
            .Value = tmpv
        End With
        Return ds
    End Function

    Private Function FindAvg(ByVal inarr() As Double, ByVal fromcnt As Integer, ByVal tocnt As Integer) As Double
        Dim n As Integer
        Dim tmp As Double = 0

        If fromcnt > tocnt Then
            fromcnt = fromcnt Xor tocnt
            tocnt = fromcnt Xor tocnt
            fromcnt = fromcnt Xor tocnt
        End If

        For n = fromcnt To tocnt
            tmp += inarr(n)
        Next
        tmp = tmp / (n - fromcnt)
        Return tmp
    End Function

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress
        Try
            If e.KeyChar = vbCr Then
                e.Handled = True
                FillListBox(TextBox1.Text)
            End If
        Catch
            Exit Sub
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        FolderBrowserDialog1.SelectedPath = TextBox1.Text
        If FolderBrowserDialog1.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
            Exit Sub
        End If

        If FolderBrowserDialog1.SelectedPath <> "" Then
            TextBox1.Text = FolderBrowserDialog1.SelectedPath
            FillListBox(TextBox1.Text)
        End If
    End Sub
    Sub FillListBox(ByVal fn As String)
        ListBox1.Items.Clear()
        Dim folderInfo As New IO.DirectoryInfo(fn)
        Dim arrFilesInFolder() As IO.FileInfo
        Dim fileInFolder As IO.FileInfo
        arrFilesInFolder = folderInfo.GetFiles("*.csv")
        For Each fileInFolder In arrFilesInFolder
            ListBox1.Items.Add(fileInFolder.Name)
        Next
        My.Settings.LastPath = TextBox1.Text.Trim
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Escape And running = True Then
            cancelflag = True
            Label2.Text = "Cancelling..."
            Label2.Refresh()
        End If
    End Sub

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox1.Text = My.Settings.LastPath
    End Sub

    Private Sub CBCreateGrafs_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CBCreateGrafs.CheckedChanged
        CBSpeed.Enabled = CBCreateGrafs.Checked
        CBDist.Enabled = CBCreateGrafs.Checked
		CBForce.Enabled = CBCreateGrafs.Checked
		CBTransparent.Enabled = CBCreateGrafs.Checked
    End Sub
End Class
