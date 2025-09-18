using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Diagnostics;

public class SceneMove : MonoBehaviour
{
    // シーン名 or インデックスを指定
    public string nextSceneName = "NextScene";
    public float time = 19f;

    // 開始時にシーン移動のコルーチンを開始
    void Start()
    {
        
    }

    public void MoveScene (){
        StartCoroutine(DelayAndLoadScene(time)); // 5秒後に移動 
    }


    IEnumerator DelayAndLoadScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(nextSceneName);
    }


}
