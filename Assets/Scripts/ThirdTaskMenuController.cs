using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class ThirdTaskMenuController : MonoBehaviour
{
    public TextMeshPro levelTextContent;
    public TextMeshPro levelTextNumber;
    public TextMeshPro levelText;
    public int currentLevel = 1;
    public int hintNumber = 0;
    public int maxLevel = 15; // maksymalna liczba poziomów
    Renderer rend;
    public GameObject TaskScreen;
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    // [SerializeField] private Material TaskHintPicturesScreen;
    // Two-dimensional array of materials
    // Variable to store the hint number
    private List<string> levelTaskTitle3 = new List<string>
    {
        "Initial Safety Check", //instrukcja detekcji nagraæ detekcjê przyk³dow¹ do komponentów, fragment z aplikacji
        "Install the RC and CR Filter Module",
        "Connect the DC Power Supply",
        "Split the Generator Signal",
        "Attach the Signal Generator",
        "Connect Filter Output to Oscilloscope",
        "Connect FILTER RC with FILTER CR. Select the Right Time Constant.", //nagrac filmik ?
        "Activate the DC Power Supply",
        "Generate a Sine Wave Signal",
        "Measure the Amplitude Response",
        "Plot Amplitude vs Frequency", //samemu nagrac filmik (jakas animacja rysowani wykresu, t³umaczaca zaleznosci) wziac fragment nagrania z calego przedstawienia (wpisywania danych do tabeli i rysowanie wykresu
        "Produce a Square Wave Signal",
        "Record the Step Response", //nagraæ filmik (pomiary skoku) z oscyloskopu
        "Match Data to an Exponential Curve", //samemu nagrac filmik (t³umaczenie teorii ?) analogicznie jak wyzej
        "Submit Your Feedback", //bez filmika i hintow
    };

    private List<string> levelTaskContent3 = new List<string>
    {
        "Turn off all devices and confirm safety. Ensure the necessary equipment is present: Oscilloscope, DC Power Supply, Signal Generator, Base Board, and CR Filter modules. If anything is missing, return to Explore Mode.", //1
        "Place the RC filter module into the FILTER 1, and CR filter module into the FILTER 2 socket on the measurement board.", //2
        "Connect the outputs of the DC power supply to the filter's input.", //3
        "Use a BNC T-connector to split the signal from the generator, connecting one output to the filter Base Board.", //4
        "Attach one output of the generator to Channel 1 of the oscilloscope.", //5
        "Connect the output from the Base Board's filter to Channel 2 of the oscilloscope.", //6
        "Connect the J11 and J13 connectors to ensure the cascading connection of both filters. Set the appropriate time constant on the filter module using the jumper J1b (2K) for FILTER RC and J1b (220pF) for FILTER CR.", //7
        "Turn on the DC power supply, setting one output to -15V and the other to +15V.", //8
        "Set the signal generator to produce a sinusoidal signal with a small amplitude of 100 [mV] (100-200 mVpp).", //9
        "Record the peak-to-peak voltage (Vpp) of the input and output signals to determine the gain. Vary the frequency from 100 Hz to 1 MHz, taking several measurements per decade.", //10
        "The amplitude characteristic (gain) Ku as a function of frequency f is shown on an additional screen. This characteristic includes the passband gain, cutoff frequency, and the asymptotic slope of the characteristic in the stopband for comparison with theoretical values.", //11
        "Set the signal generator to produce a square wave signal with a small amplitude of 100 [mV] (100-200 mVpp) and a period of 88.00 [µs], significantly greater than the filter's time constant.", //12
        "Record several voltage-time pairs on the rising edge of the filter's response to the square wave signal observed on the oscilloscope.", //13
        "Fit the collected data to an exponential function to determine the time constant and compare it with the theoretical value.", //14
        "After completing the measurements and analysis, please take a moment to provide your feedback in the About section. Your feedback is valuable and will help improve future exercises.", //15
    };

    // List of lists for materials
    [SerializeField] public List<LevelMaterials> hintPictures;
    // List of lists for video clips
    [SerializeField] public List<VideoClip> videoClips;
    [SerializeField] public List<LevelGameObjects> levelComponents;

    public Material transparentMaterial;
    public Material redLightMaterial;

    private bool isRunning = false;       // Flaga kontroluj¹ca pêtlê
    private Coroutine tutorialCoroutine;

    private List<GameObject> currentElements; // Bie¿¹ce elementy do zmiany

    public GameObject baseBoard;
    public GameObject placeForBaseBoard;
    public GameObject placeForFilter;
    public GameObject placeForFilter2;
    public GameObject Splitter;
    public GameObject graphButton;
    public GameObject endTutorial;
    
    /*  private static List<string> levelTaskHintPictures = new List<string>
        {
            "Hint1.png",
            "Hint2.png",
            "Hint3.png",
            "Hint4.png",
        };

        private static List<string> levelTaskInstructionFilm = new List<string>
        {
            "InfoFilm1.mp4",
            "InfoFilm2.mp4",
            "InfoFilm3.mp4",
            "InfoFilm4.mp4",
        };*/

    public void Update()
    {
        
    }

    public void Start()
    {
        UpdateLevelContent();
        // Na pocz¹tek ustaw przezroczysty materia³ dla wszystkich elementów
        /*foreach (var level in levelComponents)
        {
            SetAllToTransparent(level.levelElements);
        }*/
    }

    void SetAllToTransparent(List<GameObject> elements)
    {
        foreach (var element in elements)
        {
            SetMaterial(element, transparentMaterial);
        }
    }

    void SetMaterial(GameObject obj, Material material)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    /// <summary>
    /// Level position
    /// </summary>
    public void NextLevel()
    {
        Debug.Log($"NextLevel level {currentLevel}, hint {hintNumber}");
        if (currentLevel < maxLevel)
        {
            currentLevel++;
        }
        hintNumber = 0;
        UpdateLevelContent();
    }

    public void PreviousLevel()
    {
        Debug.Log($"PreviousLevel level {currentLevel}, hint {hintNumber}");
        if (currentLevel > 1)
        {
            currentLevel--;
        }
        hintNumber = 0;
        UpdateLevelContent();
    }

    // Method to increment the level and update the material
    public void NextHint()
    {
        Debug.Log($"NextHint level {currentLevel}, hint {hintNumber}");
        int maxHints = hintPictures[currentLevel-1].hints.Length;
        if (hintNumber < maxHints-1)
        {
            hintNumber++;
            //UpdateLevelContent();
        }
        UpdatePictureInstructions();

    }

    public void PreviousHint()
    {
        Debug.Log($"PreviousHint level {currentLevel}, hint {hintNumber}");
        if (hintNumber > 0)
        {
            hintNumber--;
            //UpdateLevelContent();
        }
        UpdatePictureInstructions();
    }

    private void UpdateLevelContent()
    {
        levelTextNumber.text = currentLevel.ToString();
        levelTextContent.text = levelTaskContent3[currentLevel-1];
        levelText.text = levelTaskTitle3[currentLevel - 1];

        if(currentLevel == 11 || currentLevel == 14)
        {
            graphButton.SetActive(true);
        }else
        {
            graphButton.SetActive(false);
        }

        if (currentLevel == 15)
        {
            endTutorial.SetActive(true);
        }
        else
        {
            endTutorial.SetActive(false);
        }
        //ShowPictureInstructions();
        //ShowFilmInstructions();

    }

    public void UpdatePictureInstructions()
    {
        //rend = TaskScreen.GetComponent<Renderer>();
        //rend.enabled = true;
        //rend.sharedMaterial = TaskHintPicturesScreen;
        //string instructionImageName = "Step_" + currentLevel + ".png";
        int level = currentLevel - 1;
        int hint = hintNumber;
        // Validate the level and hint numbers
        Debug.Log("Nazy: " + level + " " + hint);
        if (level >= 0 && level < hintPictures.Count && hint >= 0 && hint < hintPictures[level].hints.Length)
        {
            Material newMaterial = hintPictures[level].hints[hint];
            Debug.Log("Material : " + level + " " + hint);
            if (newMaterial != null)
            {
                Debug.Log("OK Material: " + level + " " + hint);
                rend = TaskScreen.GetComponent<Renderer>();
                if (rend == null)
                {
                    Debug.LogError("Renderer component is missing on the object!");
                }
                else
                {
                    rend.enabled = true;
                    rend.sharedMaterial = newMaterial;
                    Debug.Log($"Successfully changed material for level {level}, hint {hint}");
                }
            }
            else
            {
                Debug.LogError($"Material for level {level} and hint {hint} is null!");
            }
        }
        else
        {
            Debug.LogError($"Invalid level ({level}) or hint ({hint}) number!");
        }

        // Wyœwietlanie instrukcji dla bie¿¹cego poziomu
        // Mo¿esz u¿yæ tej nazwy pliku do wyœwietlenia odpowiedniego obrazu w interfejsie u¿ytkownika.
    }

    public void ToggleVideoPlayer()
    {
        if (videoPlayer.isPlaying)
        {
            PauseVideo();
        }
        else
        {
            PlayVideo();
        }
    }

    public void PlayVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            Debug.Log("Video is playing");
        }
    }

    public void PauseVideo()
    {
        if (videoPlayer != null)
        {
            videoPlayer.Pause();
            Debug.Log("Video is paused");
        }
    }

    public void SeekForward()
    {
        if (videoPlayer != null && videoPlayer.canSetTime)
        {
            double newTime = videoPlayer.time + 5.0;
            if (newTime < videoPlayer.length)
            {
                videoPlayer.time = newTime;
                Debug.Log("Video seeked forward by 10 seconds");
            }
        }
    }

    public void SeekBackward()
    {
        if (videoPlayer != null && videoPlayer.canSetTime)
        {
            double newTime = videoPlayer.time - 5.0;
            if (newTime > 0)
            {
                videoPlayer.time = newTime;
                Debug.Log("Video seeked backward by 10 seconds");
            }
            else
            {
                videoPlayer.time = 0;
                Debug.Log("Video seeked to the start");
            }
        }
    }

    public void ShowFilmInstructions()
    {
        int level = currentLevel - 1;
        videoPlayer.clip = videoClips[level];
        Debug.Log($"Video clip set to level {currentLevel}");
        //string instructionImageName = videoClips[currentLevel-1]; //analogicznie film
        // Wyœwietlanie instrukcji dla bie¿¹cego poziomu
        // Mo¿esz u¿yæ tej nazwy pliku do wyœwietlenia odpowiedniego obrazu w interfejsie u¿ytkownika.
    }

    public void ShowTheoryInstructions()
    {
        //jedno theroy info
        //string instructionImageName = videoClips[currentLevel - 1]; //analogicznie film
        // Wyœwietlanie instrukcji dla bie¿¹cego poziomu
        // Mo¿esz u¿yæ tej nazwy pliku do wyœwietlenia odpowiedniego obrazu w interfejsie u¿ytkownika.
    }

    public void ShowAnimationInstructions()
    {
        //switch case po numerze poziomu
    }


    /// <summary>
    /// animation
    /// </summary>

    public void StartTutorial()
    {
        int level = currentLevel - 1;
        if (!isRunning)
        {
            isRunning = true;
            currentElements = levelComponents[level].levelElements;
            tutorialCoroutine = StartCoroutine(ChangeMaterialsLoop());
        }
    }

    public void StopTutorial()
    {
        if (isRunning)
        {
            isRunning = false;
            StopCoroutine(tutorialCoroutine);
            SetAllToTransparent(currentElements); // Przywróæ przezroczystoœæ przed zatrzymaniem zatrzymaniu
            for (int i = 0; i < currentElements.Count; i++)
            {
                currentElements[i].SetActive(false);
            }

            placeForBaseBoard.SetActive(false);
            placeForFilter.SetActive(false);
            placeForFilter2.SetActive(false);
            Splitter.SetActive(false);
        }
    }

    public void ToggleTutorialPlay()
    {
        if (isRunning)
        {
            StopTutorial();
        }
        else
        {
            StartTutorial();
        }
    }

    private IEnumerator ChangeMaterialsLoop()
    {
        while (isRunning)
        {
            //int level = currentLevel - 1;
            switch (currentLevel)
            {
                case 1:
                    Step01Animation();
                    break;
                case 2:
                    placeForBaseBoard.SetActive(true);
                    yield return StartCoroutine(Step02Animation()); // Dodanie opóŸnienia w animacji
                    break;
                case 3:
                    Step03Animation();
                    break;
                case 4:
                    yield return StartCoroutine(Step04Animation()); // Dodanie opóŸnienia w animacji
                    break;
                // Dodaj wiêcej przypadków dla innych poziomów
                default:
                    break;
            }
            for (int i = 0; i < currentElements.Count; i++)
            {
                if (!isRunning) yield break; // SprawdŸ, czy pêtla powinna zostaæ przerwana
                currentElements[i].SetActive(true);
                // Zmieñ materia³ na czerwony
                SetMaterial(currentElements[i], redLightMaterial);
                yield return new WaitForSeconds(1.8f);

                
                // Zmieñ materia³ na przezroczysty
                SetMaterial(currentElements[i], transparentMaterial);
                currentElements[i].SetActive(false);
                yield return new WaitForSeconds(0.4f);
            }
            yield return new WaitForSeconds(1.7f);
        }
    }

    public void Step01Animation()
    {
        // Logika animacji dla poziomu 1
        Debug.Log("Step01Animation executed");
    }

    private IEnumerator Step02Animation()
    {
        // Odczekaj 2 sekundy przed sprawdzeniem kolizji
        Debug.Log("Green appear");
        if (placeForBaseBoard != null && baseBoard != null)
        {
            // SprawdŸ kolizjê pomiêdzy pierwszym a drugim elementem
            Collider collider1 = placeForBaseBoard.GetComponent<Collider>();
            Collider collider2 = baseBoard.GetComponent<Collider>();

            if (collider1 != null && collider2 != null)
            {
                if (collider1.bounds.Intersects(collider2.bounds))
                {
                    Debug.Log("Step01Animation: Element 1 collides with Element 2");
                    yield return new WaitForSeconds(2f);
                    placeForBaseBoard.SetActive(false);
                    placeForFilter.SetActive(true);
                    placeForFilter2.SetActive(true);
                    yield return new WaitForSeconds(10f);
                    placeForFilter.SetActive(false);
                    placeForFilter2.SetActive(false);
                    // Add additional logic to execute when collision is detected
                }
                else
                {
                    Debug.Log("Step01Animation: Element 1 does not collide with Element 2");
                }
            }
            else
            {
                Debug.LogWarning("Step01Animation: One of the elements does not have a Collider");
            }
        }
        else
        {
            Debug.LogWarning("Step01Animation: Elements for collision check are not assigned");
        }
    }

    public void Step03Animation()
    {
        // Logika animacji dla poziomu 3
        Debug.Log("Step03Animation executed");
    }

    private IEnumerator Step04Animation()
    {
        // Logika animacji dla poziomu 4
        if (Splitter != null)
        {
            Debug.Log("Step04Animation executed");
            Splitter.SetActive(true);
            yield return new WaitForSeconds(10f);
            Splitter.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Step04Animation: Elements for collision check are not assigned");
        }
    }
    






}
