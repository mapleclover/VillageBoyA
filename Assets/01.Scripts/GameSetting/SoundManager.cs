//�ۼ��� : ����ȣ
//���� : 

using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public SoundManager instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
             DontDestroyOnLoad(gameObject);
        }

         else
         Destroy(gameObject);
    }
}