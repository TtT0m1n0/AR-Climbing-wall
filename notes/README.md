**Získavanie chytov z nasnímania steny (bez manuálneho ťukania):**

budeme si ukladať: \
⦁	konzistentné ID (poradie/číslovanie). \
⦁	ich 3D pozíciu v priestore, \
⦁	ich farbu (na určenie trasy alebo kategórie), 

Výsledný JSON (ukážka) \
{
  "holds": [
    {
      "id": 1,
      "position": { "x": 1.2, "y": 0.45, "z": -2.34 },
      "color": "FFFF00"
    },
    {
      "id": 2,
      "position": { "x": 1.4, "y": 0.52, "z": -2.35 },
      "color": "FFFF00"
    }
  ]
}
