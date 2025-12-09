
using UnityEngine;

public class Wind : MonoBehaviour
{

    [SerializeField] GameObject Acorn;
    [SerializeField] float pushForce = 12f;
    
    bool acornInFront = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.attachedRigidbody == Acorn.GetComponent<Rigidbody2D>())
        {
            acornInFront = true;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.attachedRigidbody != Acorn.GetComponent<Rigidbody2D>())
        {
            acornInFront = false;
        }
    }

    void OnMouseDown()
    {
        if (acornInFront) 
        { 
            PushAcorn(); 
        }
    }

    void PushAcorn()
    {
        Vector2 direction = transform.right.normalized;
        Acorn.GetComponent<Rigidbody2D>().AddForce(direction * pushForce, ForceMode2D.Impulse); 
        AudioManager.instance.PlaySFX(AudioManager.instance.whoosh);

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
