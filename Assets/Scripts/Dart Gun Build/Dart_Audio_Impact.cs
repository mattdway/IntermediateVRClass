// This script uses the UnityEngine library.
using UnityEngine;

// This script defines a class named Dart_Audio_Impact.
public class Dart_Audio_Impact : MonoBehaviour
{
    // This method is called when a collision occurs.
    void OnCollisionEnter()
    {
        // This line gets the AudioSource component attached to the game object and plays it.
        GetComponent<AudioSource>().Play();
    }
}