using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] float floatForce = 2f;
    bool isAttached = false;
    [SerializeField] Rigidbody2D acornrb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isAttached && acornrb != null)
        {
            acornrb.linearVelocity = Vector2.up * floatForce;
            transform.position = Vector3.Lerp(transform.position, acornrb.position, 1f);
        }
        if (Input.GetButtonDown("Fire1") && isAttached)
        {
            PopBubble();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acorn" && !isAttached)
        {
            isAttached = true;
            acornrb = collision.gameObject.GetComponent<Rigidbody2D>();
            acornrb.gravityScale = 0f;
        }
    }
    void PopBubble()
    {
        if (acornrb != null)
        {
            acornrb.gravityScale = 1f;
        }
        Destroy(gameObject);
    }

}
