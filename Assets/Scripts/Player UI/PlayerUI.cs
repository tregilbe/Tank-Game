using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public TankData player;
    public Text scoreText;
    public Text livesText;
    public Text healthText;

    void Update()
    {
        scoreText.text = player.Score.ToString();
        livesText.text = "Lives: " + GameManager.Instance.PlayerOneLives.ToString();
        healthText.text = "Health: " + player.currentHealth.ToString() + " / " + player.maxHealth.ToString();
    }
}
