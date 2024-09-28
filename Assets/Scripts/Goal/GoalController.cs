using System.Collections;
using UnityEngine;

public class GoalController : MonoBehaviour
{
    public static GoalController instance;
    public bool GameFinished = false;
    [HideInInspector] public GameObject successCanvas;
    private GameObject AccidentCanvas;
    [SerializeField] public int levelRequireGoal;
    public int collectedGoal;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        successCanvas = GameObject.FindGameObjectWithTag("SuccessCanvas");
        AccidentCanvas = GameObject.FindGameObjectWithTag("AccidentCanvas");
        collectedGoal = 0;
    }
    private void Update()
    {
        if(collectedGoal >= levelRequireGoal && PlayerPrefs.GetInt("FINAL_ACCIDENT_HAPPENED") != 1)
        {
            GameFinished = true;
            LevelController.instance.IsGameInProgress = false;
            AccidentCanvas.SetActive(false);
            successCanvas.SetActive(true);
            successCanvas.GetComponent<Animator>().SetTrigger("ShowPolice");
            StartCoroutine(SimulateSlowMotion(1));
        }
    }
    public int GetLevelRequireGoal()
    {
        return levelRequireGoal;
    }
    IEnumerator SimulateSlowMotion(int firstTime)
    {
        if (firstTime == 1)
        {
            yield return new WaitForSeconds(3.5f);
            StartCoroutine(SimulateSlowMotion(2));
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            if (Time.timeScale <= 0.2)
                Time.timeScale = 0f;
            else
                Time.timeScale -= 0.20f;
            StartCoroutine(SimulateSlowMotion(2));
        }
    }
}
