using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using UnityEngine.SceneManagement;
using System.CodeDom;
using Firebase.Database;
using Firebase.Extensions;

public class AuthController : MonoBehaviour {

    public Text emailInput, passwordInput;
    public static Player aktualni=null;
    

    public void Login()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
                passwordInput.text).ContinueWithOnMainThread(( task=>
                {
                    //prekinjen
                    if (task.IsCanceled)
                    {
                        Firebase.FirebaseException e =
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;
                    }

                    //ce se je pojavila kaka napaka
                    if (task.IsFaulted)
                    {

                        Firebase.FirebaseException e = 
                        task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                        GetErrorMessage((AuthError)e.ErrorCode);

                        return;

                    }
                    //uspešen
                    if (task.IsCompleted)
                    {
                        string userid=task.Result.UserId;
                        // ustvari se nov player ki mu morama zaj skozi uid pogledat nickname
                        aktualni=new Player(userid);

                        print("Prijava uspešna!");
                        SceneManager.LoadScene("start");

                    }


                }));


    }

    private void SaveCurrentUser()
    {
        
    }
    public void Login_Anonymus()
    {
        SceneManager.LoadScene("start");
    }

    public void RegisterUser()
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text,
            passwordInput.text).ContinueWithOnMainThread((task =>
            {
                if(emailInput.text.Equals("") && passwordInput.text.Equals(""))
                {
                    print("Vnesite email in geslo!");
                    return;
                }
                //prekinjen
                if (task.IsCanceled)
                {
                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                    GetErrorMessage((AuthError)e.ErrorCode);

                    return;
                }

                //ce se je pojavila kaka napaka
                if (task.IsFaulted)
                {

                    Firebase.FirebaseException e =
                    task.Exception.Flatten().InnerExceptions[0] as Firebase.FirebaseException;

                    GetErrorMessage((AuthError)e.ErrorCode);

                    return;

                }
                //uspešen
                if (task.IsCompleted)
                {
                    print("Registracija uspešna!");
                }



            }));
    }

    
    public void Logout()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser != null)
        {
            FirebaseAuth.DefaultInstance.SignOut();
            print("Odjava uspešna!");
        }
    }

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();


        //napake in izpis
        switch (errorCode)
        {
            case AuthError.AccountExistsWithDifferentCredentials:
                break;
            case AuthError.MissingPassword:
                    print("Vpišite geslo");
                break;
            case AuthError.WrongPassword:
                    print("Napačno geslo!");
                break;
            case AuthError.InvalidEmail:
                    print("Nepravilen email!");
                break;
        }


        print(msg);
    }

} 







































