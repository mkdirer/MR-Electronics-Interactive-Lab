using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FilterMenuController : MonoBehaviour
{
    [System.Serializable]
    public class FilterInfo
    {
        public string title;
        public string shortTitle; // New field
        public Sprite filterImage;
        public Sprite schematicImage;
        public string description;
    }

    public GameObject menuFilterPanel;
    public GameObject filterInfoPanel;

    public TextMeshProUGUI titleText;
    public TextMeshProUGUI shortTitleText; // New UI element reference
    public Image filterImage;
    public Image schematicImage;
    public TextMeshProUGUI descriptionText;

    public FilterInfo[] filterInfos;

    void Start()
    {
        // Initialize by showing the menu panel and hiding the info panel
        //ShowMenuPanel();
        PopulateFilterInfos();
    }

/*    public void ShowMenuPanel()
    {
        menuPanel.SetActive(true);
        filterInfoPanel.SetActive(false);
    }*/

    public void ShowFilterInfo(int filterIndex)
    {
        if (filterIndex >= 0 && filterIndex < filterInfos.Length)
        {
            FilterInfo info = filterInfos[filterIndex];

            titleText.text = info.title;
            shortTitleText.text = info.shortTitle; // Display the short title
            filterImage.sprite = info.filterImage;
            schematicImage.sprite = info.schematicImage;
            descriptionText.text = info.description;

            menuFilterPanel.SetActive(true);
            filterInfoPanel.SetActive(true);
        }
        else
        {
            Debug.LogError("Filter index out of range");
        }
    }

    private void PopulateFilterInfos()
    {
        filterInfos = new FilterInfo[]
        {
            new FilterInfo
            {
                title = "Second Order Low-pass Filters (Butterworth)",
                shortTitle = "Low-pass Butterworth",
                filterImage = Resources.Load<Sprite>("Filters/Butt"),
                schematicImage = Resources.Load<Sprite>("Filters/CircuitButterworth"),
                description = "A second-order low-pass filter with Butterworth characteristics, providing a flat frequency response in the passband. It is often used in applications where minimizing signal distortion is important."
            },
            new FilterInfo
            {
                title = "High-pass Passive Filter (First Order CR with Compensation)",
                shortTitle = "High-pass CR Compensated",
                filterImage = Resources.Load<Sprite>("Filters/CR_z_komp"),
                schematicImage = Resources.Load<Sprite>("Filters/CircuitCRwith Comp"),
                description = "An improved high-pass filter with compensation, providing better frequency response. Used when higher precision in attenuating low-frequency signals is required."
            },
            new FilterInfo
            {
                title = "Low-pass Passive Filter (First Order RC)",
                shortTitle = "Low-pass RC",
                filterImage = Resources.Load<Sprite>("Filters/RC"),
                schematicImage = Resources.Load<Sprite>("Filters/CircuitRC"),
                description = "A low-pass filter that passes low-frequency signals and attenuates high-frequency signals. It is simple in construction, consisting of a resistor and a capacitor."
            },
            new FilterInfo
            {
                title = "Second Order Low-pass Filters (Critical Damping)",
                shortTitle = "Low-pass Critical Damping",
                filterImage = Resources.Load<Sprite>("Filters/Kryt"),
                schematicImage = Resources.Load<Sprite>("Filters/CircuitCritical"),
                description = "A second-order low-pass filter that provides critical damping. It is characterized by no overshoot and a quick response to input signal changes."
            },
            new FilterInfo
            {
                title = "Second Order Low-pass Filters (Chebyshev 0.5dB)",
                shortTitle = "Low-pass Chebyshev 0.5dB",
                filterImage = Resources.Load<Sprite>("Filters/cheb"),
                schematicImage = Resources.Load<Sprite>("Filters/CircuitChebyshev"),
                description = "A second-order low-pass filter with Chebyshev characteristics, featuring a 0.5dB ripple in the passband. It provides sharper attenuation outside the passband at the cost of slight ripples in the passband."
            },
            new FilterInfo
            {
                title = "High-pass Passive Filter (First Order CR)",
                shortTitle = "High-pass CR",
                filterImage = Resources.Load<Sprite>("Filters/CR"),
                schematicImage = Resources.Load<Sprite>("Filters/CircuitCR"),
                description = "A high-pass filter that passes high-frequency signals and attenuates low-frequency signals. It consists of a capacitor and a resistor."
            }
        };
    }
}
