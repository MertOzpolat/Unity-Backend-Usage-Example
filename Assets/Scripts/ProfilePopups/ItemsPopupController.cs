using System.Collections;
using System.Collections.Generic;
using Duck.Http;
using Duck.Http.Service;
using Newtonsoft.Json;
using UnityEngine;

public class ItemsPopupController : Singleton<ItemsPopupController>
{
    [SerializeField] ItemCardController itemCardController;
    [SerializeField] Transform itemCardParent;
    List<ItemCardController> itemCards = new List<ItemCardController>();
    public void OpenPopup()
    {
        string token = PlayerPrefs.GetString("Token");
        var headers = new[] {
            new KeyValuePair<string,string>("Content-Type","application/json"),
            new KeyValuePair<string,string>("Authorization",$"Bearer: {token}")
        };
        Http.Get($"http://localhost:5000/api/auth/items").SetHeaders(headers)
        .OnSuccess(response =>
        {
            Debug.Log("OpenPopup OnSuccess");
            GetItemsResponse(response);
        })
        .OnError(response =>
        {
            Debug.Log("OpenPopup OnError");
            GetItemsResponse(response);
        }).Send();
    }
    void GetItemsResponse(HttpResponse httpResponse)
    {

        if (httpResponse.StatusCode == 200)
        {
            Item[] items = JsonConvert.DeserializeObject<Item[]>(httpResponse.Text);

            foreach (var item in items)
            {
                ItemCardController nO = Instantiate(itemCardController, Vector3.zero, Quaternion.identity, itemCardParent);
                nO.InitItem(item);
                itemCards.Add(nO);
            }
        }
        if (httpResponse.Error != null || httpResponse.Error != "")
            Debug.Log(httpResponse.Text + " " + httpResponse.Error);
    }
    public void RemoveItemCard(ItemCardController card)
    {
        itemCards.Remove(card);
        Destroy(card.gameObject);
    }
}
