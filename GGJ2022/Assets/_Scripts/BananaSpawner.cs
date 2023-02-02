using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaSpawner : MonoBehaviour
{
    public GameObject banana;
    int xPos;
     int zPos;


    public int xPos1, xPos2;
    public int zPos1, zPos2;
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
            xPos = Random.Range(xPos1, xPos2); //-7 67
            zPos = Random.Range(zPos1, zPos2); //-335 11
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