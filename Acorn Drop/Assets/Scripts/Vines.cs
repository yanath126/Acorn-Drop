using UnityEngine;

public class Vines : MonoBehaviour
{
    [SerializeField] GameManager manager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Cutter")
        {
            Destroy(GetComponent<HingeJoint2D>());
            // Destroy(GetComponent<Rigidbody2D>());
            Destroy(gameObject);
            Debug.Log("destroyed vine segment");
        }
    }
}
