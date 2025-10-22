using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject cutterPrefab;
    bool isDragging = false;
    Vector3 ImousePosition;
    HingeJoint2D joint;
    bool LevelLost = false;
    int level = 1;
    public static GameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelLost)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetButtonDown("Fire1")) // when player clicks mouse
        {
            ImousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);
            //Debug.Log("Is Clicking");
            isDragging = true;
        }

        if (Input.GetButton("Fire1") && isDragging) // when holding down the mouse, player begins to cut the vine
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);

            //Debug.Log("Dragging");
            //check for a swipe and destroy the vines
            spawnCutter(ImousePosition, currentMousePosition);
            ImousePosition = currentMousePosition;
        }

        if (Input.GetButtonUp("Fire1")) //after mouse click releases
        {

            isDragging = false;
        }
        
    }
    void spawnCutter(Vector3 start, Vector3 end)
    {
        GameObject cutter = Instantiate(cutterPrefab, new Vector3(ImousePosition.x, ImousePosition.y, 0), Quaternion.identity);
        EdgeCollider2D edge = cutter.GetComponent<EdgeCollider2D>();
        edge.points = new Vector2[] { start, end };

    }

    public void restartLevel()
    {
        LevelLost = true;

    }

    public void nextLevel(int number)
    {
        level += number;
    }



}
