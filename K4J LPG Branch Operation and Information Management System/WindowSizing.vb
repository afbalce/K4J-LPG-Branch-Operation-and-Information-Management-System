Friend Module WindowSizing
    ' Shared starting dimensions for every application window, in logical pixels.
    Public Sub ApplyStartingSize(window As Form)
        If Not window.TopLevel Then Return

        Dim work = Screen.FromControl(window).WorkingArea
        Dim scale = window.DeviceDpi / 96.0R
        window.WindowState = FormWindowState.Normal
        window.MaximumSize = Size.Empty
        window.MinimumSize = New Size(Math.Min(work.Width, CInt(800 * scale)),
                                      Math.Min(work.Height, CInt(560 * scale)))
        Dim width = Math.Min(work.Width, CInt(1200 * scale))
        Dim height = Math.Min(work.Height, CInt(780 * scale))
        window.StartPosition = FormStartPosition.Manual
        window.Bounds = New Rectangle(work.Left + (work.Width - width) \ 2,
                                      work.Top + (work.Height - height) \ 2, width, height)
    End Sub
End Module
