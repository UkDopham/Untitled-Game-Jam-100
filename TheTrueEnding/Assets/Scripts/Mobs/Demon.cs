using UnityEngine;
public class Demon : MonoBehaviour
{
    private Animator _animator;

    private void Awake()
    {
        this._animator = GetComponent<Animator>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Death()
    {
        this._animator.SetTrigger("hit");
        Destroy(gameObject,1f);
    }
}
