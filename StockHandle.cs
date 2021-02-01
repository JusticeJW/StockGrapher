using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;
using UnityEngine.UI.Extensions;

public static class SystemInfo
{
    public static int CURRENT_YEAR = 2018;
    public static int HISTORICAL_POINTS = 100;
    public static int DATE_AMOUNT = 8;
}

public class StockHandle : MonoBehaviour
{
    public string stockSymbol = "";

    public Text highPriceText;
    public InputField stockSymbolInputField;

    public GameObject circleDotPrefab;
    public GameObject LinePrefab;
    public GameObject TextPrefab;
    public Vector2 highestGraphPosition;
    public Vector2 lowestGraphPosition;
    public Transform dotParent;
    public Transform graph;

    public bool FirstRun = true;

    float min = 0;
    float max = 0;
    float diff = 0;

    void Start()
    {
        highPriceText.text = "HELLO";
    }

    void Update()
    {

    }

    public void UpdateStockSymbol(string symbol)
    {
        stockSymbol = symbol;
    }

    public void UpdateData(string data)
    {
        //determine the highest price for the current stock symbol
        //update that highest price into the text object "highPriceText"

        List<char> rawdigs = new List<char>();
        List<List <float>> nums = new List<List <float>>();
        List<float> dates = new List<float>();
        List<float> opens = new List<float>();
        List<float> highs = new List<float>();
        List<float> lows = new List<float>();
        List<float> closes = new List<float>();
        List<float> volumes = new List<float>();
        string[] monthdata = data.Split(',');

        string temp = "";
        float temp1 = 0;
        int up = 0;
        int op = 0;
        int XC = 0;
        int p = 0;
        int u = 0;
        float gb = 0;
        int jf = 0;
        //Debug.Log(monthdata[XC]);
        
        foreach (string VB in monthdata)
        {
            p = 0;
            nums.Add(new List<float>());
            foreach (char i in monthdata[XC])
            {
                temp = "";
                if (Char.IsDigit(i) == true || i == '.')
                {
                    rawdigs.Add(monthdata[XC][p]);
                }
                else if (u < rawdigs.Count)
                {
                    for (int m = 0; u < rawdigs.Count; u++)
                    {
                        temp += rawdigs[u];
                    }
                    if (temp[temp.Length - 1] == '.')
                    {
                        temp += '0';
                    }
                    //Debug.Log(temp);
                    nums[XC].Add(float.Parse(temp));
                }
                p++;
            }
            XC++;
            //Debug.Log("break");
        }
        XC--;


        foreach (List <float> i in nums)
        {
            if (up >= 7)
            {
                if (op == 5)
                {
                    op = 0;
                }
                if (op == 0)
                {
                    opens.Add(i[4]);
                }
                if (op == 1)
                {
                    highs.Add(i[1]);
                }
                if (op == 2)
                {
                    lows.Add(i[1]);
                }
                if (op == 3)
                {
                    closes.Add(i[1]);
                }
                if (op == 4)
                {
                    volumes.Add(i[1]);
                }
                op++;
            }
            up++;
        }

        /*foreach (List <float> i in nums)
        {
            foreach (float jk in nums[op])
            {
                //Debug.Log(nums[op].Count);
                //Debug.Log(nums[op].Count);
                if (up < nums[op].Count - 5)
                {
                    if (nums[op][up] == 1 && nums[op][up + 2] == 2)
                    {
                        opens.Add(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 2 && nums[op][up + 2] == 3)
                    {
                        highs.Add(nums[op][up + 1]);
                        Debug.Log(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 3 && nums[op][up + 2] == 4)
                    {
                        lows.Add(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 4 && nums[op][up + 2] == 5)
                    {
                        closes.Add(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 5 && nums[op][up + 5] == 1)
                    {
                        volumes.Add(nums[op][up + 1]);
                    }
                }
                if (up > 5 && up < nums[op].Count - 1)
                {
                    Debug.Log(nums[op][up]);
                    if (nums[op][up] == 1 && nums[op][up - 5] == 5)
                    {
                        opens.Add(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 2 && nums[op][up - 2] == 1)
                    {
                        highs.Add(nums[op][up + 1]);
                        Debug.Log(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 3 && nums[op][up - 2] == 2)
                    {
                        lows.Add(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 4 && nums[op][up - 2] == 3)
                    {
                        closes.Add(nums[op][up + 1]);
                    }
                    if (nums[op][up] == 5 && nums[op][up - 2] == 4)
                    {
                        volumes.Add(nums[op][up + 1]);
                    }
                }
                up++;
            }
            op++;
        }*/

        //highPriceText.text = nums[12].ToString();
        //Debug.Log(highs.Count);

        List<float> sortedhighs = new List<float>();

        foreach(float i in highs)
        {
            sortedhighs.Add(i);
        }

        
        for (int j = 0; j < sortedhighs.Count; j++)
        {
            for (int i = 0; i < sortedhighs.Count - 1; i++)
            {
                if (sortedhighs[i] < sortedhighs[i + 1])
                {
                    temp1 = sortedhighs[i + 1];
                    sortedhighs[i + 1] = sortedhighs[i];
                    sortedhighs[i] = temp1;
                }
            }
        }
        

        Debug.Log(highs[0]);

        if (FirstRun)
        {
            min = sortedhighs[sortedhighs.Count - 1];
            max = sortedhighs[0];
            diff = max - min;
            FirstRun = false;
        }

        GameObject mt = Instantiate(TextPrefab, new Vector3(0, 0, 0), Quaternion.identity, graph);
        mt.transform.localPosition = new Vector3(-520, -276 + (564 * (sortedhighs[0] - sortedhighs[sortedhighs.Count - 1]) / diff), 0);
        mt.GetComponent<Text>().text = sortedhighs[0].ToString();

        GameObject nt = Instantiate(TextPrefab, new Vector3(0, 0, 0), Quaternion.identity, graph);
        nt.transform.localPosition = new Vector3(-520, -276 + (564 * (sortedhighs[sortedhighs.Count - 1] - sortedhighs[sortedhighs.Count - 1]) / diff), 0);
        nt.GetComponent<Text>().text = sortedhighs[sortedhighs.Count - 1].ToString();

        int kl = 2000;
        p = 0;
        foreach (float v in highs)
        {
            if (jf == 60)
            {
                jf = 0;
            }
            GameObject g = Instantiate(circleDotPrefab, new Vector3(0,0,0), Quaternion.identity, dotParent);
            g.transform.localPosition = new Vector3(gb, -276 + (564 * (highs[p] - sortedhighs[sortedhighs.Count - 1]) / diff), 0);
            if (jf == 0)
            {
                GameObject dm = Instantiate(TextPrefab, new Vector3(0, 0, 0), Quaternion.identity, dotParent);
                dm.transform.localPosition = new Vector3(gb, -310, 0);
                dm.GetComponent<Text>().text = kl.ToString();
                GameObject gmn = Instantiate(circleDotPrefab, new Vector3(0, 0, 0), Quaternion.identity, dotParent);
                gmn.transform.localPosition = new Vector3(gb, -284, 0);
                jf = 0;
                kl += 5;
            }
            if (p < highs.Count - 1)
            {
                GameObject z = Instantiate(LinePrefab, new Vector3(0, 0, 0), Quaternion.identity, dotParent);
                z.transform.localPosition = new Vector3(0, 0, 0);
                z.GetComponent<UILineRenderer>().Points[0] = new Vector3(gb, -276 + (564 * (highs[p] - sortedhighs[sortedhighs.Count - 1]) / diff), 0);
                z.GetComponent<UILineRenderer>().Points[1] = new Vector3(gb + 850.0f / highs.Count, -276 + (564 * (highs[p + 1] - sortedhighs[sortedhighs.Count - 1]) / diff), 0);
            }
            Debug.Log(850.0f / highs.Count);
            p++;
            gb += 850.0f / highs.Count;
            jf++;
        }
        /*GameObject bm = Instantiate(TextPrefab, new Vector3(0, 0, 0), Quaternion.identity, dotParent);
        bm.transform.localPosition = new Vector3(gb, -310, 0);
        GameObject gt = Instantiate(circleDotPrefab, new Vector3(0, 0, 0), Quaternion.identity, dotParent);
        gt.transform.localPosition = new Vector3(gb, -284, 0);
        bm.GetComponent<Text>().text = kl.ToString(); */
    }

    public void GetText2()
    {
        StartCoroutine(GetText());
    }

    IEnumerator GetText()
    {

        UnityWebRequest www = UnityWebRequest.Get
            ("https://www.alphavantage.co/query?function=TIME_SERIES_MONTHLY&symbol=" + stockSymbol + "&apikey=BV27AU75S4UV24D5");

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            //Debug.Log(www.downloadHandler.text);
            // Or retrieve results as binary data
            byte[] results = www.downloadHandler.data;
        }
        string rawData = www.downloadHandler.text;

        //string cleanParse1 = rawData.Replace("{", "");
        //string cleanParse2 = cleanParse1.Replace("}", "");
        //string helpParse1 = cleanParse2.Replace("\"", "&");

        //create an array of strings that split at the { and } symbols
        string[] rawSplitStockData = rawData.Split(new string[] { "{", "}" }, StringSplitOptions.None);

        //loop through the array above, and set ALL spaces to empty
        for(int i = 0; i < rawSplitStockData.Length; i++)
        {
            rawSplitStockData[i].Replace(" ", "");
        }
        UpdateData(rawData);
    }
}