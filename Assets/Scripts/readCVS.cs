using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Localization.SmartFormat.Utilities;

public class Country
{
    //Create the variables for each countries information
    public string Name { get; set; }
    public string Population { get; set; }
    public string Distance { get; set; }
    public string Temperature { get; set; }
    public List<string> Religion { get; set; }
    public List<string> Ethnicities { get; set; }
    public string YearlyWage { get; set; }
    public string Expenses { get; set; }
    public List<string> Language { get; set; }
    public string Urbaness { get; set; }
    public string DiplomaticStatus { get; set; }
    public string Longitude { get; set; }
    public string Latitude { get; set; }
    public static Dictionary<string, float> countryScores = new Dictionary<string, float>(); //Holds each countries name as a key and their score as a value
    public static Dictionary<string, Country> countries = new Dictionary<string, Country>(); //Dictionary of each countries name and its information
    public static Dictionary<string, List<string>> enemies = new Dictionary<string, List<string>>(); //List of each country with an enemy as a key and a list of its enemies as a value
    public static List<string> CitizenshipDifficulties = new List<string>
    {"Portugal", "Greece", "Spain", "Italy", "Malta", "Bulgaria", "Romania", "Hungary",
    "Czechia", "Belgium", "Netherlands", "Ireland", "United Kingdom", "Sweden", "France",
    "Germany", "Finland", "Denmark", "Austria", "Switzerland", "Luxembourg", "Iceland",
    "Norway", "Poland", "Slovakia", "Slovenia", "Croatia", "Cyprus", "Estonia", "Latvia",
    "Lithuania", "Liechtenstein", "Andorra", "San Marino", "Monaco", "Bosnia and Herzegovina",
    "Serbia", "Montenegro", "Albania", "North Macedonia", "Kosovo", "Moldova",
    "Armenia", "Azerbaijan", "Belarus", "Georgia", "Turkey", "Russia", "Kazakhstan", "Ukraine"}; 
    //List of each countries ordered by how hard it is to become naturalized relatively
    public Country(string name, string population, string distance, string temperature, string religion, string ethnicities, string yearlyWage, string expenses, string language, string urbaness, string diplomaticStatus, string latitude, string longitude)
    {
        //Country Constructor
        Name = name;
        Population = population;
        Distance = distance;
        Temperature = temperature;
        Religion = new List<string>(religion.Split('/'));
        Ethnicities = new List<string>(ethnicities.Split('/'));
        YearlyWage = yearlyWage;
        Expenses = expenses;
        Language = new List<string>(language.Split('/'));
        Urbaness = urbaness;
        DiplomaticStatus = diplomaticStatus;
        Latitude = latitude;
        Longitude = longitude;
        if (!countryScores.ContainsKey(Name)) //Add each countries name into the scores dictionary and give them all scores of 0
        {
            countryScores[Name] = 0;
        }

    }

