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

    int butterflyScore = 0;
    
    [SerializeField] int level = 0;
    public static GameManager instance;
    public bool LevelLost = false;
    GameObject cutter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(LevelLost == true)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Debug.Log("Lost");
            // Debug.Break();
            LevelLost = false;
        }
        if (Input.GetButtonDown("Fire1")) // when player clicks mouse
        {
            ImousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);
            //Debug.Log("Is Clicking");
            isDragging = true;

            spawnCutter();
            Debug.Log(ImousePosition);
            //Debug.Break();
        }

        if (Input.GetButton("Fire1") && isDragging) // when holding down the mouse, player begins to cut the vine
        {
            Vector3 endPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            stretchCutter(ImousePosition, endPosition);
            //transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);

            //Debug.Log("Dragging");
            //check for a swipe and destroy the vines
            
            // ImousePosition = currentMousePosition;
        }

        if (Input.GetButtonUp("Fire1")) //after mouse click releases
        {
            
            isDragging = false;
        }
        
    }
    void spawnCutter()
    {
        cutter = Instantiate(cutterPrefab, new Vector3(ImousePosition.x, ImousePosition.y, 0), Quaternion.identity);
        
        //Debug.Break();
    }

    void stretchCutter(Vector3 start, Vector3 end)
    {
        EdgeCollider2D edge = cutter.GetComponent<EdgeCollider2D>();
        float absoluteOffsetx = (end.x - ImousePosition.x);
        float absoluteOffsety = (end.y - ImousePosition.y);
        edge.points = new Vector2[] { new Vector2(0,0), new Vector2(absoluteOffsetx, absoluteOffsety)};
    }

    public void restartLevel()
    {
        LevelLost = true;

    }

    public void nextLevel(int number)
    {
        level += number;
        Debug.Log(level);
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }
    public void AddButterfly()
    {
        butterflyScore++;
    }



}
