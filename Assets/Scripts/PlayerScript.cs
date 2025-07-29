using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{

    public float health;
    public float max_health;
    public UIManager ui;
    public GameObject EnemiesParentObject;

    public float play_time;
    public void TakeDamage(float damage)
    {
        if (health-damage > 0)
        {
            health -= damage;
        }
        else
        {
            health = 0;
            Destroy(GameObject.Find("Scripts"));
            Destroy(EnemiesParentObject);
            Lose();
        }
        UpdateUI();
    }

    void Lose()
    {
        int score = (int)play_time * 5 + Data.DestroyedEnemies * 100;

        ui.EndGame(play_time, score, Data.DestroyedEnemies);
    }
    private void Start()
    {
        EnemiesParentObject = GameObject.Find("Enemies");
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }
    public void UpdateUI()
    {
        ui.UpdateHealthBar(health/max_health);
    }
    private void Update()
    {
        play_time += Time.deltaTime;
    }
}
