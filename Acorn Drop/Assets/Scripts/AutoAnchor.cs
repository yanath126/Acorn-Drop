using UnityEngine;

public class AutoAnchor : MonoBehaviour
{
    [SerializeField] Transform vinePrefab;
    [SerializeField] int vineLength = 1;
    [SerializeField] Vector2 acornAnchorOffset = new Vector2(0, 0.5f);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Acorn")
        {
            HingeJoint2D newHinge = collision.gameObject.AddComponent<HingeJoint2D>();
            newHinge.autoConfigureConnectedAnchor = false;
            newHinge.connectedBody = null;
            newHinge.connectedAnchor = transform.position;
            newHinge.anchor = acornAnchorOffset;
            newHinge.enableCollision = false;
            Transform vine = Instantiate(vinePrefab, transform.position, Quaternion.identity);
            vine.SetParent(transform);
        }
    }
}
