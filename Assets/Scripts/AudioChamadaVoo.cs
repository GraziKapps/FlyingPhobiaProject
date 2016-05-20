using UnityEngine;
using System.Collections;

public class AudioChamadaVoo : MonoBehaviour {

    public AudioSource sChamadaVoo;
    public float tempoChamadaVoo;

    // Use this for initialization
    void Start()
    {
        tempoChamadaVoo = 0;
        sChamadaVoo.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        TempoP();
    }

    void TempoP()
    {
        tempoChamadaVoo += Time.deltaTime;

        if (tempoChamadaVoo >= 20 && tempoChamadaVoo < 80)
        {
            tempoChamadaVoo += Time.deltaTime;

            if (!sChamadaVoo.isPlaying)
                sChamadaVoo.Play();
        }

        if(tempoChamadaVoo >= 80)
        {
            sChamadaVoo.Stop();
            //tempoChamadaVoo = 0;
        }

        if (tempoChamadaVoo >= 150)
        {
            tempoChamadaVoo = 0;
        }
    }
}
