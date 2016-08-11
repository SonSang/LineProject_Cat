using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager : MonoBehaviour {

    public GameObject MobileControllerUI;
    public GameOverManager GameOverUI;
    public CatChangeManager CatChangeUI;
    public GameObject HUD;
    public PauseManager PauseUI;

    private GameObject m;
    private GameOverManager g;
    private CatChangeManager c;
    private GameObject h;
    private PauseManager p;
    private Text lt;

    // Use this for initialization
    void Start () {
        m = Instantiate(MobileControllerUI);

        h = Instantiate(HUD);
        lt = h.GetComponentInChildren<Text>();

        g = Instantiate(GameOverUI);
        g.gameObject.SetActive(false);

        c = Instantiate(CatChangeUI);
        c.gameObject.SetActive(false);

        p = Instantiate(PauseUI);
        p.gameObject.SetActive(false);

        FindObjectOfType<LevelManager>().SetUI(c, g, p, lt);
        FindObjectOfType<CameraMove>().SetUI(m);
        FindObjectOfType<FindPlayerManager>().SetUI(c);
	}
}
