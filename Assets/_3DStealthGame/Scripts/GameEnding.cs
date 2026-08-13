using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameEnding : MonoBehaviour
{
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public UIDocument uiDocument;

    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;


    private VisualElement m_EndScreen;
    private VisualElement m_CaughtScreen;

    void Start()
    {
        m_EndScreen = uiDocument.rootVisualElement.Q<VisualElement>("EndScreen");
        m_CaughtScreen = uiDocument.rootVisualElement.Q<VisualElement>("CaughtScreen");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }
    }

    public void CaughtPlayer()
    {
        m_IsPlayerCaught = true;
    }

    void Update()
    {
        if (m_IsPlayerAtExit)
        {
            EndLevel(m_EndScreen, false);
        }
        else if (m_IsPlayerCaught)
        {
            EndLevel(m_CaughtScreen, true);
        }
    }

    void EndLevel(VisualElement element, bool doRestart)
    {
        m_Timer += Time.deltaTime;
        element.style.opacity = m_Timer / fadeDuration;

        if (m_Timer > fadeDuration + displayImageDuration)
        {
            if (doRestart)
            {
                SceneManager.LoadScene("Main");
            }
            else
            {
                Application.Quit();
                Time.timeScale = 0;
            }
        }
    }
}
