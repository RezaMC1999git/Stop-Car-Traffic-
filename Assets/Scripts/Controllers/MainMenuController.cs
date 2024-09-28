using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    private Animator PlayBtnAnimator;
    private void Awake()
    {
        StartCoroutine(FirstAnimations());
        PlayBtnAnimator = PlayButton.GetComponent<Animator>();
    }
    public void GoToLevel()
    {
        Application.LoadLevel("Level1");
    }
    IEnumerator FirstAnimations()
    {
        yield return new WaitForSeconds(3f);
        PlayBtnAnimator.Play("FadeIn");
    }
}
