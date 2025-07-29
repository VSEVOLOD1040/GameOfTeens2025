using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public Image Healthbar;
    public Image DamageOverlay; 
    public float fadeDuration = 0.5f;
    public float alpfa_ = 0.2f;
    private Coroutine damageEffectCoroutine;

    public TextMeshProUGUI LT_time;
    public TextMeshProUGUI LT_score;
    public TextMeshProUGUI LT_kills;
    public Image LoseBackground;
    public GameObject LoseInterface;

    public TextMeshProUGUI WaveIndex;
    public TextMeshProUGUI DestroyedEnemies;
    void Start()
    {
        
    }


    public void UpdateHealthBar(float fillamount)
    {

        Healthbar.fillAmount = fillamount;
        damageEffectCoroutine = StartCoroutine(ShowEffect(Color.red, fadeDuration, 0.1f));
    }


    public IEnumerator ShowEffect(Color color, float fadeDuration, float startAlpha)
    {


        float elapsed = 0f;
        DamageOverlay.color = new Color(color.r, color.g, color.b, startAlpha);

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, 0f, elapsed / fadeDuration);
            DamageOverlay.color = new Color(color.r, color.g, color.b, alpha);
            Debug.Log("   alpha = " + alpha);
            yield return null;
        }

        DamageOverlay.color = new Color(color.r, color.g, color.b, 0f);

    }

    public void PlayEffectFromAnotherScript(Color color, float duration, float startAlpha)
    {
        StartCoroutine(ShowEffect(color, duration, startAlpha));
    }


    public void SetTextWaveIndex(string value)
    {
        WaveIndex.text = value;
    }

    public void SetTextDestroyedEnemies(string value)
    {
        DestroyedEnemies.text = value;
    }
    public void EndGame(float Time, float Score, float Kills)
    {

        LT_score.text = $"Score: {Score.ToString()}/{PlayerPrefs.GetFloat("BestScore",0)}";
        LT_kills.text = $"Enemies destroyed: {Kills}";
        LT_time.text = $"Time: {FormatTime(Time)}";
        StartCoroutine(FadeIn(LoseBackground, 1.5f));
        LoseInterface.SetActive(true);

    }

    public string FormatTime(float totalSeconds)
    {
        float minutes = totalSeconds / 60;
        float seconds = totalSeconds % 60;
        return $"{(int)minutes}m {(int)seconds}s";
    }
    IEnumerator FadeIn(Image img, float time)
    {
        float elapsed = 0f;
        Color color = img.color;

        while (elapsed < time)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / time);
            color.a = alpha;
            img.color = color;
            yield return null;
        }

        color.a = 1f;
        img.color = color;
    }

}
