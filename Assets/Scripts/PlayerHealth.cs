using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {
    float MaxHaelth = 1000;
    public  float currentHealth = 0;
    public GameManager GM;
    public GameObject GameOver;
    public Image img;
    // Use this for initialization
    void Start()
    {
        currentHealth = MaxHaelth;

    }

    // Update is called once per frame
    void Update()
    {
        img.fillAmount = currentHealth / MaxHaelth;
    }
    public void getHurt(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            GameOver.SetActive(true);
            currentHealth = 0;
            StartCoroutine(LoadLevelAfterDelay(3));
        }
    }
    public float getHealth(){
        return currentHealth;
    }
    IEnumerator LoadLevelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(Application.loadedLevelName);
    }
}
