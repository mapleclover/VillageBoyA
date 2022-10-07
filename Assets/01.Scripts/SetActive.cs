using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActive : MonoBehaviour
{
    public GameObject ChatBox;

    public void OnOffChatBox()
    {
        if (ChatBox.activeInHierarchy)
        {
            ChatBox.SetActive(false);
        }
        else
        {
            ChatBox.SetActive(true);
        }
    }
}
