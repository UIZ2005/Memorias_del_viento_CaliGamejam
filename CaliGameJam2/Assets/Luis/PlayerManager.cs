using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public Vector3 respawn;
    public GameObject hitscreen;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setspawn(Transform spawnpoint)
    {
        respawn = spawnpoint.position;
    }
    public void getDamage()
    {
        StartCoroutine(perdervida());
    }
    IEnumerator perdervida()
    {
        audioManager.seleccionAudio(1);
        hitscreen.GetComponent<Animator>().SetBool("hit", true);
        gameObject.transform.position = respawn;
        yield return new WaitForSeconds(1f);
        hitscreen.GetComponent<Animator>().SetBool("hit", false);
        //reproducir animacion de despertar

        yield return null;
    }
}
