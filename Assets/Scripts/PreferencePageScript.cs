using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Localization; //Translate UI to different languages
using UnityEngine.Localization.Settings;
using System.Collections;
using UnityEngine.InputSystem;
using System;


public class PreferencePageScript : MonoBehaviour
{
    //all variables in script
    public TMP_Dropdown countryDropdown;
    public TMP_Dropdown religionDropdown;
    public TMP_Dropdown languageDropdown;
    public Slider temperatureSlider;
    public Slider urbanSlider;
    public Slider distanceSlider;
    public Slider temperatureWeighter;
    public Slider urbanWeighter;
    public Slider distanceWeighter;
    public Slider religionWeighter;
    public TMP_Text temperatureWeightText;
    public TMP_Text urbanWeightText;
    public TMP_Text distanceWeightText;
    public TMP_Text religionWeightText;
    public TMP_Text tempText;
    public TMP_Text urbText;
    public TMP_Text distText;
    public List<string> translatednames = new List<string>();
    public List<string> translatedreligions = new List<string>();
    public static string slecCount;
    public static string slecTemp = "";
    public static string slecRel;
    public static float slecUrb;
    public static float slecDist;
    public static float slecweightTemp;
    public static float slecweightUrb;
    public static float slecweightDist;
    public static float slecweightRelig;
    public static int tempNum;
    public Image tempHandle;
    public Image distHandle;
    public LocalizedString[] localizedOptions; 
    public static int countIndex;
    public static int relIndex;
    public static int currentIndex;
    public static int langIndex;
    
    private void Start()
    {
        StartCoroutine(DelayedSceneCheck()); //Waits a couple frames before running DelayedSceneCheck
        
        //Fill in options for the dropdowns
        PopulateLangDropdown(); 
        PopulateCountryDropdown();
        PopulateReligionDropdown();

        LocalizationSettings.SelectedLocaleChanged += (locale) =>
        { 
            //Translate all dropdown options to the chosen language
            PopulateCountryDropdown();
            PopulateReligionDropdown(); 
        };
        languageDropdown.RefreshShownValue(); //Update visual display for language dropdown
        countryDropdown.RefreshShownValue(); //Update visual display for country dropdown
        OnCountrySelected(); // Manually trigger our logic
        religionDropdown.RefreshShownValue(); //Update visual display for religion dropdown
        OnReligionSelected(); // Manually trigger our logic
    }

        public void InitializeScore() //Reset score of each country to 0
    {
        foreach (var key in Country.countryScores.Keys.ToList())
        {
            Country.countryScores[key] = 0;
        }
    }

    public void PopulateLangDropdown() //Add each language to language dropdown
    {
        languageDropdown.ClearOptions(); //Clears language dropdwon
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();

        var locales = LocalizationSettings.AvailableLocales.Locales;
        for (int i = 0; i < locales.Count; i++)
        {
            string languageName = locales[i].Identifier.CultureInfo.NativeName; // Display in native language
            options.Add(new TMP_Dropdown.OptionData(languageName));
        }

        languageDropdown.AddOptions(options);

        // Set the currently selected locale
        currentIndex = locales.IndexOf(LocalizationSettings.SelectedLocale);

        if (currentIndex >= 0)
            languageDropdown.value = currentIndex;
    }
    public void ChangeLanguage(int index) //Translate all words to selected langauges
    {
        var locales = LocalizationSettings.AvailableLocales.Locales;
        currentIndex = locales.IndexOf(LocalizationSettings.SelectedLocale);
        LocalizationSettings.SelectedLocale = locales[index]; // Change locale
        langIndex = languageDropdown.value;
    }

