using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDelMenu : MonoBehaviour
{
    [Header("ControlDelJuego")]
    public bool pausa;
    [SerializeField] GameObject panelPausa;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Pausa();
        }
    }

    public void Pausa()
    {
        pausa = !pausa;

        if (pausa)
        {
            Time.timeScale = 0;
            panelPausa.SetActive(pausa);
        }
        else
        {
            Time.timeScale = 1;
            panelPausa.SetActive(pausa);

        }
    }
}
