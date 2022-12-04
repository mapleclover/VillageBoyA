using UnityEngine;
using UnityEngine.SceneManagement;
public class GotoTitle : MonoBehaviour
{
    public void OnClickGoBack()
    {
        SceneManager.LoadScene("01.MainTitle");
    }
}
