using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathVFX;
    [SerializeField] int scorePerHit = 15;
    [SerializeField] int hitPoints = 1;
    [SerializeField] GameObject hitVFX;

    Scoreboard scoreboard;
    GameObject parentGameObject;    //doi ten de phan biet voi'parent'

    void Start()
    {
        scoreboard = FindObjectOfType<Scoreboard>();
        parentGameObject = GameObject.FindWithTag("SpawnAtRuntime");   //dung tag de dua khi tao clone onhit thi se vao SpawnAtRuntime
    }

    public void OnParticleCollision(GameObject other)
    {
        ProcessHit();
        if (hitPoints < 1)
        {
            KillEnemy();
        }
        
    }

    private void ProcessHit()
    {
        GameObject vfx = Instantiate(hitVFX, transform.position, Quaternion.identity);
        vfx.transform.parent = parentGameObject.transform;
        hitPoints--;
        scoreboard.IncreaseScore(scorePerHit);
    }

    private void KillEnemy()
    {
        GameObject vfx = Instantiate(deathVFX, transform.position, Quaternion.identity);  //identity: we don't need rotation
        vfx.transform.parent = parentGameObject.transform;
        Destroy(gameObject);
    }
}
