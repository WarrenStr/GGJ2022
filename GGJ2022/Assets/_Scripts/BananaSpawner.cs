using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    public GameObject banana;
    public int xPos;
    public int zPos;
    public int bananaCount;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(BananaDrop());
    }

    IEnumerator BananaDrop()
    {
        while (bananaCount < 5)
        {
            xPos = Random.Range(-7, 67);
            zPos = Random.Range(-35, 11);
            Instantiate(banana, new Vector3(xPos, 1.5f, zPos), Quaternion.identity);
            yield return new WaitForSeconds(10.0f);
            bananaCount += 1;
        }
    }

    public void SubtractBanana(int banana)
    {
        bananaCount -= banana;
    }
}