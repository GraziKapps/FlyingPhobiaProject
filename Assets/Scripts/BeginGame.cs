using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class BeginGame : MonoBehaviour
{
    int day, month, year;
    bool isSunny, isClouded, isRaining;
    string hour;

    [SerializeField]
    //GameObject Date;

    private InputField dayField, monthField, yearField, hourField;

    // Use this for initialization
    void Start()
    {
        dayField.onEndEdit.AddListener((value) => EditValue(value, ref day));
        monthField.onEndEdit.AddListener((value) => EditValue(value, ref month));
        yearField.onEndEdit.AddListener((value) => EditValue(value, ref year));
        
        //isSunny = false;
        //isClouded = false;
        //isRaining = false;
    }

   

    private void EditValue(string value, ref int outParameter)
    {
       outParameter = Int32.Parse(value);
    }

    //// Update is called once per frame
    //void Update () {

    //}
    
   public void startGame()
    {
        
      //  day = Int32.Parse (dayField.text);

        PlayerPrefs.SetInt("Day", day);
        PlayerPrefs.SetInt("Month", month);
        PlayerPrefs.SetInt("Year", year);

        Debug.Log(day + " " + month + " " + year);
    }

}
