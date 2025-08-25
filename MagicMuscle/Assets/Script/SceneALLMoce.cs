using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneALLMove : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene("OpeningScene");
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            SceneManager.LoadScene("checkScene");
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene("GameScene");
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("EndingScene");
        }
    }
}
