using UnityEngine;

public class Warning : MonoBehaviour
{
    private Animator warningAnimator;
    private void Awake()
    {
        warningAnimator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("UpTrain") || other.CompareTag("LeftTrain") ||
            other.CompareTag("DownTrain") || other.CompareTag("RightTrain"))
        {
            warningAnimator.SetBool("Warning", true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("UpTrain") || other.CompareTag("LeftTrain") ||
            other.CompareTag("DownTrain") || other.CompareTag("RightTrain"))
        {
            warningAnimator.SetBool("Warning", false);
        }
    }
}
