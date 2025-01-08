using UnityEngine;
using UnityEngine.UI;

public class MBSUIAudio : MonoBehaviour
{

    [SerializeField] AudioSource aMusic;
    [SerializeField] Slider sVol;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        aMusic = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        aMusic.volume = sVol.value;
    }
}
