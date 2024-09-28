using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using RTLTMPro;

public class LevelController : MonoBehaviour
{
    private bool GoToSpawnCarsCourotineOnce = true;
    private bool IsGameStartedFromSplashScreen;
    public static LevelController instance;
    int showCashCounter = 0;
    //UI Canvases
    public GameObject pauseCanvas;
    //PauseCanvas Buttons
    private RTLTextMeshPro cashText;
    private Button startGameBTN;
    private Button carsSectionBTN;
    private Button showCashBTN;
    //SuccessCanvas Buttons
    private Button nextLevelBTN;
    private Button TwoXNextLevelBTN;
    //AccidentCanvas Buttons
    private Button restartGameBTN;
    private Button TwoXRestartGameBTN;

    //For Diagnal Levels
    public bool isLevelDignotical = false;
    public float LevelDegree = 0;
    public bool IsGameInProgress = false;
    int whichWay;
    int howManyCarsUnlocked;
    [SerializeField] GameObject [] spawnPoints;
    [Tooltip("Min Value Is 1 Second")]
    public float MinTimeToWait;
    public float MaxTimeToWait;
    [HideInInspector] public int lastUpCarSpawned = -1, lastLeftCarSpawned = -1,
        lastDownCarSpawned = -1, lastRightCarSpawned = -1;
    [SerializeField] public GameObject[] UpCars;
    [SerializeField] public GameObject[] LeftCars;
    [SerializeField] public GameObject[] DownCars;
    [SerializeField] public GameObject[] RightCars;
    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI currentLevelText;
    [SerializeField] private TextMeshProUGUI nextLevelText;

    private void Awake()
    {
        Time.timeScale = 1;
        if (instance == null)
            instance = this;
        howManyCarsUnlocked = GameController.instance.GetCarsNumber();
        TakeLevelCars();
        cashText = GameObject.FindGameObjectWithTag("CashText").GetComponent<RTLTextMeshPro>();
        cashText.text = (GameController.instance.GetCurrentCash()+" $").ToString();
        PlayerPrefs.SetInt("FINAL_ACCIDENT_HAPPENED", 0);
        InitialiseUIButtons();
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        if (GameController.instance.GetGameStartedFromSplash() == 1) 
        {
            IsGameStartedFromSplashScreen = true;
            GameController.instance.SetGameStartedFromSplash(0);
        }
        else if (GameController.instance.GetGameStartedFromSplash() == 0)
            IsGameStartedFromSplashScreen = false;

        if (IsGameStartedFromSplashScreen)
        {
            IsGameInProgress = true;
            pauseCanvas.SetActive(false);
        }
        if (!IsGameStartedFromSplashScreen)
        {
            IsGameInProgress = false;
            pauseCanvas.SetActive(true);
        }
        pauseCanvas = GameObject.FindGameObjectWithTag("PauseCanvas");
        whichWay = Random.Range(0, spawnPoints.Length);
    }
    private void Start()
    {
        GetAndShowCurrentLevel();
    }

