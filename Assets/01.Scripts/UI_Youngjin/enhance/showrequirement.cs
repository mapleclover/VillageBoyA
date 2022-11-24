using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showrequirement : MonoBehaviour
{
    public TMPro.TMP_Text curAmount;
    public TMPro.TMP_Text required;
    public void UpdateNumberUI(string name, int level)
    {
        curAmount.text = $"{DataController.instance.gameData.myItemCount[name]}";
        required.text = $"{level}";
    }
    public void test()
    {
        int level = 1;
        if (DataController.instance.gameData.myItemCount[name] > level)
        {
            DataController.instance.gameData.myItemCount[name] -= level;
            //FindMySlot(myIngSlots[0].transform.GetChild(0).gameObject);
        }
        else if (DataController.instance.gameData.myItemCount[name].Equals(level))
        {
           // Destroy(myIngSlots[0].transform.GetChild(0).gameObject);
        }
    }
}
