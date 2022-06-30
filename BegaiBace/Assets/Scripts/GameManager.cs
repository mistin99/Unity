using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager instance { set; get; }
    private bool isGameStarted = false;

    // UI and fields
    private Text scoreText, bottleText, modifierText;
    private PlayerBehaviour playerBehaviour;
    public float score,modifierScore;
    public static int bottleScore;
    private int lastScore;

    //Death menu
    public Animator deathMenuAnim;
    public Text deadScoreText, deadBottleText;
    private bool isDead = false;
    private void Awake()
    {
        instance = this;
        modifierScore = 1;
        //UpdateScores();

        playerBehaviour = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerBehaviour>();
       
    }

    private void Update()
    {
        if(isDead)
        {
            return;
        }
        if(MobileInputs.Instance.Tap && !isGameStarted)
        {
            isGameStarted = true;
            playerBehaviour.StartRunning();
            FindObjectOfType<CameraController>().IsMoving=true;
        }
        if(isGameStarted)
        {
            //Bump the score up
            //bottleText.text = bottleScore.ToString();
            score += (Time.deltaTime * modifierScore);
           
            if (lastScore != (int)score)
            {
                lastScore = (int)score;
               // Debug.Log(lastScore);
               // scoreText.text = score.ToString("0");
            }
        }
    }

   

        public void UpdateModifier(float modifierAmount)
    {
        modifierScore = 1.0f + modifierAmount;
       // modifierText.text = "x" + modifierScore.ToString("0.0");
      
    }

        public void OnPlayButton()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

        public void OnDeath()
    {
        isDead = true;
        deadScoreText.text =("Score:") + score.ToString("0");
        deadBottleText.text =("Bottles Collected:") + bottleScore.ToString();
        deathMenuAnim.SetTrigger("Death");
    }
}
