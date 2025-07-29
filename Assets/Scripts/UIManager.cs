using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    public AudioClip lose_sound;

    public GameObject boostPanelPrefab;
    public Transform boostPanelParent;
    public GameObject Press123Text;
    void Start()
    {


    }


    public void UpdateHealthBar(float fillamount)
    {
        if (fillamount < Healthbar.fillAmount)
        {
            damageEffectCoroutine = StartCoroutine(ShowEffect(Color.red, fadeDuration, 0.1f));
        }
        else
        {
            damageEffectCoroutine = StartCoroutine(ShowEffect(Color.green, fadeDuration, 0.1f));

        }
        if (fillamount < 0.3)
        {
            Healthbar.color = Color.red;
        }else if (fillamount < 0.6)
        {
            Healthbar.color = Color.yellow;
        }
        else
        {
            Healthbar.color = Color.green;

        }


        Healthbar.fillAmount = fillamount;
        
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
    public void EndGame(float Time, int Score, int Kills)
    {
        Press123Text.SetActive(false);
        if (Score > PlayerPrefs.GetInt("BestScore", 0))
        { 
            PlayerPrefs.SetInt("BestScore", Score);
        }

        gameObject.GetComponent<AudioSource>().clip = lose_sound;
        gameObject.GetComponent<AudioSource>().Play();

        LT_score.text = $"Score: {Score.ToString()}/{PlayerPrefs.GetInt("BestScore",0)}";
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
    public void ToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void ShowBoostPanel(string boostName, float duration)
    {
        GameObject panel = Instantiate(boostPanelPrefab, boostPanelParent);
        Image fillImage = panel.transform.Find("TimeLeft").GetComponent<Image>();
        TextMeshProUGUI text = panel.transform.Find("Name").GetComponentInChildren<TextMeshProUGUI>();

        text.text = boostName;

        StartCoroutine(UpdateBoostPanel(panel, fillImage, duration));
    }

    private IEnumerator UpdateBoostPanel(GameObject panel, Image fillImage, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;

            fillImage.fillAmount = Mathf.Clamp01(1f - (timer / duration));
            yield return null;
        }

        Destroy(panel);
    }
}
