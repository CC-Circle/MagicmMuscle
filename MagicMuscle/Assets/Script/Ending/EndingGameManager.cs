using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class EndingGameManager : MonoBehaviour
{
    public bool SimpleScore = false;

    //trueの場合、カウントダウンが早い
    public bool isSpeedChangeMode = true;
    public bool isAutoCount;

    public bool isSqeezeStart = false;

    //結果表示が終わったか
    private bool iscomit = false;
    private bool iscomitdone = true;

    public AudioSource audiosource;
    public AudioClip notValue,Value;
    public GameObject daietto;
    public GameObject sqeeze;
    public GameObject sqeeze_sqeeze;
    public end_Score end_score;
    public EndResultSpawner endresultspawner;

    public SceneMove scenemove;
    public BG_ColorChange bg;
    public bool isCount= false;
    public Animator animator;

    public bool isCameraMove;
    public CameraRankingMove cameramove;
    public MuscleMakaer musclemaker;

    //アニメーター
    public Animator ScoreAnimation;
    void Start()
    {
        sqeeze.SetActive(false);
        sqeeze_sqeeze.SetActive(false);

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
                iscomit = true;
                ChangeMusic(Value);
            }

            if(isSqeezeStart)
            {
                if (Serial.ischarge)
                {
                    
                    if (!iscomit)
                    {
                        sqeeze.SetActive(true);
                    }
                    else
                    {
                        sqeeze.SetActive(false);
                    }
                    sqeeze_sqeeze.SetActive(false);
                }
                else
                {
                    sqeeze.SetActive(false);
                    if (!iscomit) {
                        sqeeze_sqeeze.SetActive(true);
                    }
                    else
                    {
                        sqeeze_sqeeze.SetActive(false);
                    }

                    
                }
                
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
        //１回だけよぶ
        if (iscomit && iscomitdone)
        {
            
            StartCoroutine(ShowScore());
            scenemove.MoveScene();
            iscomitdone = false;
        }
        //ループさせてる
        if (iscomit)
        {
            bg.ColorLerp();
        }
        //カメラ移動
        if (isCameraMove)
        {
            cameramove.MoveCamera();
        }
        //カメラ移動終了
        if (cameramove.isMoveEnd)
        {
            musclemaker.LightOn();
        }
        //

    }

    IEnumerator GameStart()
    {
        yield return new WaitForSeconds(3.0f);
        //スコアのカウントを開始
        end_score.StartCount();
        //オープニング画像を変更
        daietto.SetActive(false);
        isSqeezeStart = true;
    }

    IEnumerator ShowScore()
    {
        yield return new WaitForSeconds(3.0f);
        if (SimpleScore)
        {
            ScoreAnimation.SetBool("isScore", true);
        }
        else
        {
            animator.SetBool("UIMove", true);
            isCameraMove = true;
        }

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
