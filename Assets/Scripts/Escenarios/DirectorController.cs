using UnityEngine;
using UnityEngine.Playables;

public class DirectorController : MonoBehaviour
{
    public static bool shouldPlay = true;
    public PlayableDirector playableDirector;

    // Update is called once per frame
    void Update()
    {
        if (shouldPlay)
        {
            playableDirector.Play();
            shouldPlay = false;
        }
    }
}
