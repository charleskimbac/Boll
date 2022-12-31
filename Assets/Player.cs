using UnityEngine;
using TMPro;
using UnityEngine.UI;

//speed, TEXT MANAGEMENT, TIME
public class Player : MonoBehaviour
{
    public int speed;
    private Rigidbody rb;
    private float score; //score is how many triggers reached
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI winText;
    public GameObject winTextObj;
    public GameObject menuUI;
    public GameObject activeUI;
    public Slider speedSlider;
    public TextMeshProUGUI speedText;

    private const int SCORE_TO_WIN = 75;

    private float startTime;
    private bool finished;
    private float currentTime;
    private bool movementActive;

    public MapManager mapManager;

    public LeaderboardController leaderboard;

    public float getCurrentTime(){
        Debug.Log(float.Parse(currentTime.ToString("0.000")));
        return float.Parse(currentTime.ToString("0.000"));
    }

    public int getSpeed(){
        return speed;
    }

    void Start() {
        speed = 500;
        rb = GetComponent<Rigidbody>();

        winTextObj.SetActive(false);
        finished = false;
        movementActive = false;
        activeUI.SetActive(false);
    }

    void Update(){
        if (finished){
            return;
        }
        
        if (movementActive){
            currentTime = Time.time - startTime;
            /*=
            string minutes = ((int)time / 60).ToString();
            string seconds = (time % 60).ToString("f0"); //@"mm\:ss\:fff"
            timerText.text = minutes + ":" + seconds;
            */
            //=currentTime += Time.deltaTime;
            timerText.text = currentTime.ToString("0.000");
        }
    }

    public void changeSpeed(){
        speed = (int)speedSlider.value;
        speedText.text = "Speed: " + (int)speedSlider.value;
    }

    public void gameStart(){
        startTime = Time.time;
        movementActive = true;
        menuUI.SetActive(false);
        activeUI.SetActive(true);
    }

    private void FixedUpdate() {
        if (movementActive){
            if (Input.GetKey("d") || Input.GetKey(KeyCode.RightArrow)){
                rb.AddForce(speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            if (Input.GetKey("a") || Input.GetKey(KeyCode.LeftArrow)){
                rb.AddForce(-speed * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            }
            if (Input.GetKey("w") || Input.GetKey(KeyCode.UpArrow)){
                rb.AddForce(0, 0, speed * Time.deltaTime, ForceMode.VelocityChange);
            }
            if (Input.GetKey("s") || Input.GetKey(KeyCode.DownArrow)){
                rb.AddForce(0, 0, -speed * Time.deltaTime, ForceMode.VelocityChange);
            }
        }
    }

    public void reset(){
        winTextObj.SetActive(false);
        score = 0;
        finished = false;
        setScoreText();
        movementActive = false;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("trigger")){
            //other.gameObject.SetActive(false);
            score += 1;
            setScoreText();
        }

        mapManager.onCollision(other);
    }

    private void setScoreText() {
        scoreText.text = score.ToString("00.") + "/" + SCORE_TO_WIN;
        if (score >= SCORE_TO_WIN) {
            finished = true;
            winTextObj.SetActive(true);
            winText.text = "Your time was \n" + currentTime.ToString("0.000") + "s" + "\n\n Press Space to continue";
            /*
            leaderboard.changeScore(currentTime);
            leaderboard.showCurrentScore();
            */
        }
    }

    public bool finishedRound(){
        return finished;
    }
}
