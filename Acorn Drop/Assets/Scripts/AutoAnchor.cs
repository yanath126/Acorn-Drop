using Unity.VisualScripting;
using UnityEngine;

public class AutoAnchor : MonoBehaviour
{
    [SerializeField] GameObject vineParent;
    [SerializeField] float vineHeight = 0.3f;

    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (SpriteRenderer vinesr in vineParent.GetComponentsInChildren<SpriteRenderer>())
        {
            if(gameObject.tag == "Vine")
                vinesr.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "Acorn")
        {
            if (vineParent != null && !vineParent.activeSelf)
            {
                vineParent.SetActive(true);
                foreach (Transform child in vineParent.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }

            Rigidbody2D vineBottomrb = FindBottomVineSeg(vineParent);

            HingeJoint2D[] hinges = collision.GetComponents<HingeJoint2D>();
            foreach(HingeJoint2D hinge in hinges)
            {
                if (hinge.connectedBody == null)
                {
                    hinge.connectedBody = vineBottomrb;
                    hinge.autoConfigureConnectedAnchor = false;
                    hinge.connectedAnchor = new Vector2(0, -vineHeight / 2f);
                    hinge.anchor = Vector2.zero;
                    hinge.enableCollision = false;
                    break;
                }
            }
            vineParent.transform.SetParent(transform);
            
        }


    }
    
    private Rigidbody2D FindBottomVineSeg(GameObject vine)
    {
        if (vine == null) return null;
        Rigidbody2D[] bodies = vine.GetComponentsInChildren<Rigidbody2D>(true);

        if (bodies.Length == 0) return null;

        return bodies[bodies.Length - 1];

    }
}
