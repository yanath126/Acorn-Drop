using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelect : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void LoadLevel(int level)
    {
        GameManager.instance.levelSelect = false;
        GameManager.instance.hideLevelSelection();
        GameManager.instance.SetLevel(level);
        Debug.Log("Button clicked" + level);
        
        SceneManager.LoadScene(level);
    }
}
