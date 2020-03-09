using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Serializable()]
public struct UIManagerParameters
{
    [Header("Answers Options")]
    [SerializeField]
    private float margins;
    public float Margins => margins;

    [Header("Resolution Screen Options")]
    
    
    [SerializeField]
    private Sprite correctBGColor; 
    public Sprite CorrectBGColor => correctBGColor;
    
    [SerializeField] private Sprite incorrectBGColor;
    public Sprite IncorrectBGColor => incorrectBGColor;
    [SerializeField] private Color finalBGColor;
    public Color FinalBGColor => finalBGColor;
}
[Serializable()]
public struct UIElements
{
    [SerializeField] private RectTransform answersContentArea;
    public RectTransform AnswersContentArea => answersContentArea;

    [SerializeField] private TextMeshProUGUI questionInfoTextObject;
    public TextMeshProUGUI QuestionInfoTextObject => questionInfoTextObject;

    [SerializeField] private TextMeshProUGUI scoreText;
    public TextMeshProUGUI ScoreText => scoreText;

    [Space]

    [SerializeField]
    private Animator resolutionScreenAnimator;
    public Animator ResolutionScreenAnimator => resolutionScreenAnimator;

    [SerializeField] private Image resolutionBG;
    public Image ResolutionBG => resolutionBG;

    [SerializeField] private TextMeshProUGUI resolutionStateInfoText;
    public TextMeshProUGUI ResolutionStateInfoText => resolutionStateInfoText;

    [SerializeField] private TextMeshProUGUI resolutionScoreText;
    public TextMeshProUGUI ResolutionScoreText => resolutionScoreText;

    [Space]

    [SerializeField]
    private TextMeshProUGUI highScoreText;
    public TextMeshProUGUI HighScoreText => highScoreText;

    [SerializeField] private CanvasGroup mainCanvasGroup;
    public CanvasGroup MainCanvasGroup => mainCanvasGroup;

    [SerializeField] private RectTransform finishUIElements;
    public RectTransform FinishUIElements => finishUIElements;
}
public class UIManager : MonoBehaviour
{

    #region Variables
    [Header("Resolution Time")]
    public const float resolutionTime = GameUtility.ResolutionDelayTime;
    public enum ResolutionScreenType { Correct, Incorrect, Finish }

    [Header("References")]
    [SerializeField]
    private GameEvents events = null;

    [Header("UI Elements (Prefabs)")]
    [SerializeField]
    private AnswerData answerPrefab = null;

    [SerializeField] private UIElements uIElements = new UIElements();

    [Space]
    [SerializeField]
    private UIManagerParameters parameters = new UIManagerParameters();

    private List<AnswerData> currentAnswers = new List<AnswerData>();
    private int resStateParaHash = 0;

    private IEnumerator IE_DisplayTimedResolution = null;

    public static UIManager instance;

    public UIPanel _currentPanel;
    public UIPanel firstPanel, secondPanel;

    #endregion

    #region Default Unity methods

    /// <summary>
    /// Function that is called when the object becomes enabled and active
    /// </summary>
    private void OnEnable()
    {
        events.UpdateQuestionUI += UpdateQuestionUI;
        events.DisplayResolutionScreen += DisplayResolution;
        events.ScoreUpdated += UpdateScoreUI;
    }
    /// <summary>
    /// Function that is called when the behaviour becomes disabled
    /// </summary>
    private void OnDisable()
    {
        events.UpdateQuestionUI -= UpdateQuestionUI;
        events.DisplayResolutionScreen -= DisplayResolution;
        events.ScoreUpdated -= UpdateScoreUI;
    }

    /// <summary>
    /// Function that is called when the script instance is being loaded.
    /// </summary>
    private void Start()
    {
        UpdateScoreUI();
        resStateParaHash = Animator.StringToHash("ScreenState");
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    /// <summary>
    /// Function that is used to update new question UI information.
    /// </summary>
    private void UpdateQuestionUI(Question question)
    {
        uIElements.QuestionInfoTextObject.text = question.Info;
        CreateAnswers(question);
    }
    /// <summary>
    /// Function that is used to display resolution screen.
    /// </summary>
    private void DisplayResolution(ResolutionScreenType type, string interventionText, int score)
    {
        UpdateResUI(type, interventionText, score);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 2);
        uIElements.MainCanvasGroup.blocksRaycasts = false;

        if (type != ResolutionScreenType.Finish)
        {
            if (IE_DisplayTimedResolution != null)
            {
                StopCoroutine(IE_DisplayTimedResolution);
            }
            IE_DisplayTimedResolution = DisplayTimedResolution();
            StartCoroutine(IE_DisplayTimedResolution);
        }
    }

