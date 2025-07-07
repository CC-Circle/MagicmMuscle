using UnityEngine;
using UnityEngine.SceneManagement;  // シーンを切り替えるために必要

public class puroro_gu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
     // Updateは毎フレーム呼び出される関数です
    void Update()
    {
        // ステッキを握る動作の代わりにEnterキーまたはマウスクリックで反応します
        // ↓キー入力またはクリックを検出
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            // Debugログで確認（開発中用）
            Debug.Log("ステッキを握りました！（Enterキーまたはクリック）");

            // "TutorialScene" へ遷移します（シーン名に一致させてください）
            SceneManager.LoadScene("GameScene");
        }
    }
}

// using UnityEngine;
// using UnityEngine.SceneManagement;  // シーンを切り替えるために必要


// public class StartGameOnGrip : MonoBehaviour
// {
//     // Updateは毎フレーム呼び出される関数です
//     void Update()
//     {
//         // ステッキを握る動作の代わりにEnterキーまたはマウスクリックで反応します
//         // ↓キー入力またはクリックを検出
//         if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
//         {
//             // Debugログで確認（開発中用）
//             Debug.Log("ステッキを握りました！（Enterキーまたはクリック）");

//             // "TutorialScene" へ遷移します（シーン名に一致させてください）
//             SceneManager.LoadScene("GameScene");
//         }
//     }
// }
