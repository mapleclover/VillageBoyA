//작성자 : 전정우
//설명 :

using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.PlayerSettings;

public class EnhancementController : MonoBehaviour, IPointerEnterHandler
{

    public RectTransform rectTransform;

    public GameObject myPanel;

    public GameObject myUI;
    public RectTransform myInventory;
    public GameObject setMyInventory;
    public bool isOpen = false;
    public static EnhancementController inst = null;
    public Transform myInven;
    private void Awake()
    {
        inst = this;

        rectTransform = GetComponent<RectTransform>();
    }
    void Start()
    {
        myInventory = myInventory.GetComponent<RectTransform>();
        myInventory.localPosition = new Vector2(0f, 35f);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!myPanel.activeSelf)
        {
            rectTransform.SetAsFirstSibling();
        }
    }

    public void OpenUpEnhancement()
    {
        myInventory.localPosition = new Vector2(200f, 35f);
        myUI.SetActive(true);
        setMyInventory.SetActive(true);
        isOpen = true;
    }

   
    public void CloseEnhancement()
    {
        myInventory.localPosition = new Vector2(0f, 35f);
        myUI.SetActive(false);
        setMyInventory.SetActive(false);
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            OpenUpEnhancement();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseEnhancement();
        }
    }
  
}