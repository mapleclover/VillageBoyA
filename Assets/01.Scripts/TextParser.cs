///�ۼ��� : ����ȣ
///���� : ��� ��ũ��Ʈ TXT ���Ϻ��� �ҷ��� ��ȭâ�� ��ȭ�� ȭ���� �̹����� ����ִ� ��ũ��Ʈ
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CharacterInfomation : MonoBehaviour
{
    public TextMeshProUGUI Quote;
    public Image[] CharacterImage;
    private string[] lines;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadText()
    {
        lines = System.IO.File.ReadAllLines("10.Resources/TextScript/Text01.txt");
        
    }
}
