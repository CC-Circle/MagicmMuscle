using UnityEngine;
using UnityEngine.SceneManagement;  // シーンを切り替えるために必要
using System.Collections;
using UnityEngine.UI;
public class puroro_gu : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip audioclip;
    public bool isPlay = true;

    public AudioClip muscleclip;
    public bool isPlaymuscle = true;

    public Animator charanimator;
    public string  Scenename= "checkScene";
    public float waitseconds = 10;
    public bool isMove = false;

    public Image image;
    public Image Muscle;

    public byte ColorSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    //void Update()
     // Updateは毎フレーム呼び出される関数です
    void Update()
    {
        
        Serial.strong = Mathf.Abs(Serial.strong);
        Debug.Log(Serial.strong);
        if (Serial.strong > Serial.chargevalue)
        {
            // Debugログで確認（開発中用）
            Debug.Log("ステッキを握りました！（Enterキーまたはクリック）");
            
            // "TutorialScene" へ遷移します（シーン名に一致させてください）
            //SceneManager.LoadScene(Scenename);
            StartCoroutine(StartGame());
            
        }
            // ステッキを握る動作の代わりにEnterキーまたはマウスクリックで反応します
            // ↓キー入力またはクリックを検出
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0))
        {
            // Debugログで確認（開発中用）
            Debug.Log("ステッキを握りました！（Enterキーまたはクリック）");

            // "TutorialScene" へ遷移します（シーン名に一致させてください）
            //SceneManager.LoadScene(Scenename);
            StartCoroutine(StartGame());
        }
       
    }
    void FixedUpdate()
    {
        if (isMove)
        {
            
            image.color = image.color - (new Color32(ColorSpeed, ColorSpeed, ColorSpeed, 0));
            if (image.color.r <= 0) {
                
                if (isPlaymuscle)
                {
                    audioSource.Stop();
                    audioSource.PlayOneShot(muscleclip);
                    isPlaymuscle = false;
                }
                Muscle.color = Muscle.color + (new Color32(0, 0, 0, ColorSpeed));
            }
        }
    }

    IEnumerator StartGame() {
        if (isPlay) {
            audioSource.PlayOneShot(audioclip);
            isPlay = false;
        }

        
        isMove = true;
        charanimator.SetBool("IsTrue", true);
        yield return new WaitForSeconds(waitseconds);
        SceneManager.LoadScene(Scenename);
    }

}




// using UnityEngine;
// using UnityEngine.SceneManagement;  // シーンを切り替えるために必要


// public class StartGameOnGrip : MonoBehaviour
// {
//     // Updateは毎フレーム呼び出される関数です
//     void Update()
//   
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
