using System;
using System.Collections;
using System.Collections.Generic;
using Duck.Http;
using Duck.Http.Service;
using Newtonsoft.Json;
using UnityEngine;
using TMPro;

public class ClanPopupController : MonoBehaviour
{
    [SerializeField] TMP_Text clanName;
    [SerializeField] TMP_Text managers;
    public void OpenPopup()
    {
        if (String.IsNullOrEmpty(SampleSceneController.Instance.currentUser.clan))
        {
            //activate create clan form and button.
        }
        else
        {
            //activate clan info and leave clan button.
            string token = PlayerPrefs.GetString("Token");
            var headers = new[] {
                new KeyValuePair<string,string>("Content-Type","application/json"),
                new KeyValuePair<string,string>("Authorization",$"Bearer: {token}")
            };
            Http.Get($"http://localhost:5000/api/clan/{SampleSceneController.Instance.currentUser.clan}").SetHeaders(headers)
            .OnSuccess(response =>
            {
                Debug.Log("OpenPopup OnSuccess");
                GetClanResponse(response);
            })
            .OnError(response =>
            {
                Debug.Log("OpenPopup OnError");
                GetClanResponse(response);
            }).Send();
        }
    }
    void GetClanResponse(HttpResponse httpResponse)
    {
        if (httpResponse.StatusCode == 200)
        {
            Clan clan = JsonConvert.DeserializeObject<Clan>(httpResponse.Text);
            clanName.text = clan.name;
            managers.text = clan.managers[0];
            managers.text = string.Join(", ", clan.managers);
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
    public void LeaveClanButtonHandler()
    {
        string token = PlayerPrefs.GetString("Token");
        byte[] array = new byte[0];
        var headers = new[] {
            new KeyValuePair<string,string>("Content-Type","application/json"),
            new KeyValuePair<string,string>("Authorization",$"Bearer: {token}")
        };
        Http.Put($"http://localhost:5000/api/auth/exitclan", array).SetHeaders(headers)
        .OnSuccess(response =>
        {
            Debug.Log("clan exit OnSuccess");
            gameObject.SetActive(false);
        })
        .OnError(response =>
        {
            Debug.Log("clan exit OnError");
            GetClanResponse(response);
        }).Send();
    }
}
