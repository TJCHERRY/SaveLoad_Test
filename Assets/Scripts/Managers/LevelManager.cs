using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Nightmare
{
    public class LevelManager : MonoBehaviour
    {
        public string[] levels;

        private int currentLevel = 0;
        private Scene currentScene;
        private PlayerMovement playerMove;
        private Vector3 playerRespawn;
        private CinematicController cinema;
        SaveDataObject sdo;

        public int CurrentLevel { get => currentLevel; }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
            PauseManager.onSaveClick += SaveLevel;
            PauseManager.onLoadClick += LoadSave;
        }

        void Start()
        {
            cinema = FindObjectOfType<CinematicController>();
            SceneManager.LoadSceneAsync(levels[0], LoadSceneMode.Additive);
            LoadInitialLevel();
            playerMove = FindObjectOfType<PlayerMovement>();
            playerRespawn = playerMove.transform.position;
        }

        public void AdvanceLevel()
        {
            LoadLevel(currentLevel + 1);
        }

        public void LoadInitialLevel()
        {
            LoadLevel(0);
        }

        private void LoadLevel(int level)
        {
            currentLevel = level;

            //Load next level in background

            string loadingScene = levels[level % levels.Length];
            SceneManager.LoadSceneAsync(loadingScene, LoadSceneMode.Additive);
        }

        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (mode != LoadSceneMode.Additive)
                return;

            playerMove.transform.position = playerRespawn;
            SceneManager.SetActiveScene(scene);
            
            DisableOldScene();

            currentScene = scene;

            // Play realtime cinematic?
            if (currentLevel > 1)
                cinema.StartCinematic(CinematicController.CinematicType.Realtime);
            else
                cinema.StartCinematic(CinematicController.CinematicType.PreRendered);
        }

        private void DisableOldScene()
        {
            if (currentScene.IsValid())
            {
                // Disable old scene.
                GameObject[] oldSceneObjects = currentScene.GetRootGameObjects();
                for (int i = 0; i < oldSceneObjects.Length; i++)
                {
                    oldSceneObjects[i].SetActive(false);
                }

                // Unload it.
                SceneManager.UnloadSceneAsync(currentScene);
            }
        }

        void OnSceneUnloaded(Scene scene)
        {
            Debug.Log("Hey " + currentLevel);
        }

        public void SaveLevel()
        {
            sdo = new SaveDataObject(currentLevel % levels.Length, ScoreManager.score,GrenadeManager.grenades);
         
            SaveManager.Save(sdo);
        } 

        public void LoadSave()
        {
            //Debug.Log(SaveManager.Load().levelIndex);
            //Debug.Log(SaveManager.Load().playerScore);
            // Debug.Log(SaveManager.Load().grenadeCount);
            ScoreManager.score = SaveManager.Load().playerScore;
            GrenadeManager.grenades = SaveManager.Load().grenadeCount;
            currentLevel = SaveManager.Load().levelIndex;
            LoadLevel(SaveManager.Load().levelIndex);

        }


        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
            PauseManager.onSaveClick -= SaveLevel;
            PauseManager.onLoadClick -= LoadSave;
        }
    }
}