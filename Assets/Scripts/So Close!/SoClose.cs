using System.Collections;
using UnityEngine;

public class SoClose : MonoBehaviour
{
    private Animator SoColseTextAnimator;
    private void Awake()
    {
        SoColseTextAnimator = GameObject.FindGameObjectWithTag("SoCloseText").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.tag == "LeftRightCloseTrigger")
        {
            if (other.tag == "UpSide" || other.tag == "DownSide")
                StartCoroutine(WaitForAccidentReport(other));

        }
        if (gameObject.tag == "UpDownCloseTrigger")
        {
            if (other.tag == "LeftSide" || other.tag == "RightSide")
                StartCoroutine(WaitForAccidentReport(other));
        }
    }
    IEnumerator WaitForAccidentReport(Collider2D other)
    {
        yield return new WaitForSeconds(0.4f);
        if (other.GetComponent<CarMovement>().accidentHappend == false
            && GetComponentInParent<CarMovement>().accidentHappend == false)
                SoColseTextAnimator.SetTrigger("Close");
    }
}
