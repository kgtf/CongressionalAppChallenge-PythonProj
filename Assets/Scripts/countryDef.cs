using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

public class RunInfo : MonoBehaviour
{
    void Start()
    {
        Country.Main(); //Use the CSV file to create each country
    }
    void Update()
    {
    }
    public void RunInputs() //Compiles each function into one
    {
        Country.Main(); //Use the CSV data to create each country
        
        //Calculate the score of each country based on the inputs from the sliders/dropdowns and the CSV data
        Country.FindEcon();
        Country.FindDistance(PreferencePageScript.slecDist, PreferencePageScript.slecCount);
        Country.FindTemperature(PreferencePageScript.tempNum);
        Country.FindUrbaness(PreferencePageScript.slecUrb);
        Country.FindReligion(PreferencePageScript.slecRel);
        Country.FindEnemies(PreferencePageScript.slecCount);
    }

}