    public static string TranslateString(string sentence) //Translate a string by splitting it into words based on spaces and then using the string tables
    {
        string[] words = sentence.Split(" "); //Splits a string into a list of words
        string translatedstring = ""; //Initialize final string
        
        //Create any exceptions that we want to consider a single word even though it is multiple and translate them
        if (sentence == "San Marino")
        {
            string localizedword = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", sentence);
            return localizedword;
        }
        else if (sentence == "United Kingdom")
        {
            string localizedword = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", sentence);
            return localizedword;
        }
        else if (sentence == "Bosnia and Herzegovina")
        {
            string localizedword = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", sentence);
            return localizedword;
        }
        else if (sentence == "North Macedonia")
        {
            string localizedword = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", sentence);
            return localizedword;
        }

        else
        {
            foreach (string word in words) //Iterate through each word in the list of words
            {
                string localizedword = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", word); //Get the word's translation
                translatedstring = translatedstring + " "+ localizedword; //Add it to return string
            }
            return translatedstring; // Return translation
        }
    }
    void PopulateCountryDropdown() //Add all possible countries to country dropdown
    {
        // Clear existing options
        countryDropdown.ClearOptions();

        // Add a placeholder option
        List<TMP_Dropdown.OptionData> coop = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Current Country")
        };

        // Add country names from the countries dictionary
        foreach (var countryEntry in Country.countries.Values)
        {
            translatednames.Add(countryEntry.Name);
            string localizedCountryName = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", countryEntry.Name);
            coop.Add(new TMP_Dropdown.OptionData(localizedCountryName));
        }
        
