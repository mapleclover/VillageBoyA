//작성자 : 이영진
//설명 : 
using UnityEngine;

public class arrowmove : MonoBehaviour
{
    public GameObject left;
    public GameObject right;
    float currentleftPosition;
    float currentrightPosition;
    float rightend = -225.0f;
    float leftend = -200.0f;
    public float dir = 70.0f;

    void Start()
    {
        currentleftPosition = left.transform.localPosition.x;
        currentrightPosition = right.transform.localPosition.x;
    }

    void Update() //-200~-250
    {
        if (currentleftPosition.Equals(null))
        {
            currentleftPosition = right.transform.localPosition.x;
        }
        if (currentrightPosition.Equals(null))
        {
            currentrightPosition = right.transform.localPosition.x;
        }
        currentleftPosition += Time.deltaTime * dir;
        currentrightPosition -= Time.deltaTime * dir;
        if (dir > 0)
        {
            if (currentleftPosition > leftend)
            {
                dir *= -1;
            }
        }
        else
        {
            if (currentleftPosition < rightend)
            {
                dir *= -1;
            }
        }

        left.transform.localPosition = new Vector3(currentleftPosition, 0, 0);
        right.transform.localPosition = new Vector3(currentrightPosition, 0, 0);
    }
}