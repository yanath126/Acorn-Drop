using UnityEngine;

public class AutoAnchor : MonoBehaviour
{
    [SerializeField] Transform vinePrefab;
    [SerializeField] int vineLength = 1;
    [SerializeField] Vector2 acornAnchorOffset = new Vector2(0, 0.5f);
    [SerializeField] Rigidbody2D previousVine;
    [SerializeField] float vineHeight = 0.5f;
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
            previousVine = null;
            for (int i = 0; i < vineLength; i++)
            {
                Transform vine = Instantiate(vinePrefab, transform.position, Quaternion.identity);
                Rigidbody2D vinerb = vine.GetComponent<Rigidbody2D>();

                HingeJoint2D hinge = vine.gameObject.AddComponent<HingeJoint2D>();
                hinge.autoConfigureConnectedAnchor = false;

                if (i == 0)
                {
                    hinge.connectedBody = null;
                    hinge.connectedAnchor = transform.position;
                }
                else
                {
                    hinge.connectedBody = previousVine;
                    hinge.connectedAnchor = new Vector2(0, -vineHeight / 2f);
                }
                hinge.anchor = new Vector2(0, vineHeight / 2f);
                hinge.enableCollision = false;

                previousVine = vinerb;
                vine.SetParent(transform);
            }
                HingeJoint2D newHinge = collision.gameObject.AddComponent<HingeJoint2D>();
                newHinge.autoConfigureConnectedAnchor = false;
                newHinge.connectedBody = previousVine;
                newHinge.connectedAnchor = new Vector2(0, -vineHeight/2f);
                newHinge.anchor = acornAnchorOffset;
                newHinge.enableCollision = false;
            
        }
    }
}
