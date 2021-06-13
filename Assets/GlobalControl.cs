using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    private readonly string[] LEVEL_LIST = { "Title", "Level1", "Level2", "Level3", "Level4", "Level5", "Level6", "Level7", "Level8", "Level9", "Level10", "Level11", "Level12", "Level13", "Credits" };
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
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Quitting");
            Application.Quit();
        }

        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(currentLevel.Value);
        }
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
