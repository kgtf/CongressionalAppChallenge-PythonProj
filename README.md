# NewRoots

NewRoots is a cross-platform Unity application that helps users compare European countries based on personal preferences and practical factors relevant to relocation.

The project was developed collaboratively by [Krish](https://github.com/kgtf), [Aarav](https://github.com/APcool-dot), and [Ian](https://github.com/IanBarrey) for the PA Media & Design Competition. We spent approximately **210 hours** planning, developing, testing, and refining the application.

## Overview

NewRoots was inspired by a Model UN project focused on refugee resettlement during the Russo-Ukrainian war. One portion of our resolution proposed comparing potential destination countries based on several factors. We decided to turn that concept into a working application.

The goal was to create a tool that could compare European countries using user preferences while remaining accessible to people from different backgrounds and language groups.

<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 57 09 PM" src="https://github.com/user-attachments/assets/b930e44c-8a6a-4ba9-af44-5d4f09bcc67f" />
<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 57 17 PM" src="https://github.com/user-attachments/assets/f76b078b-1ccf-4d40-b81c-5bd310feda05" />
<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 57 25 PM" src="https://github.com/user-attachments/assets/baa8d59a-2e0c-4a9f-95b7-07667654f27c" />
<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 56 25 PM" src="https://github.com/user-attachments/assets/f42d9ea7-feb0-4aa1-8867-323f2bdf87fe" />
<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 56 35 PM" src="https://github.com/user-attachments/assets/c8baca44-e5b2-44fd-adae-7466e1586083" />
<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 56 50 PM" src="https://github.com/user-attachments/assets/8b2fcfbb-26cc-4276-856c-fd07e9b39e0d" />
<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 56 57 PM" src="https://github.com/user-attachments/assets/534ad9bf-c476-4576-8c4d-0e1bdbfc9c8c" />
<img width="1202" height="632" alt="Screenshot 2026-09-14 at 7 57 00 PM" src="https://github.com/user-attachments/assets/69b6175a-a7bd-4e98-9ecd-ce4736ed3f13" />

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
