using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdTaskMenuController : BaseTaskMenuController
{
    private List<string> levelTaskTitle3 = new List<string>
    {
        "Initial Safety Check",
        "Install the RC and CR Filter Module",
        "Connect the DC Power Supply",
        "Split the Generator Signal",
        "Attach the Signal Generator",
        "Connect Filter Output to Oscilloscope",
        "Connect FILTER RC with FILTER CR. Select the Right Time Constant.",
        "Activate the DC Power Supply",
        "Generate a Sine Wave Signal",
        "Measure the Amplitude Response",
        "Plot Amplitude vs Frequency",
        "Produce a Square Wave Signal",
        "Record the Step Response",
        "Match Data to an Exponential Curve",
        "Submit Your Feedback",
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

    protected override List<string> LevelTaskTitles => levelTaskTitle3;
    protected override List<string> LevelTaskContents => levelTaskContent3;

    protected override void Step01Animation()
    {
        Debug.Log("ThirdTaskMenuController -> Step01Animation executed");
    }

    protected override IEnumerator Step02Animation()
    {
        Debug.Log("ThirdTaskMenuController -> Step02Animation executed");
        if (placeForBaseBoard != null && baseBoard != null)
        {
            Collider collider1 = placeForBaseBoard.GetComponent<Collider>();
            Collider collider2 = baseBoard.GetComponent<Collider>();

            if (collider1 != null && collider2 != null && collider1.bounds.Intersects(collider2.bounds))
            {
                Debug.Log("Step02Animation: Element 1 collides with Element 2");
                yield return new WaitForSeconds(2f);
                placeForBaseBoard.SetActive(false);

                // In Task 3, we might enable two filter placements
                if (placeForFilter) placeForFilter.SetActive(true);
                if (placeForFilter2) placeForFilter2.SetActive(true);

                yield return new WaitForSeconds(10f);
                placeForFilter.SetActive(false);
                placeForFilter2.SetActive(false);
            }
        }
    }

    protected override void Step03Animation()
    {
        Debug.Log("ThirdTaskMenuController -> Step03Animation executed");
    }

    protected override IEnumerator Step04Animation()
    {
        Debug.Log("ThirdTaskMenuController -> Step04Animation executed");
        if (Splitter != null)
        {
            Splitter.SetActive(true);
            yield return new WaitForSeconds(10f);
            Splitter.SetActive(false);
        }
    }
}