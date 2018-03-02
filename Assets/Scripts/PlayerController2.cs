using UnityEngine;
using UnityEngine.UI;

public class PlayerController2 : MonoBehaviour
{
    public float speed;
    private Rigidbody rb;
    private Collider c;
    private int count1;
    private int count2;
    public Text winText;
    private float score;
    private float timer;
    public Text timeText;
    public Text scoreText;
    public GameObject endMenuUI;
    public GameObject PlayAgain;
    public GameObject NextLevel;
    public AudioClip alarm;
    public AudioClip smash;
    private AudioSource source;
    private AudioSource source1;
    private GameObject[] pickups2;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count1 = 0;
        count2 = 0;
        timer = 0;
        score = 0;
        SetTimeText();
        SetScoreText();
        winText.text = "";
        endMenuUI.SetActive(false);
        source = GetComponent<AudioSource>();
        source1 = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        timer = timer + Time.deltaTime;
        score = score - Time.deltaTime / 2;
        SetTimeText();
        SetScoreText();
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown("space"))
            rb.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUp1"))
        {
            source.PlayOneShot(alarm);
            other.gameObject.SetActive(false);
            count1 = count1 + 1;
            score = score + 5;
            SetScoreText();
            if(count1 == 6)
            {
                pickups2 = GameObject.FindGameObjectsWithTag("PickUp2");
                foreach (GameObject pickup in pickups2)
                {
                    c = pickup.GetComponent<Collider>();
                    c.isTrigger = true;
                }
            }
        }
        else if (other.gameObject.CompareTag("PickUp2") && count2 < 5 )
        {
            source.PlayOneShot(alarm);
            other.gameObject.SetActive(false);
            count2 = count2 + 1;
            score = score + 5;
            SetScoreText();
        }
        else if (other.gameObject.CompareTag("PickUp2"))
        {
            source.PlayOneShot(alarm);
            other.gameObject.SetActive(false);
            count2 = count2 + 1;
            score = score + 5;
            SetScoreText();
            if (score > 50)
            {
                winText.text = "You Win!";
                PlayAgain.SetActive(false);
                NextLevel.SetActive(true);
            }
            else
                winText.text = "You Lose!";
            Time.timeScale = 0f;
            endMenuUI.SetActive(true);
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("PickUp2"))
        {
            source1.PlayOneShot(smash);
            score = score - 5;
            SetScoreText();
        }
    }

    void SetTimeText()
    {
        timeText.text = "Time: " + timer.ToString("0.00");
    }

    void SetScoreText()
    {
        scoreText.text = "Score: " + score.ToString("0.0");
    }
}
