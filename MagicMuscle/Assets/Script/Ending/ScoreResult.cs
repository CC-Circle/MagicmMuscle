//using UnityEngine;

//[System.Serializable]
//public class ScoreResult
//{
//    public int minScore;         // このスコア以上なら表示
//    public Sprite image;         // 表示するイラスト
//    public string message;       // 表示するテキスト
//    public AudioClip audioclip;
//    public bool isSound = true;

//}
using UnityEngine;

[System.Serializable]
public class ScoreResult
{
    public int minScore;          // このスコア以上なら表示
    public GameObject prefab;     // 生成するプレハブ
    public AudioClip audioclip;
    public bool isSound = true;
}
