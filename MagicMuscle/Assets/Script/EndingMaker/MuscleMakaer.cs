using UnityEngine;
using TMPro;
public class MuscleMakaer : MonoBehaviour
{
    public Ranking rankig;
    public EndResultSpawner endresult;
    public float Distance = 20;
    public GameObject gameobject;
    public GameObject ScoreText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
            int Ranking = rankig.rankingSize - i;
            SpriteRenderer sr = gameobject.GetComponent<SpriteRenderer>();
            float prefabHeight = sr.bounds.size.y;

            // 底辺がY=0に来るように調整
            Vector3 spawnPos = new Vector3(0, prefabHeight / 2f, 0);

            Instantiate(gameobject, this.transform.position + new Vector3(Distance * i, prefabHeight / 2f, 0), Quaternion.identity);
            Instantiate(ScoreText, this.transform.position + new Vector3(Distance * i, prefabHeight / 2f, 0), Quaternion.identity);
            TextMeshPro tm = ScoreText.GetComponent<TextMeshPro>();
            //tm.text = rankig.rankingValue[i].ToString();

            try
            {
                //tm.SetText(rankig.rankingValue[i].ToString());

            }
            catch
            {

            }


        }
    }

}
