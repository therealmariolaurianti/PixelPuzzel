using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Data;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameObjects
{
    public class UIManager : MonoBehaviour
    {
        public GameObject CollectedText;
        public GameObject DeathText;
        public TMP_Text LevelFourText;
        public TMP_Text LevelOneText;
        public TMP_Text LevelThreeText;
        public TMP_Text LevelTwoText;
        public GameObject PausePanel;
        public GameObject Player;
        public TMP_Text TimeText;

        public int TotalMelons;
        private int _deaths;
        private int _melonsCollected;
        private bool _timeIsRunning;
        private float _timeRemaining;

        private Level CurrentLevel => Helper.GetLevel();
        private bool IsFirstLevel => CurrentLevel == Level.One;

        private void Awake()
        {
            PausePanel.SetActive(false);
        }

        private void Start()
        {
            var data = DataWriter.Read();
            var levelData = data?.OrderBy(ld => ld.Level).ToList();

            SetAllLevelTimes(levelData);
            SetTimerText(levelData);
            SetCollectedText(true);
            SetDeathText(levelData, true);
        }

        private void Update()
        {
            if (_timeIsRunning)
                if (_timeRemaining >= 0)
                {
                    _timeRemaining += Time.deltaTime;
                    TimeText.text = Helper.TimeFromFloat(_timeRemaining);
                }
        }

        private void OnEnable()
        {
            CharacterEvents.MelonCollected += OnMelonCollected;
            CharacterEvents.PlayerDeath += OnPlayerDeath;
            CharacterEvents.StopTimer += OnStopTimer;
        }

        private void OnDisable()
        {
            CharacterEvents.MelonCollected -= OnMelonCollected;
            CharacterEvents.PlayerDeath -= OnPlayerDeath;
            CharacterEvents.StopTimer -= OnStopTimer;
        }

        private void SetAllLevelTimes(List<LevelData> levelData)
        {
            if (levelData is null)
                return;

            foreach (var data in levelData)
            {
                var level = (Level)data.Level;
                SetLevelText(level, data.TimeInSeconds);
            }

            SetLevelText(CurrentLevel, 0);
        }

        private void SetDeathText(List<LevelData> levelData, bool isStarting)
        {
            if (isStarting)
            {
                var lastLevel = levelData?.LastOrDefault();
                _deaths = lastLevel?.Deaths ?? 0;
            }

            var component = DeathText.GetComponent<TMP_Text>();
            component.text = $"Deaths: {_deaths}";
        }

        private void SetTimerText(List<LevelData> levelData)
        {
            if (IsFirstLevel)
            {
                _timeRemaining = 0;
            }
            else
            {
                var lastLevel = levelData?.LastOrDefault();
                _timeRemaining = lastLevel?.TimeInSeconds ?? 0;
            }

            _timeIsRunning = true;
        }

        private void OnStopTimer()
        {
            _timeIsRunning = false;
            
            DataWriter.Write(_deaths, _timeRemaining, Helper.GetLevelInt());

            SetLevelText(CurrentLevel);
        }

        private void SetLevelText(Level level, float? timeRemaining = null)
        {
            var time = Helper.TimeFromFloat(timeRemaining ?? _timeRemaining);
            switch (level)
            {
                case Level.One:
                    LevelOneText.text = $"Level 1: {time}";
                    break;
                case Level.Two:
                    LevelTwoText.text = $"Level 2: {time}";
                    break;
                case Level.Three:
                    LevelThreeText.text = $"Level 3: {time}";
                    break;
                case Level.Four:
                    LevelFourText.text = $"Level 4: {time}";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnMelonCollected()
        {
            SetCollectedText();
        }

        private void SetCollectedText(bool isStarting = false)
        {
            if (isStarting)
                _melonsCollected = 0;
            else
                _melonsCollected++;

            var component = CollectedText.GetComponent<TMP_Text>();
            component.text = $"   Collected: {_melonsCollected} / {TotalMelons}";

            if (_melonsCollected != TotalMelons)
                return;

            CharacterEvents.PortalOpened?.Invoke();
            CharacterEvents.AllItemsCollected?.Invoke();
        }

        private void OnPlayerDeath()
        {
            _deaths++;
            SetDeathText(null, false);
        }

        public void OnPause()
        {
            var animator = Player.GetComponent<Animator>();
            var rigidBody = Player.GetComponent<Rigidbody2D>();

            if (PausePanel.activeSelf)
                DoUnPause(animator, rigidBody);
            else
                DoPause(animator, rigidBody);
        }

        private void DoUnPause(Animator animator, Rigidbody2D rigidBody)
        {
            PausePanel.SetActive(false);

            Cursor.visible = false;
            _timeIsRunning = true;
            animator.enabled = true;
            rigidBody.bodyType = RigidbodyType2D.Dynamic;
        }

        private void DoPause(Animator animator, Rigidbody2D rigidBody)
        {
            PausePanel.SetActive(true);

            Cursor.visible = true;
            _timeIsRunning = false;
            animator.enabled = false;
            rigidBody.bodyType = RigidbodyType2D.Static;
        }

        public void Quit()
        {
            Helper.Quit(name, GetType());
        }

        public void Menu()
        {
            SceneManager.LoadScene(0);
        }

        public void Resume()
        {
            OnPause();
        }
    }
}