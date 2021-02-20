using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField]private Image _progressBar;
    public string nextScene = "Field";

    // Start is called before the first frame update
    void Start()
    {
        //start the async operation
        StartCoroutine(LoadAsyncOperation());
    }

    IEnumerator LoadAsyncOperation()
    {
        //get the async operation manager
        AsyncOperation gameLevel = SceneManager.LoadSceneAsync(nextScene);


        while(gameLevel.progress < 1)
        {
            _progressBar.fillAmount = gameLevel.progress;
            yield return new WaitForEndOfFrame();
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
