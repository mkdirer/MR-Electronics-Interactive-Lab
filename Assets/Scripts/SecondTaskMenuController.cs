using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondTaskMenuController : BaseTaskMenuController
{
    private List<string> levelTaskTitle2 = new List<string>
    {
    "Secure Start",
    "Install the CR Filter Module",
    "Attach the DC Power Supply",
    "Divide the Generator Signal",
    "Link the Signal Generator",
    "Connect Filter Output to Oscilloscope",
    "Choose the Correct Time Constant",
    "Power on the DC Supply",
    "Produce a Sinusoidal Signal",
    "Record Amplitude Response",
    "Collect data for Amplitude Characteristic",
    "Generate a Square Wave Signal",
    "Capture the Step Response", 
    "Step Response as a Exponential Curve", 
    "Provide Your Feedback", 
    };

    private List<string> levelTaskContent2 = new List<string>
    {
        "Ensure all devices are off for safety. Verify the availability of the necessary equipment: Oscilloscope, DC Power Supply, Signal Generator, Base Board, and CR Filter modules. If any are missing, switch back to Explore Mode.", //1
        "Insert the RC filter module into the FILTER 1 slot on the measurement board.", //2
        "Link the DC power supply outputs to the filter's input.", //3
        "Use a BNC T-connector to divide the signal from the generator, connecting one output to the filter Base Board.", //4
        "Attach one generator output to Channel 1 of the oscilloscope.", //5
        "Link the Base Board's filter output to Channel 2 of the oscilloscope.", //6
        "Adjust the time constant on the filter module with the J1b (J1a...e) jumper.", //7
        "Activate the DC power supply, setting one output to -15V and the other to +15V.", //8
        "Set the signal generator to emit a sinusoidal signal with a small amplitude of 100 [mV] (100-200 mVpp).", //9
        "Measure the peak-to-peak voltage (Vpp) of both input and output signals to determine the gain. Adjust the frequency from 100 Hz to 1 MHz, recording several measurements per decade.", //10
        "The amplitude characteristic (gain) Ku versus frequency f is displayed on an additional screen, including passband gain, cutoff frequency, and stopband slope for comparison with theoretical values.", //11
        "Configure the signal generator to produce a square wave signal with a small amplitude of 100 [mV] (100-200 mVpp) and a period of 88.00 [µs], significantly greater than the filter's time constant.", //12
        "Record several voltage-time pairs on the rising edge of the filter's response to the square wave signal on the oscilloscope.", //13
        "Fit the recorded data to an exponential function to determine the time constant and compare it with the theoretical value.", //14
        "After completing your measurements and analysis, please provide feedback in the About section. Your input is important and will help improve future exercises.", //15
    };

    protected override List<string> LevelTaskTitles => levelTaskTitle2;
    protected override List<string> LevelTaskContents => levelTaskContent2;

    protected override void Step01Animation()
    {
        Debug.Log("SecondTaskMenuController -> Step01Animation executed");
    }

    protected override IEnumerator Step02Animation()
    {
        Debug.Log("SecondTaskMenuController -> Step02Animation executed");
        yield return base.Step02Animation();
    }
}