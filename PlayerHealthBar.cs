using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthBar : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI TextPro;
    public RectTransform rectTransform;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        
        Vector2 newsize = new Vector2(health*2, rectTransform.sizeDelta.y);
        rectTransform.sizeDelta = newsize;
        
        TextPro.text = slider.value + " / " + slider.maxValue + "  HP";
    }

    public void SetHealth(int health)
    {
        slider.value = health;

        TextPro.text = slider.value + " / " + slider.maxValue + "  HP";
    }
}

