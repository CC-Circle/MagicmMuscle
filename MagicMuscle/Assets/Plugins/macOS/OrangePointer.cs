

//using UnityEngine;
//using System.Diagnostics;
//using System.IO;

//public class OrangePointer : MonoBehaviour
//{
//    Process cppProcess;
//    string filePathX;
//    string filePathY;
//    public static float pointerX;
//    public static float pointerY;

//    void Start()
//    {
//        pointerX = 0.5f;
//        pointerY = 0.5f;

//        filePathX = "/tmp/pointingX.txt";
//        filePathY = "/tmp/pointingY.txt";

//        string exePath;
//        if (Application.isEditor)
//        {
//            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
//        }
//        else
//        {
//            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
//        }



//        ProcessStartInfo psi = new ProcessStartInfo
//        {
//            FileName = exePath,
//            UseShellExecute = false,
//            RedirectStandardOutput = false,
//            CreateNoWindow = true
//        };

//        try
//        {
//            cppProcess = Process.Start(psi);
//        }
//        catch (System.Exception e)
//        {
//            UnityEngine.Debug.LogError("C++プログラム起動失敗: " + e.Message);
//        }
//    }

//    void Update()
//    {
//        if (File.Exists(filePathX))
//        {
//            try
//            {
//                string val = File.ReadAllText(filePathX);
//                if (float.TryParse(val, out float px))
//                {
//                    pointerX = px;
//                    UnityEngine.Debug.Log("X:" + pointerX);
//                }
//            }
//            catch (IOException) { }
//        }

//        if (File.Exists(filePathY))
//        {
//            try
//            {
//                string val = File.ReadAllText(filePathY);
//                if (float.TryParse(val, out float py))
//                {
//                    pointerY = py;
//                    UnityEngine.Debug.Log("Y:" + pointerY);
//                }
//            }
//            catch (IOException) { }
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
using TMPro;
public class OrangePointer : MonoBehaviour
{
    Process cppProcess;
    string filePathX;
    string filePathY;
    public static float pointerX;
    public static float pointerY;
    public TextMeshProUGUI TM;

    void Start()
    {
        pointerX = 0.5f;
        pointerY = 0.5f;

        // macOSの一時ディレクトリを使用（より安全）
        string tempDir = Path.GetTempPath();
        filePathX = Path.Combine(tempDir, "pointingX.txt");
        filePathY = Path.Combine(tempDir, "pointingY.txt");

        string exePath = GetExecutablePath();

        if (string.IsNullOrEmpty(exePath) || !File.Exists(exePath))
        {
            
            UnityEngine.Debug.LogError("実行ファイルが見つかりません: " + exePath);
            TM.SetText("実行ファイルが見つかりません");
            return;
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
            TM.SetText("C++プログラムを起動しました:");
            UnityEngine.Debug.Log("C++プログラムを起動しました: " + exePath);
        }
        catch (System.Exception e)
        {
            TM.SetText("C++プログラム起動失敗:");
            UnityEngine.Debug.LogError("C++プログラム起動失敗: " + e.Message);
        }
    }

    string GetExecutablePath()
    {
        if (Application.isEditor)
        {
            // エディタの場合：Assets/Plugins/macOS/a.out
            return Path.Combine(Application.dataPath, "Plugins", "macOS", "a.out");
        }
        else
        {
            // ビルド後の場合：StreamingAssetsを使用
            // StreamingAssetsフォルダにa.outを配置する必要があります
            string streamingPath = Path.Combine(Application.streamingAssetsPath, "a.out");
            if (File.Exists(streamingPath))
            {
                return streamingPath;
            }

            // 代替案：アプリケーションバンドル内のResourcesフォルダ
            string resourcePath = Path.Combine(Application.dataPath, "a.out");
            if (File.Exists(resourcePath))
            {
                return resourcePath;
            }

            UnityEngine.Debug.LogError("実行ファイルが見つかりません。StreamingAssetsまたはResourcesに配置してください。");
            return null;
        }
    }

    void Update()
    {
        ReadPointerValue(filePathX, ref pointerX, "X");
        ReadPointerValue(filePathY, ref pointerY, "Y");
    }

    void ReadPointerValue(string filePath, ref float currentValue, string axis)
    {
        if (!File.Exists(filePath))
            return;

        try
        {
            string content = File.ReadAllText(filePath);
            if (float.TryParse(content.Trim(), out float newValue))
            {
                if (newValue != currentValue)
                {
                    currentValue = newValue;
                    UnityEngine.Debug.Log(axis + ":" + currentValue);
                }
            }
        }
        catch (IOException e)
        {
            UnityEngine.Debug.LogWarning($"ファイル読み込みエラー ({axis}): " + e.Message);
        }
        catch (System.UnauthorizedAccessException e)
        {
            UnityEngine.Debug.LogWarning($"ファイルアクセス権限エラー ({axis}): " + e.Message);
        }
    }

    void OnApplicationQuit()
    {
        if (cppProcess != null && !cppProcess.HasExited)
        {
            try
            {
                cppProcess.Kill();
                cppProcess.WaitForExit(1000); // 1秒待機
                UnityEngine.Debug.Log("C++プロセスを終了しました");
            }
            catch (System.Exception e)
            {
                UnityEngine.Debug.LogWarning("プロセス終了エラー: " + e.Message);
            }
        }

        // 一時ファイルのクリーンアップ
        CleanupTempFiles();
    }

    void CleanupTempFiles()
    {
        try
        {
            if (File.Exists(filePathX))
                File.Delete(filePathX);
            if (File.Exists(filePathY))
                File.Delete(filePathY);
        }
        catch (System.Exception e)
        {
            UnityEngine.Debug.LogWarning("一時ファイル削除エラー: " + e.Message);
        }
    }
}