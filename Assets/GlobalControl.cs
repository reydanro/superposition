using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    private string[] LEVEL_LIST = { "Title", "Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7", "Level8" };
    private const string DEATH_LEVEL = "DEATH";
    public static GlobalControl Instance;
    private LinkedList<string> levels;
    private LinkedListNode<string> currentLevel;

    void Awake()
    {
        if (Instance == null)
        {
            levels = new LinkedList<string>(LEVEL_LIST);
            Debug.Log(gameObject.scene.name);
            currentLevel = levels.Find(gameObject.scene.name);
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GotoNextLevel()
    {
        if (currentLevel.Next != null)
        {
            currentLevel = currentLevel.Next;
            SceneManager.LoadScene(currentLevel.Value);
        } else
        {
            Debug.Log("You win!");
        }
    }

    public void HandleDeath()
    {
        string failedLevel = currentLevel.Value;
        SceneManager.LoadScene(DEATH_LEVEL);
        StartCoroutine(RestartLevel(failedLevel));
    }

    IEnumerator<UnityEngine.WaitForSecondsRealtime> RestartLevel(string level)
    {
        yield return new WaitForSecondsRealtime(0.6f);
        SceneManager.LoadScene(level);
    }
}
