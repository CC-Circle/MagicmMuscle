

using UnityEngine;
using System.Diagnostics;
using System.IO;

public class OrangePointer : MonoBehaviour
{
    Process cppProcess;
    string filePathX;
    string filePathY;
    public static float pointerX;
    public static float pointerY;

    void Start()
    {
        pointerX = 0.5f;
        pointerY = 0.5f;

        filePathX = "/tmp/pointingX.txt";
        filePathY = "/tmp/pointingY.txt";

        string exePath;
        if (Application.isEditor)
        {
            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
        }
        else
        {
            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
        }

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = exePath,
            UseShellExecute = false,
            RedirectStandardOutput = false,
            CreateNoWindow = true
        };

        try
        {
            cppProcess = Process.Start(psi);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogError("C++プログラム起動失敗: " + e.Message);
        }
    }

    void Update()
    {
        if (File.Exists(filePathX))
        {
            try
            {
                string val = File.ReadAllText(filePathX);
                if (float.TryParse(val, out float px))
                {
                    pointerX = px;
                    UnityEngine.Debug.Log("X:" + pointerX);
                }
            }
            catch (IOException) { }
        }

        if (File.Exists(filePathY))
        {
            try
            {
                string val = File.ReadAllText(filePathY);
                if (float.TryParse(val, out float py))
                {
                    pointerY = py;
                    UnityEngine.Debug.Log("Y:"+pointerY);
                }
            }
            catch (IOException) { }
        }
    }

    void OnApplicationQuit()
    {
        if (cppProcess != null && !cppProcess.HasExited)
        {
            cppProcess.Kill();
        }
    }
}
