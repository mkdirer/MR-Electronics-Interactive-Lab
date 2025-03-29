using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstTaskMenuController : BaseTaskMenuController
{
    private List<string> levelTaskTitle1 = new List<string>
    {
        "Safe start", 
        "Insert the RC Filter Module",
        "Connect the Power Supply DC",
        "Split the signal from the generator",
        "Connect the Signal Generator",
        "Connect the Filter Output to the Oscilloscope",
        "Select the Appropriate Time Constant", 
        "Turn on the DC power supply",
        "Generate a Sinusoidal Signal",
        "Measure Amplitude Response",
        "Plot the Amplitude Characteristic Ku=f(f)", 
        "Generate a Square Wave Input Signal",
        "Measure the Step Response",
        "Fit the Data to an Exponential Curve", 
        "Your Feedback",
    };

    private List<string> levelTaskContent1 = new List<string>
    {
        "Turn off all devices and ensure safety. Check that the require equipment are avaliable: Oscilloscope, DC Power Supply, Generator Funk,Base Bord and Filter modules (RC). If not, turn back to Explore Mode", //1
        "Place the RC filter module into the FILTER 1 socket on the measurement board.", //2
        "Connect DC supply outputs to the input of the filter.", //3
        "Use a BNC T-connector to split the signal from the generator and connect the one of generator outputs to filter Base Board", //4
        "Connect one of generator outputs to Channel 1 of the oscilloscope.", //5
        "Connect the output of the Base Board for filter to Channel 2 of the oscilloscope.", //6
        "Set the appropriate time constant on the filter module using the jumper J1a (J1a...e).", //7
        "Turn on the DC power supply and set one output to -15V and the other to +15V.", //8
        "Set the signal generator to produce a sinusoidal signal with a small amplitude  100 [mV] (100-200 mVpp).", //9
        "Record the peak-to-peak voltage (Vpp) of the input and output signals and the gain value will be determined. Vary the frequency from 100 Hz to 1 MHz, taking several measurements per decade.", //10
        "The amplitude characteristic (gain) Ku as a function of frequency f is shown on an additional screen. This characteristic includes the passband gain, cutoff frequency, and the asymptotic slope of the characteristic in the stopband. This allows you to compare the measured values with the theoretical ones.", //11
        "Set the signal generator to produce a square wave signal with a small amplitude 100 [mV] (100-200 mVpp) and a period 18.80 [µs] greater than 10-20 times the filter's time constant.", //12
        "Record several pairs of points (voltage + time) on the rising edge of the filter's response to the square wave signal observed on the oscilloscope.", //13
        "In this part, the collected data is fitted to an exponential function, and based on this, the time constant is determined and compared with the theoretical value.", //14
        "After completing the measurements and analysis, please take a moment to provide your feedback at the About section. Your feedback is valuable and will help improve future iterations of this exercise.", //15
    };

    protected override List<string> LevelTaskTitles => levelTaskTitle1;
    protected override List<string> LevelTaskContents => levelTaskContent1;

    protected override void Step01Animation()
    {
        Debug.Log("FirstTaskMenuController -> Step01Animation executed");
    }

    protected override IEnumerator Step02Animation()
    {
        Debug.Log("FirstTaskMenuController -> Step02Animation executed");
        if (placeForBaseBoard != null && baseBoard != null)
        {
            Collider collider1 = placeForBaseBoard.GetComponent<Collider>();
            Collider collider2 = baseBoard.GetComponent<Collider>();

            if (collider1 != null && collider2 != null && collider1.bounds.Intersects(collider2.bounds))
            {
                Debug.Log("Step02Animation: Element 1 collides with Element 2");
                yield return new WaitForSeconds(2f);
                placeForBaseBoard.SetActive(false);
                if (placeForFilter) placeForFilter.SetActive(true);
                yield return new WaitForSeconds(10f);
                placeForFilter.SetActive(false);
            }
        }
    }

    protected override void Step03Animation()
    {
        Debug.Log("FirstTaskMenuController -> Step03Animation executed");
    }

    protected override IEnumerator Step04Animation()
    {
        Debug.Log("FirstTaskMenuController -> Step04Animation executed");
        if (Splitter != null)
        {
            Splitter.SetActive(true);
            yield return new WaitForSeconds(10f);
            Splitter.SetActive(false);
        }
    }
}
