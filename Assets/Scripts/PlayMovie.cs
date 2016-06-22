using UnityEngine;
using System.Collections;

public class PlayMovie : MonoBehaviour
{
    public MovieTexture movTexture;
    public Texture2D mapTexture;
    [SerializeField]
    private AudioSource commandersVoice;
    void Start()
    {
        
        
       // movTexture.Play();
    }

   public void ActuallyPlay(AudioSource audio)
    {
        GetComponent<Renderer>().material.mainTexture = movTexture;
        audio.Play();
        movTexture.Play();
        StartCoroutine(waitSeconds());
    }


   IEnumerator waitSeconds()
   {
       Debug.Log("Waiting Video ..");
       yield return new WaitForSeconds(390);
       //StartFlying();
       //  Debug.Log("Acabouuu!!! É tetraaaaa!");
       GetComponent<Renderer>().material.mainTexture = mapTexture;
       playCommanderMessage();
   }

   private void playCommanderMessage()
   {
       commandersVoice.Play();
   }
}