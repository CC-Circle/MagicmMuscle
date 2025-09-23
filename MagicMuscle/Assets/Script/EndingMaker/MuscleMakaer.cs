using UnityEngine;
using TMPro;
using System;

public class MuscleMakaer : MonoBehaviour
{
    public Ranking rankig;
    public EndResultSpawner endresult;
    public float Distance = 20;
    public GameObject gameobject;
    public GameObject ScoreText;
    public GameObject RankText;
    public GameObject spotlight;
    public CameraRankingMove cameramove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int YourMaxRank;
    public Vector3 TextVectorFix,TexRankingVectorFix;

    private GameObject NumberOne_Light=null;
    void Start()
    {
        CreateMuscles();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void CreateMuscles()
    {
        for (int i = 0; i < rankig.rankingSize; i++)
        {
            int Ranking = rankig.rankingSize - 1 - i;
            if (Score.score == rankig.rankingValue[Ranking])
            {
                YourMaxRank = Ranking;
            }
        }
            for (int i = 0; i < rankig.rankingSize; i++)
        {
            //Debug.Log("cnt" + i);
            int Ranking = rankig.rankingSize -1 - i;
            SpriteRenderer sr = gameobject.GetComponent<SpriteRenderer>();
            float prefabHeight = sr.bounds.size.y;

            // 底辺がY=0に来るように調整
            Vector3 spawnPos = new Vector3(0, prefabHeight / 2f, 0);
            //Debug.Log("cnt" + i);

            //オブジェクトの生成
            Instantiate(gameobject, this.transform.position + new Vector3(Distance * i, prefabHeight / 2f, 0), Quaternion.identity);
            GameObject textObj　= Instantiate(ScoreText, this.transform.position + new Vector3(Distance * i,0, 0)+TextVectorFix, Quaternion.identity);

            //スコアテキスト
            TextMeshPro tm = textObj.GetComponent<TextMeshPro>();
            tm.text = rankig.rankingValue[Ranking].ToString();

            //ランキングテキスト
            GameObject rankObj = Instantiate(RankText, this.transform.position + new Vector3(Distance * i, 0, 0) + TexRankingVectorFix, Quaternion.identity);
            TextMeshPro textmesh = rankObj.GetComponent<TextMeshPro>();
            textmesh.text = rankig.rankingSize-i + "位";

            //ライトの生成
            // YとZはプレハブの初期値を利用する例
            Vector3 originalPos = spotlight.transform.position;
            Vector3 spawnLigntPos = new Vector3(Distance * i, originalPos.y, originalPos.z);

            // 生成（回転はプレハブのデフォルトを使う）
            GameObject LightObject = Instantiate(spotlight, spawnLigntPos, spotlight.transform.rotation);
            Light light = LightObject.GetComponent<Light>();
            light.enabled = false;
            if (Score.score == rankig.rankingValue[Ranking] && YourMaxRank == Ranking)
            {
                NumberOne_Light = LightObject;
                //light.enabled = true;
                CameraRankingMove.target = this.transform.position + new Vector3(Distance * i, 0, 0);
            }
            //tm.text = i.ToString(); 
            //Debug.Log(i+":val"+rankig.rankingValue[i]);
        }
    }

    public void LightOn()
    {
        if (NumberOne_Light != null)
        {
            Light light = NumberOne_Light.GetComponent<Light>();
            light.enabled = true;
        }
        
    }
}
