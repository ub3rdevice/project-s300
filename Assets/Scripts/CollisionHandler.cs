using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
   
    void OnCollisionEnter(Collision other) 
    {
         switch (other.gameObject.tag)
         {
            case "Friendly":
                Debug.Log("This is a friendly thing");
                break;
            case "Finish":
                Debug.Log("Congratz you finished!");
                break;
            case "Fuel":
                Debug.Log("You've picked up fuel");
                break;
            default:
                Debug.Log("Sorry, you're done!");
                ReloadLevel();
                break;
         }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

}
