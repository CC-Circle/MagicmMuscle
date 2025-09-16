using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class EndingGameManager : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip notValue,Value;
    public GameObject daietto;
    public end_Score end_score;
    public EndResultSpawner endresultspawner;
    public bool isCount= false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(GameStart());
    }

    // Update is called once per frame
    void Update()
    {
        //
        if (Serial.ischarge)
        {
            end_score.isPauseByCharge = true;
        }
        else {
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
