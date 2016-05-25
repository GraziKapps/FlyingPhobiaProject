using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {
    InformationSaver info;
	// Use this for initialization
	void Start () {
	
	}
	/*A scene loader. See if its using the mobile controler. If no, load another canvas, ask some dumb questions about statical values and
     go to application. If yes, wait for confirmation from the controller to load the next scene properly. 
     LOOK INTO: How to send values to another scene. Maybe a struct would help.*/
	// Update is called once per frame
    //Options: DontDestroyOnLoad
    //Static Class that is not a monobehaviour <- this one seems cool enough. Class would have all fields I need. Singleton maybe?
    /*
     *  public class ScoreManager {
     *  private static ScoreManager scoreManager = null;
     *  private int score = 0;
     *  public static ScoreManager getInstance() { if(scoreManager == null) { scoreManager = new ScoreManager(); }
     *      return scoreManager; }
     *      public int getScore() { return score; }
     *      public int setScore(int score) { this.score = score; } } 
     */
    //^^Example above.

    public void LoadScene()
    {
        info = InformationSaver.getInstance();
        info.IsNetworked = false;
        SceneManager.LoadScene("lightPlane");
 
    }

    public void LoadWithMobile()
    {
        info = InformationSaver.getInstance();
        info.IsNetworked = true;
        SceneManager.LoadScene("lightPlane");
 
    }
}
