

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

//        //string exePath;
//        //if (Application.isEditor)
//        //{
//        //    exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
//        //}
//        //else
//        //{
//        //    exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
//        //}

//        string exePath;
//        if (Application.isEditor)
//        {
//            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
//        }
//        else
//        {
//            // macOSビルド後は.appバンドル内のContents/Resources/Data/
//            exePath = Path.Combine(Application.streamingAssetsPath, "a.out");
//            // または
//            // exePath = Path.Combine(Application.dataPath, "../Resources/a.out");
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
//                    UnityEngine.Debug.Log("Y:"+pointerY);
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
using System.Text;

public class OrangePointer : MonoBehaviour
{
    [Header("Debug UI")]
    public TextMeshProUGUI debugText; // インスペクターでアサインしてください

    Process cppProcess;
    string filePathX;
    string filePathY;
    public static float pointerX;
    public static float pointerY;

    private StringBuilder debugLog = new StringBuilder();
    private float lastUpdateTime = 0f;

    void Start()
    {
        AddDebugLog("=== OrangePointer Start ===");

        pointerX = 0.5f;
        pointerY = 0.5f;

        filePathX = "/tmp/pointingX.txt";
        filePathY = "/tmp/pointingY.txt";

        AddDebugLog($"Application.isEditor: {Application.isEditor}");
        AddDebugLog($"Application.platform: {Application.platform}");
        AddDebugLog($"Application.dataPath: {Application.dataPath}");
        AddDebugLog($"Application.streamingAssetsPath: {Application.streamingAssetsPath}");

        string exePath;
        if (Application.isEditor)
        {
            exePath = Path.Combine(Application.dataPath, "Plugins/macOS/a.out");
            AddDebugLog($"Editor mode - exePath: {exePath}");
        }
        else
        {
            // ビルド後のパスを試行
            exePath = Path.Combine(Application.streamingAssetsPath, "a.out");
            AddDebugLog($"Build mode - exePath (StreamingAssets): {exePath}");

            // ファイルが存在するかチェック
            if (!File.Exists(exePath))
            {
                // 代替パスを試行
                string altPath1 = Path.Combine(Application.dataPath, "../Resources/a.out");
                string altPath2 = Path.Combine(Application.dataPath, "a.out");
                string altPath3 = Path.Combine(System.IO.Directory.GetCurrentDirectory(), "a.out");

                AddDebugLog($"File not found at: {exePath}");
                AddDebugLog($"Trying alternative paths:");
                AddDebugLog($"  altPath1: {altPath1} - Exists: {File.Exists(altPath1)}");
                AddDebugLog($"  altPath2: {altPath2} - Exists: {File.Exists(altPath2)}");
                AddDebugLog($"  altPath3: {altPath3} - Exists: {File.Exists(altPath3)}");

                if (File.Exists(altPath1)) exePath = altPath1;
                else if (File.Exists(altPath2)) exePath = altPath2;
                else if (File.Exists(altPath3)) exePath = altPath3;

                AddDebugLog($"Final exePath: {exePath}");
            }
        }

        // 実行ファイルの存在確認
        AddDebugLog($"File.Exists(exePath): {File.Exists(exePath)}");

        if (File.Exists(exePath))
        {
            // ファイル権限チェック（Unix系）
            try
            {
                var fileInfo = new FileInfo(exePath);
                AddDebugLog($"File size: {fileInfo.Length} bytes");
                AddDebugLog($"File attributes: {fileInfo.Attributes}");
            }
            catch (System.Exception ex)
            {
                AddDebugLog($"FileInfo error: {ex.Message}");
            }
        }

        // tmp ディレクトリの確認
        AddDebugLog($"Directory.Exists('/tmp'): {Directory.Exists("/tmp")}");

        // プロセス起動
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = exePath,
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        try
        {
            AddDebugLog("Attempting to start C++ process...");
            cppProcess = Process.Start(psi);

            if (cppProcess != null)
            {
                AddDebugLog($"C++ process started successfully. PID: {cppProcess.Id}");

                // 標準出力とエラー出力を非同期で読み取り
                cppProcess.OutputDataReceived += (sender, e) => {
                    if (!string.IsNullOrEmpty(e.Data))
                        AddDebugLog($"C++ stdout: {e.Data}");
                };

                cppProcess.ErrorDataReceived += (sender, e) => {
                    if (!string.IsNullOrEmpty(e.Data))
                        AddDebugLog($"C++ stderr: {e.Data}");
                };

                cppProcess.BeginOutputReadLine();
                cppProcess.BeginErrorReadLine();
            }
            else
            {
                AddDebugLog("Process.Start returned null");
            }
        }
        catch (System.Exception e)
        {
            AddDebugLog($"C++ process start failed: {e.GetType().Name}");
            AddDebugLog($"Error message: {e.Message}");
            if (e.InnerException != null)
                AddDebugLog($"Inner exception: {e.InnerException.Message}");
            AddDebugLog($"Stack trace: {e.StackTrace}");
        }

        UpdateDebugDisplay();
    }

