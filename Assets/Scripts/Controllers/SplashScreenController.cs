using System.Collections;
using UnityEngine;

public class SplashScreenController : MonoBehaviour
{
    int level;
    public Animator BalotAnimation;
    private void Start()
    {
        StartCoroutine(LoadCurrentLevel());
        level = GameController.instance.GetCurrentLevel();
    }
    IEnumerator LoadCurrentLevel()
    {
        BalotAnimation.SetTrigger("FadeIn");
        yield return new WaitForSeconds(4f);
        Application.LoadLevel("Level" + level);
    }
}
