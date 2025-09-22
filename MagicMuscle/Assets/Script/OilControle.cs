using UnityEngine;
using UnityEngine.UI;
using System.Collections;
public class OilControle : MonoBehaviour
{
    public AudioSource audio;
    public AudioClip clip;
    public Image targetImage; // 表示切替したいImageをInspectorで指定
    public static bool isOil = false;
    public float waittime;
    private void Start()
    {
        isOil = false;
        HideImage();
    }

    public void Update()
    {
        if (isOil == true)
        {
            audio.PlayOneShot(clip);
            isOil = false;
            DrawOil();

        }
    }

    // 表示・非表示を切り替える
    public void ToggleImage()
    {
        if (targetImage != null)
        {
            targetImage.enabled = !targetImage.enabled;
        }
    }

    // 強制的に表示
    public void ShowImage()
    {
        if (targetImage != null)
        {
            targetImage.enabled = true;
        }
    }

    // 強制的に非表示
    public void HideImage()
    {
        if (targetImage != null)
        {
            targetImage.enabled = false;
        }
    }
    public void DrawOil()
    {
        StartCoroutine(Oiling());
    }



    IEnumerator Oiling()
    {
        ShowImage();
        yield return new WaitForSeconds(waittime);
        HideImage();
    }
    

}
