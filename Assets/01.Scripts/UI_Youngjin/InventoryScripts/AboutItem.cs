using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AboutItem : MonoBehaviour
{
    public GameObject myInfoBox;
    public TMPro.TMP_Text myInfoText;
    public GameObject myParty;
    public GameObject myPanel;
    public static AboutItem instance = null;
}
