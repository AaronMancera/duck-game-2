using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    private string nombreEscena;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Salir(){
        Application.Quit();
    }

    public void Play(){
        
        int escena = Random.Range(1,4);

        escena = 1;

        SceneManager.LoadScene(escena);
    }
}
