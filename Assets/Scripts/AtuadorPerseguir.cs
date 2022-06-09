using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtuadorPerseguir : MonoBehaviour
{

    public Transform alvo;
    public float velocidade;
    
   
    void Start()
    {
        
    }

 
    void Update()
    {
        //Perseguir();
    }

    //public void Perseguir(Vector3 direcao)
    public void Perseguir()
    {
        //Rotação de Objetos.

        Quaternion girar = Quaternion.LookRotation(alvo.position - transform.position);

        //Outro jeito de se fazer o mesmo comando de Rotação.
        //Quaternion girar = Quaternion.LookRotation(direcao);

        //O Intuito é Girar o Personagem , então é necessário apenas do eixo Y.
        girar.x = 0;
        girar.z = 0;

        //Calculo do Giro para o NPC
        transform.rotation = girar;


        transform.Translate(new Vector3(0,0,1) * velocidade * Time.deltaTime);

        //Outra forma de calcular 
        //transform.Translate(Vector3.forward * velocidade * Time.deltaTime);
    }
}
