using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{

    [SerializeField] AudioClip cajaChocarConSuelo;
    [SerializeField] AudioClip cajaChocarConJugador;
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si la colisión es con el jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<ControlJugador>().EfectoNegativo("SoltarArmas");
            //Animacion caja y eliminar caja.
            //Destroy(gameObject);
            StartCoroutine(Destruir());

            AudioManager.instanceAudioManager.PlaySFX(cajaChocarConJugador);

        }

        // Destruir la caja solo si no es una colisión con el jugador
        if (!collision.gameObject.CompareTag("Player"))
        {
            AudioManager.instanceAudioManager.PlaySFX(cajaChocarConSuelo);

            //Destroy(gameObject);//Animacion caja se rompe
            StartCoroutine(Destruir());
        }
    }
    private IEnumerator Destruir()
    {
        gameObject.GetComponent<Collider2D>().enabled=false;
        gameObject.GetComponent<Rigidbody2D>().Sleep();
        //Animacion to guapa
        yield return new WaitForSeconds(5);
        Destroy(gameObject);   

    }

}
