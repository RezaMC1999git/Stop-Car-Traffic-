using System.Collections;
using UnityEngine;

public class CreateAndKillTrain : MonoBehaviour
{
    [SerializeField] private GameObject[] Trains;
    public int waitUntilNextRespawn;
    private bool createFirstTrain = true;
    public bool isTrainGoingDown = false;
    public bool isTrainGoingRight = false;
    public bool isTrainGoingUp = false;
    public bool isTrainGoingLeft = false;
    private void Start()
    {
        StartCoroutine(SpawnNewTrain());
    }
    IEnumerator SpawnNewTrain()
    {
        bool CreateUpTrainOnce = true;
        bool CreateLeftTrainOnce = true;
        bool CreateDownTrainOnce = true;
        bool CreateRightTrainOnce = true;
        if(createFirstTrain)
            yield return new WaitForSeconds(1.5f);
        if(createFirstTrain == false)
            yield return new WaitForSeconds(waitUntilNextRespawn);
        createFirstTrain = false;
        if (isTrainGoingDown && CreateUpTrainOnce)
        { 
            Instantiate(Trains[0], transform.GetChild(0).position,Quaternion.identity);
            CreateUpTrainOnce = false;
        }

        if (isTrainGoingRight && CreateLeftTrainOnce)
        {
            Instantiate(Trains[1], transform.GetChild(0).position, Quaternion.Euler(0f, 0f, -90));
            CreateLeftTrainOnce = false;
        }

        if (isTrainGoingUp && CreateDownTrainOnce)
        {
            Instantiate(Trains[2], transform.GetChild(0).position, Quaternion.identity);
            CreateDownTrainOnce = false;
        }

        if (isTrainGoingLeft && CreateRightTrainOnce)
        {
            Instantiate(Trains[3], transform.GetChild(0).position, Quaternion.Euler(0f, 0f, -90));
            CreateRightTrainOnce = false;
        }
        StartCoroutine(SpawnNewTrain());
    }
}