    private static void AddScore(string countryName, float score)
    {
        if (!countryScores.ContainsKey(countryName)) //Add the country into the scores dictionary if it isn't already present
        {
            countryScores[countryName] = 0;

        }


        countryScores[countryName] += score; //Add the given score to the countries score
    }
    private static float CalcDistance(Country country1, Country country2)
    {
        //Use the formula for distance using latitude and longitude to calculate the real world distance between two countries
        //Note: Distance is calculated from capital to capital and in Kilometers

        //Find both countries latitude and longitude in the CSV data
        float country1latrad = float.Parse(country1.Latitude) * Mathf.Deg2Rad;
        float country2latrad = float.Parse(country2.Latitude) * Mathf.Deg2Rad;
        float country1longrad = float.Parse(country1.Longitude) * Mathf.Deg2Rad;
        float country2longrad = float.Parse(country2.Longitude) * Mathf.Deg2Rad;
        float r = 6371; //Radius of the earth


        float a = Mathf.Pow((Mathf.Sin((country2latrad - country1latrad) / 2)), 2) + Mathf.Cos(country1latrad) * (Mathf.Cos(country2latrad) * Mathf.Pow(Mathf.Sin((country2longrad - country1longrad) / 2), 2));
        float c = 2 * Mathf.Atan2(Mathf.Sqrt(a), Mathf.Sqrt(1 - a));
        float d = r * c;
        return d;
    }
    public static void Main()
    {
        //csvData is a string that holds all the data for each country
        //Note: Several pieces of information were orginally added but are not used in this CSV data such as some of the minor languages as well as the variables Ethnicity and Diplomatic Status 
        string csvData = @"Country, Population, Distance, Temperature, Religion, Ethnicities, Yearly Wage,Expenses,Language,Urbaness,Diplomatic Status, Latitude, Longitude
Albania,2.8,200,Moderate,Islam/Christianity,Albanian/Greek/Vlach/Macedonian,8000,5000,Greek,60,Republic,41.1533,20.1683
Andorra,0.08,200,Cold,Christianity,Andorran/Spanish,35000,30000,Catalan/French,90,Parliamentary,42.5078,1.5211
Armenia,3,500,Hot,Christianity,Armenian/Yezidi,5000,4000,Armenian,60,Republic,40.0691,45.0382
Austria,9,600,Cold,Christianity,Austrian/Slovenian/Croatian,45000,30000,German,70,Neutral,47.5162,14.5501
Azerbaijan,10,1000,Hot,Islam/Christianity,Azerbaijani/Lezgian/Russian/Talysh,8500,7000,Russian,50,Republic,40.1431,47.5769
Belarus,9.4,300,Cold,Christianity/Catholic,Belarusian/Russian/Polish,8000,6000,Russian,75,Republic,53.7098,27.9534
Belgium,11.5,200,Moderate,Christianity,Belgian/German/Italian,52000,35000,Dutch/French/German,98,Federal Parliamentary,50.8503,4.3517
Bosnia and Herzegovina,3.3,400,Moderate,Islam/Christianity,Bosnian/Serbian/Croatian,7000,4500,Serbian,60,Republic,43.9159,17.6791
Bulgaria,7,150,Moderate,Christianity,Bulgarian/Turkish/Roma,12000,8000,Bulgarian,70,Republic,42.7339,25.4858
Croatia,4,100,Moderate,Christianity,Croat/Serb/Italian/Hungarian,18000,13000,Serbian/Italian,60,Republic,45.1,15.2
Cyprus,1.2,800,Hot,Christianity,Greek/Turkish/Maronite,28000,18000,Greek,75,Republic,35.1264,33.4299
Czechia,10.7,300,Cold,Christianity,Czech/Slovak/Polish,25000,20000,Czech,70,Parliamentary Republic,49.8175,15.473
Denmark,5.8,200,Moderate,Christianity,Danish,55000,35000,Danish,85,Constitutional Monarchy,56.2639,9.5018
Estonia,1.3,200,Cold,Christianity,Estonian/Russian,22000,18000,Russian,70,Parliamentary Republic,58.5953,25.0136
Finland,5.5,100,Cold,Christianity,Finnish/Swedish,45000,30000,Swedish,70,Republic,61.9241,25.7482
France,67,300,Moderate,Christianity,French/African/Arab,40000,30000,French,80,Republic,46.6034,1.8883
Georgia,3.7,600,Moderate,Christianity,Georgian/Armenian/Russian/Ossetian,6000,5000,Russian,55,Republic,42.3154,43.3569
Germany,83,300,Moderate,Christianity,German/Turkish/Polish/Russian,50000,35000,German/Polish/English,75,Federal Republic,51.1657,10.4515
Greece,10.5,500,Hot,Christianity,Greek/Turkish/Macedonian,20000,15000,Greek,80,Parliamentary Republic,39.0742,21.8243
Hungary,9.7,100,Cold,Christianity,Hungarian/Roma,15000,12000,Hungarian,70,Republic,47.1625,19.5033
Iceland,0.35,500,Cold,Christianity,Icelandic,60000,40000,French/German,90,Republic,64.9631,-19.0208
Ireland,5,200,Moderate,Christianity,Irish,45000,30000,English,70,Parliamentary Republic,53.4129,-8.2439
Italy,60,300,Hot,Christianity,Italian/Albanian/Romanian,30000,22000,Italian/German,70,Republic,41.8719,12.5674
Kosovo,1.8,200,Moderate,Islam/Christianity,Albanian/Serbian,10000,7000,Serbian,40,Republic,42.6026,20.902
Latvia,1.9,100,Cold,Christianity,Latvian/Russian,18000,14000,Russian,70,Republic,56.8796,24.6032
Liechtenstein,0.04,300,Cold,Christianity,Liechtensteinian/Swiss,120000,80000,German,90,Principality,47.166,9.5554
Lithuania,2.8,200,Cold,Christianity,Lithuanian/Polish,20000,15000,Polish,70,Republic,55.1694,23.8813
Luxembourg,0.65,100,Moderate,Christianity,Luxembourger/French/German,100000,70000,French/German,85,Grand Duchy,49.8153,6.1296
Malta,0.5,200,Hot,Christianity,Maltese/English,30000,22000,English,95,Republic,35.9375,14.3754
Moldova,2.7,250,Moderate,Christianity/Catholic,Moldovan/Russian,5000,4500,Russian,60,Republic,47.4116,28.3699
Monaco,0.04,100,Hot,Christianity/Catholic,French,190000,150000,French,100,Constitutional Monarchy,43.7384,7.4246
Montenegro,0.62,150,Hot,Christianity,Montenegrin/Serbian,9000,6000,Serbian,60,Republic,42.7087,19.3744
Netherlands,17.4,300,Moderate,Christianity,Dutch/Frisian/Indonesian,55000,38000,Dutch/English,92,Constitutional Monarchy,52.1326,5.2913
North Macedonia,2.1,100,Moderate,Islam/Christianity,Macedonian/Albanian/Serbian,8000,6000,Serbian,60,Republic,41.9981,21.4254
Norway,5.4,100,Cold,Christianity,Norwegian/Sami,60000,35000,Swedish/English,85,Constitutional Monarchy,60.472,8.4689
Poland,38.3,500,Moderate,Christianity,Polish,15000,10000,Polish,60,Republic,51.9194,19.1451
Portugal,10.2,800,Moderate,Christianity,Portuguese,28000,18000,Portuguese,66,Republic,39.3999,-8.2245
Romania,19.4,200,Moderate,Christianity,Romanian/Hungarian/Roma,12000,8000,Hungarian,55,Republic,45.9432,24.9668
Russia,110,800,Cold,Christianity,Russian/Tatar/Ukrainian/Bashkir/Chechen,12000,9000,Russian,75,Federal Republic,61.524,105.3188
San Marino,0.034,50,Moderate,Christianity,Sammarinese/Italian,55000,40000,Italian,94,Parliamentary Republic,43.9333,12.45
Serbia,6.9,300,Moderate,Christianity,Serbian/Hungarian/Bosniak,8500,6500,Serbian,60,Republic,44.0165,21.0059
Slovakia,5.4,150,Moderate,Christianity,Slovak,20000,15000,Czech,70,Parliamentary Republic,48.669,19.699
Slovenia,2.1,100,Moderate,Christianity,Slovenian,30000,22000,Czech,70,ParliamentaryRepublic,46.1512,14.9955
Spain,47.4,200,Moderate,Christianity,Spanish/Catalan/Galician/Basque,30000,22000,Spanish/Catalan,81,Parliamentary Monarchy,40.4637,-3.7492
Sweden,10.2,200,Cold,Christianity,Swedish/Sami/Finnish,50000,35000,Swedish,85,Constitutional Monarchy,60.1282,18.6435
Switzerland,8.7,300,Cold,Christianity,Swiss/German/French/Italian/Romansh,60000,40000,German/French/Italian,75,FederalRepublic,46.8182,8.2275
Ukraine,41,600,Cold,Christianity,Ukrainian/Russian/Crimean Tatar,5000,3500,Russian,69,Republic,48.3794,31.1656
United Kingdom,67.2,100,Moderate,Christianity,British/Irish,45000,35000,English,83,Constitutional Monarchy,55.3781,-3.436";

        LoadCountries(csvData); //Create each country using the data

        //Add each country with enemies and the enemies into the enemies dictionary
        enemies["Ukraine"] = new List<string> { "Russia", "Belarus" };
        enemies["Russia"] = new List<string> { "Ukraine" };
        enemies["Belarus"] = new List<string> { "Ukraine" };
    }
    static void LoadCountries(string csvData) //Parses the CSV data
    {
        // Split the string into lines
        string[] lines = csvData.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

        // Skip the header and iterate through the remaining lines
        foreach (string line in lines[1..])
        {
            string[] parts = line.Split(',');

            // Ensure the row has each columns
            if (parts.Length < 13) continue;

            // Create a new country object using the constructor
            Country country = new Country(
                parts[0], parts[1], parts[2], parts[3], parts[4], parts[5],
                parts[6], parts[7], parts[8], parts[9], parts[10], parts[11], parts[12]
            );

            // Add the country and its information to the dictionary
            countries[parts[0]] = country;
        }
    }
    public static string FindTemperature2(string countri) //Find the climate of a specific country and return it
    {
        Country country = countries[countri]; //Get data for the country
        return country.Temperature;

    }
    public static string FindReligion2(string countri) //Find all of the religions of a specific country and return them
    {
        Country country = countries[countri]; //Get data for the country
        string String = ""; //create string
        String = string.Join(" | ", country.Religion);//properly display string
        Debug.Log(String);
        return String;
    }
    public static float FindUrbaness2(string countri) //Find the urbaness of a specific country and return it
    {
        Country country = countries[countri]; //Get data for the country
        float floatUrbaness = float.Parse(country.Urbaness);
        return floatUrbaness;
    }
    public static float FindEcon2(string countri) //Find the economy score of a single country and return it
    {
        Country country = countries[countri]; //Get data for the country
        float countryWage = float.Parse(country.YearlyWage.Trim());
        float countryExpense = float.Parse(country.Expenses.Trim());
        float score = (Mathf.Abs(countryWage - countryExpense)) / countryWage; //Calculates the percent of the wage left over after expenses
        return score;
    }
    public static float FindDistance2(string home, string countri) //Find the distance score of a single country and return it
    {
        Country country = countries[countri]; //Get data for the country
        Country pos = countries[home]; //Get data for home country
        float distance = CalcDistance(pos, country); //Calculates distance between the home country and the given country
        return Mathf.Round(distance);
    }
    public static string FindLanguage2(string countri) //Find Languages spoken of a single country
    {
        Country country = countries[countri]; //Get data for the country
        string String = ""; //create string
        String = string.Join(" | ", country.Language); //Properly display string
        return String;
    }
    public static void FindTemperature(int temp) //Find the temperature score of every country and add it to their score
    {
        // Iterate through all countries in the dictionary
        foreach (var country in countries.Values)
        {
            float Tempnum = 0;
            float score = 0;
            string countryTemp = country.Temperature.Trim();
            //Sets temperature as a value between 0-2
            if (countryTemp == "Cold")
            {
                Tempnum = 0;
            }
            else if (countryTemp == "Moderate")
            {
                Tempnum = 1;
            }
            else if (countryTemp == "Hot")
            {
                Tempnum = 2;
            }
            score = 1 - (Mathf.Abs(Tempnum - temp) / 2); //Calculates percent difference between the target value and the actual value
            AddScore(country.Name, score * PreferencePageScript.slecweightTemp); //Adds the score to the country in the dictionary

        }
    }
    public static void FindReligion(string rel) //Check if the country has the target religion and if so, add to their score
    {

        rel = rel.Trim().ToLower();
        //Iterate through all countries in the dictionary
        foreach (var country in countries.Values)
        {
            float weight = PreferencePageScript.slecweightRelig; //Set the score value
            if (country.Religion.Exists(r => r.Trim().ToLower() == rel)) //Check if country has the target religion
            {
                AddScore(country.Name, weight); //Adds the score to the country in the dictionary

            }


        }
    }
    public static void FindLanguage(string lang) //Check if the country has the target language and if so, add to their score
    {
        
        // Iterate through all countries in the dictionary
        foreach (var country in countries.Values)
        {
            foreach (string Language in country.Language)
            {
                if (Language == lang)
                {
                    AddScore(country.Name, 1.5f); //Adds the score to the country in the dictionary
                }
            }
        }
    }
    public static void FindUrbaness(float urban) //Find the urban score of every country and add it to their score
    {
        //Iterate through all countries in the dictionary
        foreach (var country in countries.Values)
        {
            float match = 0;
            float floatUrbaness = float.Parse(country.Urbaness);
            match = Mathf.Max(0, 1 - Mathf.Pow((floatUrbaness - urban) / (100 + 50f * Mathf.Pow(urban, 0.5f)), 2)); //Calculates percent difference between the target value and the actual value
            AddScore(country.Name, 2 * match * PreferencePageScript.slecweightUrb); //Adds the score to the country in the dictionary
        }

    }
    public static void FindEcon() //Find the temperature score of every country and add it to their score
    {
        //Iterate through all countries in the dictionary
        foreach (var country in countries.Values)
        {
            float countryWage = float.Parse(country.YearlyWage.Trim());
            float countryExpense = float.Parse(country.Expenses.Trim());
            float score = (Mathf.Abs(countryWage - countryExpense)) / countryWage; //Calculates percent difference between the target value and the actual value
            AddScore(country.Name, score); //Adds the score to the country in the dictionary
        }
    }

