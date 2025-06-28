using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
public class CountryController : MonoBehaviour
{
    public List<GameObject> countries;  // This will hold the GameObjects for each country
    public float maxscore = 0;
    public List<string> topcountries = new List<string>();
    public List<float> scorelist = new List<float>();
    public List<float> colorscorelist = new List<float>();
    public TMP_Text first;
    public TMP_Text second;
    public TMP_Text third;
    private BoxCollider2D boxCollider;
    private RectTransform rectTransform;
    public Image circlesImg;
    public GameObject map;
    void Start()
    {
        ColorCountry();
        Hitbox();
    }
    public void ColorCountry()
    {
        //Create three lists to hold current score and color value for each country and the top 3 countries
        scorelist.Clear();
        colorscorelist.Clear();
        topcountries.Clear();

        maxscore = 0; //Reset max score before running
        float colorscore = 0;
        foreach (var country in countries)
        {
            //Find the name and score of each Gameobject
            string countryname = country.name;
            float score = Country.countryScores[countryname];
            if (score > maxscore)
            {
                maxscore = score; //Calculate the highest current score
            }

        }
        foreach (var country in countries)
        {
            string countryname = country.name;
            float score = Country.countryScores[countryname];
            if (countryname == PreferencePageScript.slecCount)
            {
                colorscore = -1; //Assign unique value to the home country

            }
            else if (score == maxscore)
            {
                colorscore = 1; //Set the country with the highest score to the highest color score
            }
            else
            {
                colorscore = 0.5f - (Mathf.Abs(score - maxscore) / maxscore); //Create a new number between 0-1 that determines how far off the countries score is to the highest score
            }
            Color colornew = new Color(1 - (colorscore), (colorscore), 0, 1); //Make every country be greener the higher their colorscore
            country.GetComponent<Image>().color = colornew; //Set gameobject color to their color
            if (colorscore < 0.32 && colorscore > 0.20)
            {
                Color color = new Color(1, 135 / 255f, 0, 1); //Make countries within this color range orange as otherwise, it turns brown
                country.GetComponent<Image>().color = color;
            }
            else if (colorscore < 0.40 && colorscore > 0.32)
            {
                Color color = new Color(1, 1, 0, 1); //Make countries within this color range yellow as otherwise, it turns brown
                country.GetComponent<Image>().color = color;
            }
            else if (colorscore == -1)
            {
                Color color = new Color(0.5f, 0.5f, 0.5f, 1); //Set home country's color as gray
                country.GetComponent<Image>().color = color;
            }
            else
            {
                Color color = new Color(1 - (2 * colorscore), (colorscore), 0, 1); //Make every other country have a color shade from green to red
                country.GetComponent<Image>().color = color;
            }
            scorelist.Add(score); //Add the actual score of each country to the list
            colorscorelist.Add(colorscore * 10); //Add the color score to the list and multiplies it by 10 so it gives a rating between 0-10
        }
        
        //Sorts both lists
        scorelist.Sort((x, y) => y.CompareTo(x));
        colorscorelist.Sort((x, y) => y.CompareTo(x));

        for (int i = 0; i < 4; i++) //Iterate through the 4 highest scored countries
        {
            float targetvalue = scorelist[i];
            foreach (var kvp in Country.countryScores) //Use the score to find the name of the country
            {
                if (kvp.Value == targetvalue)
                {
                    topcountries.Add(kvp.Key); //Add country name to a list of the top 5 country's names
                }
            }
        }

        //Set the text in the textboxes to be the names of the top 3 countries and their 0-10 rating, tranlate it as well
        //Note: Since the home country will always have the highest score, we display the 2nd, 3rd, and 4th countries instead
        first.text = "#1 " + PreferencePageScript.TranslateString(topcountries[1]) + PreferencePageScript.TranslateString("has a score of") + " " + ((float)System.Math.Round(colorscorelist[1], 1)*2).ToString();
        second.text = "#2 " + PreferencePageScript.TranslateString(topcountries[2]) + PreferencePageScript.TranslateString("has a score of") + " " + ((float)System.Math.Round(colorscorelist[2], 1) * 2).ToString();
        third.text = "#3 " + PreferencePageScript.TranslateString(topcountries[3]) + PreferencePageScript.TranslateString("has a score of") + " " + ((float)System.Math.Round(colorscorelist[3], 1) * 2).ToString();
    }
    public void Hitbox() // Use to make hitboxes for the countries
    {
        foreach (var country in countries)
        {
            if (country.name == "Portugal") //Allow Portugal to be manually set as it has irregular shape
            {
                continue;
            }
            else if (country.name == "Norway") //Allow Norway to be manually set as it has irregular shape
            {
                continue;
            }
            else if (country.name == "Croatia") //Allow Croatia to be manually set as it has irregular shape
            {
                continue;
            }
            else
            {
                //Create a hitbox for each country and resize it based on the GameObject's properties
                rectTransform = country.GetComponent<RectTransform>();
                boxCollider = country.AddComponent<BoxCollider2D>();
                boxCollider.size = (rectTransform.sizeDelta) / 2;

                //Draw a circle in the center of the hitbox for each country
                Image circle = Instantiate(circlesImg, map.transform);
                circle.GetComponent<RectTransform>().anchoredPosition = rectTransform.anchoredPosition;

            }
        }
    }
}
