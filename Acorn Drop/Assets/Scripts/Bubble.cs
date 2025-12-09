using NUnit.Framework;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    
    [SerializeField] float floatForce = 2f;
    bool isAttached = false;
    [SerializeField] GameObject Acorn;
    [SerializeField] Rigidbody2D AcornRB;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached && Acorn.GetComponent<Rigidbody2D>() != null)
        {
            Acorn.GetComponent<Rigidbody2D>().linearVelocity = Vector2.up * floatForce;
            transform.position = Vector3.Lerp(transform.position, Acorn.GetComponent<Rigidbody2D>().position, 1f); // referenced from 2d tutorial
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acorn" && !isAttached)
        {
            isAttached = true;
            AcornRB = collision.gameObject.GetComponent<Rigidbody2D>();
            Acorn.GetComponent<Rigidbody2D>().gravityScale = 0f;
        }
    }

    void OnMouseDown()
    {
        if (isAttached)
        {
            PopBubble();
            AudioManager.instance.PlaySFX(AudioManager.instance.pop);
        }
    }
    void PopBubble()
    {
        if (Acorn.GetComponent<Rigidbody2D>() != null)
        {
            Acorn.GetComponent<Rigidbody2D>().gravityScale = 1f;
        }
        Destroy(gameObject);
    }

}
