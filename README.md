# StandardConsumption
Standard energy consumption related to outside temperature

Följande applikation är ett test för att se om vi kan skapa en automatiserad uträkning av Normalårskorrigerad förbrukning.
Underlag är informationsmöte och exempelfil "Exempel på beräkning_ver2.xlsx".
Applikationsfoldern innehåller en Excel-fil "values.xlsx". Kolumn A innehåller x-värden med Dygnsmedeltemperatur. Kolumn B innehåller y-värden med Förbrukning per dag (MWh). Överför önskad data till denna fil för beräkning.
För närvarande innehåller filen samma data som i exempelfil.

För att sen köra programmet - Dubbelklicka på filen "StandardConsumption.exe".
Programmet gör följande:
- Läser in värden från Excel-fil
- Tar fram max- och min dygnstemperatur (ex -14/19)
- Mellan dessa värden, för varje heltal/gränsvärde, görs test för att finna modell med minst avvikelse
- Linjär regression beräknas för x-värden under gränsvärdet med tillhörande y-värden (ex y = 1.5390000000001147 - 0.0099999999999909051x)
- För återstående y-värden beräknas ett genomsnitt (ex y = 0.74632450331125877)
- Därefter jämförs samtliga x-värden och y-värden från modellen mot orginalvärden. 
  Om x är mindre är gränsvärdet används linjär ekvation (difference = intercept + slope * xValue - yValue)
  Om x är större än gränsvärdet används konstant värde (difference = återstående ygenomsnitt - yValue)
- Skillnaderna mot orginalet för varje mätpunkt sparas och jämförs med övriga heltalstemperaturer/gränsvärden

Programmet levererar värden för modell med minimal avvikelse:
- Gränsvärde
- RSquared för linjär regression
- Intercept för linjär regression
- Slope för linjär regression
- MValue för oberoende kurva efter gränsvärde