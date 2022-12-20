using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class playerController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Text uitWelcome;
    public Text uitDial;
    public Text uitPickUp;
    private AudioSource audioPickUp;

    private Rigidbody rb;

    public int pickUpCount;
    public int lifeCount = 3;
    Vector3 originalPos;

    public Text uitTimer;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
    public Text uitRecord;
    private float recordSeconds;
    private int recordMinute;
    private int recordHour;
    public GameObject finalCastle;
    public Text uitStats;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        pickUpCount = 0;
        uitWelcome.enabled = true;
        Invoke("welcomeDecay", 15f);
        uitDial.enabled = false;
        finalCastle.SetActive(false);
        audioPickUp = GetComponent<AudioSource>();
        uitStats.enabled = false;
    }

    void Awake()
    {
        if (PlayerPrefs.HasKey("secondsCount")) //Only checking if it has a record for seconds since if it has that, it should have fulfilled the other requirements to have an updating record. But, may need to add && minuteCount && hourCount... Just maybe.
        {
            recordSeconds = PlayerPrefs.GetFloat("secondsCount");
            recordMinute = PlayerPrefs.GetInt("minuteCount");
            recordHour = PlayerPrefs.GetInt("hourCount");
        }
        PlayerPrefs.SetFloat("recordSeconds", secondsCount);
        PlayerPrefs.SetInt("recordMinute", minuteCount);
        PlayerPrefs.SetInt("recordHour", hourCount);
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimerUI();
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("07-Prototype3");
        }

        //if (hourCount < PlayerPrefs.GetInt("recordHour"))
        //{
        //    if (minuteCount < PlayerPrefs.GetInt("recordMinute"))
        //    {
        //        if (secondsCount < PlayerPrefs.GetFloat("recordSeconds"))
        //        {
        //            PlayerPrefs.SetInt("recordHour", hourCount);
        //            PlayerPrefs.SetInt("recordMinute", minuteCount);
        //            PlayerPrefs.SetFloat("recordSeconds", secondsCount);
        //        }
        //    }
        //}

    }

    void welcomeDecay()
    {
        uitWelcome.enabled = false;
    }

    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        uitTimer.text = hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s";
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }

    void SetPUCount()
    {
        uitPickUp.text = "Fruit Count: " + (pickUpCount);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PickUps"))
        {
            other.gameObject.SetActive(false);
            audioPickUp.Play();
            pickUpCount = pickUpCount + 1;
            SetPUCount();
            if (pickUpCount == 10)
            {
                finalCastle.SetActive(true);
                uitStats.text = "Congratulations! Your record run was " + recordHour + "h:" + recordMinute + "m:" + (int)recordSeconds + "s! This run was " + hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s";
                uitStats.enabled = true;
                if (hourCount < PlayerPrefs.GetInt("recordHour"))
                {
                    if (minuteCount < PlayerPrefs.GetInt("recordMinute"))
                    {
                        if (secondsCount < PlayerPrefs.GetFloat("recordSeconds"))
                        {
                            PlayerPrefs.SetInt("hourCount", hourCount);
                            PlayerPrefs.SetInt("minuteCount", minuteCount);
                            PlayerPrefs.SetFloat("secondsCount", secondsCount);
                        }
                    }
                }
            }
        }


        if (other.gameObject.CompareTag("Water"))
        {
            SceneManager.LoadScene("07-Prototype3");
        }

        if (other.gameObject.CompareTag("Portal"))
        {
            uitDial.text = "This is where you came through. It doesn't seem to work this way.";
            uitDial.enabled = true;
        }
        if (other.gameObject.CompareTag("Pug1"))
        {
            uitDial.text = "Hey! Like my sandcastle?";
            uitDial.enabled = true;
        }
        if (other.gameObject.CompareTag("Pug2"))
        {
            uitDial.text = "Have a fruit! I think there's 10 on Arkosa...";
            uitDial.enabled = true;
        }
        if (other.gameObject.CompareTag("Pug3"))
        {
            uitDial.text = "I saw some berries in there but I'm scared to go...";
            uitDial.enabled = true;
        }
        if (other.gameObject.CompareTag("Pug4"))
        {
            uitDial.text = "I hear this place is why the water hurts when ya step in it.";
            uitDial.enabled = true;
        }
        if (other.gameObject.CompareTag("Pug5"))
        {
            if (pickUpCount == 10)
            {
                uitDial.text = "Congratulations! You collected all berries and beat the game at the time of: " + hourCount + "h:" + minuteCount + "m:" + (int)secondsCount + "s" + "!";

            } else
            {
                uitDial.text = "You don't have 10 berries yet... Can you come back with 10?";
            }
            uitDial.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        uitDial.enabled = false;
    }
}