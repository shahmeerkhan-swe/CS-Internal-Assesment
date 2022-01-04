using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour

{
    public static HealthBarUI instance { get; private set; }
    public Image healthmask;
    float originalSize;

     void Awake()
    {
        instance = this;    
    }



    // Start is called before the first frame update
    void Start()
    {
        originalSize = healthmask.rectTransform.rect.width;
    }

    // Update is called once per frame
    public void SetValue(float value)
    {
        healthmask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);
    }
}