    void TakeLevelCars()
    {
        UpCars = new GameObject[howManyCarsUnlocked];
        LeftCars = new GameObject[howManyCarsUnlocked];
        DownCars = new GameObject[howManyCarsUnlocked];
        RightCars = new GameObject[howManyCarsUnlocked];
        for (int i=0 ; i<howManyCarsUnlocked ; i++)
        {
            UpCars[i] = AllCars.instance.UpCars[i].gameObject;
            LeftCars[i] = AllCars.instance.LeftCars[i].gameObject;
            DownCars[i] = AllCars.instance.DownCars[i].gameObject;
            RightCars[i] = AllCars.instance.RightCars[i].gameObject;
        }
    }
    void InitialiseUIButtons()
    {
        //PauseCanvas Buttons Initialize
        startGameBTN = GameObject.FindGameObjectWithTag("StartGameBTN").GetComponent<Button>();
        startGameBTN.onClick.RemoveAllListeners();
        startGameBTN.onClick.AddListener(() => StartGame());

        carsSectionBTN = GameObject.FindGameObjectWithTag("CarsSectionBTN").GetComponent<Button>();
        carsSectionBTN.onClick.RemoveAllListeners();
        carsSectionBTN.onClick.AddListener(() => GoToCarsSection());

        showCashBTN = GameObject.FindGameObjectWithTag("ShowCash").GetComponent<Button>();
        showCashBTN.onClick.RemoveAllListeners();
        showCashBTN.onClick.AddListener(() => SlideShowCash());

        //SuccessCanvas Buttons Initialize
        nextLevelBTN = GameObject.FindGameObjectWithTag("NextLevelBTN").GetComponent<Button>();
        nextLevelBTN.onClick.RemoveAllListeners();
        nextLevelBTN.onClick.AddListener(() => GoToNextLevel1X());

        TwoXNextLevelBTN = GameObject.FindGameObjectWithTag("2XNextLevelBTN").GetComponent<Button>();
        TwoXNextLevelBTN.onClick.RemoveAllListeners();
        TwoXNextLevelBTN.onClick.AddListener(() => GoToNextLevel2X());

        //AccidentCanvas Buttons Initialize
        restartGameBTN = GameObject.FindGameObjectWithTag("restartGameBTN").GetComponent<Button>();
        restartGameBTN.onClick.RemoveAllListeners();
        restartGameBTN.onClick.AddListener(() => RestartLevel1X());

        TwoXRestartGameBTN = GameObject.FindGameObjectWithTag("2XRestartGameBTN").GetComponent<Button>();
        TwoXRestartGameBTN.onClick.RemoveAllListeners();
        TwoXRestartGameBTN.onClick.AddListener(() => RestartLevel2X());
    }
    private void Update()
    {
        if (IsGameInProgress && GoToSpawnCarsCourotineOnce)
        {
            GoToSpawnCarsCourotineOnce = false;
            StartCoroutine(SpawnCar());
        }
    }
    void GetAndShowCurrentLevel() 
    {
        currentLevelText.text = GameController.instance.GetCurrentLevel().ToString();
        nextLevelText.text = (GameController.instance.GetCurrentLevel() + 1).ToString();
    }
    IEnumerator SpawnCar()
    {
        bool canSpawnUp = true;
        bool canSpawnLeft = true;
        bool canSpawnDown = true;
        bool canSpawnRight = true;
        yield return new WaitForSeconds(Random.Range(MinTimeToWait, MaxTimeToWait));
        //Spawn Car At UpSide( Not The Reapeted Car )
        if(spawnPoints[whichWay].CompareTag("UpSideSpawn") && canSpawnUp)
        {
            int tempUp = Random.Range(0, UpCars.Length);
            if (tempUp != lastUpCarSpawned)
            {
                Instantiate(UpCars[tempUp], spawnPoints[whichWay].transform, false);
                lastUpCarSpawned = tempUp;
            }
            canSpawnUp = false;
        }
        //Spawn Car At LeftSide( Not The Reapeted Car )
        if (spawnPoints[whichWay].CompareTag("LeftSideSpawn") && canSpawnLeft)
        {
            int tempLeft = Random.Range(0, LeftCars.Length);
            if (tempLeft != lastLeftCarSpawned)
            {
                Instantiate(LeftCars[tempLeft], spawnPoints[whichWay].transform, false);
                lastLeftCarSpawned = tempLeft;
            }
            canSpawnLeft = false;
        }
        //Spawn Car At DownSide( Not The Reapeted Car )
        if (spawnPoints[whichWay].CompareTag("DownSideSpawn") && canSpawnDown)
        {
            int tempDown = Random.Range(0, DownCars.Length);
            if (tempDown != lastDownCarSpawned)
            {
                Instantiate(DownCars[tempDown], spawnPoints[whichWay].transform, false);
                lastDownCarSpawned = tempDown;
            }
            canSpawnDown = false;
        }
        //Spawn Car At RightSide( Not The Reapeted Car )
        if (spawnPoints[whichWay].CompareTag("RightSideSpawn") && canSpawnRight)
        {
            int tempRight = Random.Range(0, RightCars.Length);
            if (tempRight != lastRightCarSpawned)
            {
                Instantiate(RightCars[tempRight], spawnPoints[whichWay].transform, false);
                lastRightCarSpawned = tempRight;
            }
            canSpawnRight = false;
        }
        ChooseNextWay(whichWay);
    }
    void ChooseNextWay(int way)
    {
        //If CurrentCar is UpSide Next Car Should Be Right or Left Side ( by chanse of 66.66 )
        if (spawnPoints[way].CompareTag(("UpSideSpawn")))
        {
            int chance = Random.Range(0, 4);
            whichWay = Random.Range(0, spawnPoints.Length);
            while (chance == 0 || chance == 1 || chance == 2)
            {
                if ((spawnPoints[whichWay].CompareTag("RightSideSpawn")) || (spawnPoints[whichWay].CompareTag("LeftSideSpawn")))
                {
                    break;
                }
                else if ((spawnPoints[whichWay].CompareTag("UpSideSpawn")) || (spawnPoints[whichWay].CompareTag("DownSideSpawn")))
                {
                    whichWay = Random.Range(0, spawnPoints.Length);
                    continue;
                }
            }
            if(chance == 3)
            {
                whichWay = Random.Range(0, spawnPoints.Length);
            }
        }

        //If CurrentCar is LeftSide Next Car Should Be Up or Down Side ( by chanse of 66.66 )
        if (spawnPoints[way].CompareTag(("LeftSideSpawn")))
        {
            int chance = Random.Range(0, 4);
            whichWay = Random.Range(0, spawnPoints.Length);
            while (chance == 0 || chance == 1 || chance == 2)
            {
                if (spawnPoints[whichWay].CompareTag("UpSideSpawn") || spawnPoints[whichWay].CompareTag("DownSideSpawn") )
                {
                    break;
                }
                if (spawnPoints[whichWay].CompareTag("LeftSideSpawn") || spawnPoints[whichWay].CompareTag("RightSideSpawn") )
                {
                    whichWay = Random.Range(0, spawnPoints.Length);
                    continue;
                }
            }
            if (chance == 3)
            {
                whichWay = Random.Range(0, spawnPoints.Length);
            }
        }

        //If CurrentCar is DownSide Next Car Should Be Right or Left Side ( by chanse of 66.66 )
        if (spawnPoints[way].CompareTag(("DownSideSpawn")))
        {
            int chance = Random.Range(0, 4);
            whichWay = Random.Range(0, spawnPoints.Length);
            while (chance == 0 || chance == 1 || chance == 2)
            {
                if (spawnPoints[whichWay].CompareTag("RightSideSpawn") || spawnPoints[whichWay].CompareTag("LeftSideSpawn") )
                {
                    break;
                }
                if (spawnPoints[whichWay].CompareTag("UpSideSpawn") || spawnPoints[whichWay].CompareTag("DownSideSpawn") )
                {
                    whichWay = Random.Range(0, spawnPoints.Length);
                    continue;
                }
            }
            if (chance == 3)
            {
                whichWay = Random.Range(0, spawnPoints.Length);
            }
        }

        //If CurrentCar is RightSide Next Car Should Be Up or Down Side ( by chanse of 66.66 )
        if (spawnPoints[way].CompareTag(("RightSideSpawn")))
        {
            int chance = Random.Range(0, 4);
            whichWay = Random.Range(0, spawnPoints.Length);
            while (chance == 0 || chance == 1 || chance == 2)
            {
                if (spawnPoints[whichWay].CompareTag("UpSideSpawn") || spawnPoints[whichWay].CompareTag("DownSideSpawn") )
                {
                    break;
                }
                if (spawnPoints[whichWay].CompareTag("RightSideSpawn") || spawnPoints[whichWay].CompareTag("LeftSideSpawn") )
                {
                    whichWay = Random.Range(0, spawnPoints.Length);
                    continue;
                }
            }
            if (chance == 3)
            {
                whichWay = Random.Range(0, spawnPoints.Length);
            }
        }

        if (IsGameInProgress)
            StartCoroutine(SpawnCar());
    }

