using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDieScript : MonoBehaviour
{
    //Variables
    public bool Invincible;
    public bool Dead = false;
    public GameObject DeathCanvas;
    public GameObject PauseCanvas;
    public GameObject UICanvas;
    public AudioClip ExplosionSound;
    GameObject Explosion;
    GameObject Smoke;
    AudioSource AudioSource;

    //Coroutines
    public IEnumerator Die()
    {
        Debug.Log("Dead");
        Dead = true;
        Time.timeScale = 0;
        Time.fixedDeltaTime = 0;

        //Play explosion sound
        AudioSource.clip = ExplosionSound;
        AudioSource.volume = 0.5f;
        AudioSource.Play();

        //Spawn explosion and smoke particles
        Explosion.transform.position = transform.position + Vector3.up * 1.5f;
        Smoke.transform.position = transform.position;

        Explosion.GetComponent<ParticleSystem>().Play();
        Smoke.GetComponent<ParticleSystem>().Play();

        //Make player invisible and deactivate move scripts
        foreach(Renderer r in gameObject.GetComponentsInChildren<MeshRenderer>())
        {
            r.enabled = false;
        }
        GetComponent<Collider>().enabled = false;
        GetComponent<PlayerMoveScript>().enabled = false;
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraScript>().enabled = false;
        GameObject.Find("DEBUG FPS Counter").GetComponent<TMPro.TextMeshProUGUI>().enabled = false;

        yield return new WaitForSecondsRealtime(0.5f);

        //Show the death screen with fade-in animation
        DeathCanvas.GetComponent<Canvas>().enabled = true;
        PauseCanvas.GetComponent<Canvas>().enabled = false;
        UICanvas.GetComponent<Canvas>().enabled = false;

        float count = 0;
        while (count <= 0.75f)
        {
            //Menu BG
            DeathCanvas.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.25f, count);
            //Dead Text BG
            DeathCanvas.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.03137255f, 0.1215686f, 0.2745098f, count / 0.75f);
            DeathCanvas.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5294118f, 0.5294118f, 0.5294118f, count / 0.75f);
            //Score Text BG
            DeathCanvas.transform.GetChild(0).GetChild(1).gameObject.GetComponent<Image>().color = new Color(0.03137255f, 0.1215686f, 0.2745098f, count / 0.75f);
            DeathCanvas.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5294118f, 0.5294118f, 0.5294118f, count / 0.75f);
            //High Score Text BG
            DeathCanvas.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Image>().color = new Color(0.03137255f, 0.1215686f, 0.2745098f, count / 0.75f);
            DeathCanvas.transform.GetChild(0).GetChild(2).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5294118f, 0.5294118f, 0.5294118f, count / 0.75f);
            //Restart Button
            DeathCanvas.transform.GetChild(0).GetChild(3).gameObject.GetComponent<Image>().color = new Color(0.03137255f, 0.1215686f, 0.2745098f, count / 0.75f);
            DeathCanvas.transform.GetChild(0).GetChild(3).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5294118f, 0.5294118f, 0.5294118f, count / 0.75f);
            //Main Menu Button
            DeathCanvas.transform.GetChild(0).GetChild(4).gameObject.GetComponent<Image>().color = new Color(0.03137255f, 0.1215686f, 0.2745098f, count / 0.75f);
            DeathCanvas.transform.GetChild(0).GetChild(4).GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().color = new Color(0.5294118f, 0.5294118f, 0.5294118f, count / 0.75f);

            count += 0.0175f;
            yield return new WaitForSecondsRealtime(0.001f);
        }
        DeathCanvas.transform.GetChild(0).gameObject.GetComponent<Image>().color = new Color(0.2f, 0.2f, 0.25f, 0.75f);

        yield return null;
    }

    //Start
    private void Start()
    {
        Explosion = GameObject.Find("ExplosionParticleSystem");
        Smoke = GameObject.Find("SmokeParticleSystem");
        AudioSource = GetComponent<AudioSource>();
    }

    //OnCollisionEnter
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Hazard" && !FadeScript.Fade.Faded && Invincible == false)
        {
            StartCoroutine(Die());
        }
    }
}
