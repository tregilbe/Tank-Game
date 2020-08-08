using System.Collections;
using System.Collections.Generic;
using ChrisTutorials.Persistent;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Powerup powerup;
    public AudioClip FeedbackAudioClip;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnTriggerEnter(Collider other)
    {
        // Get the other object's powerup controller
        PowerupController powerupController = other.gameObject.GetComponent<PowerupController>();

        if (powerupController != null)
        {
            powerupController.AddPowerup(powerup);

            Destroy(this.gameObject);
        }
    }

    void Awake()
    {
        GameManager.Instance.activePickups.Add(this.gameObject.GetComponent<Pickup>());      
    }

    void OnDestroy()
    {
        AudioManager.Instance.Play(FeedbackAudioClip, transform);
        GameManager.Instance.activePickups.Remove(this.gameObject.GetComponent<Pickup>());
    }
}
