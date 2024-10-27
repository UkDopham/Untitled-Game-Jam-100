using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    [SerializeField]
    private AudioSource _audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayOnClick()
    {
        if(this._audioSource != null)
        {
            this._audioSource.Play();
        }
        SceneManager.LoadScene((int)Scene.Level);
    }
    public void TutorialOnClick()
    {
        if (this._audioSource != null)
        {
            this._audioSource.Play();
        }
        SceneManager.LoadScene((int)Scene.Tutorial);
    }
    public void CreditsOnClick()
    {
        if (this._audioSource != null)
        {
            this._audioSource.Play();
        }
        SceneManager.LoadScene((int)Scene.Credits);
    }
    public void MenuOnClick()
    {
        if (this._audioSource != null)
        {
            this._audioSource.Play();
        }
        SceneManager.LoadScene((int)Scene.Menu);
    }
}
