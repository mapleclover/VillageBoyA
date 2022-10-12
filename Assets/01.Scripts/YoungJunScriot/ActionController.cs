using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// �ڿ��� ��ȣ�ۿ� ��ũ��Ʈ

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range; // raycast ��������

    private bool pickNpcActivated = false;
    private bool pickItemActivated = false;

    private RaycastHit hitInfo;

    [SerializeField]
    private LayerMask layerMask; // �ش緹�̾�� �����ϰԲ�.

    //�ʿ��� ������Ʈ
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
    // Npc���� tag Ȯ��
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
        if(pickItemActivated) // ������ Ȼ��Ȱ���ÿ��� ���
        {
            if(hitInfo.transform != null)
            {
                Destroy(hitInfo.transform.gameObject);
                ItemInfoDisappear();
            }
        }
    }

    // npc ����â ����
    private void NpcInfoAppear()
    {
        pickNpcActivated = true;
        CheckText.gameObject.SetActive(true); // �ؽ�Ʈâ Ȱ��ȭ
        CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().npc.npcName + "</color>" + "�� ��ȭ�Ͻðڽ��ϱ�?" + "<color=yellow>" + " (Y)" + "</color>";
    }
    private void ItemInfoAppear()
    {
        pickItemActivated = true;
        CheckText.gameObject.SetActive(true);
        CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().item.itemName + "</color>" + "ȹ��" + "<color=yellow>" + " (E)" + "</color>";
    }

    // npc ����â Ŭ����
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
