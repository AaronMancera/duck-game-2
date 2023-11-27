using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPJugador : MonoBehaviour
{
    [Header("Jugador")]
    [SerializeField] GameObject jugador;

    #region LIMITES PANTALLA
    private float limiteEjeX = 9.45f;
    private float limiteEjeYInferior = -7.85f, limiteEjeYSuperior = 3.20f;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float posicionX = jugador.transform.position.x;
        float posicionY = jugador.transform.position.y;

        if (posicionX > limiteEjeX) // Pasa por derecha del todo
        {
            jugador.transform.position = new Vector2(-limiteEjeX, posicionY);
        }
        
        if (posicionX < -limiteEjeX) // Pasa por izquierda del todo
        {
            jugador.transform.position = new Vector2(limiteEjeX, posicionY);

        }
        
        if(posicionY > limiteEjeYSuperior) // Pasa por arriba del todo
        {
            jugador.transform.position = new Vector2(posicionX, limiteEjeYInferior);

        }
        
        if(posicionY < limiteEjeYInferior) // Cae por abajo del todo
        {
            jugador.transform.position = new Vector2(posicionX, limiteEjeYSuperior);
        }
    }
}
