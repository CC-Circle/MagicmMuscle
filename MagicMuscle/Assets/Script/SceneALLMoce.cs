using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneALLMove : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("1_OpeningScene");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("2a_CheckScene");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("3_checkScene(W)");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("4_Tutorial");
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            SceneManager.LoadScene("5_GameScene_Simple");
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("6_EndingScene");
        }
    }
}