        // Add options to the dropdown
        countryDropdown.AddOptions(coop);
    }

    // Method to handle selection
    public void OnCountrySelected()
    {
        countIndex = countryDropdown.value;
        if (countIndex == 0)
        {
            Debug.Log("Please select a country.");
        }
        else
        {
            slecCount = translatednames[countIndex];
            countIndex = countryDropdown.value;
            Debug.Log(countIndex);
        }

        // Get the selected country's name
        string selectedCountryName = countryDropdown.options[countIndex].text;

        // Retrieve the Country object using the name
        if (Country.countries.TryGetValue(selectedCountryName, out Country selectedCountry))
        {
        }
    }

    void PopulateReligionDropdown() //Add all possible religions to the religion dropdown
    {
        // Clear existing options
        religionDropdown.ClearOptions();

        // Add a placeholder option
        List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>
        {
            new TMP_Dropdown.OptionData("Select Religion")
        };

        // Collect all religions from all countries and remove duplicates
        HashSet<string> uniqueReligions = new HashSet<string>();

        foreach (var country in Country.countries.Values)
        {
            foreach (var religion in country.Religion)
            {
                if (!string.IsNullOrWhiteSpace(religion))
                {
                    string localizedReligion = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", religion.Trim());
                    uniqueReligions.Add(localizedReligion);
                }
            }
        }

        // Add the unique religions to the dropdown
        foreach (var religion in uniqueReligions)
        {
            options.Add(new TMP_Dropdown.OptionData(religion));
        }

        // Add options to the dropdown
        religionDropdown.AddOptions(options);
    }

    // Handle selection
    public void OnReligionSelected()
    {
        relIndex = religionDropdown.value;
        if (relIndex == 0)
        {

            return;
        }
        else
        {
            slecRel = religionDropdown.options[relIndex].text;

        }
    }

    public void SliderTempMove() //Return selected value of temperature slider 
    {
        if (temperatureSlider.value == 1)
        {
            slecTemp = "Moderate";
            tempHandle.GetComponent<Image>().color = Color.white;
            tempNum = 1;
        }
        else if (temperatureSlider.value == 2)
        {
            slecTemp = "Hot";
            tempHandle.GetComponent<Image>().color = Color.red;
            tempNum = 2;
        }
        else if(temperatureSlider.value == 0)
        {
            slecTemp = "Cold";
            tempHandle.GetComponent<Image>().color = Color.blue;
            tempNum = 0;
        }
        tempText.text = slecTemp; //Display selected temperature


        string localizedValue = LocalizationSettings.StringDatabase.GetLocalizedString("UI_Texts", slecTemp);
        tempText.text = localizedValue;
        slecTemp = localizedValue;
    }
    public void SliderDistMove() //Return selected value of distance slider
    {
        if (distanceSlider.value <1200)
        {
            slecDist = distanceSlider.value;
            distHandle.GetComponent<Image>().color = Color.green;
        }
        else if (distanceSlider.value >= 1200 && distanceSlider.value <= 1500)
        {
            slecDist = distanceSlider.value;
            string limeHex = "#00FF00"; // Lime color hex code

            if (ColorUtility.TryParseHtmlString(limeHex, out Color limeColor))
            {
                distHandle.GetComponent<Image>().color = limeColor; // Apply lime color
            }
        }else if (distanceSlider.value > 1500 && distanceSlider.value <= 3000)
        {
            slecDist = distanceSlider.value;
            distHandle.GetComponent<Image>().color = Color.yellow;
        }
        else if(distanceSlider.value > 3000 && distanceSlider.value <= 4000)
        {
            slecDist = distanceSlider.value;
            string orangeHex = "#FFA500"; // Lime color hex code

            if (ColorUtility.TryParseHtmlString(orangeHex, out Color orangeColor))
            {
                distHandle.GetComponent<Image>().color = orangeColor; // Apply lime color
            }
        }
        else if (distanceSlider.value > 4000)
        {
            slecDist = distanceSlider.value;
            distHandle.GetComponent<Image>().color = Color.red;
        }
        distText.text = (slecDist.ToString() + " km");

    }
    public void SliderUrbMove() //Return selected value of urbaness slider
    {
        slecUrb = urbanSlider.value;
        urbText.text = slecUrb.ToString() + " %";
    }

    public void WeighterTempMove() //Return selected value of temperature weighter
    {
         slecweightTemp = temperatureWeighter.value;
        temperatureWeightText.text = (Math.Round(slecweightTemp * 50)).ToString() + "%";
    }

    public void WeighterUrbMove() //Return selected value of urbaness weighter
    {
        slecweightUrb = urbanWeighter.value;
        urbanWeightText.text = (Math.Round(slecweightUrb * 50)).ToString() + "%";
    }

    public void WeighterDistMove() //Return selected value of distance weighter
    {
        slecweightDist = distanceWeighter.value;
        distanceWeightText.text = (Math.Round(slecweightDist * 50)).ToString() + "%";
    }

    public void WeighterReligMove() //Return selected value of religion weighter
    {
        slecweightRelig = religionWeighter.value;
        religionWeightText.text = (Math.Round(slecweightRelig * 50)).ToString() + "%";
    }

    public void SubmitPreferences() //Submit selected preferences and move to weights scene
    {
        string temperaturePreference = slecTemp;
        float urbanPreference = slecUrb;
        float distancePreference = slecDist;

        if (temperaturePreference == null || urbanPreference == 0 || distancePreference == 0 || countIndex == 0 || relIndex == 0) //Check that user has filled in each preference
        {
            return;
        }
        else
        {         
            NextScene(); //Go to weights scene
        }  
    }

    public void SubmitWeights() //Submit selected weights and move to final scene
    {
        float weightTemp = slecweightTemp;
        float weightUrb = slecweightUrb;
        float weightDist = slecweightDist;
        float weightRelig = slecweightRelig;
        NextScene(); //Go to final scene
    }

    public void ApplyChange() //Apply selected preferences and transfer selections to next scene
    {
        countryDropdown.value = countIndex;
        religionDropdown.value = relIndex;
        temperatureSlider.value = tempNum;
        urbanSlider.value = slecUrb;
        distanceSlider.value = slecDist;
        languageDropdown.value = langIndex;
        ChangeLanguage(langIndex);
    }

    private IEnumerator DelayedSceneCheck() //Call ApplyChangeFunction
    {
        yield return null; //Wait one frame to ensure everything is initialized

        if (SceneManager.GetActiveScene().buildIndex == 2) //Transfers preferences from one scene to the other
        {
            ApplyChange();
        }
        if (SceneManager.GetActiveScene().buildIndex == 1) //Transfers preferences from one scene to the other
        {
            languageDropdown.value = langIndex;
            ChangeLanguage(langIndex);
        }
    }

    public void NextScene() //Change scene to submit preference scene
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void backButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);

    }
    public void quitButton()
    {
        Application.Quit();
    }
    public void ReloadTranslate()
    {
        ChangeLanguage(langIndex);
    }

}