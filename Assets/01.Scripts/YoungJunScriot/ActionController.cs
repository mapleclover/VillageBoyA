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
    // ������� tag���� tag Ȯ��
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
    // ������üũ �� pickup �Լ�Ȱ��ȭ
    private void TryPickupAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckObject();
            CanPickUp();
        }
    }
    // ������ȹ�氡��������ȯ.
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
        npcTextBackground.gameObject.SetActive(true);
        CheckText.gameObject.SetActive(true); // �ؽ�Ʈâ Ȱ��ȭ
        CheckText.alignment = TMPro.TextAlignmentOptions.Right;
        CheckText.text = "<color=blue>" + hitInfo.transform.GetComponent<Pickup>().npc.npcName + "</color>" + "�� ��ȭ�Ͻðڽ��ϱ�?" + "<color=yellow>" + " (Y) " + "</color>";
    }
    // item ����â ����
    private void ItemInfoAppear()
    {
        pickItemActivated = true;
        itemTextBackground.gameObject.SetActive(true);
        CheckText.gameObject.SetActive(true);
        CheckText.alignment = TMPro.TextAlignmentOptions.Center;
        CheckText.text = "<color=red>" + hitInfo.transform.GetComponent<Pickup>().item.itemName + "</color>" + "ȹ��" + "<color=yellow>" + " (E) " + "</color>";
    }

    // npc ����â Ŭ����
    private void NpcInfoDisappear()
    {
        pickNpcActivated = false;
        npcTextBackground.gameObject.SetActive(false);
        CheckText.gameObject.SetActive(false);
    }
    // ������ ����â Ŭ����
    private void ItemInfoDisappear()
    {
        pickItemActivated = false;
        itemTextBackground.gameObject.SetActive(false);
        CheckText.gameObject.SetActive(false);
    }
}
