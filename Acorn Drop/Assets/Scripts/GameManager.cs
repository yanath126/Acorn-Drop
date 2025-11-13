using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;


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
    [SerializeField] float timer = 3f;
    public float butterflytimer = 5f;
    bool sceneLoaded = false;
    public bool levelSelect = true;
    

    [SerializeField] TextMeshProUGUI butterflyText;
    [SerializeField] TextMeshProUGUI LevelLostText;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject LevelLostScreen;
    [SerializeField] TextMeshProUGUI InstructionsText;
    [SerializeField] GameObject LevelSelection;


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Canvas);
        // DontDestroyOnLoad(butterflyText);
        // DontDestroyOnLoad(LevelLostText);
        DontDestroyOnLoad(LevelLostScreen);
        
    }
    void Start()
    {
        LevelLostText.gameObject.SetActive(false);
        LevelLostScreen.SetActive(false);
        InstructionsText.gameObject.SetActive(false);
        butterflyText.gameObject.SetActive(false);
        StartCoroutine(WaitUntilSceneLoads());
    }

    // Update is called once per frame
    void Update()
    {
        if (level >=1 && levelSelect == false)
        {
            LevelSelection.gameObject.SetActive(false);
            butterflyText.gameObject.SetActive(true);
        }
        else
        {
            butterflyText.gameObject.SetActive(false);
        }
        if (level == 5 && sceneLoaded == true)
        {
            level4Timer();
            if (butterflytimer > 0)
            {
                butterflytimer -= Time.deltaTime;
            }
            
        }
        if(LevelLost == true)
        {
            timer -= Time.deltaTime;
        }
        if(LevelLost == true && timer <=0)
        {
            
            //Debug.Log("Restart!");
            //Debug.Break();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Debug.Log("Lost");
            // Debug.Break();
            LevelLost = false;
            LevelLostText.gameObject.SetActive(false);
            LevelLostScreen.SetActive(false);
            InstructionsText.gameObject.SetActive(false);
            butterflyScore = 0;
            timer = 4;
            butterflytimer = 5;
            butterflyText.text = "Butterflies Collected: " + butterflyScore + "/3";
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
        LevelLostText.gameObject.SetActive(true);
        LevelLostScreen.SetActive(true);
        
    }

    public void nextLevel(int number)
    {
        level += number;
        //Debug.Log(level);
        butterflyScore = 0;
        butterflyText.text = "Butterflies Collected: " + butterflyScore + "/3";
        InstructionsText.gameObject.SetActive(false);
        if (level < 10)
        {
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
        
    }
    public void AddButterfly()
    {
        butterflyScore++;
        butterflyText.text = "Butterflies Collected: " + butterflyScore + "/3";
    }

    public void level4Timer()
    {
        InstructionsText.gameObject.SetActive(true);
        InstructionsText.text = "Collect all the butterflies before time runs out! Timer: " + (int)butterflytimer + "s";
    }

    IEnumerator WaitUntilSceneLoads()
    {
        yield return new WaitForSecondsRealtime(1f);
        sceneLoaded = true;
    }

    public void hideLevelSelection()
    {
        LevelSelection.SetActive(false);
    }

    public void SetLevel(int levelNumber)
    {
        level = levelNumber;
    }
}
