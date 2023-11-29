using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;

public class VolumeManager : MonoBehaviour
{
    public Slider VolumeSlider;

    
    // Start is called before the first frame update
    void Start()
    {
        //volume.TryGetSettings(out exposureVol);
        if (PlayerPrefs.GetFloat("Volumen") != 0)
        {

            VolumeSlider.value = PlayerPrefs.GetFloat("Volumen");
            //exposureVol.keyValue.value = PlayerPrefs.GetFloat("Volumen");

        }
        AjustarVolumen(VolumeSlider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AjustarVolumen(float value)
    {

        //VolumeSlider.value = value;
        PlayerPrefs.SetFloat("Volumen", value);

        //Debug.Log(value);
        Debug.Log(PlayerPrefs.GetFloat("Volumen"));

    }
}
