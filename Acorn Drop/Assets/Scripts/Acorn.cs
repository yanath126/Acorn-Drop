using UnityEngine;

public class Acornn : MonoBehaviour
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Totoro")
        {
            Destroy(gameObject, 0.5f);
            GameManager.instance.nextLevel(1);
        }
        else if (collision.gameObject.tag == "Respawn")
        {
            //restart the game
            Destroy(gameObject, 0.5f);
            GameManager.instance.restartLevel();
        }
        else
        {
            //??
        }
    }
}
