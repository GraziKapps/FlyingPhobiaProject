using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour
{
    public MovieTexture movTexture;
    [SerializeField]
    private AudioSource commandersVoice;
    void Start()
    {
        GetComponent<Renderer>().material.mainTexture = movTexture;
        
       // movTexture.Play();
    }

   public void ActuallyPlay(AudioSource audio)
    {
        audio.Play();
        movTexture.Play();
        StartCoroutine(waitSeconds());
    }


   IEnumerator waitSeconds()
   {
       Debug.Log("Waiting Video ..");
       yield return new WaitForSeconds(160);
       //StartFlying();
       //  Debug.Log("Acabouuu!!! É tetraaaaa!");
       playCommanderMessage();
   }

   private void playCommanderMessage()
   {
       commandersVoice.Play();
   }
}