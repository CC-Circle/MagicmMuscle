using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewMonoBehaviourScript1 : MonoBehaviour
{


        bool gameEnded;
    float gameTime;
    Text timerText;
    
    // Start is called once befre the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //gameTime = 3;

    }



    // public float gameTime = 60f; // ゲームの制限時間（秒）
        // public Text timerText;       // UI に残り時間を表示（オプション）

        // private bool gameEnded = false;


        // void EndGame()
        // {
        //     SceneManager.LoadScene("EndingScene");
        // }
        // }



    void Update()  //マイフレームごとの処理
    {
        if (gameEnded) return;


        gameTime -= Time.deltaTime;

        // if (timerText != null)
        // {
        //     timerText.text = Mathf.Ceil(gameTime).ToString("00");
        // }

        // if (gameTime <= 0f)
        // {
        //     gameEnded = true;
        //     EndGame();
        // }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            gameEnded = true;
            EndGame();
        }

    }

 void EndGame()
    {
        SceneManager.LoadScene("EndingScene");
    }
}
