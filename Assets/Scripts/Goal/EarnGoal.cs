using UnityEngine;
using TMPro;

public class EarnGoal : MonoBehaviour
{
    public static EarnGoal instance;
    [SerializeField] Animator EarnAnimator;
    [SerializeField] Animator EarnDollarSign;
    [SerializeField] Animator EarnDollarValue;
    [SerializeField] TextMeshProUGUI carValueText;
    private CarMovement car;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        //UpPoint - DownCar
        if(gameObject.CompareTag("UpGoal") && other.CompareTag("DownSide"))
        {
            car = other.GetComponent<CarMovement>();
            EarnAnimator.SetTrigger("EarnGoal");
            GoalController.instance.collectedGoal += car.GetValue();
            if (car.accidentHappend == false && LevelController.instance.IsGameInProgress)
            {
                int CurrentDollar = DollarCounter.instance.totallDollarEarned;
                ProgressBar.instance.IncreaseSliderValue(car.GetValue());
                EarnDollarSign.SetTrigger("DollarEarned");
                carValueText.text = (CurrentDollar + CarMovement.instance.GetValue()).ToString();
                DollarCounter.instance.totallDollarEarned += car.GetValue();
                EarnDollarValue.SetTrigger("DollarEarned");
            }
        }
        //LeftPoint - RightCar
        else if(gameObject.CompareTag("LeftGoal") && other.CompareTag("RightSide"))
        {
            car = other.GetComponent<CarMovement>();
            EarnAnimator.SetTrigger("EarnGoal");
            GoalController.instance.collectedGoal += car.GetValue();
            if (car.accidentHappend == false && LevelController.instance.IsGameInProgress)
            {
                int CurrentDollar = DollarCounter.instance.totallDollarEarned;
                ProgressBar.instance.IncreaseSliderValue(car.GetValue());
                EarnDollarSign.SetTrigger("DollarEarned");
                carValueText.text = (CurrentDollar + CarMovement.instance.GetValue()).ToString();
                DollarCounter.instance.totallDollarEarned += car.GetValue();
                EarnDollarValue.SetTrigger("DollarEarned");
            }
        }
        //DownPoint - UpCar
        else if(gameObject.CompareTag("DownGoal") && other.CompareTag("UpSide"))
        {
            car = other.GetComponent<CarMovement>();
            EarnAnimator.SetTrigger("EarnGoal");
            GoalController.instance.collectedGoal+= car.GetValue();
            if (car.accidentHappend == false && LevelController.instance.IsGameInProgress)
            {
                int CurrentDollar = DollarCounter.instance.totallDollarEarned;
                ProgressBar.instance.IncreaseSliderValue(car.GetValue());
                EarnDollarSign.SetTrigger("DollarEarned");
                carValueText.text = (CurrentDollar + CarMovement.instance.GetValue()).ToString();
                DollarCounter.instance.totallDollarEarned += car.GetValue();
                EarnDollarValue.SetTrigger("DollarEarned");
            }
        }
        //RightPoint - LeftCar
        else if(gameObject.CompareTag("RightGoal") && other.CompareTag("LeftSide"))
        {
            car = other.GetComponent<CarMovement>();
            EarnAnimator.SetTrigger("EarnGoal");
            GoalController.instance.collectedGoal += car.GetValue();
            if (car.accidentHappend == false && LevelController.instance.IsGameInProgress)
            {
                int CurrentDollar = DollarCounter.instance.totallDollarEarned;
                ProgressBar.instance.IncreaseSliderValue(car.GetValue());
                EarnDollarSign.SetTrigger("DollarEarned");
                carValueText.text = (CurrentDollar + CarMovement.instance.GetValue()).ToString();
                DollarCounter.instance.totallDollarEarned += car.GetValue();
                EarnDollarValue.SetTrigger("DollarEarned");
            }
        }
    }
}
