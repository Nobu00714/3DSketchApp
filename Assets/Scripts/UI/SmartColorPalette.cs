using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class SmartColorPalette : MonoBehaviour // MonoBehaviourである必要がなければ static class にしても良い
{
    public void GetHueCircleColors(Color32 grayscale, int divisions = 16)
    {
        Color baseColor = grayscale;
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);

        // 彩度が0ならグレースケールなので、彩度は1にして色をつける
        s = 1f;

        Color32[] result = new Color32[divisions];
        float alpha = grayscale.a;

        for (int i = 0; i < divisions; i++)
        {
            float hue = (float)i / divisions;
            Color rgb = Color.HSVToRGB(hue, s, v);
            result[i] = new Color32(
                (byte)(rgb.r * 255),
                (byte)(rgb.g * 255),
                (byte)(rgb.b * 255),
                (byte)alpha
            );
        }
        this.GetComponent<ColorPalette>().itemColors = result;
    }
    public void GetBrightnessVariations(Color32 baseColor32, int levels = 16)
    {
        Color baseColor = baseColor32;
        Color.RGBToHSV(baseColor, out float h, out float s, out float v);

        Color32[] result = new Color32[levels];
        byte alpha = baseColor32.a;

        for (int i = 0; i < levels; i++)
        {
            float brightness = (float)i / (levels - 1); // 0〜1の明度
            Color adjustedColor = Color.HSVToRGB(h, s, brightness);
            result[i] = new Color32(
                (byte)(adjustedColor.r * 255),
                (byte)(adjustedColor.g * 255),
                (byte)(adjustedColor.b * 255),
                alpha
            );
        }
        this.GetComponent<ColorPalette>().itemColors = result;
    }
}