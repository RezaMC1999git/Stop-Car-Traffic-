using UnityEngine;

public class TrainMovement : MonoBehaviour
{
    private Rigidbody2D MyRB;
    public float trainSpeed;
    private void Awake()
    {
        MyRB = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (gameObject.CompareTag("UpTrain"))
        {
            MyRB.velocity = new Vector2(0f, -transform.lossyScale.y) * trainSpeed;
        }

        if (gameObject.CompareTag("LeftTrain"))
        {
            MyRB.velocity = new Vector2(transform.lossyScale.x, 0f) * trainSpeed;
        }

        if (gameObject.CompareTag("DownTrain"))
        {
            MyRB.velocity = new Vector2(0f, transform.lossyScale.y) * trainSpeed;
        }

        if (gameObject.CompareTag("RightTrain"))
        {
            MyRB.velocity = new Vector2(-transform.lossyScale.x, 0f) * trainSpeed;
        }
    }
}
