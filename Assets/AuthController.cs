using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

public class AuthController : MonoBehaviour {

    public Text emailInput, passwordInput;  

    public void Login()
    {
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(emailInput.text,
                passwordInput.text).ContinueWith(( task=>
                {
                    //prekinjen
                    if (task.IsCanceled)
                    {
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

                    }




                }));


    }

    public void Login_Anonymus()
    {

    }

    public void RegisterUser()
    {
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(emailInput.text,
            passwordInput.text).ContinueWith((task =>
            {
                if(emailInput.text.Equals("") && passwordInput.text.Equals(""))
                {
                    print("Vnesite email in geslo");
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
                    print("registracija uspesna");
                }



            }));
    }

    public void Logout()
    {

    }

    void GetErrorMessage(AuthError errorCode)
    {
        string msg = "";
        msg = errorCode.ToString();

        print(msg);
    }

} 







































