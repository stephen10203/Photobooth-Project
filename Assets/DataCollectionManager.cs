using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VRKeyboard.Utils;
using System.IO;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Linq;
using UnityEngine.EventSystems;

public class DataCollectionManager : MonoBehaviour
{
    public Text[] Textboxes;

    public bool Caps;
    public string character;
    public int termsAlpha;
    public int step;

    [Serializable]
    public class MyClass
    {
        public string Firstname;
        public string Surname;
        public string PhoneNumber;
        public string EmailAddress;
        public string Day;
        public string Month;
        public string Year;
        public string Country;
    }

    public void Start()
    {
        Caps = false;
        GameObject.Find("TermsImage").GetComponent<CanvasGroup>().alpha = 0;
        termsAlpha = 0;
        GameObject.Find("Save").GetComponent<Button>().interactable = false;
        ClearDropBoxOptions();
        PopulateComboBoxes();
        TickBoxDefault();
        step = 0;
    }

    public void Update()
    {
       
        //Textboxes[step].text = GameObject.Find("Input").GetComponent<Text>().text.Trim();
    }

    public void Next()
    {
        if (Validate())
        {
            step = Math.Min(6, step + 1);
            if (step == 3)
            {
                Caps = false;
            }
            else
            {
                Caps = false;
            }
            if (step == 6)
            {
                GameObject.Find("Save").GetComponent<Button>().interactable = true;
                GameObject.Find("Next").GetComponent<Button>().interactable = false;
            }
            Textboxes[step].text="";
        }
    }

    // Update is called once per frame

    public void TickBoxDefault()
    {
        GameObject.Find("Toggle").GetComponent<Toggle>().isOn = false;
    }

    public bool TickBoxValue()
    {
        return GameObject.Find("Toggle").GetComponent<Toggle>().isOn;
    }

    public void ClearDropBoxOptions()
    {
        GameObject.Find("Country").GetComponent<Dropdown>().options.Clear();
        GameObject.Find("Country").GetComponent<Dropdown>().captionText.text = "Country of Residence";
    }

    public void ReturnToStartButton()
    {
        if (!Validate())
        {
            return;
        }
        if (GameObject.Find("Country").GetComponent<Dropdown>().captionText.text == "")
        {
            return;
        }
        if (!TickBoxValue())
        {
            return;
        }
        writeStuffToFile();
        SceneManager.LoadScene("Start");
    }

    public void Terms()
    {
        if (termsAlpha == 0)
        {
            GameObject.Find("TermsImage").GetComponent<CanvasGroup>().alpha = 1;
            termsAlpha = 1;
        }
        else
        {
            GameObject.Find("TermsImage").GetComponent<CanvasGroup>().alpha = 0;
            termsAlpha = 0;
        }
    }

    public bool Validate()
    {
        bool success = true;
        switch (step)
        {
            case 0:
                if (!IsValidFirstName(Textboxes[0].text))
                {
                    Textboxes[0].GetComponent<Text>().color = Color.red;
                    success = false;
                }
                break;

            case 1:
                if (!IsValidSurname(Textboxes[1].text))
                {
                    Textboxes[1].GetComponent<Text>().color = Color.red;
                    success = false;
                }
                break;

            case 2:
                if (!IsPhoneNumber(Textboxes[2].text))
                {
                    Textboxes[2].GetComponent<Text>().color = Color.red;
                    success = false;
                }
                break;

            case 3:
                if (!IsValidEmail(Textboxes[3].text))
                {
                    Textboxes[3].GetComponent<Text>().color = Color.red;
                    success = false;
                }
                break;

            case 4:
                if (!IsValidDay(Textboxes[4].text))
                {
                    Textboxes[4].GetComponent<Text>().color = Color.red;
                    success = false;
                }
                break;

            case 5:
                if (!IsValidMonth(Textboxes[5].text))
                {
                    Textboxes[5].GetComponent<Text>().color = Color.red;
                    success = false;
                }
                break;

            case 6:
                if (!IsValidYear(Textboxes[6].text))
                {
                    Textboxes[6].GetComponent<Text>().color = Color.red;
                    success = false;
                }
                break;
        }

        return success;
    }

    public void writeStuffToFile()
    {
        MyClass myObject = new MyClass();
        myObject.Firstname = Textboxes[0].text;
        myObject.Surname = Textboxes[1].text;
        myObject.PhoneNumber = Textboxes[2].text;
        myObject.EmailAddress = Textboxes[3].text;
        myObject.Day = Textboxes[4].text;
        myObject.Month = Textboxes[5].text;
        myObject.Year = Textboxes[6].text;
        myObject.Country = GameObject.Find("Country").GetComponent<Dropdown>().captionText.text;
        string json = JsonUtility.ToJson(myObject) + "\r\n";
        PersistentManager pm = GameObject.Find("PersistentManager").GetComponent<PersistentManager>();
        File.AppendAllText(pm.FolderPath() + "data.json", json);
    }