    void Update()
    {
        // プロセス状態チェック
        if (cppProcess != null && Time.time - lastUpdateTime > 5f) // 5秒ごと
        {
            try
            {
                AddDebugLog($"Process status - HasExited: {cppProcess.HasExited}");
                if (cppProcess.HasExited)
                {
                    AddDebugLog($"Process exit code: {cppProcess.ExitCode}");
                }
            }
            catch (System.Exception ex)
            {
                AddDebugLog($"Process status check error: {ex.Message}");
            }
            lastUpdateTime = Time.time;
        }

        // ファイル読み取り
        ReadPointerFile(filePathX, "X", ref pointerX);
        ReadPointerFile(filePathY, "Y", ref pointerY);

        UpdateDebugDisplay();
    }

    void ReadPointerFile(string filePath, string axis, ref float pointerValue)
    {
        if (File.Exists(filePath))
        {
            try
            {
                string val = File.ReadAllText(filePath);
                if (float.TryParse(val, out float parsedValue))
                {
                    pointerValue = parsedValue;
                    // ログが多すぎる場合はコメントアウト
                    // AddDebugLog($"{axis}: {pointerValue}");
                }
                else
                {
                    AddDebugLog($"Failed to parse {axis} value: '{val}'");
                }
            }
            catch (IOException ioEx)
            {
                AddDebugLog($"IO error reading {axis} file: {ioEx.Message}");
            }
            catch (System.Exception ex)
            {
                AddDebugLog($"Unexpected error reading {axis} file: {ex.Message}");
            }
        }
        else
        {
            // ファイルが存在しない場合の詳細ログ（最初の数回のみ）
            if (Time.time < 10f) // 最初の10秒間のみ
            {
                AddDebugLog($"{axis} file does not exist: {filePath}");
            }
        }
    }

    void AddDebugLog(string message)
    {
        string timestampedMessage = $"[{Time.time:F2}] {message}";
        debugLog.AppendLine(timestampedMessage);
        UnityEngine.Debug.Log(timestampedMessage);

        // ログが長くなりすぎないように制限
        if (debugLog.Length > 5000)
        {
            string currentLog = debugLog.ToString();
            debugLog.Clear();
            debugLog.AppendLine("... (log truncated) ...");
            debugLog.AppendLine(currentLog.Substring(currentLog.Length / 2));
        }
    }

    void UpdateDebugDisplay()
    {
        if (debugText != null)
        {
            string displayText = debugLog.ToString();

            // 表示文字数制限（TMPの表示限界を考慮）
            if (displayText.Length > 3000)
            {
                displayText = "... (truncated) ...\n" + displayText.Substring(displayText.Length - 3000);
            }

            debugText.text = displayText + $"\n\nCurrent Values:\nX: {pointerX:F3}\nY: {pointerY:F3}";
        }
    }

    void OnApplicationQuit()
    {
        AddDebugLog("Application quitting...");

        if (cppProcess != null)
        {
            try
            {
                if (!cppProcess.HasExited)
                {
                    AddDebugLog("Killing C++ process...");
                    cppProcess.Kill();
                    AddDebugLog("C++ process killed");
                }
                else
                {
                    AddDebugLog("C++ process already exited");
                }
            }
            catch (System.Exception ex)
            {
                AddDebugLog($"Error during process cleanup: {ex.Message}");
            }
        }
        else
        {
            AddDebugLog("No C++ process to clean up");
        }
    }

    // デバッグ情報を手動でクリアするメソッド
    [ContextMenu("Clear Debug Log")]
    public void ClearDebugLog()
    {
        debugLog.Clear();
        UpdateDebugDisplay();
    }
}