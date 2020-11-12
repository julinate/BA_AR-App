using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioControllerOnTab : MonoBehaviour
{

    [SerializeField]
    AudioClip tabSound;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayTabSound()
    {
        audioSource.PlayOneShot(tabSound);
    }
}