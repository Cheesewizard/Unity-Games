using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int maxHealth;
    public int currentHealth;

    private PlayerStats thePlayerStats;

    public int expToGive;

    public string enemyQuestName;
    private QuestManager theQM;

    private SFXManager sfxMan;


   

    // Use this for initialization
    void Start()
    {
        theQM = FindObjectOfType<QuestManager>();
        currentHealth = maxHealth;
        thePlayerStats = FindObjectOfType<PlayerStats>();
        sfxMan = FindObjectOfType<SFXManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0)
        {
            theQM.enemysKilled = enemyQuestName;
            sfxMan.slimeDie.Play();
            Destroy(gameObject);

            thePlayerStats.AddExperience(expToGive);
        }
    }

    public void HurtEnemy(int damageToGive)
    {
        currentHealth -= damageToGive;


    }


    public void SetMaxHealth()
    {
        currentHealth = maxHealth;
    }
}
