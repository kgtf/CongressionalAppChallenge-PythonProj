using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenLink : MonoBehaviour
{
    // Base URL for the site
    private string baseUrl = "https://help.unhcr.org/";


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    public void OpenCountryLink()
    {
        if (string.IsNullOrEmpty(DataClassSaves.selectedCountry))
        {
            Debug.LogWarning("Selected country is null or empty!");
            return;
        }

        string countryForUrl = DataClassSaves.selectedCountry.Replace(" ", "%20");

        string fullUrl = baseUrl + countryForUrl;

        Application.OpenURL(fullUrl);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
