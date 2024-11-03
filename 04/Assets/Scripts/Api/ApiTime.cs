using System;
using System.Globalization;
using System.Threading.Tasks;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Networking;

public class ApiTime : MonoBehaviour
{
    private const string API_TIME_TOKYO = @"https://worldtimeapi.org/api/timezone/Asia/Tokyo";

    private const string ERROR_MESSAGE_FAILED_REQUEST = "UnityWebRequest Error: {0}";

    public static async Task<DateTime?> GetTimeTokyo()
    {
        return await GetTime(API_TIME_TOKYO);
    }

    public static DateTime? GetTimeTokyoSync()
    { 
        return GetTimeSync(API_TIME_TOKYO);
    }

    private static async Task<DateTime?> GetTime(string url)
    {
        using (var unityWebRequest = UnityWebRequest.Get(url))
        {
            var operation = unityWebRequest.SendWebRequest();
            while (!operation.isDone)
                await Task.Yield();

            return ConvertResponse2Datetime(unityWebRequest);
        }
    }

    private static DateTime? GetTimeSync(string url)
    {
        using (var unityWebRequest = UnityWebRequest.Get(url))
        {
            var operation = unityWebRequest.SendWebRequest();
            while (!operation.isDone) { }

            return ConvertResponse2Datetime(unityWebRequest);
        }
    }

    private static DateTime? ConvertResponse2Datetime(UnityWebRequest unityWebRequest)
    {
        if (unityWebRequest.result == UnityWebRequest.Result.ConnectionError ||
            unityWebRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(string.Format(ERROR_MESSAGE_FAILED_REQUEST, unityWebRequest.error));
            return null;
        }

        string response = unityWebRequest.downloadHandler.text;
        var parse = fsJsonParser.Parse(response);
        var datetime = parse.AsDictionary["datetime"].ToString();
        datetime = datetime.Replace("\"", "");
        return DateTime.ParseExact(datetime, "yyyy-MM-ddTHH:mm:ss.ffffffK", CultureInfo.InvariantCulture);
    }
}
