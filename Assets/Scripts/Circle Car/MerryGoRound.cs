using UnityEngine;

public class MerryGoRound : MonoBehaviour
{
    public float speed = 0.4f;
    private Vector3 _vector3= new Vector3(0,0,1);

    private void FixedUpdate()
    {
        transform.Rotate(_vector3,-speed);
    }
}
