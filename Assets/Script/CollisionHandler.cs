using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //bool collisionDisabled = false;
    //bool isTransitioning = false;
    [SerializeField] float invokeTime = 1f;
    [SerializeField] ParticleSystem deathPar;

    void Start()
    {
        deathPar.Stop();

    }

    void Update()
    {

    }


    void OnTriggerEnter(Collider other)
    {
        StartCrashEvent();
    }

    void StartCrashEvent()
    {
        GetComponent<PlayerControls>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        deathPar.Play();
        Invoke("ReloadLevel", invokeTime);
        
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
