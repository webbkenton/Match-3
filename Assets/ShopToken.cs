using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopToken : MonoBehaviour
{
    public bool playerSelected;
    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && this.playerSelected == true)
        {
            StartCoroutine(LoadLevel());
        }
    }
    public IEnumerator LoadLevel()
    {
        StartCoroutine(PersistantData.data.TransitionIn());
        yield return new WaitForSeconds(2f);
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().map.transform.localScale = new Vector3(0, 0, 0);
        //GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().Match3UI.SetActive(true);
        //GameObject.FindGameObjectWithTag("UIManager").GetComponent<MenuScript>().Match3Board.SetActive(true);
        SceneManager.LoadScene("Shop Scene");
    }
}
