
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusScript : MonoBehaviour
{
    public PlayerScript player;
    public PlayerController playerController;
    public AudioSource audioSource;
    public AudioClip clip;
    public WeaponController weaponController;

    public GameObject Shield;
    public BoxCollider2D boxCollider;

    public UIManager ui;
    void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    public void ActivateRandomBonus()
    {
        audioSource.clip = clip;
        audioSource.Play();
        List<System.Func<IEnumerator>> bonusCoroutines = new List<System.Func<IEnumerator>>()
        {
            Heal,
            SpeedUp,
            ShootSpeedUp,
            EnableShield,
            Rockets,
        };

        int randomIndex = Random.Range(0, bonusCoroutines.Count);
        StartCoroutine(bonusCoroutines[randomIndex]());
    }

    IEnumerator Heal()
    {
        
        player.health = player.max_health;
        player.UpdateUI();
        ui.ShowBoostPanel("Heal", 1);
        yield return new WaitForSeconds(1);
    }
    IEnumerator SpeedUp()
    {
        playerController.moveSpeed *= 2;
        ui.ShowBoostPanel("Speed up", 15);
        yield return new WaitForSeconds(15);
        playerController.moveSpeed /= 2;
    }
    IEnumerator ShootSpeedUp()
    {
        weaponController.attackMode = 1;
        weaponController.bonus_shootrate_up = true;
        ui.ShowBoostPanel("Very fast shooting mode", 10);
        yield return new WaitForSeconds(10);
        weaponController.bonus_shootrate_up = false;

    }
    IEnumerator EnableShield()
    {
        ui.ShowBoostPanel("Shield", 10);

        boxCollider.enabled = false;
        Shield.SetActive(true);
        yield return new WaitForSeconds(10);
        boxCollider.enabled = true;
        Shield.SetActive(false);

    }
    IEnumerator Rockets()
    {
        ui.ShowBoostPanel("Fast rocket launch", 4);

        for (int i = 0; i <= 5; i++)
        {
            yield return new WaitForSeconds(0.8f);
            weaponController.LaunchRocket();
        }
        yield return new WaitForSeconds(2);

    }
}
