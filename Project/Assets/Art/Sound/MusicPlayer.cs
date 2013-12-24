using UnityEngine;
using System.Collections;

public static class MusicPlayer {

    static bool playing = false;

    public static bool isPlaying()
    {
        return playing;
    }

    public static void setPlaying(bool play)
    {
        playing = play;
    }
}
