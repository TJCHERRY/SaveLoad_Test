using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Nightmare
{
    public class ScoreManager : MonoBehaviour
    {
        public LevelManager levelManager;
        public static int score;
        private int levelThreshhold;
        const int LEVEL_INCREASE = 50;


        Text sText;
        void Awake ()
        {
            sText = GetComponent <Text> ();

            score = 0;
            levelThreshhold = LEVEL_INCREASE;
            
        }


        void Update ()
        {
            if (levelManager.CurrentLevel == 0)
            {
                levelThreshhold = LEVEL_INCREASE;
            }
            sText.text = "Score: " + score;
            if (score >= levelThreshhold)
            {
                AdvanceLevel();
            }
        }

        void ManageThreshHold()
        {
           
        }

        private void AdvanceLevel()
        {
            
            levelThreshhold = score + LEVEL_INCREASE;
            LevelManager lm = FindObjectOfType<LevelManager>();
            lm.AdvanceLevel();
        }



    }
}