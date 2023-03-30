using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GeneralSetting
{
    // Set the background music to on, its volume to 1. Set the voiceId to the first one or zero and
    // set the number of narrators for the project here as well as the number of steps for each part.
    // in this case it is only PartOne with 25 steps.
    public static bool isBGMOn = true;
    public static float bgmVolume = 1f;
    public static float narratorVolume = 1f;
    public static int voiceId = 0;
    public static int numNarrators = 4;
    public static int numPartOneSteps = 25;
}
