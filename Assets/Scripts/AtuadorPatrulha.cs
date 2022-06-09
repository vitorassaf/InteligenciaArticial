using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AtuadorPatrulha : MonoBehaviour
{
    
    //Tipo de Variavel criada pelo Usuário
    public enum tipoPatrulha { Quadrado , Circular , Rota };
    //Posição original.
    public Vector3 origem;
    //Vai setar o limite de campo de patrulha do NPC em todos os Quadrantes.
    public float limite;
    private Vector3 limiteSuperior;
    private Vector3 limiteInferior;
    private float tempo = 0;
    public float velocidade;
    private float tempoLimite;
    public float grauDoGiro;
    public float tempoLimiteAndar;
    
    //Lista de GameObjects.
    public List<GameObject> waypoints;
    //public GameObject[] testewaypoints;

    public List<GameObject> waypointsPassados;
    
   

    void Start()
    {
        //No inicio do game , ele vai traçar a posicão original
        origem = transform.position;
        //Vai traçar o limite da parte superior do quadrado.
        limiteSuperior = new Vector3(origem.x - limite, 0, origem.z + limite);
        limiteInferior = new Vector3(origem.x + limite, 0, origem.z - limite);
        tempoLimite = Random.Range(1, tempoLimiteAndar);
        //o Metodo Range faz com que ele utilize os dois parametros colocados
        //por exemplo , o tempo limite vai estar entre 1 e 4.
        

        foreach(var item in GameObject.FindGameObjectsWithTag("wayPointss"))
        {
            //Está pegando cada item e colocando na variavel waypoints.
            waypoints.Add(item);

         
        }

       // testewaypoints = GameObject.FindGameObjectsWithTag("wayPoint");


       
       
       
    }   

   
    void Update()
    {
        //Patrulha();
    }

    public void Patrulha(tipoPatrulha t )
    {
        if(t == tipoPatrulha.Rota)
        {
            rotaPoints();
        }
        
        else
        {
             
        
              
              tempo += Time.deltaTime; 

              if(tempo < 3)
              {
                    transform.Translate(0, 0, velocidade * Time.deltaTime);

              }
              else
              {

                    if(t == tipoPatrulha.Circular)
                    {
                        AreaCircular();
                    }

                    else
                    {
                        #region Verificação do Limite da   Àrea de Patrulha
                        Debug.DrawLine(limiteSuperior, new Vector3(limiteInferior.x, 0, limiteSuperior.z), Color.red);
                        Debug.DrawLine(limiteSuperior, new Vector3(limiteSuperior.x, 0, limiteInferior.z), Color.red);
                        Debug.DrawLine(limiteInferior, new Vector3(limiteSuperior.x, 0, limiteInferior.z), Color.red);
                        Debug.DrawLine(limiteInferior, new Vector3(limiteInferior.x, 0, limiteSuperior.z), Color.red);
                        #endregion
                         AreaQuadrada();//Escolher uma direção para ir.
                         
                    }

                     tempo = 0;
                     tempoLimite = Random.Range(1, tempoLimiteAndar);
                   
              }
        }
        
        
       
    }

    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Precisa do limite , que seria o raio , e a origem da circunferencia.
        Gizmos.DrawSphere(origem, limite);
    }
    */

    private void AreaQuadrada()
    {
        
        if(transform.position.z  > limiteSuperior.z || transform.position.z < limiteInferior.z || transform.position.x > limiteInferior.x || transform.position.x < limiteSuperior.x)

        {
            //Fora da Área de Patrulha.
            //Para achar a direção que NPC vai realizar a Patrulha.
            Vector3 direcaoOrigem = (origem - transform.position).normalized;
            Quaternion virar = Quaternion.LookRotation(direcaoOrigem);
            virar.x = 0;
            virar.z = 0;

            transform.rotation = virar;

            //Codigo exatamente igual para achar a direção do alvo do outro script.

        }
        else
        {
            //Dentro da Área de Patrulha.
            //Vai gira numa direção randomica.
            //Vai multiplicar 0 ou 1 por 360 , com isso , ele vai realizar o giro do NPC.
            float giro = Random.value * grauDoGiro;

            //Vai fazer com que o NPC Gire em graus.
            transform.eulerAngles = new Vector3(0, giro, 0);
        }
    }


    private void AreaCircular()
    {
        //Vai Calcular o Quanto o NPC já Andou , assim se obtem a Distância. O Transform.position é o ponto que o NPC parou , com isso , ele vai traçar uma distancia , no qual , o NPC , parou até a Origem do Circulo.
        float distanciaDaOrigem = Vector3.Distance(origem, transform.position);

        /*
        float distanciaDaOrigem2 = (origem - transform.position).magnitude;

        Vector3 posicao = (origem - transform.position);
        */

        /*
       
        float distanciaDaOrigem3 = Mathf.Sqrt(posicao.x * posicao.x + posicao.y * posicao.y + posicao.z + posicao.z); 
        //Depois ele tira a raiz Quadrada do Cálculo.
        */
        if(distanciaDaOrigem > limite)
        {
            //Fora dos Limites de Patrulha Circular.
            Vector3 direcaoOrigem = (origem - transform.position).normalized;
            Quaternion virar = Quaternion.LookRotation(direcaoOrigem);
            virar.x = 0;
            virar.z = 0;
            transform.rotation = virar;

        }
        else
        {
            //Dentro dos Limites de Patrulha circular.
            float giro = Random.value * grauDoGiro;
        
            //Vai fazer com que o NPC Gire em graus.
            transform.eulerAngles = new Vector3(0, giro, 0);
        }
       
     
    }

     private void rotaPoints()
     {
         //Desenho do Trajeto que o NPC vai Seguir.
         
         //Origem do NPC, ou seja , Posição Inicial.

         #region 
         Vector3 pontoInicial = transform.position;

         int cont = 0;

         while(cont < waypoints.Count)
         {
             //Vai traçar uma linha entre todos os Pontos.
            Debug.DrawLine(pontoInicial, waypoints[cont].transform.position, Color.green);

            pontoInicial = waypoints[cont].transform.position;
            cont++;
         }

         #endregion;
         
         float distancia = Vector3.Distance(transform.position, waypoints[0].transform.position);

         if(distancia > 0.5f)
         {
            Vector3 direcao = waypoints[0].transform.position - transform.position;
            transform.rotation = Quaternion.LookRotation(direcao);
            transform.Translate(0,0, velocidade * Time.deltaTime);
         }
         else
         {
            waypointsPassados.Add(waypoints[0]);
            //Remover os Pontos da Lista, de acordo da Trajetória.
            waypoints.RemoveAt(0);

            if(waypoints.Count == 0)
            {
                //Vai Recomeçar a Trajetória.
                //waypoints = waypointsPassados;


                //A Cada Ponto que ele passar ele vai deletando um por um.
                foreach(var ponto in waypointsPassados)
                {
                    waypoints.Add(ponto);
                }

                waypointsPassados.Clear();
            }
         }
     }
       

}



    


