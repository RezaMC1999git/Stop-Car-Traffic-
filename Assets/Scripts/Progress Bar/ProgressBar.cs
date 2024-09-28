using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    private Slider slider;
    public Animator nextLevelButton;
    public static ProgressBar instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        slider = GetComponent<Slider>();      
    }
    private void Start()
    {
        slider.maxValue = GoalController.instance.GetLevelRequireGoal();
    }
    private void Update()
    {
        if (slider.value == GoalController.instance.GetLevelRequireGoal())
            nextLevelButton.SetTrigger("LevelPassed");
    }
    public void IncreaseSliderValue(int amount)
    {
        StartCoroutine(IncValue(amount));
    }
    
    IEnumerator IncValue(int amount)
    {
        yield return new WaitForSeconds(0.25f);
        GetComponent<Slider>().value+= amount;
    }
}
