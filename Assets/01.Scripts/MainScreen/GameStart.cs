//작성자 : 이현호
//설명 : 
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void OnCilck()
    {
        SceneManager.LoadScene(1);
    }
}