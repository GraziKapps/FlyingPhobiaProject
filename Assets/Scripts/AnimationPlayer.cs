using UnityEngine;
using System.Collections;

public class AnimationPlayer : MonoBehaviour {

    Animator headAnimator;
    void Awake ()
    {
        headAnimator = GetComponent<Animator>();
        headAnimator.enabled = false;
        
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        headAnimator.enabled = true;
    }

    void OnTriggerExit(Collider other)
    {
        headAnimator.enabled = false;
    }

}
