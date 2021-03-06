using TheseusAndTheMinotaur.Common;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace TheseusAndTheMinotaur.Gameplay
{
    public class GameManagerMB : MonoBehaviour
    {
        [SerializeField]
        private UnityEvent _onLevelFinished;

        [SerializeField]
        private UnityEvent _onLevelLost;

        [SerializeField]
        private IntReference _currentLevel;
        
        [SerializeField]
        private IntReference _levelCountOutput;

        [SerializeField]
        private StringReference _levelComment;

        private IMinotaurBehavior _minotaurBehavior;
        private ITheseusBehavior _theseusBehavior;
        private IPuzzleLevelAssetDatabase _puzzleLevelAssetDatabase;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void RuntimeInit()
        {
            SceneManager.sceneLoaded -= ResumeTime;
            SceneManager.sceneLoaded += ResumeTime;
        }

        private static void ResumeTime(Scene scene, LoadSceneMode loadSceneMode)
        {
            Services.TimeService.ResumeTime();
        }

        private void Start()
        {
            _minotaurBehavior = GetComponentInChildren<IMinotaurBehavior>();
            _theseusBehavior = GetComponentInChildren<ITheseusBehavior>();
            TryGetComponent(out _puzzleLevelAssetDatabase);

            _levelComment.Value = _puzzleLevelAssetDatabase.GetCurrentLevelComment();
            _levelCountOutput.Value = _puzzleLevelAssetDatabase.GetLevelCount();
            _minotaurBehavior.ArrivedAtTheseus += OnLevelLost;
            _theseusBehavior.ReachedLevelEnd += OnLevelFinished;
        }

        private void OnDestroy()
        {
            _minotaurBehavior.ArrivedAtTheseus -= OnLevelLost;
            _theseusBehavior.ReachedLevelEnd -= OnLevelFinished;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel()
        {
            if (!_puzzleLevelAssetDatabase.CheckHasNextLevel())
            {
                return;
            }

            _currentLevel.Value += 1;
            RestartLevel();
        }

        public void LoadPreviousLevel()
        {
            if (!_puzzleLevelAssetDatabase.CheckHasPreviousLevel())
            {
                return;
            }

            _currentLevel.Value -= 1;
            RestartLevel();
        }

        private void OnLevelFinished()
        {
            Services.TimeService.PauseTime();
            _onLevelFinished?.Invoke();
        }

        private void OnLevelLost()
        {
            Services.TimeService.PauseTime();
            _onLevelLost?.Invoke();
        }
    }
}