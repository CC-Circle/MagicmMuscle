using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    
    public static bool GameStart = false;
    public static bool muscleTime = false;
    public int muscleTime_cnt = 10;
    public int startWaitTime = 1;
    
    public Slider slider;
    //private static bool isStart = false;
    ////
    //private static bool isOil = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (YourPower.maxValue == 0)
        {
            YourPower.maxValue = 400;
        }
    }
    void Start()
    {
        
        GameStart = false;
        muscleTime = false;
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        if (slider != null)
        {
            MuscleTimeControle();
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("");
            }
        }
       
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

    IEnumerator StartGame()
    {

        yield return new WaitForSeconds(startWaitTime);
        GameStart = true;
    }
}
