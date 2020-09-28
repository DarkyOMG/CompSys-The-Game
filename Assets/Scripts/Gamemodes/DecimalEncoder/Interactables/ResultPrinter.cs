using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPrinter : Interactable
{
    [SerializeField] GameObjectSO _resBoard;
    [SerializeField] GameObject _sphere;
    private Coroutine _sphereCoroutine;
    public override void Activate()
    {
        if(_sphereCoroutine == null && _resBoard.go.GetComponent<ResultBoard>().value > 0)
        {
            StartCoroutine(SpawnSphere());
        }
    }
    IEnumerator SpawnSphere()
    {
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(4);
        gameObject.GetComponent<ParticleSystem>().Stop();
        GameObject tempObj = Instantiate(_sphere, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        tempObj.GetComponent<PickupBlock>().value = _resBoard.go.GetComponent<ResultBoard>().value;
        tempObj.GetComponent<Renderer>().material.color = Color.yellow;

        _resBoard.go.GetComponent<ResultBoard>().Reset();
    }
    
}
