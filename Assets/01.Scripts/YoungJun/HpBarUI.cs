using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 박영준 , HPBar UI
public class HpBarUI : MonoBehaviour
{
    [SerializeField]
    private Slider HPBar;
    [SerializeField]
    private Slider BackHPBar;


    float maxHP = 100;
    float curHP = 100;
    // Start is called before the first frame update
    void Start()
    {
        HPBar.value = (float)curHP / maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))  // 스페이스 바 누를떄 (적에게 맞았을때)
        {
            if (curHP > 0)
            {
                curHP -= 10;
                StopAllCoroutines();
                StartCoroutine(HPSimulCoroutine());
            }
            else
            {
                curHP = 0;
            }
        }
        HpSimul();
    }

    IEnumerator HPSimulCoroutine()
    {

        yield return new WaitForSeconds(0.5f);
        while (BackHPBar.value > 0)
        {
            BackHPBar.value = Mathf.Lerp(BackHPBar.value, (float)curHP / maxHP, 20 * Time.deltaTime);
            yield return null;
        }

    }

    private void HpSimul()
    {
        HPBar.value = Mathf.Lerp(HPBar.value, (float)curHP / maxHP, 10 * Time.deltaTime);
    }
}
