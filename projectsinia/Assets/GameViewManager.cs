using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameViewManager : MonoBehaviour
{
    [SerializeField] private Slider energySlider;
    [SerializeField] private GameObject GameOverScreen;

    [HideInInspector] public float energyScore;

    private Health playerHealth;

    private float score;
    private float currentVelocity;

    // Start is called before the first frame update
    void Start()
    {
        GameOverScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        float currentScore = Mathf.SmoothDamp(energySlider.value, energyScore, ref currentVelocity, 10 * Time.deltaTime);
        energySlider.value = currentScore;
    }

    public void GameOver()
    {
        GameOverScreen.SetActive(true);
    }

    public void changeValue()
    {
        float currentScore = Mathf.SmoothDamp(energySlider.value, score, ref currentVelocity, 100 * Time.deltaTime);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