    public float GetLevelDegree()
    {
        return LevelDegree;
    }
    public void GoToNextLevel1X()
    {
        int level = GameController.instance.GetCurrentLevel();
        int cash = GameController.instance.GetCurrentCash();
        cash += DollarCounter.instance.totallDollarEarned;
        level++;
        GameController.instance.SetCurrentCash(cash);
        if(level != 6)
            GameController.instance.SetCurrentLevel(level);
        else
            GameController.instance.SetCurrentLevel(5);
        if(level != 6)
            Application.LoadLevel("Level" + level);
        else
            Application.LoadLevel("Level5");
    }
    public void GoToNextLevel2X()
    {
        int level = GameController.instance.GetCurrentLevel();
        int cash = GameController.instance.GetCurrentCash();
        cash += (DollarCounter.instance.totallDollarEarned * 2);
        level++;
        GameController.instance.SetCurrentCash(cash);
        if (level != 6)
            GameController.instance.SetCurrentLevel(level);
        else
            GameController.instance.SetCurrentLevel(5);
        if (level != 6)
            Application.LoadLevel("Level" + level);
        else
            Application.LoadLevel("Level5");
    }
    public void RestartLevel1X()
    {
        int cash = GameController.instance.GetCurrentCash();
        cash += DollarCounter.instance.totallDollarEarned;
        GameController.instance.SetCurrentCash(cash);
        Application.LoadLevel(Application.loadedLevel);
    }
    public void RestartLevel2X()
    {
        int cash = GameController.instance.GetCurrentCash();
        cash += (DollarCounter.instance.totallDollarEarned * 2);
        GameController.instance.SetCurrentCash(cash);
        Application.LoadLevel(Application.loadedLevel);
    }
    public void StartGame()
    {
        IsGameInProgress = true;
        pauseCanvas.SetActive(false);
    }
    public void GoToCarsSection()
    {
        Application.LoadLevel("CarsSection");
    }
    public void SlideShowCash()
    {
        if(showCashCounter %2 == 0)
        {
            showCashBTN.GetComponent<Animator>().SetTrigger("In");
            showCashCounter++;
        }
        else
        {
            showCashBTN.GetComponent<Animator>().SetTrigger("Out");
            showCashCounter++;
        }
    }
}
