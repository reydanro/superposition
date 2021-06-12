using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    private string[] LEVEL_LIST = { "Title", "Level1", "Level2", "Level3" };
    public static GlobalControl Instance;
    private LinkedList<string> levels;
    private LinkedListNode<string> currentLevel;

    void Awake()
    {
        if (Instance == null)
        {
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
        levels = new LinkedList<string>(LEVEL_LIST);
        currentLevel = levels.First;
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
}
