using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Rendering.PostProcessing;

public class BrilloManager : MonoBehaviour
{
    public Slider brightnessSlider;

    public PostProcessProfile brightness;
    public PostProcessLayer layer;

    AutoExposure exposure;
    void Start()
    {


        brightness.TryGetSettings(out exposure);
        if (PlayerPrefs.GetFloat("Brillo") != 0)
        {
            //Debug.Log("holaaaa");

            brightnessSlider.value = PlayerPrefs.GetFloat("Brillo");
            exposure.keyValue.value = PlayerPrefs.GetFloat("Brillo");

        }
        AdjustaBrillo(brightnessSlider.value);

    }
    private void Update()
    {

    }

    public void AdjustaBrillo(float value)
    {

        exposure.keyValue.value = value;
        PlayerPrefs.SetFloat("Brillo", value);

        //Debug.Log(value);
        Debug.Log(PlayerPrefs.GetFloat("Brillo"));

    }

}
