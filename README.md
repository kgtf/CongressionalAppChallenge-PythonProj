# NewRoots

NewRoots is a cross-platform Unity application that helps users compare European countries based on personal preferences and practical factors relevant to relocation.

The project was developed collaboratively by [Krish]([url](https://github.com/kgtf)), [Aarav]([url](https://github.com/APcool-dot)), and [Ian]([url](https://github.com/IanBarrey)) for the PA Media & Design Competition. We spent approximately **210 hours** planning, developing, testing, and refining the application.

## Overview

NewRoots was inspired by a Model UN project focused on refugee resettlement during the Russo-Ukrainian war. One portion of our resolution proposed comparing potential destination countries based on several factors. We decided to turn that concept into a working application.

The goal was to create a tool that could compare European countries using user preferences while remaining accessible to people from different backgrounds and language groups.

<img width="2280" height="1080" alt="Screenshot_20250227-143434_New Roots" src="https://github.com/user-attachments/assets/bcee0d59-d6c4-4de3-b6c4-34615f3d8d01" /><img width="2280" height="1080" alt="Screenshot_20250227-143604_New Roots" src="https://github.com/user-attachments/assets/e0f39b79-aae5-4e86-a0ca-9240e5b14d87" />
<img width="2280" height="1080" alt="Screenshot_20250227-143551_New Roots" src="https://github.com/user-attachments/assets/1b3ed702-7fd0-4843-b080-fea77dbffbbf" />
<img width="2280" height="1080" alt="Screenshot_20250227-143533_New Roots" src="https://github.com/user-attachments/assets/da5dafed-ed73-440d-a3ab-af2d570b4660" />
<img width="2280" height="1080" alt="Screenshot_20250227-143520_New Roots" src="https://github.com/user-attachments/assets/2d22c07f-8276-44d5-ad74-001c89f6822a" />
<img width="2280" height="1080" alt="Screenshot_20250227-143458_New Roots" src="https://github.com/user-attachments/assets/69aa33f9-f036-475d-ad1e-7ab39e8df06b" />
## Features

* Interactive map of Europe
* Individual Unity objects for each supported country
* Country recommendation and scoring system
* Comparison based on multiple user-selected factors
* Geographic distance calculations
* Multi-language localization
* Cross-platform builds for:

  * macOS
  * Windows
  * iOS
  * Android

## Country Recommendation System

The application stores information for European countries and compares that information against preferences supplied by the user.

Factors considered include:

* Temperature
* Language
* Religion
* Ethnicity
* Population
* Urbanization
* Wages
* Expenses
* Geographic distance

Because these variables use very different numerical scales, simply adding the values together would not produce meaningful results. We therefore developed a scoring system that uses an **inverse quadratic function** to normalize the effect of different variables and control how strongly each factor influences a country's overall score.

Countries are then ranked according to how closely they match the user's preferences.

## Geographic Distance

We wanted distance between countries to reflect real geography rather than simply measuring positions on the Unity map.

To accomplish this, we created a dataset containing the latitude and longitude of European capitals and used geographic calculations to estimate capital-to-capital distances while accounting for the curvature of the Earth.

These values could then be incorporated into the country scoring system.

## Interactive Map

The map was one of the core parts of the project.

Each country was separated into its own object within Unity so that it could be individually selected, displayed, and manipulated by the application.

This structure allowed the interface and recommendation system to interact directly with individual countries.

## Localization

Because the application was designed with refugees and international users in mind, accessibility across languages was an important part of the project.

We used the **Unity Localization Package** along with spreadsheet-based translation data to implement support for multiple languages commonly spoken in Europe.

## Technologies

* Unity
* C#
* Visual Studio 2022
* Git
* GitHub
* Unity Localization Package
* Microsoft Excel
* Google Sheets
* Xcode
* Canva

GitHub was used throughout development for version control and collaboration between all three team members.

## Platforms

NewRoots was built and tested for:

* macOS
* Windows
* iOS
* Android

### macOS

1. Download the `.dmg` file from the **Releases** section.
2. Open the disk image.
3. Open or install the NewRoots `.app`.

### Windows

1. Download the Windows `.zip` file from the **Releases** section.
2. Extract the archive.
3. Open the `.exe` displaying the NewRoots tree logo.
4. If Windows displays a security warning, select **More info** and then **Run anyway**.

## Development

Development began with planning and designing the structure of the application before implementation.

The project was built primarily in Unity using C#. Two of the three team members had not previously worked with C#, making much of the development process an opportunity to learn the language and Unity framework while building the application.

The team worked collaboratively throughout the project rather than dividing the application into completely separate components.

## User Testing

Before submitting the project, we had the opportunity to demonstrate NewRoots to a recent Ukrainian refugee, and receive feedback from the perspective of someone with direct experience relocating to another country.

His feedback highlighted several possible directions for future development, particularly adding:

* Housing information
* Street and address information
* Points of interest
* More detailed country information
* A more modern user interface

This helped us better understand the difference between selecting a destination and helping someone successfully settle there.

## AI and External Resources

ChatGPT was used as a learning resource while the team was becoming familiar with C# and Unity concepts.

The NewRoots logo was created using ChatGPT image generation.

The Europe map was based on:

> Ted Grajeda, *Vector Map of Europe with Countries – Single Color*, FreeVectorMaps.com, 2025.
