using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CombatManager : MonoBehaviour
{
    public bool inCombat = false;
    public EnemyTable enemyTable;
    public Slider playerHpBar;
    public Slider enemyHpBar;
    public InventoryManager invMan;
    public GameObject enemyGO;
    public GameObject playerGO;
    private float enemyAttackInterval = 1.0f;
    private float playerAttackInterval = 1.0f;
    private float enemyHp;
    private float enemyMaxHp;
    private string enemyName;
    private float enemyStrength;
    private float enemyDefense;
    private SpriteRenderer spriteRenderer;
    private Vector3 scaleChange;
    public TMP_Text enemyText;
    public TMP_Text playerhptext;
    public TMP_Text enemyhptext;



    void Start()
    {
        spriteRenderer = enemyGO.GetComponent<SpriteRenderer>();
        InvokeRepeating("PlayerCombat", playerAttackInterval, playerAttackInterval);
        InvokeRepeating("EnemyCombat", enemyAttackInterval, enemyAttackInterval);
    }

    void Update()
    {
        playerHpBar.maxValue = DataHandler.playerMaxHealthPoints;
        enemyHpBar.maxValue = enemyMaxHp;
        playerHpBar.value = DataHandler.playerHealthPoints;
        enemyHpBar.value = enemyHp;
        enemyText.text = enemyName;
        playerhptext.text = DataHandler.playerHealthPoints.ToString("#");
        enemyhptext.text = enemyHp.ToString("#");
    }

    private void PlayerCombat()
    {
        if(inCombat == true && DataHandler.playerHealthPoints >= 0)
        {
            enemyHp -= DataHandler.playerStrengthStat;
        } else if (DataHandler.playerHealthPoints <= 0){
            inCombat = false;
            DataHandler.playerHealthPoints = DataHandler.playerMaxHealthPoints;
        }


    }

    private void EnemyCombat()
    {
        if(inCombat == true && enemyHp >= 0)
        {
            DataHandler.playerHealthPoints -= enemyStrength;
        } else if (enemyHp <= 0)
        {
            invMan.AddNewItem();
            inCombat = false;
            BeginCombat();
        }
    }

    public void BeginCombat()
    {
        inCombat = true;
        EnemyData enemy = enemyTable.GetRandomEnemy();
        enemyMaxHp = enemy.enemyTotalHP;
        enemyHp = enemy.enemyTotalHP;
        enemyStrength = enemy.enemyStrengthStat;
        enemyDefense = enemy.enemyDefStat;
        enemyName = enemy.enemyName;
        spriteRenderer.sprite = enemy.enemySprite;
    }
}
