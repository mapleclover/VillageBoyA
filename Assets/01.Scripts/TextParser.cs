///작성자 : 유은호
///설명 : 대사 스크립트 TXT 파일부터 불러와 대화창에 대화와 화자의 이미지를 띄워주는 스크립트
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
