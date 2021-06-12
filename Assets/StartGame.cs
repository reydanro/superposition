using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(LoadScene1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadScene1()
    {
        SceneManager.LoadScene("1Level");
    }
}
