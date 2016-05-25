using UnityEngine;
using System.Collections;

public class ArrowBlink : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, Mathf.Sin(Time.time *5));
	
	}
}