    private IEnumerator DisplayTimedResolution()
    {
        yield return new WaitForSeconds(resolutionTime);
        uIElements.ResolutionScreenAnimator.SetInteger(resStateParaHash, 1);
        uIElements.MainCanvasGroup.blocksRaycasts = true;
    }

    /// <summary>
    /// Function that is used to display resolution UI information.
    /// </summary>
    ///
    /// 
    /// Rename the variable "score" to something more appropriate
    private void UpdateResUI(ResolutionScreenType type, string interventionText ,int score)
    {
        var highscore = PlayerPrefs.GetInt(GameUtility.SavePrefKey);

        switch (type)
        {
            case ResolutionScreenType.Correct:
                uIElements.ResolutionBG.GetComponent<Image>().sprite = parameters.CorrectBGColor;
                uIElements.ResolutionStateInfoText.text = interventionText;
                uIElements.ResolutionScoreText.text = $"+{score}";
                break;
            case ResolutionScreenType.Incorrect:
                uIElements.ResolutionBG.GetComponent<Image>().sprite = parameters.IncorrectBGColor;
                uIElements.ResolutionStateInfoText.text = interventionText;
                uIElements.ResolutionScoreText.text = $"-{score}";
                break;
            case ResolutionScreenType.Finish:
                uIElements.ResolutionBG.color = parameters.FinalBGColor;
                uIElements.ResolutionStateInfoText.text = "FINAL SCORE";

                StartCoroutine(CalculateScore());
                uIElements.FinishUIElements.gameObject.SetActive(true);
                uIElements.HighScoreText.gameObject.SetActive(true);
                uIElements.HighScoreText.text = ((highscore > events.StartupHighscore) ? "<color=yellow>new </color>" : string.Empty) + "Highscore: " + highscore;
                break;
        }
    }

    /// <summary>
    /// Function that is used to calculate and display the score.
    /// </summary>
    private IEnumerator CalculateScore()
    {
        var scoreValue = 0;
        while (scoreValue < events.CurrentFinalScore)
        {
            scoreValue++;
            uIElements.ResolutionScoreText.text = scoreValue.ToString();

            yield return null;
        }
    }

    /// <summary>
    /// Function that is used to create new question answers.
    /// </summary>
    private void CreateAnswers(Question question)
    {
        EraseAnswers();

        float offset = 0 - parameters.Margins;
        for (int i = 0; i < question.Answers.Length; i++)
        {
            AnswerData newAnswer = (AnswerData)Instantiate(answerPrefab, uIElements.AnswersContentArea);
            newAnswer.UpdateData(question.Answers[i].Info, i);

            newAnswer.Rect.anchoredPosition = new Vector2(0, offset);

            offset -= (newAnswer.Rect.sizeDelta.y + parameters.Margins);
            uIElements.AnswersContentArea.sizeDelta = new Vector2(uIElements.AnswersContentArea.sizeDelta.x, offset * -1);

            currentAnswers.Add(newAnswer);
        }
    }
    /// <summary>
    /// Function that is used to erase current created answers.
    /// </summary>
    private void EraseAnswers()
    {
        foreach (var answer in currentAnswers)
        {
            Destroy(answer.gameObject);
        }
        currentAnswers.Clear();
    }

    /// <summary>
    /// Function that is used to update score text UI.
    /// </summary>
    private void UpdateScoreUI()
    {
        uIElements.ScoreText.text = "Score: " + events.CurrentFinalScore;
    }

    public void TriggerPanelTransition(UIPanel panel)
    {
        TriggerOpenPanel(panel);
    }

    private void TriggerOpenPanel(UIPanel panel)
    {
        if (_currentPanel != null)
        {
            TriggerClosePanel(_currentPanel);
        }
        _currentPanel = panel;
        panel.OpenBehaviour();
    }

    private void TriggerClosePanel(UIPanel panel)
    {
        panel.CloseBehaviour();
    }

}

/*public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public UIPanel _currentPanel;
    public UIPanel firstPanel, secondPanel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else {
             Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (_currentPanel != null)
        {
            _currentPanel.UpdateBehaviour();
        }
    }

    public void TriggerPanelTransition(UIPanel panel)
    {
        TriggerOpenPanel(panel);
    }

    void TriggerOpenPanel(UIPanel panel) {
        if (_currentPanel != null)
        {
            TriggerClosePanel(_currentPanel);
        }
        _currentPanel = panel;
        panel.OpenBehaviour();
    }

    void TriggerClosePanel(UIPanel panel) {
        panel.CloseBehaviour();
    }
}
*/
