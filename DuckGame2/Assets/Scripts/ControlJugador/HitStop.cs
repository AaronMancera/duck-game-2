using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStop : MonoBehaviour
{
    bool Activado;
    public void HitStopMecanica()
    {
        Activado = !Activado;
        if (Activado)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;

        }
    }
    
}
