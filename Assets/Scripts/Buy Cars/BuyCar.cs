using UnityEngine;
using UnityEngine.UI;
using RTLTMPro;

public class BuyCar : MonoBehaviour
{
    public int CarPrice;
    private int currentCash;
    private Button CarShopBTN;
    private GameObject priceLable;
    private RTLTextMeshPro cashText;

    private void Awake()
    {
        currentCash = GameController.instance.GetCurrentCash();
        cashText = GameObject.FindGameObjectWithTag("CashText").GetComponent<RTLTextMeshPro>();
        priceLable = transform.GetChild(0).gameObject;
        CarShopBTN = transform.GetChild(4).GetComponent<Button>();
        CarShopBTN.onClick.RemoveAllListeners();
        CarShopBTN.onClick.AddListener(() => OwnCar());
    }
    public void OwnCar()
    {
        if(currentCash >= CarPrice)
        {
            int numberOfCars = GameController.instance.GetCarsNumber();
            numberOfCars++;
            GameController.instance.SetCarsNumber(numberOfCars);
            GameController.instance.SetCurrentCash(currentCash - CarPrice);
            cashText.text = GameController.instance.GetCurrentCash().ToString();
            Destroy(priceLable);
            Destroy(CarShopBTN.gameObject);
        }
    }
}
