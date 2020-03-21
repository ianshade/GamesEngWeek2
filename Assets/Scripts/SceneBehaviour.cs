using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneBehaviour : MonoBehaviour
{
    public GameObject barrel;
    public GameObject textMesh;
    private bool finished = false;
    private bool paused = false;

    void Start()
    {
        StartCoroutine(InstantiateBarrels());
    }

    void Update()
    {
        if(paused && Input.anyKeyDown)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1.0f;
            paused = false;
        }
    }

    IEnumerator InstantiateBarrels()
    {
        while (!finished)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 7.0f));
            Instantiate(barrel, new Vector3(0, 6, 0), Quaternion.identity);
        }
    }

    public void Dead()
    {
        textMesh.GetComponent<TextMeshPro>().text = "Dedłeś";
        Pause();
    }

    public void Win()
    {
        textMesh.GetComponent<TextMeshPro>().text = "You win";
        Pause();
    }

    private void Pause()
    {
        Time.timeScale = 0.0f;
        paused = true;
    }
}
