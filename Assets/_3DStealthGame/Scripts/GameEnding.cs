using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameEnding : MonoBehaviour
{
    private float m_Demo_GameTimer = 0f;
    private bool m_Demo_GameTimerIsTicking = false;
    private Label m_Demo_GameTimerLabel;

    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public UIDocument uiDocument;
    public AudioSource exitAudio;
    public AudioSource caughtAudio;

    bool m_HasAudioPlayed;
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;

    private VisualElement m_EndScreen;
    private VisualElement m_CaughtScreen;

    private Button m_RestartButton;
    private Button m_CaughtRestartButton;

    void Start()
    {
        var root = uiDocument.rootVisualElement;

        m_EndScreen = root.Q<VisualElement>("EndScreen");
        m_CaughtScreen = root.Q<VisualElement>("CaughtScreen");
        m_Demo_GameTimerLabel = root.Q<Label>("TimerLabel");

        // Buttons suchen
        m_RestartButton = root.Q<Button>("RestartButton");
        m_CaughtRestartButton = root.Q<Button>("CaughtRestartButton");

        // Klick-Funktion zuweisen
        if (m_RestartButton != null)
        {
            m_RestartButton.clicked += RestartGame;
        }

        if (m_CaughtRestartButton != null)
        {
            m_CaughtRestartButton.clicked += RestartGame;
        }

        m_Demo_GameTimer = 0f;
        m_Demo_GameTimerIsTicking = true;

        Demo_UpdateTimerLabel();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
            m_Demo_GameTimerIsTicking = false;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
        m_Demo_GameTimerIsTicking = false;
    }

    void Update()
    {
        if (m_Demo_GameTimerIsTicking)
        {
            m_Demo_GameTimer += Time.deltaTime;
            Demo_UpdateTimerLabel();
        }

        if (m_IsPlayerAtExit)
        {
            EndLevel(m_EndScreen, exitAudio);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(m_CaughtScreen, caughtAudio);
        }
    }

    void EndLevel(VisualElement element, AudioSource audioSource)
    {
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = true;
        }

        m_Timer += Time.deltaTime;

        element.style.opacity = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            // Timer stoppen, aber Spiel NICHT beenden.
            m_Demo_GameTimerIsTicking = false;
        }
    }

    private void RestartGame()
    {
        // Sicherheitshalber Spielzeit wieder normal setzen.
        Time.timeScale = 1f;

        // Main-Szene komplett neu laden.
        SceneManager.LoadScene("Main");
    }

    void Demo_UpdateTimerLabel()
    {
        if (m_Demo_GameTimerLabel != null)
        {
            m_Demo_GameTimerLabel.text =
                m_Demo_GameTimer.ToString("0.00");
        }
    }
}