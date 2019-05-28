using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSVReaderTester : MonoBehaviour
{
    // Start is called before the first frame update

    PlayerManager playerManager;
    void Start()
    {
        StartCoroutine("LoadData");
    }

    IEnumerator LoadData()
    {
        yield return new WaitForEndOfFrame();

        playerManager = PlayerManager.Instance;

        List<Dictionary<string, object>> data = CSVReader.Read("Data");




        for (int i = 0; i < data.Count; i++)
        {


            playerManager.minesByDifficulty[i] = (int)data[i]["minesByDifficulty"];
            playerManager.countByDifficulty[i] = (int)data[i]["countsByDifficulty"];
            playerManager.hiddensByDifficulty[i] = (int)data[i]["hiddensByDifficulty"];
            playerManager.flipsByDifficulty[i] = (int)data[i]["flipsByDifficulty"];
        }
    }
}
