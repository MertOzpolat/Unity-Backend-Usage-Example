using UnityEngine;

public class SampleSceneController : Singleton<SampleSceneController>
{
    public User currentUser;
    void Start(){
        UIController.Instance.OpenSection(SectionType.Auth);
    }
    public void SetCurrentUser(User cu,string token){
        currentUser=cu;
        PlayerPrefs.SetString("Token",token);
        UIController.Instance.OpenSection(SectionType.App);
    }
}

