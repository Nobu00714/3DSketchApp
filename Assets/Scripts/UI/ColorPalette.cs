using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPalette : MonoBehaviour
{
    public bool isColor;
    public Color32[] itemColors = new Color32[16];
    void Start()
    {
        if (isColor)
        {
            for (int i = 0; i < 16; i++)
            {
                float hue = (float)i / 16;         // 0.0〜0.9375
                float saturation = 1f;                    // 彩度最大
                float value = 1f;                         // 明度最大
                Color rgb = Color.HSVToRGB(hue, saturation, value);
                itemColors[i] = new Color32(
                    (byte)(rgb.r * 255),
                    (byte)(rgb.g * 255),
                    (byte)(rgb.b * 255),
                    255 // alpha 固定
                );
            }
        }
    }
}
