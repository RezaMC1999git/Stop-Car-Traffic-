using UnityEngine;
using RTLTMPro;

public class ShopController : MonoBehaviour
{
    public RTLTextMeshPro cashText;
    private void Awake()
    {
        Time.timeScale = 1;
        cashText.text = GameController.instance.GetCurrentCash().ToString();
    }
    public void GoBackToLevel()
    {
        int level = GameController.instance.GetCurrentLevel();
        Application.LoadLevel("Level" + level);
    }
}
