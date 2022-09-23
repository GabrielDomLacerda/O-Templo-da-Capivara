using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PergaminhoController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Component scrollComponent;
    private int actualScroll = 0;
    private List<Component> pergaminhos = new List<Component>();
    private List<string> _nomePergaminhos = new List<string>() {
        "Pergaminho 1", "Pergaminho 2", "Pergaminho 3", "Pergaminho 4", "Pergaminho 5"
    };
    void Start()
    {
        foreach (Component obj in scrollComponent.GetComponentsInChildren<Component>())
        {
            foreach (string perg in _nomePergaminhos)
            {
                if (obj.name == perg)
                {
                    pergaminhos.Add(obj);
                    _nomePergaminhos.Remove(perg);
                    break;
                }

            }
        }
        foreach (Component perg in pergaminhos)
        {
            perg.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
        pergaminhos[actualScroll].GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    public void SetNext()
    {
        TurnInvisible(actualScroll);
        if (actualScroll + 1 == pergaminhos.Count)
        {
            Win();
            actualScroll = (actualScroll + 1) % pergaminhos.Count;
            return;
        }
        actualScroll = (actualScroll + 1) % pergaminhos.Count;
        TurnVisible(actualScroll);
    }

    private void TurnVisible(int index)
    {
        pergaminhos[index].GetComponentInChildren<MeshRenderer>().enabled = true;
    }

    private void TurnInvisible(int index)
    {
        pergaminhos[index].GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    private void Win()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
