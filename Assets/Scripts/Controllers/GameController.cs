using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public const string CURRENT_LEVEL = "CURRENT_LEVEL";
    public const string CURRENT_CASH = "CURRENT_CASH";
    public const string HOW_MANY_CARS_UNLOCKED = "HOW_MANY_CARS_UNLOCKED";
    public const string IS_GAME_STARTED_FROM_SPLASH = "IS_GAME_STARTED_FROM_SPLASH";

    private void Awake()
    {
        PlayerPrefs.DeleteAll();
        MakeSingleton();
        Initialize();
    }
    void MakeSingleton()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    void Initialize()
    {
        if (!PlayerPrefs.HasKey("FirstInitial")) 
        {
            PlayerPrefs.SetInt(CURRENT_LEVEL, 1);
            PlayerPrefs.SetInt(CURRENT_CASH, 0);
            PlayerPrefs.SetInt(IS_GAME_STARTED_FROM_SPLASH, 1);
            PlayerPrefs.SetInt(HOW_MANY_CARS_UNLOCKED, 4);
            PlayerPrefs.SetInt("FirstInitial", 1);
        }
        else
        {
            PlayerPrefs.SetInt(IS_GAME_STARTED_FROM_SPLASH, 1);
        }
    }

    //GETTERS AND SETTERS

    //Current_Level
    public void SetCurrentLevel(int level)
    {
        PlayerPrefs.SetInt(CURRENT_LEVEL, level);
    }
    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(CURRENT_LEVEL);
    }
    
    //Current_Level
    public void SetCurrentCash(int cash)
    {
        PlayerPrefs.SetInt(CURRENT_CASH, cash);
    }
    public int GetCurrentCash()
    {
        return PlayerPrefs.GetInt(CURRENT_CASH);
    }

    //is_Game_Started_From_Splash
    public void SetGameStartedFromSplash(int cash)
    {
        PlayerPrefs.SetInt(IS_GAME_STARTED_FROM_SPLASH, cash);
    }
    public int GetGameStartedFromSplash()
    {
        return PlayerPrefs.GetInt(IS_GAME_STARTED_FROM_SPLASH);
    }

    //How_Many_Cars_Unlocked
    public void SetCarsNumber(int number)
    {
        PlayerPrefs.SetInt(HOW_MANY_CARS_UNLOCKED, number);
    }
    public int GetCarsNumber()
    {
        return PlayerPrefs.GetInt(HOW_MANY_CARS_UNLOCKED);
    }
}
