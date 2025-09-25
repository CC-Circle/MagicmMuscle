using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    
    public static bool GameStart = false;
    public static bool muscleTime = false;
    public static bool GameEnd = false;


    public static int Time = 120;
    public int time = 120;

    public int muscleTime_cnt = 10;
    public int startWaitTime = 1;
    
    public Slider slider;

    public SpawnManager spawnmanager;

    public AudioSource audiosource;
    public AudioClip start,yhea;

    public SceneMove scenemove;

    public TextMeshProUGUI endText;
    //private static bool isStart = false;
    ////
    //private static bool isOil = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        Time = time;
        if (YourPower.maxValue == 0)
        {
            YourPower.maxValue = 400;
        }
    }
    void Start()
    {
        endText.enabled = false;
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
        if (spawnmanager != null)
        {
            if (spawnmanager.isEnd)
            {
                if (scenemove != null)
                {
                    scenemove.MoveScene();
                }
            }
        }

        if (spawnmanager.isEnd) {
            if (!GameEnd) {
                StartCoroutine(EndGame());
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
        audiosource.PlayOneShot(start);

    }
    IEnumerator EndGame()
    {
        GameEnd = true;
        yield return new WaitForSeconds(4);
        
        endText.enabled = true;
        endText.text = "終了〜!";
        audiosource.PlayOneShot(yhea);

    }
}
