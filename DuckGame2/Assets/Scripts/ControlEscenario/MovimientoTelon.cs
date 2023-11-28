using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoTelon : MonoBehaviour
{
    public Transform telonIzq;
    public Transform telonDcha;
    private float velocidad = 2f;
    private float limiteDcha = 14f;
    private float limiteIzq = -14f;
    public ControlDeJuego controlDeJuego;
    public bool telonAbierto;
    private Vector3 PosicionInicialTelonIzq;
    private Vector3 PosicionInicialTelonDcha;


    // Start is called before the first frame update
    void Start()
    {
        PosicionInicialTelonIzq = telonIzq.position;
        PosicionInicialTelonDcha = telonDcha.position;
        StartCoroutine(AbrirTelon());
    }

    // Update is called once per frame
    void Update()
    {
        //if (!controlDeJuego.finDeRonda && !telonAbierto && !controlDeJuego.finDePartida)
        //{
        //    StartCoroutine(AbrirTelon());
        //}
    }

    public IEnumerator AbrirTelon()
    {
       yield return new WaitForSecondsRealtime(1f); // me gustaria que las plataformas no se movieran mientras se abre el telon pero el unsacled usado en cerrar telon aqui va mal la velocidad se pone progresiva
       while (!telonAbierto)
        {
            //Debug.Log("abrir telon");
            telonIzq.Translate(Vector3.left * velocidad * Time.deltaTime);
            telonDcha.Translate(Vector3.right * velocidad * Time.deltaTime);
            // Mover hacia la izquierda hasta el límite izquierdo
            if (telonIzq.position.x <= limiteIzq)
            {
                telonIzq.position = new Vector3(limiteIzq, telonIzq.position.y, telonIzq.position.z);
                telonAbierto = true;
            }
            // Mover hacia la derecha hasta el límite derecho
            if (telonDcha.position.x >= limiteDcha)
            {
                telonDcha.position = new Vector3(limiteDcha, telonDcha.position.y, telonDcha.position.z);
                telonAbierto = true;
            } 
            yield return null;
        }

    }
    public void CerrarTelon()
    {
        //Debug.Log("cerrar telon");
        telonIzq.Translate(Vector3.right * velocidad * Time.unscaledDeltaTime);
        telonDcha.Translate(Vector3.left * velocidad * Time.unscaledDeltaTime);
        // Mover hacia la izquierda hasta el límite izquierdo
        if (telonIzq.position.x > PosicionInicialTelonIzq.x )
        {
            telonIzq.position = PosicionInicialTelonIzq;
            telonAbierto = false;
        }
        // Mover hacia la derecha hasta el límite derecho
        if (telonDcha.position.x < PosicionInicialTelonDcha.x)
        {
            telonDcha.position = PosicionInicialTelonDcha;
            telonAbierto = false;
        }
    }
}
