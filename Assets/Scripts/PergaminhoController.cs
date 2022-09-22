using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log(actualScroll + " Desativado");
        pergaminhos[actualScroll].GetComponentInChildren<MeshRenderer>().enabled = false;
        actualScroll = (actualScroll + 1) % pergaminhos.Count;
        Debug.Log(actualScroll + " Ativado");
        pergaminhos[actualScroll].GetComponentInChildren<MeshRenderer>().enabled = true;
    }
}
