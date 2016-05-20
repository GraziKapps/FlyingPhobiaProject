using System.Collections;

public class InformationSaver{

    /*  public class ScoreManager {
     *  private static ScoreManager scoreManager = null;
     *  private int score = 0;
     *  public static ScoreManager getInstance() { if(scoreManager == null) { scoreManager = new ScoreManager(); }
     *      return scoreManager; }
     *      public int getScore() { return score; }
     *      public int setScore(int score) { this.score = score; } } 
     */

    private static InformationSaver informationSaver = null;
    private bool isNetworked = false;

    public bool IsNetworked
    {
        get { return isNetworked; }
        set { isNetworked = value; }
    }
    public static InformationSaver getInstance()
    {
        if (informationSaver==null)
            informationSaver=new InformationSaver();
        return informationSaver;
    }


	
}
