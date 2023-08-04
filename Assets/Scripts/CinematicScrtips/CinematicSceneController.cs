using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CinematicSceneController : MonoBehaviour
{

    [Header("Cameras")]
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera spaceShipCamera;
    [SerializeField] Camera thirdCamera;
    [SerializeField] Camera fourthCamera;

    [Header("Canvas Objects")]
    [SerializeField] public TextMeshProUGUI dateText;
    [SerializeField] public TextMeshProUGUI titleText;
    [SerializeField] public TextMeshProUGUI gsogamingText;
    [SerializeField] public TextMeshProUGUI inGameText;
    [SerializeField] public TextMeshProUGUI speakersName;
    [SerializeField] GameObject dialogueHolder;
    [SerializeField] GameObject title;
    [SerializeField] GameObject yourLookAtPos;
    [SerializeField] Button startButton;
    [Range(0f, 3f)] // This sets the range of the slider to be between 0 and 3    
    [SerializeField] float writeSpeed;
    [SerializeField] float secondsAfterTextIsErased;

    private TextWriter.TextWriterSingle textWriterSingle;
    string storedText;

    [Header("In Game objects for reference")]
    [SerializeField] GameObject planet;
    [SerializeField] GameObject spaceShip;

    AudioSource music;
    Vector3 shipsOriginalPos;
    Vector3 planetOriginalPos;


    [SerializeField] float firstSection, secondSection, thirdSection,fourthSection,finalSection;

    bool firstShotDone;
    bool secondShotDone;
    bool thirdShotDone;
    bool fourthShotDone;
    bool fifthShotDone;


    private void Awake()
    {
        textWriterSingle = TextWriter.AddWriter_Static(dateText, dateText.text, writeSpeed, true, true);
    }


    // Start is called before the first frame update
    void Start()
    {
        shipsOriginalPos = spaceShip.transform.position;
        planetOriginalPos = planet.transform.position;
        Debug.Log(shipsOriginalPos);
        firstShotDone = false;
        secondShotDone = false;
        thirdShotDone = false;
        fourthShotDone = false;
        fifthShotDone = false;
        music = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        FirstShot();
        SecondShot();
        ThirdShot();
        FourthShot();
        FifthShot();


    }

    private void FirstShot()
    {
        if (!firstShotDone)
        {

            if (spaceShip.transform.position.z <= firstSection)
            {

                mainCamera.gameObject.SetActive(false);
                spaceShipCamera.gameObject.SetActive(true);
                spaceShip.transform.position = shipsOriginalPos;
                spaceShip.GetComponent<SpaceShuttleController>().moveSpeed /= 2;
                dateText.GetComponent<Animator>().SetBool("fade", true);
                writeSpeed = 0.2f;
                gsogamingText.gameObject.SetActive(true);
                textWriterSingle = TextWriter.AddWriter_Static(gsogamingText, gsogamingText.text, writeSpeed, true, true);
                firstShotDone = true;
            }
        }
    }

    private void SecondShot()
    {
        if (firstShotDone)
        {

            
            planet.transform.Translate(Vector3.back * 0.2f * Time.deltaTime);

            if (spaceShip.transform.position.z <= secondSection && !secondShotDone)
            {
                dialogueHolder.SetActive(true);
                gsogamingText.GetComponent<Animator>().SetBool("fade", true);
                writeSpeed = 0.1f;
                speakersName.text = "Lukas";
                textWriterSingle = TextWriter.AddWriter_Static(inGameText, inGameText.text, writeSpeed, true, true);
                secondShotDone = true;
            }

        }
    }

    private void ThirdShot()
    {
        if (secondShotDone)
        {
            
            if (spaceShip.transform.position.z <= thirdSection && !thirdShotDone)
            {
                speakersName.text = "Captain";
                textWriterSingle = TextWriter.AddWriter_Static(inGameText, "Can you decipher them Lukas?" , writeSpeed, true, true);
                spaceShip.transform.position = shipsOriginalPos;
                planet.transform.position = planetOriginalPos;
                thirdCamera.gameObject.SetActive(true);
                spaceShipCamera.gameObject.SetActive(false);
                spaceShip.GetComponent<SpaceShuttleController>().moveSpeed = 3;
                thirdShotDone = true;
            }
        }
    }

    private void FourthShot()
    {
        if (thirdShotDone)
        {

            if (spaceShip.transform.position.z <= fourthSection && !fourthShotDone)
            {
                speakersName.text = "Lucas";
                textWriterSingle = TextWriter.AddWriter_Static(inGameText, "Negative sir. What are your orders sir, Shall we go back?", writeSpeed, true, true);
                spaceShip.transform.position = shipsOriginalPos;
                planet.transform.position = planetOriginalPos;
                planet.transform.localScale = new Vector3(200, planet.transform.localScale.y, planet.transform.localScale.z);
                fourthCamera.gameObject.SetActive(true);
                thirdCamera.gameObject.SetActive(false);
                spaceShip.GetComponent<SpaceShuttleController>().moveSpeed = .5f;
                fourthShotDone = true;
            }          


        }
    }

    private void FifthShot()
    {
        if (fourthShotDone)
        {
            if (spaceShip.transform.position.z <= finalSection && !fifthShotDone)
            {
                speakersName.text = "Captain";
                textWriterSingle = TextWriter.AddWriter_Static(inGameText, "No son, we never go back", writeSpeed, true, true);
                fifthShotDone = true;
                title.SetActive(true);                
                fifthShotDone = true;

                StartCoroutine(FifthShotCoroutine());
                

            }
        }
    }
   

    private IEnumerator FifthShotCoroutine()
    {
        // Slowly move the camera upwards
        Vector3 lookAtPos = yourLookAtPos.transform.position;
        Quaternion initialCamRot = fourthCamera.transform.rotation;
        Quaternion targetCamRot = Quaternion.LookRotation(lookAtPos - fourthCamera.transform.position);
        float startTime = Time.time;

        while (Time.time - startTime < 10)
        {
            bool shouldHideDialogue = true;
            float t = (Time.time - startTime) / 10;
            fourthCamera.transform.rotation = Quaternion.Slerp(initialCamRot, targetCamRot, t);
            if (Time.time - startTime > 5 && shouldHideDialogue)
            {
                dialogueHolder.SetActive(false);
                startButton.gameObject.SetActive(true);
                shouldHideDialogue = false;
            }
            yield return null;
        }

    }



 }
