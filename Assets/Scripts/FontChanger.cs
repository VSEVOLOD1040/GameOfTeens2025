using UnityEngine;
using TMPro;
using UnityEditor;

public class TMPFontChanger : MonoBehaviour
{
    
    void Start()
    {
        TMP_FontAsset newFontAsset = Resources.Load<TMP_FontAsset>("Jersey15-Regular SDF");

        TMP_Text[] texts = FindObjectsOfType<TMP_Text>(true);

        int count = 0;
        foreach (TMP_Text txt in texts)
        {
            txt.font = newFontAsset;
            count++;
        }
        
    }


}
