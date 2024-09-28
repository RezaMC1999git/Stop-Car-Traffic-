using UnityEngine;

public class CarTouched : MonoBehaviour
{
    public bool amITouched = false;
    public Animator TouchAnimator;
    public static CarTouched instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Update()
    {
        if(amITouched && true)
        {
            TouchAnimator.SetTrigger("Touched");
            amITouched = false;
        }
    }
}
