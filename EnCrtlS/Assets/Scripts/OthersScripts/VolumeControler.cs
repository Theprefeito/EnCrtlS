using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class AudioControler : MonoBehaviour
{
    public AudioMixer audioMixer;

    public float volume = 0;

    public Slider slider;

    public TMP_Text texto;


    void Start()
    {
        audioMixer.GetFloat("Master", out volume);
        slider.value = -4;  //slider.value = volume //Por algum motivo não começa no que esta setado em volume, então mudança manual para não deixar ninguem surdo
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
        {
            volume += 1;
            audioMixer.SetFloat("Master", volume);
        }

        if (Input.GetKeyDown(KeyCode.S) && volume > -80)
        {
            volume -= 1;
            audioMixer.SetFloat("Master", volume);
        }

        volume = slider.value;

        texto.text = volume.ToString();

        if (volume <= -40)
        {
            audioMixer.SetFloat("Master", -80);
        }
        else
        {
            audioMixer.SetFloat("Master", volume);

        }


    }
}