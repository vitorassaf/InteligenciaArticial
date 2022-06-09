using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class inteligenciaController : MonoBehaviour
{
    [Header("Sensores")]
    [SerializeField]
    private sensorVision visao;  //sensorVision é uma classe de herança , assim podemos herdar todos os componentes dos scripts.

    [Header("Atuadores")]
    [SerializeField]
    private AtuadorPerseguir pegar; //Vai Executar o Código herdado do outro script.
    [SerializeField]
    private AtuadorPatrulha patrulhar;
    [SerializeField]
    private AtuadorPatrulha.tipoPatrulha tipoDePatrulha;
    
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        
    }

    
    void Update()
    {

       

        if (visao.vendo)
        {
            Debug.Log("Achei Você !! ");

            //Ele vai Disparar a variavel booleana que foi criada no animator na Unity. 
            anim.SetBool("podeAndar", true);

            //Encapsulamento de comandos.
            //Quando ele estiver vendo , ele vai executar os comandos dentro do método Perseguir;

            pegar.Perseguir();
            //pegar.Perseguir(visao.direcao_visao);

            
        }

        else
        {
            /*
            Debug.Log("Vou te Achar !! ");
            anim.SetBool("podeAndar", false);
            */

            //Quando NPC não achar seu Alvo , ele irá Patrulhar.
            patrulhar.Patrulha(tipoDePatrulha);
            anim.SetBool("podeAndar", true);
        }


        
        
        
    }
}
