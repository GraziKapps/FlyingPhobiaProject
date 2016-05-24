using UnityEngine;
using System.Collections;

public class EstacionarAviao : MonoBehaviour {

    public float VelocidadeAceleracao;
    public float VelocidadeAtual;
    public float VelocidadeMax;
    public float tempoParar;
    public float VelocidadeDesaceleracao;
    public float contador;

    float rotacaoY;
    
    bool acelerar;
    bool desacelerar;
    bool check;



    // Use this for initialization
    void Start () {
        acelerar = false;
        desacelerar = false;
        check = false;
        tempoParar = 0;
        rotacaoY = 0;
        contador = 0;

    }
	
	// Update is called once per frame
	void Update () {

        contador += Time.deltaTime;//comaça a contar o tempo para iniciar a parada

        if (contador >= 10) //depois de 1 minuto será iniciado o procedimento de parada
        {       

            //começar aceleração
            if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0 && acelerar == false && desacelerar == false)
            {
                acelerar = true;
                VelocidadeAtual = VelocidadeAtual + VelocidadeAceleracao * Time.deltaTime;
            }

            //continua acelerando
            if (acelerar == true && desacelerar == false)
            {
                VelocidadeAtual = VelocidadeAtual + VelocidadeAceleracao * Time.deltaTime;
                //tem que ter o translate para o aviao acelerar
                transform.Translate(0, 0, -VelocidadeAtual);
            }

            // verifica a velocidade  -- não deixa ficar muito rápido
            if (VelocidadeAtual > VelocidadeMax && desacelerar == false)
            {
                VelocidadeAtual = VelocidadeMax;
                //transform.Translate(VelocidadeAtual / 2, 0, VelocidadeAtual / 2);

                tempoParar += Time.deltaTime; //começa a contar o tempo
            }

            if (tempoParar >= 17 && VelocidadeAtual == VelocidadeMax) //apos 15 segundos
            {
                desacelerar = true;

                VelocidadeAtual = VelocidadeAtual - VelocidadeDesaceleracao * Time.deltaTime;
                transform.Translate(0, 0, -VelocidadeAtual);

            }

            if (desacelerar == true)
            {
                VelocidadeAtual = VelocidadeAtual - VelocidadeDesaceleracao * Time.deltaTime;
                transform.Translate(0, 0, -VelocidadeAtual);
            } 
                
            if (VelocidadeAtual <= 0 && desacelerar == true)
            {
                VelocidadeAtual = 0;
          

            }

        }

        

    }

   
}
