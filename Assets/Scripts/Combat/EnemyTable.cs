using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[CreateAssetMenu(fileName = "EnemyData", menuName = "EnemyData")]
public class EnemyTable : ScriptableObject
{
    [SerializeField] private List<EnemyData> enemies;
    private float _totalWeight;
    [System.NonSerialized] private bool isInitialized = false;

    private void Initialize()
    {
        if (!isInitialized)
        {
            _totalWeight = enemies.Sum(enemies=>enemies.weight);
            isInitialized = true;
        }
    }

    public EnemyData GetRandomEnemy()
    {
        Initialize();
        float diceRoll = Random.Range(0f, _totalWeight);

        foreach(var enemy in enemies)
        {
            if(enemy.weight >= diceRoll)
            {
                return enemy;
            }

            diceRoll -= enemy.weight;
        }

        Debug.Log("something went sideways");
        return null;
    }

}

[System.Serializable]
public class EnemyData
{
    public float enemyStrengthStat;
    public float enemyDefStat;
    public float enemyTotalHP;
    public float enemyCurrentHP;
    public string enemyName;
    public Sprite enemySprite;
    public float weight;
}
