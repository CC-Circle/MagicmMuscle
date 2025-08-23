using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Remoting.Lifetime;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Slider : MonoBehaviour
{
    public UnityEngine.UI.Slider slider;
    public float pollenPoint;
    public int pollenReleaseRate;
    public UnityEngine.UI.Image sliderImage; //connected the Image Fill from the slider
    [HideInInspector] public bool noseScaleChange = false;

    public bool feverFlag;

    // Start is called before the first frame update
    void Start()
    {
        if (slider != null)
        {
            slider.value = 0;
            pollenPoint = 0;
            feverFlag = false;

        }
    }
    private void Update()
    {
        FeverTime();
    }
    public void CollectObject()
    {
        if (!feverFlag)
        {
            pollenPoint++;
        }
        UpdateSlider();
    }

    public bool EmittingObject()
    {

        if (pollenPoint < pollenReleaseRate)
        {

            return false;
        }
        UpdateSlider();
        return true;
    }

    public void FeverTime()
    {

        UpdateSlider();
        if (pollenPoint >= slider.maxValue)
        {
            feverFlag = true;
        }
        else
        {
            feverFlag = false;
        }
    }

    // Update is called once per frame
    private void UpdateSlider()
    {
        if (slider != null)
        {
            slider.value = pollenPoint;

            if (pollenPoint > 100)
            {
                noseScaleChange = true;
            }
            else
            {
                noseScaleChange = false;
            }

            if (feverFlag)
                sliderImage.color = new Color32(255, 209, 0, 255);
            else if (pollenPoint > 100)
                sliderImage.color = new Color32(255, 209, 0, 255);
            else
                sliderImage.color = new Color32(80, 255, 0, 255);
        }
    }
}
