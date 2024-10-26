using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
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
        SceneManager.LoadScene((int)Scene.Level);
    }
    public void TutorialOnClick()
    {
        SceneManager.LoadScene((int)Scene.Tutorial);
    }
    public void CreditsOnClick()
    {
        SceneManager.LoadScene((int)Scene.Credits);
    }
    public void MenuOnClick()
    {
        SceneManager.LoadScene((int)Scene.Menu);
    }
}
