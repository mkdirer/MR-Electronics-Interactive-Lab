using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

/// <summary>
/// Helper classes to handle materials and objects.
/// They can be in a separate file if you prefer.
/// </summary>
[System.Serializable]
public class LevelMaterials
{
    public Material[] hints;
}

[System.Serializable]
public class LevelGameObjects
{
    public List<GameObject> levelElements;
}

/// <summary>
/// A base controller for all tasks.
/// Contains shared logic such as handling levels, hints, video playback,
/// animations, and references to common scene objects.
/// </summary>
public abstract class BaseTaskMenuController : MonoBehaviour
{
    // Shared fields
    [Header("Level Data")]
    public TextMeshPro levelTextContent;
    public TextMeshPro levelTextNumber;
    public TextMeshPro levelText;

    [Header("Level & Hints")]
    public int currentLevel = 1;
    public int hintNumber = 0;
    [SerializeField] protected int maxLevel = 15;

    [Header("UI & Video")]
    public GameObject TaskScreen;
    public VideoPlayer videoPlayer;

    // Collections of hint materials (images/textures) for each level
    [SerializeField] public List<LevelMaterials> hintPictures;
    // Video clips for each level
    [SerializeField] public List<VideoClip> videoClips;
    // A list of lists of objects for each level
    [SerializeField] public List<LevelGameObjects> levelComponents;

    [Header("Highlighting Materials")]
    public Material transparentMaterial;
    public Material redLightMaterial;

    // Flag and coroutine for running the highlight/animation loop
    protected bool isRunning = false;
    protected Coroutine tutorialCoroutine;

    // Current set of objects to be animated/highlighted
    protected List<GameObject> currentElements;

    // Scene references – not all will be used in every controller
    [Header("Scene Objects (optional)")]
    public GameObject baseBoard;
    public GameObject placeForBaseBoard;
    public GameObject placeForFilter;
    public GameObject placeForFilter2; // Used for ThirdTaskMenuController
    public GameObject Splitter;
    public GameObject graphButton;
    public GameObject endTutorial;

    // Abstract or virtual properties that must be provided in derived classes
    protected abstract List<string> LevelTaskTitles { get; }
    protected abstract List<string> LevelTaskContents { get; }

    /// <summary>
    /// Called on Start. Updates the UI content for the current level.
    /// </summary>
    public virtual void Start()
    {
        UpdateLevelContent();
    }

    protected virtual void Update()
    {

    }

    /// <summary>
    /// Sets all objects in the given list to the transparent material.
    /// </summary>
    protected void SetAllToTransparent(List<GameObject> elements)
    {
        foreach (var element in elements)
        {
            SetMaterial(element, transparentMaterial);
        }
    }

