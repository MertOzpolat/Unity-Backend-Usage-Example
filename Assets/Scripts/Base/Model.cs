using UnityEngine;
using UnityEngine.UI;

public class LoginRequest
{
    public string email;
    public string password;
}
public class RegisterRequest
{
    public string name;
    public string email;
    public string password;
}
public class AuthResponse
{
    public bool success;
    public string access_token;
    public User user;
}
public class User
{
    public string _id;
    public string name;
    public string email;
    public string password;
    public string createdAt;
    public string about;
    public string profile_image;
    public string blocked;
    public string resetPasswordToken;
    public string resetPasswordExpire;
    public int level;
    public int exprience;
    public string clan;//clan ref object.
    public string[] friends;
    public string[] blockList;
    public int balance;
    public string[] inventory;
}
public class Item
{
    public string _id;
    public string name;
    public string image;
    public string info;
}
[System.Serializable]
public class Clan{
    public string _id;
    public string name;
    public string[] managers;
    public string[] users;
    public string image;
    public string clanExperience;
    public string clanLevel;
    public Item[] inventory;
    
}
[System.Serializable]
public class MenuItem
{
    public MenuType type;
    public GameObject menuPanel;
    public Button menuButton;
}
[System.Serializable]
public enum MenuType
{
    Profile,
    Clan,
    Item,
    User
}

[System.Serializable]
public class Section
{
    public SectionType sectionType;
    public GameObject sectionObject;
}
[System.Serializable]
public enum SectionType
{
    Auth,
    App
}