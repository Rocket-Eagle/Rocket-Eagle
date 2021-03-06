using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoading : MonoBehaviour
{
    [SerializeField]private Image _progressBar;

    //the scene that is supposted to be loaded next
    //as we only have 1 map this is easy for now, 
    //but will need to be changed in the sprint where 
    //we add more maps
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

        //while the level is still loading, update the progress bar
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
