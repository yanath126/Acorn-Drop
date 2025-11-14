
using UnityEngine;

public class Wind : MonoBehaviour
{
    [SerializeField] float pushForce = 12f;
    Vector2 psuhDirection = Vector2.up;

    float activeTime = .15f;

    bool isActive = false;

    void OnMouseDown()
    {
        Activate();
    }

    void Activate()
    {
        isActive = true;
        CancelInvoke(nameof(Deactivate));
        Invoke(nameof(Deactivate), activeTime);
    }

    void Deactivate()
    {
        isActive = false;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (!isActive) return;

        if (collision.gameObject.tag == "Acorn")
        {
            Rigidbody2D rb = collision.rigidbody;
            if (rb != null)
            {
            rb.AddForce(psuhDirection.normalized * pushForce, ForceMode2D.Impulse);
            }
        }

        

        
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
