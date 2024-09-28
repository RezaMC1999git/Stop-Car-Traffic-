using UnityEngine;

public class AllCars : MonoBehaviour
{
    public static AllCars instance;
    [SerializeField] public GameObject[] UpCars;
    [SerializeField] public GameObject[] LeftCars;
    [SerializeField] public GameObject[] DownCars;
    [SerializeField] public GameObject[] RightCars;
    private void Awake()
    {
        MakeSingleton();
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
}
