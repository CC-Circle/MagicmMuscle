using UnityEngine;

[System.Serializable]
public class SpawnEvent
{
    public float time;
    public SpawnPatternType pattern;
    public GameObject enemyPrefab;
    public Vector3 basePosition;
    public string animationTrigger;
}