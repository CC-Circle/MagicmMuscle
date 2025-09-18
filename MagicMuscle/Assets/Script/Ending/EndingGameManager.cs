using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class EndingGameManager : MonoBehaviour
{
    //trueの場合、カウントダウンが早い
    public bool isSpeedChangeMode = true;
    public bool isAutoCount;

    //結果表示が終わったか
    private bool iscomit = false;
    private bool iscomitdone = true;

    public AudioSource audiosource;
    public AudioClip notValue,Value;
    public GameObject daietto;
    public end_Score end_score;
    public EndResultSpawner endresultspawner;
    public SceneMove scenemove;
    public BG_ColorChange bg;
    public bool isCount= false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GameStart());
        if (isSpeedChangeMode)
        {
            endresultspawner.ShowResult(end_Score.countup_score);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (isSpeedChangeMode)
        {
            
            if (Serial.ischarge||isAutoCount==true)
            {
                end_score.isPauseByCharge = true;
            }
            else
            {
                end_score.isPauseByCharge = false;
            }

            if (Score.score <= end_Score.countup_score)
            {
                if (endresultspawner.ShowResult(end_Score.countup_score))
                {
                    iscomit = true;
                    ChangeMusic(Value);
                }

            }
            if (Serial.ischarge)
            {
                Debug.Log("MaxScoreInput:"+endresultspawner.maxScoreInputScore(Score.score));
               
            }
            //カウント中なら音楽を変更する
            if (end_score.IsCount)
            {
                ChangeMusic(notValue);
            }
            else
            {
                ChangeMusic(Value);
            }
        }
        else {
            if (Score.score <= end_Score.countup_score)
            {
                iscomit = true;

            }
            if (Serial.ischarge)
            {
                end_score.isPauseByCharge = true;
            }
            else
            {
                end_score.isPauseByCharge = false;
            }
            if (endresultspawner.ShowResult(end_Score.countup_score))
            {
                end_score.StopCount();
            }
            //カウント中なら音楽を変更する
            if (end_score.IsCount)
            {
                ChangeMusic(notValue);
            }
            else
            {
                ChangeMusic(Value);
            }
        }

        //カウントが終わった後の処理

        if (iscomit && iscomitdone)
        {
            scenemove.MoveScene();
            iscomitdone = false;
        }
        if (iscomit)
        {
            bg.ColorLerp();
        }

    }
    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3.0f);
        //スコアのカウントを開始
        end_score.StartCount();
        //オープニング画像を変更
        daietto.SetActive(false);
    }

    // 曲を切り替える関数
    public void ChangeMusic(AudioClip newClip)
    {
        // 今の曲と同じなら何もしない
        if (audiosource.clip == newClip)
            return;

        // 曲を切り替えて再生
        audiosource.Stop();
        audiosource.clip = newClip;
        audiosource.Play();
    }
}
