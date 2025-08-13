//using UnityEngine;
//using System.Diagnostics;
//using System.IO;

//public class OrangePointer : MonoBehaviour
//{
//    Process cppProcess;
//    string filePath = "/tmp/pointingX.txt"; // Macの一時ファイル
//    public static float pointerX;

//    void Start()
//    {
//        pointerX = 0.5f;
//        // C++プログラム起動
//        ProcessStartInfo psi = new ProcessStartInfo
//        {
//            FileName = "Assets/Plugins/macOS/a.out", // ビルドした実行ファイル
//            UseShellExecute = false,
//            RedirectStandardOutput = false, // ファイルで受け取るので不要
//            CreateNoWindow = true
//        };

//        cppProcess = Process.Start(psi);
//    }

//    void Update()
//    {
//        if (File.Exists(filePath))
//        {
//            try
//            {
//                string val = File.ReadAllText(filePath);
//                if (float.TryParse(val, out float pointingX))
//                {
//                    UnityEngine.Debug.Log("PointingX = " + pointingX);
//                    pointerX = pointingX;
//                }
//            }
//            catch (IOException)
//            {
//                // 他プロセスがファイル書き込み中
//            }
//        }
//    }

//    void OnApplicationQuit()
//    {
//        if (cppProcess != null && !cppProcess.HasExited)
//        {
//            cppProcess.Kill();
//        }
//    }
//}


using UnityEngine;
using System.Diagnostics;
using System.IO;

public class OrangePointer : MonoBehaviour
{
    Process cppProcess;
    string filePath;
    public static float pointerX;

    void Start()
    {
        pointerX = 0.5f;

        // 共有ファイルパス（Macなら /tmp/ が安全）
        filePath = "/tmp/pointingX.txt";

        // 実行ファイルパスを取得
        string exePath;
        if (Application.isEditor)
        {
            // エディタ実行時（Assetsフォルダ直下を参照）
            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
        }
        else
        {
            // ビルド後（.appパッケージ内のPluginsフォルダを参照）
            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
        }

        // プロセス起動
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
        if (File.Exists(filePath))
        {
            try
            {
                string val = File.ReadAllText(filePath);
                if (float.TryParse(val, out float pointingX))
                {
                    pointerX = pointingX;
                }
            }
            catch (IOException)
            {
                // 他プロセスが書き込み中
            }
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
