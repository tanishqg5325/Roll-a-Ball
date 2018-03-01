using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody rb;
    public Text scoreText;
    public float speed;
    private int count;
    private float score;
    public Text winText;
    private float timer;
    public Text timeText;
    public GameObject endMenuUI;
    public GameObject PlayAgain;
    public GameObject NextLevel;
    public AudioClip alarm;
    private AudioSource source;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        score = 0;
        timer = 0;
        winText.text = "";
        SetScoreText();
        SetTimeText();
        endMenuUI.SetActive(false);
        source = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        timer = timer + Time.deltaTime;
        score = score - Time.deltaTime/2;
        SetScoreText();
        SetTimeText();

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        if (Input.GetKeyDown("space"))
            rb.AddForce(new Vector3(0.0f, 300.0f, 0.0f));
        rb.AddForce(movement * speed);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickup"))
        {
            source.PlayOneShot(alarm);
            other.gameObject.SetActive(false);
            count = count + 1;
            score = score + 5;
            SetScoreText();
        }
    }

    void SetScoreText ()
    {
        scoreText.text = "Score: " + score.ToString("0.0");
        if (count == 12)
        {
            endMenuUI.SetActive(true);
            Time.timeScale = 0f;
            if (score > 55)
            {
                winText.text = "You Win!";
                PlayAgain.SetActive(false);
                NextLevel.SetActive(true);
            }
            else
                winText.text = "You Lose!";
        }
    }

    void SetTimeText()
    {
        timeText.text = "Time: " + timer.ToString("0.00");
    }
}
