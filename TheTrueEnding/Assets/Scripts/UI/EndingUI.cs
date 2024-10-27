using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _endingCounter;
    [SerializeField]
    private TextMeshProUGUI _endingText;
    [SerializeField]
    private int _endingTotalCount = 5;
    [SerializeField]
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Knight _knight;
    [SerializeField]
    private AudioSource _audioSourceMusic;
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private TextMeshProUGUI _restartCount;

    private void Awake()
    {
        this._endingCounter.text = $"{EndingManager.CurrentEndings.Count} / {this._endingTotalCount} endings completed";
    }

    public void LoadEndingText(string text)
    {
        this._endingCounter.text = $"{EndingManager.CurrentEndings.Count} / {this._endingTotalCount} endings completed";
        this._endingText.text = text;
        this._animator.SetTrigger("start");
        this._knight.Stop();
        this._audioSourceMusic.Stop();
        this._audioSource.Play();
        StartCoroutine(StartAgain());
    }

    IEnumerator StartAgain()
    {
        int countdown = 10; // Countdown timer in seconds

        while (countdown > 0)
        {
            _restartCount.text = $"Restarting in {countdown}...";
            yield return new WaitForSeconds(1);
            countdown--;
        }

        SceneManager.LoadScene((int)Scene.Level);
    }
}
