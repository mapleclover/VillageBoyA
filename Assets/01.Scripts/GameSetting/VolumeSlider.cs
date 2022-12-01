using UnityEngine.UI;
using UnityEngine;

public class VolumeSlider : MonoBehaviour
{
    public void changeVolume(int type)
    {
        Slider mySlider = this.GetComponent<Slider>();
        SoundTest.instance.ChangeVolume(mySlider.value, type);
    }
}
