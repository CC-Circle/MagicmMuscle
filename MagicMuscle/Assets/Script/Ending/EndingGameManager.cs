using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using UnityEngine.Video;
using UnityEngine.UI;
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

    public AudioSource audiosource,dramaudio;
    public AudioClip notValue,Value,roll,yhea;
    public GameObject daietto;
    public GameObject sqeeze;
    public GameObject sqeeze_sqeeze;
    public end_Score end_score;
    public EndResultSpawner endresultspawner;

    public GameObject ThankYou;

    public SceneMove scenemove;
    public BG_ColorChange bg;
    public bool isCount= false;
    public Animator animator;
    //カメラ移動
    public bool isCameraMove;
    public CameraRankingMove cameramove;
    public MuscleMakaer musclemaker;

    //ビデオ
    public RawImage videoraw;
    public VideoPlayer videoPlayer;

    //ライト
    public Light DirectLight;

    //アニメーター

    public Animator ScoreAnimation;
    void Start()
    {
        ThankYou.SetActive(false);

        // 最初は非表示
        videoraw.enabled = false;
        //videoPlayer.waitForFirstFrame = false; // 再生開始まで何も出さない

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

                    ChangeMusic(audiosource,Value);
                }

            }
            if (Serial.ischarge)
            {
                Debug.Log("MaxScoreInput:"+endresultspawner.maxScoreInputScore(Score.score));
               
            }
            //カウント中なら音楽を変更する
            if (end_score.IsCount)
            {
                ChangeMusic(audiosource,notValue);
            }
            else
            {
                iscomit = true;
                ChangeMusic(audiosource,Value);
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
                ChangeMusic(audiosource,notValue);
            }
            else
            {
                ChangeMusic(audiosource,Value);
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
            if (!dramaudio.isPlaying) // 再生中でなければ
            {
                dramaudio.Play();
            }
        }
        
        //dramaudio.Play();
        //カメラ移動終了
        if (cameramove.isMoveEnd)
        {
            StartCoroutine(ThankYouEnd());
            musclemaker.ShowPoepleNumber();
            dramaudio.loop = false;
            ChangeMusic(dramaudio, yhea);
            DirectLight.intensity = 0.5f;
            musclemaker.LightOn();
            //ズームする
            cameramove.ZoomTarget();

            videoraw.enabled = true;
            videoPlayer.Play();
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

    IEnumerator ThankYouEnd()
    {
        yield return new WaitForSeconds(4.0f);
        ThankYou.SetActive(true);

    }
    // 曲を切り替える関数
    public void ChangeMusic( AudioSource audiosource,AudioClip newClip)
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
