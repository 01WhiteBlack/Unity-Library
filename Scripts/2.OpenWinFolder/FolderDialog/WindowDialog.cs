using Ookii.Dialogs.WinForms;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System;
using UnityEngine;
/// <summary>
/// ´ò¿ªWindows ´°¿Ú
/// </summary>
public class WindowDialog
{
    [DllImport("user32.dll")]
    private static extern IntPtr GetActiveWindow();
    FilterFileParameters fileParameters;

    public FilterFileParameters FilterFileParameters
    {
        get
        {
            return fileParameters;
        }
        set
        {
            fileParameters = value;
        }
    }

    public void OpenWindowFolder(Action<string> callback)
    {
        var openFileDialog = new VistaOpenFileDialog();
        openFileDialog.Multiselect = fileParameters.iSMultiSelect;
        openFileDialog.Title = fileParameters.WindowTitle;
        openFileDialog.InitialDirectory = fileParameters.firstOpenPath;
        openFileDialog.RestoreDirectory = fileParameters.RestoreDirectory;
        openFileDialog.FilterIndex = fileParameters.filterIndex;
        openFileDialog.Filter = fileParameters.SelectFileType;
        try
        {
            if (openFileDialog.ShowDialog(new WindowFolder(GetActiveWindow())) == DialogResult.OK)
            {
                callback?.Invoke(openFileDialog.FileName);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
            Debug.Log(e.Source);
            Debug.Log(e.Message);
        }
    }

    public void OpenWindowFolder(Action<string[]> callback)
    {
        var openFileDialog = new VistaOpenFileDialog();
        openFileDialog.Multiselect = fileParameters.iSMultiSelect;
        openFileDialog.Title = fileParameters.WindowTitle;
        openFileDialog.InitialDirectory = fileParameters.firstOpenPath;
        openFileDialog.RestoreDirectory = fileParameters.RestoreDirectory;
        openFileDialog.FilterIndex = fileParameters.filterIndex;
        openFileDialog.Filter = fileParameters.SelectFileType;
        try
        {
            DialogResult dialogResult = openFileDialog.ShowDialog(new WindowFolder(GetActiveWindow()));
            if (dialogResult == DialogResult.OK)
            {
                callback?.Invoke(openFileDialog.FileNames);
            }
            else
                callback?.Invoke(null);
            //Debug.Log(dialogResult);
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
            Debug.Log(e.Source);
            Debug.Log(e.Message);
        }
    }
}