    /// <summary>
    /// Changes the material of a given object.
    /// </summary>
    protected void SetMaterial(GameObject obj, Material material)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material = material;
        }
    }

    /// <summary>
    /// Moves to the next level.
    /// </summary>
    public void NextLevel()
    {
        Debug.Log($"NextLevel -> level {currentLevel}, hint {hintNumber}");
        if (currentLevel < maxLevel)
        {
            currentLevel++;
        }
        hintNumber = 0;
        UpdateLevelContent();
    }

    /// <summary>
    /// Moves to the previous level.
    /// </summary>
    public void PreviousLevel()
    {
        Debug.Log($"PreviousLevel -> level {currentLevel}, hint {hintNumber}");
        if (currentLevel > 1)
        {
            currentLevel--;
        }
        hintNumber = 0;
        UpdateLevelContent();
    }

    /// <summary>
    /// Moves to the next hint (if available).
    /// </summary>
    public void NextHint()
    {
        Debug.Log($"NextHint -> level {currentLevel}, hint {hintNumber}");
        int maxHints = hintPictures[currentLevel - 1].hints.Length;
        if (hintNumber < maxHints - 1)
        {
            hintNumber++;
        }
        UpdatePictureInstructions();
    }

    /// <summary>
    /// Moves to the previous hint (if available).
    /// </summary>
    public void PreviousHint()
    {
        Debug.Log($"PreviousHint -> level {currentLevel}, hint {hintNumber}");
        if (hintNumber > 0)
        {
            hintNumber--;
        }
        UpdatePictureInstructions();
    }

    /// <summary>
    /// Updates the UI texts and other elements according to the current level.
    /// </summary>
    protected virtual void UpdateLevelContent()
    {
        levelTextNumber.text = currentLevel.ToString();
        levelTextContent.text = LevelTaskContents[currentLevel - 1];
        levelText.text = LevelTaskTitles[currentLevel - 1];

        // Example logic for enabling buttons based on level:
        if (currentLevel == 11 || currentLevel == 14)
        {
            if (graphButton != null) graphButton.SetActive(true);
        }
        else
        {
            if (graphButton != null) graphButton.SetActive(false);
        }

        if (currentLevel == maxLevel)
        {
            if (endTutorial != null) endTutorial.SetActive(true);
        }
        else
        {
            if (endTutorial != null) endTutorial.SetActive(false);
        }
    }

    /// <summary>
    /// Displays the hint material on the TaskScreen.
    /// </summary>
    public virtual void UpdatePictureInstructions()
    {
        int levelIndex = currentLevel - 1;
        int hintIndex = hintNumber;

        if (levelIndex >= 0 && levelIndex < hintPictures.Count &&
            hintIndex >= 0 && hintIndex < hintPictures[levelIndex].hints.Length)
        {
            Material newMaterial = hintPictures[levelIndex].hints[hintIndex];
            if (newMaterial != null)
            {
                Debug.Log($"OK Material: level={levelIndex}, hint={hintIndex}");
                Renderer screenRenderer = TaskScreen.GetComponent<Renderer>();
                if (screenRenderer != null)
                {
                    screenRenderer.enabled = true;
                    screenRenderer.sharedMaterial = newMaterial;
                }
            }
            else
            {
                Debug.LogError($"Material is null for level={levelIndex}, hint={hintIndex}!");
            }
        }
        else
        {
            Debug.LogError($"Invalid levelIndex={levelIndex} or hintIndex={hintIndex}!");
        }
    }

    /// <summary>
    /// Handles toggling video playback: play/pause.
    /// </summary>
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
                Debug.Log("Video seeked forward by 5 seconds");
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
                Debug.Log("Video seeked backward by 5 seconds");
            }
            else
            {
                videoPlayer.time = 0;
                Debug.Log("Video seeked to the start");
            }
        }
    }

    /// <summary>
    /// Default method for showing video instructions for the current level.
    /// </summary>
    public virtual void ShowFilmInstructions()
    {
        int levelIndex = currentLevel - 1;
        if (levelIndex >= 0 && levelIndex < videoClips.Count && videoClips[levelIndex] != null)
        {
            videoPlayer.clip = videoClips[levelIndex];
            Debug.Log($"Video clip set for level {currentLevel}");
        }
    }

    public virtual void ShowTheoryInstructions()
    {
    }

    public virtual void ShowAnimationInstructions()
    {
    }

    /// <summary>
    /// Starts the "tutorial" (highlighting objects in a loop).
    /// </summary>
    public virtual void StartTutorial()
    {
        int levelIndex = currentLevel - 1;
        if (!isRunning && levelIndex >= 0 && levelIndex < levelComponents.Count)
        {
            isRunning = true;
            currentElements = levelComponents[levelIndex].levelElements;
            tutorialCoroutine = StartCoroutine(ChangeMaterialsLoop());
        }
    }

    public virtual void StopTutorial()
    {
        if (isRunning)
        {
            isRunning = false;
            if (tutorialCoroutine != null)
            {
                StopCoroutine(tutorialCoroutine);
            }
            if (currentElements != null)
            {
                SetAllToTransparent(currentElements);
                for (int i = 0; i < currentElements.Count; i++)
                {
                    currentElements[i].SetActive(false);
                }
            }
            // Disable other objects
            if (placeForBaseBoard) placeForBaseBoard.SetActive(false);
            if (placeForFilter) placeForFilter.SetActive(false);
            if (placeForFilter2) placeForFilter2.SetActive(false);
            if (Splitter) Splitter.SetActive(false);
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

    /// <summary>
    /// A coroutine that loops through objects, flashing red/transparent materials.
    /// </summary>
    protected virtual IEnumerator ChangeMaterialsLoop()
    {
        while (isRunning)
        {
            switch (currentLevel)
            {
                case 1:
                    Step01Animation();
                    break;
                case 2:
                    yield return StartCoroutine(Step02Animation());
                    break;
                case 3:
                    Step03Animation();
                    break;
                case 4:
                    yield return StartCoroutine(Step04Animation());
                    break;
                // Add more cases as needed
                default:
                    break;
            }

            // Common "flashing" logic for every object in the currentElements
            if (currentElements != null)
            {
                for (int i = 0; i < currentElements.Count; i++)
                {
                    if (!isRunning) yield break; // Stop if the tutorial is halted
                    currentElements[i].SetActive(true);
                    SetMaterial(currentElements[i], redLightMaterial);
                    yield return new WaitForSeconds(1.8f);

                    SetMaterial(currentElements[i], transparentMaterial);
                    currentElements[i].SetActive(false);
                    yield return new WaitForSeconds(0.4f);
                }
            }
            yield return new WaitForSeconds(1.7f);
        }
    }

    /// <summary>
    /// Example virtual/abstract methods for derived classes
    /// to implement their own step-by-step animations.
    /// </summary>
    protected virtual void Step01Animation()
    {
        Debug.Log("BaseTaskMenuController -> Step01Animation");
    }

    protected virtual IEnumerator Step02Animation()
    {
        Debug.Log("BaseTaskMenuController -> Step02Animation");
        yield break;
    }

    protected virtual void Step03Animation()
    {
        Debug.Log("BaseTaskMenuController -> Step03Animation");
    }

    protected virtual IEnumerator Step04Animation()
    {
        Debug.Log("BaseTaskMenuController -> Step04Animation");
        yield break;
    }
}
