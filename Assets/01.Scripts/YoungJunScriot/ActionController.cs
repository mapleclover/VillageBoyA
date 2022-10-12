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
   

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CheckObject();
        TryAction();
    }
    // Npc인지 tag 확인
    private void CheckObject()
    {
        if(Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if(hitInfo.transform.tag == "Npc")
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

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckObject();
            CanPickUp();
        }
    }

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
        CheckText.gameObject.SetActive(true); // 텍스트창 활성화
        CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().npc.npcName + "</color>" + "와 대화하시겠습니까?" + "<color=yellow>" + " (Y)" + "</color>";
    }
    private void ItemInfoAppear()
    {
        pickItemActivated = true;
        CheckText.gameObject.SetActive(true);
        CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().item.itemName + "</color>" + "획득" + "<color=yellow>" + " (E)" + "</color>";
    }

    // npc 정보창 클로즈
    private void NpcInfoDisappear()
    {
        pickNpcActivated = false;
        CheckText.gameObject.SetActive(false);
    }

    private void ItemInfoDisappear()
    {
        pickItemActivated = false;
        CheckText.gameObject.SetActive(false);
    }
}