    public static void FindCitizenshipDifficulty()
    {
        foreach (var country in countries.Values)
        {
            int index = CitizenshipDifficulties.IndexOf(country.Name);
            AddScore(country.Name, 2 - index/25);
        }
    }

    public static void FindDistance(float dist, string home) //Find the distance score of every country and add it to their score
    {
        Country pos = countries[home]; //Use the name of the home country to get the information for it

        //Iterate through all countries in the dictionary
        foreach (var country in countries.Values)
        {
            float distance = CalcDistance(pos, country); //Find the real world distance of the two countries
            float match = 0;
            if (dist == distance)
            {
                AddScore(country.Name, 1); //Check if the country is the exact distance away and if yes, give the highest score
            }
            else
            {
                match = Mathf.Max(0, 1 - Mathf.Pow((distance - dist) / (100 + 50f * Mathf.Pow(dist, 0.5f)), 2)); //Calculate percent difference between the target value and the actual value
                AddScore(country.Name, match/2 * PreferencePageScript.slecweightDist); //Add the score to the country in the dictionary
            }
        }
    }
    public static void FindEnemies(string home) //Find any enemies of the home country
    {
        foreach (var country in countries.Values)
        {
            if (enemies.ContainsKey(home)) //Check if home country has any enemies
            {
                if (enemies[home].Contains(country.Name)) //If country is an enemy of the home country, set score equal to 0
                {
                    countryScores[country.Name] = 0;
                }
            }
        }
    }
}


