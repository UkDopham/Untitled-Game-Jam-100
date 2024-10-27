using UnityEngine;

public class Princess : MonoBehaviour
{
    private Animator _animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    private void Awake()
    {
        this._animator = GetComponent<Animator>();
    }
    
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
        Destroy(gameObject);
    }
}
