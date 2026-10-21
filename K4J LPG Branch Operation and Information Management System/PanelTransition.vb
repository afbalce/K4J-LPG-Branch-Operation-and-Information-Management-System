Imports System.Runtime.InteropServices

' Keep the current frame visible while a synchronous panel switch finishes layout.
Friend NotInheritable Class PanelTransition
    Implements IDisposable

    Private ReadOnly host As Control
    Private ReadOnly frozenHandle As IntPtr
    Private disposed As Boolean

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, message As Integer,
                                        wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function RedrawWindow(hWnd As IntPtr, updateRect As IntPtr,
                                         updateRegion As IntPtr, flags As UInteger) As Boolean
    End Function

    Public Sub New(container As Control)
        host = container
        host.SuspendLayout()
        If host.IsHandleCreated AndAlso host.Visible Then
            frozenHandle = host.Handle
            SendMessage(frozenHandle, &HB, IntPtr.Zero, IntPtr.Zero)
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If disposed Then Return
        disposed = True
        Try
            If Not host.IsDisposed Then host.ResumeLayout(True)
        Finally
            If frozenHandle <> IntPtr.Zero AndAlso Not host.IsDisposed AndAlso
                host.IsHandleCreated AndAlso host.Handle = frozenHandle Then
                SendMessage(frozenHandle, &HB, New IntPtr(1), IntPtr.Zero)
                ' Invalidate the frame and all children after the final layout is ready.
                RedrawWindow(frozenHandle, IntPtr.Zero, IntPtr.Zero, &H485UI)
            End If
        End Try
    End Sub
End Class
