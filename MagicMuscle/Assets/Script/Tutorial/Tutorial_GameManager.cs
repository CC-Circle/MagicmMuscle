using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;
// テキストとboolをまとめたデータ構造
[System.Serializable] // ← インスペクターで表示可能にする
public class TutorialStep
{
    public string text;
    public bool isCompleted;   // そのステップが完了したかどうか
}

public class Tutorial_GameManager : MonoBehaviour
{

    private bool IsSteckUp = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    // 複数のステップを管理できるようにリスト化
    public List<TutorialStep> tutorialSteps = new List<TutorialStep>();
    public TextMeshProUGUI TM;
    private int currentStepIndex = 0;

    private Serial serial;
    public SliderCharge slider;
    private bool isMoveTextTimer = false;
    public string scenename;

    // クラスのメンバに追加
    public AudioSource audioSource;   // 音声再生用
    public AudioClip[] voiceClips;    // 各セリフに対応する音声


    void Start()
    {
        
        serial = GameObject.Find("Serial").GetComponent<Serial>();
    }
    void Update()
    {


        if (tutorialSteps.Count == 0) return;

       
        switch (currentStepIndex)
        {
            case 0:
                //教えたる
                if (!isMoveTextTimer)
                {
                    StartCoroutine(MoveTextTime(3.0f));
                    isMoveTextTimer = true;
                }
                break;
                //
            case 1:
                if (Serial.deg<0.3)
                {
                    MoveNextStep();
                }
                break;
            case 2:
                if (Serial.isDegShake)
                {
                    MoveNextStep();
                }
                break;
            case 3:
                if (!isMoveTextTimer)
                {
                    StartCoroutine(MoveTextTime(3.0f));
                    isMoveTextTimer = true;
                }
                break;
            case 4:
                if (slider.charge>0)
                {
                    MoveNextStep();
                }
                break;
            case 5:
                if (!isMoveTextTimer)
                {
                    StartCoroutine(MoveTextTime(3.0f));
                    isMoveTextTimer = true;
                }
                break;
            
            case 6:
                if (Serial.isDegShake)
                {
                    MoveNextStep();
                }
                break;
            //ええぞ敵が吹っ飛んでった
            case 7:
                if (!isMoveTextTimer)
                {
                    StartCoroutine(MoveTextTime(2.0f));
                    isMoveTextTimer = true;
                }
                break;
            //にぎにぎ1回目
            case 8:
                if (slider.charge > 0)
                {
                    MoveNextStep();
                }
                break;
            //にぎにぎ2回目
            case 9:
                if (slider.charge > 1)
                {
                    MoveNextStep();
                }
                break;
            //にぎにぎ3回目
            case 10:
                if (slider.charge > 2)
                {
                    MoveNextStep();
                }
                break;
            //にぎにぎ3回目
            case 11:
                if (Serial.isDegShake)
                {
                    MoveNextStep();
                }
                break;
            //最高やいっぱい握る
            case 12:
                if (!isMoveTextTimer)
                {
                    StartCoroutine(MoveTextTime(3.0f));
                    isMoveTextTimer = true;
                }
                break;
            case 13:
                //if (!isMoveTextTimer)
                //{
                //    StartCoroutine(MoveTextTime(1.0f));
                //    isMoveTextTimer = true;
                //}
                StartCoroutine(MoveScene(2.0f));
                break;

        }
        //// スペースキーで次のステップに進む例
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    MoveNextStep();
        //}
        TM.SetText(tutorialSteps[currentStepIndex].text);
    }


    void MoveNextStep()
    {
        CompleteCurrentStep();
        NextStep();
    }
    void CompleteCurrentStep() 
    {
        if (currentStepIndex < tutorialSteps.Count)
        {
            tutorialSteps[currentStepIndex].isCompleted = true;
           
        }
    }

    void NextStep()
    {
        if (audioSource != null && voiceClips != null && currentStepIndex < voiceClips.Length)
        {
            audioSource.Stop();
            audioSource.clip = voiceClips[currentStepIndex];
            audioSource.Play();
        }
        currentStepIndex++;
        if (currentStepIndex < tutorialSteps.Count)
        {

        }
        else
        {
            Debug.Log("チュートリアル終了！");
        }
    }


    IEnumerator MoveTextTime(float time)
    {
        yield return new WaitForSeconds(time);
        isMoveTextTimer = false;
        MoveNextStep();
    }

    IEnumerator MoveScene(float time)
    {
        yield return new WaitForSeconds(time);
        SceneManager.LoadScene(scenename);
    }

}
