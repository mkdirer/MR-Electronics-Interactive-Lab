using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthTaskMenuController : BaseTaskMenuController
{
    private List<string> levelTaskTitle4 = new List<string>
    {
    "Secure Start", 
    "Install the Critical Damping or Butterworth Filter Module",
    "Attach the DC Power Supply",
    "Divide the Generator Signal",
    "Link the Signal Generator",
    "Connect Filter Output to Oscilloscope",
    "Calculate the Cutoff Frequency",
    "Power on the DC Supply",
    "Produce a Sinusoidal Signal",
    "Record Amplitude Response",
    "Collect data for Amplitude Characteristic",
    "Generate a Square Wave Signal",
    "Capture the Step Response",
    "Step Response as a Exponential Curve",
    "Provide Your Feedback",
    };

    private List<string> levelTaskContent4 = new List<string>
    {
        "Ensure all devices are off for safety. Verify the availability of the necessary equipment: Oscilloscope, DC Power Supply, Signal Generator, Base Board, and CR Filter modules. If any are missing, switch back to Explore Mode.", //1
        "Insert the Critical Damping or Butterworth filter module into the FILTER 1 slot on the measurement board.", //2
        "Link the DC power supply outputs to the filter's input.", //3
        "Use a BNC T-connector to divide the signal from the generator, connecting one output to the filter Base Board.", //4
        "Attach one generator output to Channel 1 of the oscilloscope.", //5
        "Link the Base Board's filter output to Channel 2 of the oscilloscope.", //6
        "The circuit uses identical resistive components. The values of resistance and capacitance can be found on the module boards. To determine the cutoff frequency, refer to the hand menu.", //7
        "Activate the DC power supply, setting one output to -15V and the other to +15V.", //8
        "Set the signal generator to emit a sinusoidal signal with a small amplitude of 100 [mV] (100-200 mVpp).", //9
        "Measure the peak-to-peak voltage (Vpp) of both input and output signals to determine the gain. Adjust the frequency from 100 Hz to 1 MHz, recording several measurements per decade.", //10
        "The amplitude characteristic (gain) Ku versus frequency f is displayed on an additional screen, including passband gain, cutoff frequency, and stopband slope for comparison with theoretical values.", //11
        "Configure the signal generator to produce a square wave signal with a small amplitude of 100 [mV] (100-200 mVpp) and a period of 1 [ms], significantly greater than the filter's time constant.", //12
        "Record several voltage-time pairs on the rising edge of the filter's response to the square wave signal on the oscilloscope.", //13
        "Fit the recorded data to an exponential function to determine the time constant and compare it with the theoretical value.", //14
        "After completing your measurements and analysis, please provide feedback in the About section. Your input is important and will help improve future exercises.", //15
    };

    protected override List<string> LevelTaskTitles => levelTaskTitle4;
    protected override List<string> LevelTaskContents => levelTaskContent4;

    protected override void Step01Animation()
    {
        Debug.Log("FourthTaskMenuController -> Step01Animation executed");
    }

    protected override IEnumerator Step02Animation()
    {
        Debug.Log("FourthTaskMenuController -> Step02Animation executed");
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
        Debug.Log("FourthTaskMenuController -> Step03Animation executed");
    }

    protected override IEnumerator Step04Animation()
    {
        Debug.Log("FourthTaskMenuController -> Step04Animation executed");
        if (Splitter != null)
        {
            Splitter.SetActive(true);
            yield return new WaitForSeconds(10f);
            Splitter.SetActive(false);
        }
    }
}