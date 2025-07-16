using System.Xml.Linq;
using TMPro;
using UnityEngine;

public class NewCountryInfo : MonoBehaviour
{

    public TMP_Text countryName;
    public TMP_Text distancescore;
    public TMP_Text tempscore;
    public TMP_Text urbanscore;
    public TMP_Text religionscore;
    public TMP_Text languagescore;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countryName.text = DataClassSaves.selectedCountry;
        distancescore.text = PreferencePageScript.TranslateString("Distance")
                + ": " + Country.FindDistance2(PreferencePageScript.slecCount, countryName.text);
        tempscore.text = PreferencePageScript.TranslateString("Temperature")
            + ": " + PreferencePageScript.TranslateString(Country.FindTemperature2(countryName.text).ToString());
        urbanscore.text = PreferencePageScript.TranslateString("Urbaness")
            + ": " + Country.FindUrbaness2(countryName.text);
        religionscore.text = PreferencePageScript.TranslateString("Religion")
            + ": " + PreferencePageScript.TranslateString(Country.FindReligion2(countryName.text).ToString());
        languagescore.text = PreferencePageScript.TranslateString("Language")
            + ": " + PreferencePageScript.TranslateString(Country.FindLanguage2(countryName.text).ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
