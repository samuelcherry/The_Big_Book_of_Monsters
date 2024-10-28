using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private Text score;
    private int scoreAmount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreAmount = 0;
        score = GetComponent<Text>();

    }

    // Update is called once per frame
    private void Update()
    {
        score.text = scoreAmount.ToString();
    }

    public void AddScore()
    {
        scoreAmount += 10;
    }
}