    public bool IsValidEmail(string email)
    {
        string Email = email.Trim();
        if (Email.Length == 0)
        {
            return false;
        }
        try
        {
            var addr = new System.Net.Mail.MailAddress(Email);
            return addr.Address == Email;
        }
        catch
        {
            return false;
        }
    }

    public bool IsNumber(string number)
    {
        number = number.Trim();
        if (number.Length == 0)
        {
            return false;
        }
        char[] s = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
        for (int i = 0; i < number.Length; i++)
        {
            if (!s.Contains(number[i]))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsPhoneNumber(string number)
    {
        number = number.Trim();
        if (number.Length != 10)
        {
            return false;
        }
        if (!IsNumber(number))
        {
            return false;
        }

        return true;
    }

    public bool IsValidFirstName(string name)
    {
        name = name.Trim();
        if (name.Length == 0)
        {
            return false;
        }
        if (!Regex.IsMatch(name.Trim(), @"^[\p{L}\p{M}' \.\-]+$"))
        {
            return false;
        }
        return true;
    }

    public bool IsValidSurname(string name)
    {
        name = name.Trim();
        if (name.Length == 0)
        {
            return false;
        }
        if (!Regex.IsMatch(name.Trim(), @"^[\p{L}\p{M}' \.\-]+$"))
        {
            return false;
        }
        return true;
    }

    private bool IsValidDay(string day)
    {
        day = day.Trim();
        if (day.Length == 0)
        {
            return false;
        }
        if (!IsNumber(day) || day.Length != 2)
        {
            return false;
        }
        int dayInt = Convert.ToInt32(day);
        if (dayInt < 1 || dayInt > 31)
        {
            return false;
        }
        return true;
    }

    private bool IsValidMonth(string month)
    {
        month = month.Trim();
        if (month.Length == 0)
        {
            return false;
        }
        if (!IsNumber(month) || month.Length != 2)
        {
            return false;
        }
        int monthInt = Convert.ToInt32(month);
        if (monthInt < 1 || monthInt > 12)
        {
            return false;
        }
        return true;
    }

    private bool IsValidYear(string year)
    {
        year = year.Trim();
        if (year.Length == 0)
        {
            return false;
        }
        if (!IsNumber(year) || year.Length != 4)
        {
            return false;
        }
        int yearInt = Convert.ToInt32(year);
        if (yearInt < 1800 || yearInt > 2500)
        {
            return false;
        }
        return true;
    }

    public void Close()
    {
        SceneManager.LoadScene("Start");//?
    }

    public void PopulateComboBoxes()
    {
        RegionInfo country = new RegionInfo(new CultureInfo("en-US", false).LCID);
        List<string> countryNames = new List<string>();
        foreach (CultureInfo cul in CultureInfo.GetCultures(CultureTypes.SpecificCultures))
        {
            country = new RegionInfo(new CultureInfo(cul.Name, false).LCID);
            countryNames.Add(country.DisplayName.ToString());
        }
        GameObject.Find("Country").GetComponent<Dropdown>().captionText.text = "Country of Residence";
        GameObject.Find("Country").GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = "" });
        foreach (string item in (IEnumerable)countryNames.OrderBy(names => names).Distinct())
        {
            GameObject.Find("Country").GetComponent<Dropdown>().options.Add(new Dropdown.OptionData() { text = item });
        }
    }
    public void CapsPress()
    {
        Caps = !Caps;
    }
    //Keyboard Buttons
    public void ClearPress()
    {
        Textboxes[step].text = "";
    }
    public void BackSpace()
    {
        if (Textboxes[step].text.Length ==0)
        {
            return;
        }
        if (Textboxes[step].text.Length == 1)
        {
            Textboxes[step].text = "";
        }
        if (Textboxes[step].text.Length == 2)
        {
            Textboxes[step].text = Textboxes[step].text[0].ToString();
        }
        Textboxes[step].text = Textboxes[step].text.Substring(0, Textboxes[step].text.Length - 2);
    }
    public void symbol()
    {
        if (step == 0 && GameObject.Find("FirstNameOverlay").GetComponent<Text>().text == "Name")
        {
            GameObject.Find("FirstNameOverlay").GetComponent<Text>().text = "";
        }
    }
    public void A()
    {
        symbol();
        character = "a";
        if (Caps)
        {
            character = character.ToUpper();
        }
        Textboxes[step].text = Textboxes[step].text + character;
    }
    public void One()
    {
        symbol();
        character = "1";
        Textboxes[step].text = Textboxes[step].text + character;
    }
    public void Nine()
    {
        symbol();
        character = "9";
        Textboxes[step].text = Textboxes[step].text + character;
    }
    public void At()
    {
        symbol();
        character = "@";
        Textboxes[step].text = Textboxes[step].text + character;
    }
}