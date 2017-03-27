using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour {

    AsyncOperation levelAsyncLoad;
    public void StartGame () {
        levelAsyncLoad = SceneManager.LoadSceneAsync ("Map_01", LoadSceneMode.Single);
        StartCoroutine (TrackProgress ());
    }

    IEnumerator TrackProgress () {
        while (!levelAsyncLoad.isDone) { 
            print (levelAsyncLoad.progress*100 + "%");
            yield return null;
        }
        print ("done");
    }

}
