using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    float delay = 2f;

    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successPS;
    [SerializeField] ParticleSystem crashPS;
    
    bool isTransitioning = false;
    bool debugCollisions = false;

    AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        DoDebugStuff();
    }

    void DoDebugStuff()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            LoadNextLevel();
            Debug.Log("NEEEEEEEEEEEEEEEEXT");
        }
        if(Input.GetKeyDown(KeyCode.C)){
            debugCollisions = !debugCollisions;
            if(debugCollisions)
                Debug.Log(">:( Collisions)");
            else
                Debug.Log(":) Collisions)");
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || debugCollisions) {return;}
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("dw u good");
                break;
            case "Finish":
                Debug.Log("we did it :D");
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("drink gasoline");
                break;
            case "Obstacle":
                Debug.Log("ow");
                StartCrashSequence();
                break;
        }
    }

    void StartSuccessSequence()
    {
        successPS.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        // particles
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", delay);
    }

    void StartCrashSequence()
    {
        crashPS.Play();
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        // particles
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", delay);
    }

    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex >= SceneManager.sceneCountInBuildSettings)
            nextSceneIndex = 0;
        SceneManager.LoadScene(nextSceneIndex);
    }

    void ReloadScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
