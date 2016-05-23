using UnityEngine;
using System.Collections;

public class AtivaDesativaAvatares : MonoBehaviour {

    GameObject avataresAviao;
    GameObject avataresSala;
    GameObject aviaoInterno;
    GameObject aeroportoInterno;

	// Use this for initialization

	void Start () {
        avataresAviao = GameObject.Find("avataresAviao");
        avataresSala = GameObject.Find("avataresSalaEspera");
        aviaoInterno = GameObject.Find("aviaoInterno");
        aeroportoInterno = GameObject.Find("aeroportoInterno");

        avataresAviao.SetActive(false);
        avataresSala.SetActive(true);
        aeroportoInterno.SetActive(true);
        aviaoInterno.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision Col)
    {
        Debug.Log(Col.gameObject.name);
        if(Col.gameObject.name == "Saida_Corredor")
        {
            avataresSala.SetActive(false);
            aeroportoInterno.SetActive(false);
            avataresAviao.SetActive(true);
            aviaoInterno.SetActive(true);
        }

        if (Col.gameObject.name == "Rotatoria02")
        {
            avataresSala.SetActive(true);
            aeroportoInterno.SetActive(true);
            avataresAviao.SetActive(false);
            aviaoInterno.SetActive(false);
        }
    }
}
