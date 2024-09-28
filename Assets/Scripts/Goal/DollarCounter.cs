using UnityEngine;
using RTLTMPro;

public class DollarCounter : MonoBehaviour
{
    public static DollarCounter instance;
    public int totallDollarEarned = 0;
    public RTLTextMeshPro AccidentCanvasTotallCash;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        ZeroCash();
    }
    private void Update()
    {
        AccidentCanvasTotallCash.text = (totallDollarEarned + " $").ToString();
    }
    public void ZeroCash()
    {
        totallDollarEarned = 0;
    }
}
