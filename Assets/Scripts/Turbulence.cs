using UnityEngine;
using System.Collections;

public class Turbulence : MonoBehaviour {

	// Use this for initialization
    float speed = 1000.0f; //how fast it shakes
   // float amount = 1.0f; //how much it shakes
    bool turbulence = false;
    bool once = false;
    int lastMultiplier;
    float multiplier = 0;
    int i = 0;
    int hi = 0;
    [SerializeField]
    private AudioSource turbulenceWarning;
    public float Multiplier
    {
        get { return multiplier; }
        set { multiplier = value; }
    }

    public bool VerticalTurbulence
    {
        get { return turbulence; }
        set { turbulence = value; }
    }
    bool hturbulence = false;
	void Start () {
        lastMultiplier = 0;
	
	}
	
    /*
     * TODO: Keep reference to last camera state before starting the "turbulence". Use it to restore normality after disabling the turbulence itself
     */
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.T))
        {
          
            if (multiplier == 0)
                multiplier = 1;

            else 
                multiplier = 0;
        }
        if( multiplier!=0)
        {
           // Multiplier = 1;
            if (once == false)
            {
                turbulence = !turbulence;
                once = true;
                lastMultiplier = (int)multiplier;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.P))
        {
          //  Multiplier = 1;
            hturbulence = !hturbulence;
        }
        

        startTurbulence();
        startHorizontalTurbulence();
        if (lastMultiplier != multiplier)
        {
            if(once==true)
            {
                once = false;
            }
        }
	
	}
    

    private void startHorizontalTurbulence()
    {
        if (hturbulence)
        {
            
            if (i == 0)
                StartCoroutine(waitSeconds());
            if(i>=700)
                transform.position = new Vector3(transform.position.x, transform.position.y, ((Mathf.Sin(Time.time * speed) / 100) * Multiplier) + transform.position.z);
            
            i++;
        }
        else
            i = 0;
        
        Debug.Log("i é " + i);
    }

    IEnumerator waitSeconds()
    {
        Debug.Log("Waiting Audio ..");
        yield return new WaitForSeconds(1);
        //StartFlying();
        //  Debug.Log("Acabouuu!!! É tetraaaaa!");
        playTurbulenceMessage();
    }

    private void playTurbulenceMessage()
    {
        turbulenceWarning.Play();
    }

    private void startTurbulence()
    {
        //shake dat ass
        if (turbulence)
        {
            if (hi == 0)
                StartCoroutine(waitSeconds());
            if(hi>=700)
                transform.position = new Vector3(transform.position.x, ((Mathf.Sin(Time.time * speed) / 100) * Multiplier) + transform.position.y, transform.position.z);
            
            
            hi++;

        }
        else
            hi = 0;
        
        Debug.Log("i é " + i);
    }
}
