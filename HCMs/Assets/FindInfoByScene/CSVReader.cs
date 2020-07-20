using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVReader : MonoBehaviour
{
    TextAsset csvFile;  // CSVファイル
    public int height;  // CSVの行数
    List<string[]> csvDatas = new List<string[]>(); // CSVの中身を入れるリスト;

    // Start is called before the first frame update
    public List<string[]> LoadCSV(string fileName)
    {
        csvFile = Resources.Load(fileName) as TextAsset;
        Debug.Log(csvFile.text);
        StringReader strReader = new StringReader(csvFile.text);

        while(strReader.Peek() > -1)
        {
            string line = strReader.ReadLine();
            csvDatas.Add(line.Split(','));
            height++;
        }

        Debug.Log(csvDatas[0][1]);

        return csvDatas;
    }
}
