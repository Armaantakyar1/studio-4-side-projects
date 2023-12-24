using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class QualityOptions : MonoBehaviour
{
    private int currentQualityLevel;

    void Start()
    {
        currentQualityLevel = QualitySettings.GetQualityLevel();
    }

    public void SetTextureQuality(int qualityLevel)
    {
        QualitySettings.SetQualityLevel(qualityLevel);
        currentQualityLevel = qualityLevel;
    }

    public void IncreaseQuality()
    {
        if (currentQualityLevel < QualitySettings.names.Length - 1)
        {
            currentQualityLevel++;
            SetTextureQuality(currentQualityLevel);
        }
    }

    public void DecreaseQuality()
    {
        if (currentQualityLevel > 0)
        {
            currentQualityLevel--;
            SetTextureQuality(currentQualityLevel);
        }
    }
}
