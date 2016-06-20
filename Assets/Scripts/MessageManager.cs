using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;


public  class MessageManager:MonoBehaviour{

    [SerializeField]
    private Turbulence turbulenceScript;

    [SerializeField]
    private GameObject silverLiningObject;

    private SilverLining silverLining;

    [SerializeField]
    private GameObject rain;

    [SerializeField]
    private GameObject thunder;

    [SerializeField]
    private GameObject takeOffReference;

    private TakeOff takeOffScript;
    private enum ClimateStates { aberto, nublado, chuvoso, temporal }
    void Start()
    {
        silverLining = silverLiningObject.GetComponent<SilverLining>();
        takeOffScript = takeOffReference.GetComponent<TakeOff>();


    }


    public void handleTurbulence(string toggle)
    {

        turbulenceScript.Multiplier = Int32.Parse(toggle);
    }


    public void handleStressLevels(string toggle)
    {
        int stress = Int32.Parse(toggle);
        if(stress ==2)
            SceneManager.LoadScene("mainScene");
        else
        //turbulenceScript.Multiplier = Int32.Parse(toggle);

        SceneManager.LoadScene(toggle);
    }

    public void handleTime(string time)
    {
        string[] words = time.Split(':');
        silverLining.hour = Int32.Parse(words[0]);
        silverLining.minutes = Int32.Parse(words[1]);

    }






    //private enum ClimateStates { aberto, nublado, chuvoso, temporal }
    public void handleClimate(int clima)
    {
        switch(clima)
        {
            case 0:
                silverLining.hasStratusClouds = false;
                rain.SetActive(false);
                thunder.SetActive(false);
                break;
            case 1:
                silverLining.hasStratusClouds = true;
                rain.SetActive(false);
                thunder.SetActive(false);
                silverLining.stratusThickness = 0.7f;
                break;
            case 2:
                silverLining.hasStratusClouds = true;
                rain.SetActive(true);
                thunder.SetActive(false);
                silverLining.stratusThickness = 0.8f;
                break;

            case 3:
                silverLining.hasStratusClouds = true;
                rain.SetActive(true);
                //rain.
                thunder.SetActive(true);
                silverLining.stratusThickness = 1.0f;
                break;
        }
        
    }

    public void handleDown()
    {
        if (takeOffScript.CurrentState == TakeOff.PlaneStates.cruise)
        {
            takeOffScript.CurrentState = TakeOff.PlaneStates.fall;
            Debug.Log("Caindo aaaaa"); 
        }
        else 
        {
            Debug.Log("Não pode!");
        }
        //throw new NotImplementedException();
    }

    public void handleNewLape(string lapse)
    {
        silverLining.continuousTimeAdvance = !silverLining.continuousTimeAdvance;
         silverLining.continuousTimeAdvanceRate = float.Parse(lapse);
        //throw new NotImplementedException();
    }
}

