using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 박영준 상호작용 스크립트

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // raycast 범위지정

    private bool pickNpcActivated = false;
    private bool pickItemActivated = false;

    private RaycastHit hitInfo;
    

    [SerializeField]
    private LayerMask layerMask; // 해당레이어에만 반응하게끔.

    //필요한 컴포넌트
    [SerializeField]
    private TMPro.TextMeshProUGUI CheckText;
    [SerializeField]
    private Image npcTextBackground;
    [SerializeField]
    private Image itemTextBackground;
    [SerializeField]
    private Transform accesssTransform;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
        TryPickupAction();
    }
    // 어떤종류의 tag인지 tag 확인
    private void CheckObject()
    {
        
        Collider[] list = Physics.OverlapSphere(accesssTransform.position, 0.7f, layerMask);
        Vector3 colPos = Vector3.zero;
        foreach (Collider col in list)
        {
            if (col.transform.tag == "Item" || col.transform.tag == "Npc")
            {
                colPos = col.transform.position;
            }
        }

        if(Physics.Raycast(transform.position, colPos - transform.position, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Npc")
            {
                NpcInfoAppear();
            }
            else if(hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
        {
            NpcInfoDisappear();
            ItemInfoDisappear();
        }
    }
    // 아이템체크 후 pickup 함수활성화
    private void TryPickupAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckObject();
            CanPickUp();
        }
    }
    // 아이템획득가능으로전환.
    private void CanPickUp()
    {
        if(pickItemActivated) // 아이템 횔득활성시에만 기능
        {
            if(hitInfo.transform != null)
            {
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }

    // npc 정보창 오픈
    private void NpcInfoAppear()
    {
        pickNpcActivated = true;
        npcTextBackground.gameObject.SetActive(true);
        CheckText.gameObject.SetActive(true); // 텍스트창 활성화
        CheckText.alignment = TMPro.TextAlignmentOptions.Right;
        CheckText.text = "<color=blue>" + hitInfo.transform.GetComponent<Pickup>().npc.npcName + "</color>" + "와 대화하시겠습니까?" + "<color=yellow>" + " (Y) " + "</color>";
    }
    // item 정보창 오픈
    private void ItemInfoAppear()
    {
        pickItemActivated = true;
        itemTextBackground.gameObject.SetActive(true);
        CheckText.gameObject.SetActive(true);
        CheckText.alignment = TMPro.TextAlignmentOptions.Center;
        CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().item.itemName + "</color>" + "획득" + "<color=yellow>" + " (E) " + "</color>";
    }

    // npc 정보창 클로즈
    private void NpcInfoDisappear()
    {
        pickNpcActivated = false;
        npcTextBackground.gameObject.SetActive(false);
        CheckText.gameObject.SetActive(false);
    }
    // 아이템 정보창 클로즈
    private void ItemInfoDisappear()
    {
        pickItemActivated = false;
        itemTextBackground.gameObject.SetActive(false);
        CheckText.gameObject.SetActive(false);
    }
}
