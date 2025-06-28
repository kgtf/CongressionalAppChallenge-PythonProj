using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Localization.Settings;

public class clickDetector : MonoBehaviour
{
    public TMP_Text countryName;
    public TMP_Text econscore; 
    public TMP_Text distancescore;
    public TMP_Text tempscore;
    public TMP_Text urbanscore;
    public TMP_Text religionscore;
    public TMP_Text languagescore;

    void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame) // New Input System
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()); //Find the vector position of the click
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero); //Find the hitbox at the vector position
            if (hit.collider != null) //Check if mouse clicked a hitbox
            {
                string Name = hit.collider.gameObject.name;
                countryName.text = PreferencePageScript.TranslateString(Name);

                //Find the scores of the clicked country for each variable
                //econscore.text = Country.FindEcon2(Name).ToString() + "/10";
                distancescore.text = PreferencePageScript.TranslateString("Distance")+ ": " + Country.FindDistance2(PreferencePageScript.slecCount, Name);
                tempscore.text = PreferencePageScript.TranslateString("Temperature") + ":" + PreferencePageScript.TranslateString(Country.FindTemperature2(Name).ToString());
                urbanscore.text = PreferencePageScript.TranslateString("Urbaness")+ ": " + Country.FindUrbaness2(Name);
                religionscore.text = PreferencePageScript.TranslateString("Religion") +":"+ PreferencePageScript.TranslateString(Country.FindReligion2(Name).ToString());
                languagescore.text = PreferencePageScript.TranslateString("Language") +":"+ PreferencePageScript.TranslateString(Country.FindLanguage2(Name).ToString());
            }
        }
        if (Touchscreen.current != null)
        {
            // We’ll look at the primary touch only, for single-touch use cases
            var primaryTouch = Touchscreen.current.primaryTouch;

            // If you need multi-touch, you would iterate through all touches.
            if (primaryTouch.press.wasPressedThisFrame)
            {
                Vector2 touchPos = primaryTouch.position.ReadValue();
                ProcessClick(touchPos);
            }
        }
    }
    private void ProcessClick(Vector2 screenPosition)
    {
        // Convert screen position to world position
        Vector2 worldPoint = Camera.main.ScreenToWorldPoint(screenPosition);

        // Raycast in 2D space
        RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);
        if (hit.collider != null)
        {
            string Name = hit.collider.gameObject.name;
            countryName.text = PreferencePageScript.TranslateString(Name);

            distancescore.text = PreferencePageScript.TranslateString("Distance")
                + ": " + Country.FindDistance2(PreferencePageScript.slecCount, Name);
            tempscore.text = PreferencePageScript.TranslateString("Temperature")
                + ": " + PreferencePageScript.TranslateString(Country.FindTemperature2(Name).ToString());
            urbanscore.text = PreferencePageScript.TranslateString("Urbaness")
                + ": " + Country.FindUrbaness2(Name);
            religionscore.text = PreferencePageScript.TranslateString("Religion")
                + ": " + PreferencePageScript.TranslateString(Country.FindReligion2(Name).ToString());
            languagescore.text = PreferencePageScript.TranslateString("Language")
                + ": " + PreferencePageScript.TranslateString(Country.FindLanguage2(Name).ToString());
        }
    }
}