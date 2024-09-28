using System.Collections;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    public static CarMovement instance;
    [HideInInspector] public bool FinalAccidentHappenedDontShowSuccessPanel = false;
    [SerializeField] AudioClip CrashSFX;
    private bool ShouldIPlayTheCrashSFX = true;
    private AudioSource audioSource;
    public GameObject AccidentCanvas;
    private GameObject SuccessCanvas;
    [SerializeField] public bool[] canMoveDownSide;
    [SerializeField] public bool[] canMoveRightSide;
    [SerializeField] public bool[] canMoveUpSide;
    [SerializeField] public bool[] canMoveLeftSide;
    [SerializeField] public int numberOfCar;
    private Animator KharFly;
    public int Value;
    private GameObject EgzozParticle;
    [HideInInspector] public int howManyCarsUnlocked;
    public bool accidentHappend = false;
    public float speed = 0;
    private Rigidbody2D MyRB;
    private void Awake()
    {
        AccidentCanvas = GameObject.FindGameObjectWithTag("AccidentCanvas");
        SuccessCanvas = GameObject.FindGameObjectWithTag("SuccessCanvas");
        EgzozParticle = transform.GetChild(2).gameObject;
        audioSource = GetComponent<AudioSource>();
        howManyCarsUnlocked = GameController.instance.GetCarsNumber();
        if (gameObject.CompareTag("KharUpSide") || gameObject.CompareTag("KharLeftSide") ||
            gameObject.CompareTag("KharDownSide") || gameObject.CompareTag("KharRightSide"))
            KharFly = GetComponent<Animator>();
        MyRB = GetComponent<Rigidbody2D>();
        if (instance == null)
            instance = this;
        if (LevelController.instance.isLevelDignotical)
            RotateCars();
        
    }
    private void Start()
    {
        InitialCars();
    }
    void InitialCars()
    {
        canMoveDownSide = new bool[howManyCarsUnlocked];
        canMoveLeftSide = new bool[howManyCarsUnlocked];
        canMoveUpSide = new bool[howManyCarsUnlocked];
        canMoveRightSide = new bool[howManyCarsUnlocked];

        for(int i = 0; i < canMoveDownSide.Length; i++)
        {
            canMoveDownSide[i] = false;
        }

        for (int i = 0; i < canMoveLeftSide.Length; i++)
        {
            canMoveLeftSide[i] = false;
        }

        for (int i = 0; i < canMoveUpSide.Length; i++)
        {
            canMoveUpSide[i] = false;
        }

        for (int i = 0; i < canMoveRightSide.Length; i++)
        {
            canMoveRightSide[i] = false;
        }

        if (gameObject.CompareTag("UpSide") || gameObject.CompareTag("KharUpSide"))
            canMoveDownSide[numberOfCar - 1] = true;
        if (gameObject.CompareTag("LeftSide") || gameObject.CompareTag("KharLeftSide"))
            canMoveRightSide[numberOfCar - 1] = true;
        if (gameObject.CompareTag("DownSide") || gameObject.CompareTag("KharDownSide"))
            canMoveUpSide[numberOfCar - 1] = true;
        if (gameObject.CompareTag("RightSide") || gameObject.CompareTag("KharRightSide"))
            canMoveLeftSide[numberOfCar - 1] = true;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction,100);
            if(hit.collider != null)
            {
                if (hit.collider.GetComponent<CarMovement>() != null)
                {
                    int touchedCar = hit.collider.GetComponent<CarMovement>().numberOfCar;
                    if (hit.collider.CompareTag("UpSide"))
                    {
                        if (canMoveDownSide[touchedCar - 1] == true)
                            EgzozParticle.SetActive(false);
                        if (canMoveDownSide[touchedCar - 1] == false)
                            EgzozParticle.SetActive(true);
                        canMoveDownSide[touchedCar - 1] = !canMoveDownSide[touchedCar - 1];
                    }

                    if (hit.collider.CompareTag("LeftSide"))
                    {
                        if (canMoveRightSide[touchedCar - 1] == true)
                            EgzozParticle.SetActive(false);
                        if (canMoveRightSide[touchedCar - 1] == false)
                            EgzozParticle.SetActive(true);
                        canMoveRightSide[touchedCar - 1] = !canMoveRightSide[touchedCar - 1];
                    }

                    if (hit.collider.CompareTag("DownSide"))
                    {
                        if (canMoveUpSide[touchedCar - 1] == true)
                            EgzozParticle.SetActive(false);
                        if (canMoveUpSide[touchedCar - 1] == false)
                            EgzozParticle.SetActive(true);
                        canMoveUpSide[touchedCar - 1] = !canMoveUpSide[touchedCar - 1];
                    }

                    if (hit.collider.CompareTag("RightSide"))
                    {
                        if (canMoveLeftSide[touchedCar - 1] == true)
                            EgzozParticle.SetActive(false);
                        if (canMoveLeftSide[touchedCar - 1] == false)
                            EgzozParticle.SetActive(true);
                        canMoveLeftSide[touchedCar - 1] = !canMoveLeftSide[touchedCar - 1];
                    }

                    //Khar Part !!
                    if (hit.collider.CompareTag("KharLeftSide"))
                    {
                        int KharLeftSideTask = Random.Range(1, 4);
                        if (KharLeftSideTask == 1)
                            canMoveRightSide[touchedCar - 1] = !canMoveRightSide[touchedCar - 1];
                        if (KharLeftSideTask == 2)
                            MyRB.velocity = new Vector2(0f, -transform.lossyScale.y) * speed;
                        if (KharLeftSideTask == 3)
                        {
                            speed += 2;
                            //Play Horse Sound
                        }
                        if (KharLeftSideTask == 4)
                            KharFly.SetTrigger("Fly");
                    }

                    //Khar Part !!
                    if (hit.collider.CompareTag("KharDownSide"))
                    {
                        int KharDownSideTask = Random.Range(1, 4);
                        if (KharDownSideTask == 1)
                            canMoveUpSide[touchedCar - 1] = !canMoveUpSide[touchedCar - 1];
                        if (KharDownSideTask == 2)
                            MyRB.velocity = new Vector2(0f, -transform.lossyScale.y) * speed;
                        if (KharDownSideTask == 3)
                        {
                            speed += 2;
                            //Play Horse Sound
                        }
                        if (KharDownSideTask == 4)
                            KharFly.SetTrigger("Fly");
                    }

                    //Khar Part !!
                    if (hit.collider.CompareTag("KharRightSide"))
                    {
                        int KharRightSideTask = Random.Range(1, 4);
                        if (KharRightSideTask == 1)
                            canMoveLeftSide[touchedCar - 1] = !canMoveLeftSide[touchedCar - 1];
                        if (KharRightSideTask == 2)
                            MyRB.velocity = new Vector2(0f, -transform.lossyScale.y) * speed;
                        if (KharRightSideTask == 3)
                        {
                            speed += 2;
                            //Play Horse Sound
                        }
                        if (KharRightSideTask == 4)
                            KharFly.SetTrigger("Fly");
                    }

                    //Khar Part !!
                    if (hit.collider.CompareTag("KharUpSide"))
                    {
                        int KharUpSideTask = Random.Range(1, 4);
                        if (KharUpSideTask == 1)
                            canMoveDownSide[touchedCar - 1] = !canMoveDownSide[touchedCar - 1];
                        if (KharUpSideTask == 2)
                            MyRB.velocity = new Vector2(0f, -transform.lossyScale.y) * speed;
                        if (KharUpSideTask == 3)
                        {
                            speed += 2;
                            //Play Horse Sound
                        }
                        if (KharUpSideTask == 4)
                            KharFly.SetTrigger("Fly");
                    }

                    if (hit.collider.CompareTag("KharLeftSide"))
                    {
                        canMoveRightSide[touchedCar - 1] = !canMoveRightSide[touchedCar - 1];
                    }
                    if (hit.collider.CompareTag("KharDownSide"))
                    {
                        canMoveUpSide[touchedCar - 1] = !canMoveUpSide[touchedCar - 1];
                    }
                    if (hit.collider.CompareTag("KharRightSide"))
                    {
                        canMoveLeftSide[touchedCar - 1] = !canMoveLeftSide[touchedCar - 1];
                    }
                }
                else
                {
                    if(gameObject.tag == "NextLevelButton" || gameObject.tag == "2XNextLevelButton")
                    {

                    }
                }
            }
        }
    }
    private void FixedUpdate()
    {
        if(LevelController.instance.isLevelDignotical== false)
        {
            if (gameObject.CompareTag("UpSide") || gameObject.CompareTag("KharUpSide"))
            {
                if (canMoveDownSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(0f, -transform.lossyScale.y) * speed;
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }

            if (gameObject.CompareTag("LeftSide") || gameObject.CompareTag("KharLeftSide"))
            {
                if (canMoveRightSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(transform.lossyScale.x, 0f) * speed;
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }

            if (gameObject.CompareTag("DownSide") || gameObject.CompareTag("KharDownSide"))
            {
                if (canMoveUpSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(0f, transform.lossyScale.y) * speed;
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }

            if (gameObject.CompareTag("RightSide") || gameObject.CompareTag("KharRightSide"))
            {
                if (canMoveLeftSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(-transform.lossyScale.y, 0f) * speed;
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }
        }
        else
        {
            if (gameObject.CompareTag("UpSide"))
            {
                if (canMoveDownSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(-transform.lossyScale.x, -transform.lossyScale.y) * (speed - 0.35f);
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }

            if (gameObject.CompareTag("LeftSide"))
            {
                if (canMoveRightSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(transform.lossyScale.x, -transform.lossyScale.y) * (speed - 0.55f);
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }

            if (gameObject.CompareTag("DownSide"))
            {
                if (canMoveUpSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(transform.lossyScale.x, transform.lossyScale.y) * (speed - 0.35f);
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }

            if (gameObject.CompareTag("RightSide"))
            {
                if (canMoveLeftSide[numberOfCar - 1])
                    MyRB.velocity = new Vector2(-transform.lossyScale.x, transform.lossyScale.y) * (speed - 0.55f);
                else
                    MyRB.velocity = new Vector2(0f, 0f);
            }
        }
    }

    void RotateCars()
    {
        //Rotate Car
        Vector3 tempo = transform.rotation.eulerAngles;
        tempo.z -= LevelController.instance.GetLevelDegree();
        transform.rotation = Quaternion.Euler(tempo);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
            if (gameObject.tag != other.gameObject.tag)
            {
                accidentHappend = true;
                if (ShouldIPlayTheCrashSFX)
                {
                    if(other.gameObject.GetComponent<CarMovement>() == true)
                        other.gameObject.GetComponent<CarMovement>().ShouldIPlayTheCrashSFX = false;
                    audioSource.PlayOneShot(CrashSFX);
                    ShouldIPlayTheCrashSFX = false;
                }
                if(GoalController.instance.GameFinished == false)
                {
                    StartCoroutine(SimulateSlowMotion(1));
                    StartCoroutine(StopTheCarWithDelay());
                    PlayerPrefs.SetInt("FINAL_ACCIDENT_HAPPENED", 1);
                    SuccessCanvas.SetActive(false);
                    AccidentCanvas.SetActive(true);
                    AccidentCanvas.GetComponent<Animator>().SetTrigger("Accident");
                    LevelController.instance.IsGameInProgress = false;
                }
            }
            if (other.gameObject.GetComponent<CarMovement>() == true)
            {
                float otherCarSpeed = other.gameObject.GetComponent<CarMovement>().speed;
                if (speed <= otherCarSpeed && accidentHappend == false)
                    UnlockAccidentedCar(other.gameObject.GetComponent<CarMovement>().numberOfCar, other.gameObject.tag, other.gameObject);
            }
    }
     void UnlockAccidentedCar(int numberOfCar,string TagName,GameObject OtherCar)
     {
        if(TagName == "UpSide")
        {
            OtherCar.GetComponent<CarMovement>().canMoveDownSide[numberOfCar - 1] = true;
            if (speed >= 2.5)
                OtherCar.GetComponent<CarMovement>().speed = speed;
            else
                OtherCar.GetComponent<CarMovement>().speed = speed + 1f;
        }
        if (TagName == "LeftSide")
        {
            OtherCar.GetComponent<CarMovement>().canMoveRightSide[numberOfCar - 1] = true;
            if (speed >= 2.5)
                OtherCar.GetComponent<CarMovement>().speed = speed;
            else
                OtherCar.GetComponent<CarMovement>().speed = speed + 1f;
        }
        if (TagName == "DownSide")
        {
            OtherCar.GetComponent<CarMovement>().canMoveUpSide[numberOfCar - 1] = true;
            if (speed >= 2.5)
                OtherCar.GetComponent<CarMovement>().speed = speed;
            else
                OtherCar.GetComponent<CarMovement>().speed = speed + 1f;
        }
        if (TagName == "RightSide")
        {
            OtherCar.GetComponent<CarMovement>().canMoveLeftSide[numberOfCar - 1] = true;
            if (speed >= 2.5)
                OtherCar.GetComponent<CarMovement>().speed = speed;
            else
                OtherCar.GetComponent<CarMovement>().speed = speed + 1f;
        }
    }
   IEnumerator SimulateSlowMotion(int firstTime)
   {
        if(firstTime == 1)
        {
            yield return new WaitForSeconds(3.5f);
            StartCoroutine(SimulateSlowMotion(2));
        }
        else
        {
            yield return new WaitForSeconds(0.2f);
            if (Time.timeScale <= 0.3)
                Time.timeScale = 0f;
            else
                Time.timeScale -= 0.25f;
            StartCoroutine(SimulateSlowMotion(2));
        }
   }
    IEnumerator StopTheCarWithDelay()
    {
        while (speed > 0)
        {
            speed = speed / 2;
            yield return new WaitForSeconds(0.2f);
            if (speed <= 0.25f)
            {
                speed = 0f;
                break;
            }
        }
    }
    public int GetValue()
    {
        return Value;
    }
}
