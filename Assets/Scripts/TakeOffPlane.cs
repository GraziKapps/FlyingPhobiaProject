using UnityEngine;
using System.Collections;

public class TakeOffPlane : MonoBehaviour {

    // controla as velocidades do avião - aceleração
    public float VelocidadeAceleracao;
    public float VelocidadeAtual;
    public float VelocidadeDesaceleracao;
    public float VelocidadeMax;

    // controla as rotações do aviao
    float RotacaoDecolagem;

    // controle
    public bool decolar;
    public bool acelerar;
    public float anguloX;
    public float anguloY;
    // public float anguloZ;
    // public int check;


    // Use this for initialization
    void Start ()
    {
        decolar = false;
        acelerar = false;

        RotacaoDecolagem = 1;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
        /** ACELERAÇÃO **/
        // mexer o mouse -- o aviao será acelerado 
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0 && acelerar == false)
        {
            acelerar = true;
            VelocidadeAtual = VelocidadeAtual + VelocidadeAceleracao * Time.deltaTime;
        }

        //continua acelerando para a decolagem
        if (acelerar == true)
        {
            VelocidadeAtual = VelocidadeAtual + VelocidadeAceleracao * Time.deltaTime;
        }

        // verifica a velocidade  -- não deixa ficar muito rápido
        if (VelocidadeAtual > VelocidadeMax)
        {
            VelocidadeAtual = VelocidadeMax;
            anguloX = transform.position.x;
            anguloY = transform.position.y;
            //anguloZ = transform.position.z;
        }

        // verica a velocidade e decola
        if (VelocidadeAtual >= (VelocidadeMax / 3) && decolar == false && acelerar == true)
        {
            transform.Rotate(RotacaoDecolagem, 0, RotacaoDecolagem);
            RotacaoDecolagem = RotacaoDecolagem + RotacaoDecolagem / 2;
        }

        if (RotacaoDecolagem >= 4 && RotacaoDecolagem <= 6)
        {
            decolar = true;
        }

        // quando o avião sumir do campo de visão do usuário, ele é destruído
        if (anguloX > 490 && anguloY > 270)
        {
            //check = 1;
            Destroy(gameObject);
        }

        //evitar que o aviao vá para trás a qualquer momento
        if (VelocidadeAtual < 0)
        {
            VelocidadeAtual = 0;
        }

        //tem que ter o translate para o aviao acelerar
        transform.Translate(0, 0, -VelocidadeAtual);

    } // fim update

} // fim class
