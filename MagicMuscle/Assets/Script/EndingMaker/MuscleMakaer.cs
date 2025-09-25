using UnityEngine;
using TMPro;
using System;
using System.Collections.Generic;

public class MuscleMakaer : MonoBehaviour
{
    public Ranking rankig;
    public EndResultSpawner endresult;
    public float Distance = 20;
    public List<GameObject> gameobject;
    public GameObject ScoreText;
    public GameObject RankText;
    public GameObject spotlight;
    public GameObject peopleText;
    public CameraRankingMove cameramove;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int YourMaxRank;
    public Vector3 TextVectorFix,TexRankingVectorFix,TexPeople;


    private GameObject NumberOne_Light=null;

    private GameObject currentPeople;

    private int maxpeople;
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

            if (rankig.rankingValue[Ranking] > 0)
            {
                maxpeople++;

            }
            if (Score.score == rankig.rankingValue[Ranking])
            {
                YourMaxRank = Ranking;
            }
        }
        for (int i = 0; i < rankig.rankingSize; i++)
        {
            //Debug.Log("cnt" + i);
            int Ranking = rankig.rankingSize -1 - i;
            SpriteRenderer sr = gameobject[endresult.maxScoreGameObjectInputScore(rankig.rankingValue[Ranking])].GetComponent<SpriteRenderer>();
            float prefabHeight = sr.bounds.size.y;

            // 底辺がY=0に来るように調整
            Vector3 spawnPos = new Vector3(0, prefabHeight / 2f, 0);
            //Debug.Log("cnt" + i);

            //オブジェクトの生成
            Instantiate(gameobject[endresult.maxScoreGameObjectInputScore(rankig.rankingValue[Ranking])].GetComponent<SpriteRenderer>(), this.transform.position + new Vector3(Distance * i, prefabHeight / 2f, 0), Quaternion.identity);
            GameObject textObj　= Instantiate(ScoreText, this.transform.position + new Vector3(Distance * i,0, 0)+TextVectorFix, Quaternion.identity);

            //スコアテキスト
            TextMeshPro tm = textObj.GetComponent<TextMeshPro>();
            tm.text = rankig.rankingValue[Ranking].ToString();

            //ランキングテキスト
            GameObject rankObj = Instantiate(RankText, this.transform.position + new Vector3(Distance * i, 0, 0) + TexRankingVectorFix, Quaternion.identity);
            TextMeshPro textmesh = rankObj.GetComponent<TextMeshPro>();
            textmesh.text = rankig.rankingSize-i + "位";
            if(rankig.rankingSize - i == 1)
            {
                textmesh.color = Color.yellow;
            }
            if (rankig.rankingSize - i == 2)
            {
                textmesh.color = Color.silver;
            }
            if (rankig.rankingSize - i == 3)
            {
                textmesh.color = Color.sandyBrown;
            }
            //何人中
            GameObject peopleObj = Instantiate(peopleText, this.transform.position + new Vector3(Distance * i, 0, 0) + TexPeople, Quaternion.identity);
            TextMeshPro textpeople = peopleObj.GetComponent<TextMeshPro>();
            textpeople.text = maxpeople + "人中";
            peopleObj.SetActive(false);

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
                currentPeople = peopleObj;
                NumberOne_Light = LightObject;
                //light.enabled = true;
                CameraRankingMove.target = this.transform.position + new Vector3(Distance * i, 0, 0);
            }
            //tm.text = i.ToString(); 
            //Debug.Log(i+":val"+rankig.rankingValue[i]);
        }
    }

    public void ShowPoepleNumber()
    {
        currentPeople.SetActive(true);
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
