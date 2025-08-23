using UnityEngine;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static bool muscleTime = false;
    public int muscleTime_cnt = 10;
    public Slider slider;
    //private static bool isStart = false;
    ////
    //private static bool isOil = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        muscleTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        MuscleTimeControle();
    }

    void MuscleTimeControle()
    {
        if (muscleTime==false&&slider.feverFlag)
        {
            muscleTime = true;
            StartCoroutine(MuscleTimeCnt());
        }
    }
    IEnumerator MuscleTimeCnt()
    {
        yield return new WaitForSeconds(muscleTime_cnt);
        muscleTime = false;
        slider.pollenPoint = 0;
    }
}
