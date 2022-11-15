using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public string itemName;
    public int price;
    public ShopManager shopManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (price > shopManager.GoldAmount)
        {
            this.GetComponent<Button>().interactable = false;
        }
        else
        {
            this.GetComponent<Button>().interactable = true;
        }
    }
}
