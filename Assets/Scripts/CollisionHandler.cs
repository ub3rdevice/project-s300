using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float lvlLoadDelay = 2f;
    [SerializeField] AudioClip obstacleHitSound;
    [SerializeField] AudioClip successSound;
    AudioSource audioSource;

    void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    void OnCollisionEnter(Collision other) 
    {
         switch (other.gameObject.tag)
         {
            case "Friendly":
                Debug.Log("This is a friendly thing");
                break;
            case "Finish":
                Debug.Log("Congratz you finished!");
                StartLoadingSequence();
                break;
            default:
                Debug.Log("Sorry, you're done!");
                StartCrashSequence();
                break;
         }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex +1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings){
            nextSceneIndex = 0;
        }
        
        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartCrashSequence()
    {
        audioSource.PlayOneShot(obstacleHitSound);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",lvlLoadDelay);
    }

    void StartLoadingSequence()
    {
        audioSource.PlayOneShot(successSound);
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel",lvlLoadDelay);
    }
}
