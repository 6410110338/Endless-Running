using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScore : MonoBehaviour
{
    public float score = 0.0f;
    [SerializeField] private Text scoreText;

    [SerializeField] private int difficultyLevel = 1;
    [SerializeField] private int maxDifficultyLevel = 10;
    [SerializeField] private int scoreToNextLevel = 10;

    private bool isDeath = false;
    public DeathMenu deathMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isDeath)
            return;

        if (score >= scoreToNextLevel)
            LevelUp();

        score += Time.deltaTime;
        scoreText.text = ((int)score).ToString();
    }

    void LevelUp()
    {
        if (difficultyLevel == maxDifficultyLevel)
            return;

        scoreToNextLevel *= 2;
        difficultyLevel++;

        GetComponent<PlayerMovement>().SetSpeed(difficultyLevel);
    }

    public void OnDeath()
    {
        isDeath = true;
        deathMenu.ToggleEndMenu(score);
    }

    public void AddScore(int modifier)
    {
        score += modifier;
    }
}
