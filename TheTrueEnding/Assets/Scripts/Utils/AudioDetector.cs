using UnityEngine;

public class AudioDetector : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;

    public void PlayAudio()
    {
        this._audioSource.Play();
    }
}
