using System.Collections;
using System.Collections.Generic;
using Duck.Http;
using Duck.Http.Service;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCardController : MonoBehaviour
{
    internal Item cardItem;
    [SerializeField] TMP_Text itemName;
    [SerializeField] Image itemImage;
    public void InitItem(Item item)
    {
        cardItem = item;
        itemName.text = item.name;
        //img and source works will be here
    }
    public void RemoveItemButtonHandler()
    {
        byte[] array = new byte[0];
        string token = PlayerPrefs.GetString("Token");
        var headers = new[] {
            new KeyValuePair<string,string>("Content-Type","application/json"),
            new KeyValuePair<string,string>("Authorization",$"Bearer: {token}")
        };
        Http.Put($"http://localhost:5000/api/auth/{cardItem._id}/removeitem", array).SetHeaders(headers)
        .OnSuccess(response =>
        {
            Debug.Log("RemoveItemButtonHandler OnSuccess");
            RemoveItemResponseHandler(response);
        })
        .OnError(response =>
        {
            Debug.Log("RemoveItemButtonHandler OnError");
            RemoveItemResponseHandler(response);
        }).Send();
    }
    public void RemoveItemResponseHandler(HttpResponse httpResponse)
    {
        if (httpResponse.StatusCode == 200)
        {
            AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(httpResponse.Text);
            ItemsPopupController.Instance.RemoveItemCard(this);
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
}
