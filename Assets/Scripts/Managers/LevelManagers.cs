using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace pixalquarks.bgj2022_2
{
    public class LevelManagers : MonoBehaviour
    {
        public Canvas canvas;
        public GameObject curtain;
        public Button levelButton;
        public Color activeColor;
        public Color inactiveColor;
        public int numberOfLevels;
        public Vector2 topLeft;
        public Vector2 bottomRight;
        public float horizontalSpacing;
        public float verticalSpacing;
        
        public const string MaxUnlockedLevel = "MaxUnlockedLevel";
        
        private List<Button> levelButtons = new List<Button>();
        
        private Animator _curtainAnimator;
        private static readonly int CloseIn = Animator.StringToHash("CloseIn");

        private int _maxUnlockedLevel;

        public void Awake()
        {
            _maxUnlockedLevel = PlayerPrefs.GetInt(MaxUnlockedLevel, 1);
            GenerateLevelCards();
            _curtainAnimator = curtain.GetComponent<Animator>();
            print(_maxUnlockedLevel);
        }

        private void GenerateLevelCards()
        {
            int xCounter = 0;
            int yCounter = 0;
            for (int i = 1; i <= numberOfLevels; i++)
            {
                var levelBtn = Instantiate(levelButton, canvas.transform, false);
                levelBtn.GetComponentInChildren<TextMeshProUGUI>().text = (i).ToString();
                levelBtn.GetComponent<RectTransform>().anchoredPosition = new Vector2(
                    topLeft.x + xCounter * horizontalSpacing,
                    topLeft.y - yCounter * verticalSpacing);
                var l = i;
                // print($"{l}  {_maxUnlockedLevel}");
                levelBtn.GetComponent<Image>().color = l <= _maxUnlockedLevel ? activeColor : inactiveColor;
                levelBtn.onClick.AddListener(() => LoadLevel(l));
                levelButtons.Add(levelBtn);
                xCounter++;
                if (topLeft.x + xCounter * horizontalSpacing > bottomRight.x)
                {
                    xCounter = 0;
                    yCounter++;
                }
            }
        }

        public void LoadMainMenu()
        {
            _curtainAnimator.SetTrigger(CloseIn);
            SceneManager.LoadScene(0);
        }

        private void LoadLevel(int level)
        {
            if (level <= _maxUnlockedLevel)
            {
                _curtainAnimator.SetTrigger(CloseIn);
                SceneManager.LoadScene(level+1);
            }
        }
    }
}
