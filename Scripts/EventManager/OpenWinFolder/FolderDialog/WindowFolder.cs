using System;
using System.Windows.Forms;
public class WindowFolder : IWin32Window
{
    IntPtr handle;
    public WindowFolder(IntPtr handle)
    {
        this.handle = handle;
    }
    public IntPtr Handle
    {
        get { return handle; }
    }
}
