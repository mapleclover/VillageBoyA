//�ۼ��� : ������
//���� : 1025 

using UnityEngine;

public class Tooltip : MonoBehaviour
{
    //public TextMeshProUGUI nameTxt;
    //public TextMeshProUGUI descriptionTxt;
    public GameObject myTarget;
    private RectTransform rectTransform;

    Vector2 pos = new Vector2(10f, 150f);
    /*public void SetupTooltip(string name, string des, int atk)
    {
        //nameTxt.text = name;
        //descriptionTxt.text = des;

        //�ּ� �ɸ� �ڵ� ������ �����ּ��� ���߿� �ؽ�Ʈ������ �����Ϸ�����
    }*/

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //rectTransform.position = Input.mousePosition; //���콺 ��ġ�� ����ϰ� ���� �� �Ǻ� ��ġ�� 0���� �����ּ���

        rectTransform.transform.position = myTarget.transform.position;
    }
}