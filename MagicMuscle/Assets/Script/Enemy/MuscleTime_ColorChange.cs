using System.Collections;
using UnityEngine;
using UnityEngine.UI; // RawImageに必要

public class MuscleTime_ColorChange : MonoBehaviour
{
    bool dash_flag = true;

    Material material = null;
    RawImage rawImage = null; // RawImage対応用

    bool isChangeColor = false;

    [Header("色変更スパン")]
    public float Chnge_Color_Time = 0.01f;

    [Header("変更の滑らかさ")]
    public float Smooth = 0.1f;

    [Header("色彩")]
    [Range(0, 1)] public float HSV_Hue = 1.0f;

    [Header("彩度")]
    [Range(0, 1)] public float HSV_Saturation = 1.0f;

    [Header("明度")]
    [Range(0, 1)] public float HSV_Brightness = 1.0f;

    [Header("色彩 MAX")]
    [Range(0, 1)] public float HSV_Hue_max = 1.0f;

    [Header("色彩 MIN")]
    [Range(0, 1)] public float HSV_Hue_min = 0.0f;

    void Start()
    {
        // どちらがアタッチされているか判定
        if (TryGetComponent<Renderer>(out Renderer rend))
        {
            material = rend.material;
        }
        else if (TryGetComponent<RawImage>(out RawImage img))
        {
            rawImage = img;
        }

        HSV_Hue = HSV_Hue_min;
        HSV_Saturation = 0.0f;
        StartCoroutine(Change_Color());
    }

    void Update()
    {
        
        if (GameManager.muscleTime || ScreenToWorldShot.charge||YourPower.iscorrect) {
            isChangeColor = true;
        }
        else
        {
            isChangeColor = false; 
        }

        Color col = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness);
        ApplyColor(col);

        if (isChangeColor && dash_flag)
        {

            StartCoroutine(Change_Color());
        }
        
    }

    public IEnumerator Change_Color()
    {
        if (isChangeColor)
        {
            HSV_Saturation = 0.6f;
            dash_flag = false;
            HSV_Hue += Smooth;
            if (HSV_Hue >= HSV_Hue_max)
            {
                HSV_Hue = HSV_Hue_min;
            }
            Color col = Color.HSVToRGB(HSV_Hue, HSV_Saturation, HSV_Brightness);
            ApplyColor(col);

            yield return new WaitForSeconds(Chnge_Color_Time);
            StartCoroutine(Change_Color());
        }
        else
        {
            HSV_Saturation = 0.0f;
            dash_flag = true;
        }
    }

    /// <summary>
    /// Renderer/RawImageどちらにも対応する共通カラー適用関数
    /// </summary>
    private void ApplyColor(Color col)
    {
        if (material != null)
        {
            material.color = col;
        }
        if (rawImage != null)
        {
            rawImage.color = col;
        }
    }
}
