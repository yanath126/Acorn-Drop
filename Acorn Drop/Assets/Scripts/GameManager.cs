using System.Runtime.CompilerServices;
using Unity.Collections;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEditor;
using System.Collections;
using UnityEngine.UI;
using Unity.VisualScripting;


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
    bool isGameOver = false;
   
    

    [SerializeField] TextMeshProUGUI butterflyText;
    [SerializeField] TextMeshProUGUI LevelLostText;
    [SerializeField] GameObject Canvas;
    [SerializeField] GameObject LevelLostScreen;
    [SerializeField] TextMeshProUGUI InstructionsText;
    [SerializeField] GameObject LevelSelection;
    [SerializeField] GameObject levelSelectObject;
    [SerializeField] GameObject BackButton;
    


    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
                
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(Canvas);
        // DontDestroyOnLoad(butterflyText);
        // DontDestroyOnLoad(LevelLostText);
        DontDestroyOnLoad(LevelLostScreen);
        DontDestroyOnLoad(levelSelectObject);

        
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
            BackButton.gameObject.SetActive(true);
            
        }
        else
        {
            butterflyText.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(false);
        }
        if (level == 5 && sceneLoaded == true)
        {
            level5Timer();
            if (butterflytimer > 0)
            {
                butterflytimer -= Time.deltaTime;
            }
        }
        
        else if (level == 6 && sceneLoaded == true)
        {
            level6Instructions();
        }
        else if (level == 9 && sceneLoaded == true)
        {
            level9Instructions();
        }
        else
        {
            InstructionsText.gameObject.SetActive(false);
        }
        if (LevelLost == true)
            {
                timer -= Time.deltaTime;
            }
        if(LevelLost == true && timer <=0)
        {
            
            //Debug.Log("Restart!");
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            // Debug.Log("Lost");
            
            LevelLost = false;
            LevelLostText.gameObject.SetActive(false);
            LevelLostScreen.SetActive(false);
            InstructionsText.gameObject.SetActive(false);
            butterflyScore = 0;
            timer = 4;
            butterflytimer = 5;
            butterflyText.text = "Butterflies Collected: " + butterflyScore + "/3";
        }
        if (isGameOver == true)
        {
            timer -= Time.deltaTime;
        }
        if (isGameOver == true && timer <= 0)
        {
            isGameOver = false;
            LevelLostText.gameObject.SetActive(false);
            LevelLostScreen.SetActive(false);
            timer = 4;
            gameOver();
        }
        if (Input.GetButtonDown("Fire1")) // when player clicks mouse
        {
            ImousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            //transform.position = new Vector3(ImousePosition.x, ImousePosition.y, 0);
            //Debug.Log("Is Clicking");
            isDragging = true;

            spawnCutter();
            Debug.Log(ImousePosition);
            
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
        LevelLostText.text = "Uh oh! Try again..";
        AudioManager.instance.PlaySFX(AudioManager.instance.death);
        LevelLostText.gameObject.SetActive(true);
        LevelLostScreen.SetActive(true);
        BackButton.gameObject.SetActive(false);
    }

    public void nextLevel(int number)
    {
        level += number;
        //Debug.Log(level);
        butterflyScore = 0;
        butterflyText.text = "Butterflies Collected: " + butterflyScore + "/3";
        InstructionsText.gameObject.SetActive(false);
        if (level < 11)
        {
            SceneManager.LoadScene(level, LoadSceneMode.Single);
        }
        else
        {
            isGameOver = true;
            LevelLostScreen.SetActive(true);
            LevelLostText.text = "Congrats! You finished the game :) Thank you for playing.";
            LevelLostText.gameObject.SetActive(true);
            InstructionsText.gameObject.SetActive(false);
            butterflyText.gameObject.SetActive(false);
            BackButton.gameObject.SetActive(false);
            level = 0;
        }
        
    }
    public void AddButterfly()
    {
        butterflyScore++;
        butterflyText.text = "Butterflies Collected: " + butterflyScore + "/3";
    }

    public void level5Timer()
    {
        InstructionsText.gameObject.SetActive(true);
        InstructionsText.text = "Collect all the butterflies before time runs out! Timer: " + (int)butterflytimer + "s";
    }
    public void level6Instructions()
    {
        InstructionsText.gameObject.SetActive(true);
        InstructionsText.text = "Keep the acorn from getting infected..";
    }

    public void level9Instructions()
    {
        InstructionsText.gameObject.SetActive(true);
        InstructionsText.text = "Click on the shells to blow air and move the acorn.";
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

    public void gameOver()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
        BackButton.gameObject.SetActive(false);
        butterflyText.gameObject.SetActive(false);
    }
}
