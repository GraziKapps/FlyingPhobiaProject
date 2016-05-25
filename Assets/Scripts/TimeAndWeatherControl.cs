using UnityEngine;
using System.Collections;

public class TimeAndWeatherControl : MonoBehaviour
{

    // Use this for initialization

    //Serialize for editor.
    [SerializeField]
    private GameObject silverLiningObject;
    [SerializeField]
    private GameObject rain;
    [SerializeField]
    private GameObject thunder;

    private SilverLining silverLining;

    public int hour, minute, seconds;
    public bool isActive;

    //Overwrite silverlining
    void Start()
    {
        silverLining = silverLiningObject.GetComponent<SilverLining>();
        silverLining.hour = hour;
        silverLining.minutes = minute;
        silverLining.seconds = seconds;


    }

    // Update is called once per frame
    /*
     * TODO: 
     * 1:Overwrite silverlining. Disabled now for test purposes. 
     * 2: Create cumulus clouds without performance hits.
     */
    void Update()
    {
        //silverLining.hour = hour;
        //silverLining.minutes = minute;
        //silverLining.seconds = seconds;
        rain.SetActive(isActive);

        if (Input.GetKeyDown(KeyCode.R))
        {
            isActive = !isActive;

        }

        if (isActive)
            startRaining();
        else
            stopRaining();
    }

    private void stopRaining()
    {
        // silverLining.cumulusBrightness = 0.7f;
        //ilverLining.cumulusCoverage = 0.2f;
        silverLining.hasStratusClouds = false;
        rain.SetActive(isActive);
        thunder.SetActive(isActive);
        //throw new System.NotImplementedException();
    }

    public void startRaining()
    {
        //    silverLining.cumulusBrightness = 0.3f;
        //silverLining.cumulusCoverage = 0.9f;
        silverLining.hasStratusClouds = true;
        //silverLining.enableStratusClouds();
        rain.SetActive(isActive);
        thunder.SetActive(isActive);
        // isActive = !isActive;
        //change cumulus cloud color and it's density here.

    }
}
