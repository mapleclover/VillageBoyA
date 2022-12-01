//작성자 : 이현호
//설명 : 

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