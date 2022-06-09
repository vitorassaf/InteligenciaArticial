using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sensorVision : MonoBehaviour
{

    public GameObject alvo;
    public bool vendo;
    public float campo_visao;
    public float limite_visao;
    public Vector3 direcao;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vendo = false;
        //Achar a direção do player, posicão do meu alvo menos a posição do player.
        direcao = (alvo.transform.position - transform.position).normalized;

        //Angulo de visão do NPC
        float angulo_alvo = Vector3.Angle(transform.forward, direcao);

        //a variável campo de visão terá um angulo de visão que vamos definir
        if (angulo_alvo <= campo_visao)
        {
            //significa que o algo está no campo de visão

            //está no limite e não está escondido
            RaycastHit retorno_visao;
            Physics.Raycast(transform.position, direcao, out retorno_visao, limite_visao);

            if(retorno_visao.collider.tag == "PlayerCena")
            {
                vendo = true;

            }

            //Se a visão estiver no limite e o player estiver atrás da parede , o NPC não está vendo,
            // ou seja , a variável boleana estará com valor falso.
            if(retorno_visao.collider.tag == "ParedeColisão")
            {
                vendo = false;

            }




        }
    }

    
}
