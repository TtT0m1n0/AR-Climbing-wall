// používame aby sme si niekde ukladali akú wall a aku rouzte sme si zvolili

using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string selectedWallId = "wall_001";

    // sem si uložíme cestu k JSON súboru steny
    public string selectedWallPath;

    // sem si uložíme ID trasy, ktorú chce používateľ
    public string selectedRouteId;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
