using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

[RequireComponent(typeof(GameManager))]
public class CommandLine : MonoBehaviour
{
    private readonly float Y = 0.30f;
    public InputField commandLine;

    [SerializeField] private GameObject CircuitBoard;

    [SerializeField] private GameObject PositionIndicator;

    public Material GoodIndication;
    public Material BadIndication;

    public static int Clamp(int val, int min, int max)
    {
        return (val < min) ? min : (val > max) ? max : val;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            string _result = ExecuteCommand(commandLine.text);
            string[] _results = _result.Split('#');

            Color _color = new Color();

            switch(_results[0])
            {
                case "good":
                    _color = Color.green;
                break;

                case "bad":
                    _color = Color.red;
                break;
            }

            GameManager.Instance.ui.SetCommandResult(_results[1], _color);

            commandLine.text = "";
            commandLine.ActivateInputField();  
        }

        if(PositionIndicator.GetComponent<PositionIndicator>().Valid)
        {
            PositionIndicator.GetComponent<MeshRenderer>().material = GoodIndication;
        }
        else
        {
            PositionIndicator.GetComponent<MeshRenderer>().material = BadIndication;
        }
    }

    public void OnEdit()
    {
        string[] _tokens = commandLine.text.Split(new char[0]);
        
        switch(_tokens[0])
        {
            case "create":
                if(_tokens.Length == 5)
                {   
                    if(Regex.IsMatch(_tokens[3], @"^-?[0-9]\d*(\.\d+)?$") && Regex.IsMatch(_tokens[4], @"^-?[0-9]\d*(\.\d+)?$"))
                    {
                        PositionIndicator.GetComponent<PlaceableObject>().Move(NewPos(_tokens[3], _tokens[4], Y));

                    }
                    else
                    {               
                        PositionIndicator.GetComponent<PlaceableObject>().Move(NewPos("0", "0", 15f));     
                    }
                }
                else
                {
                    PositionIndicator.GetComponent<PlaceableObject>().Move(NewPos("0", "0", 15f));
                }
            break;
            case "move":
                if(_tokens.Length == 4)
                {
                    if(Regex.IsMatch(_tokens[2], @"^-?[0-9]\d*(\.\d+)?$$") && Regex.IsMatch(_tokens[3], @"^-?[0-9]\d*(\.\d+)?$"))
                    {
                        PositionIndicator.GetComponent<PlaceableObject>().Move(NewPos(_tokens[2], _tokens[3], Y));

                    }
                    else
                    {
                        PositionIndicator.GetComponent<PlaceableObject>().Move(NewPos("0", "0", 15f));
                    }
                }
                else
                {
                    PositionIndicator.GetComponent<PlaceableObject>().Move(NewPos("0", "0", 15f));
                }
            break;
        }
    }

    public void OnCommandEnd()
    {
        PositionIndicator.GetComponent<PlaceableObject>().Move(NewPos("0", "0", 15f));
    }

    private string ExecuteCommand(string _command)
    {
        string[] _tokens  = _command.Split(new char[0]);
    
        switch(_tokens[0])
        {
            case "create":
                if(GameManager.Instance.Towers.ContainsKey(_tokens[1]))
                {
                    if(_tokens[2] != "")
                    {
                        if(_tokens.Length == 5)
                        {
                            if(FindObjectByName(_tokens[2]) == null)
                            {
                                if(GameManager.Instance.Purchase(GameManager.Instance.Towers[_tokens[1]].GetComponent<PlaceableObject>().Cost))
                                {
                                    if(_tokens[1] == "firewall" || _tokens[1] == "advfirewall")
                                    {
                                        if(!PositionIndicator.GetComponent<PositionIndicator>().Valid)
                                        {
                                             GameObject _instantiated_object = Instantiate(GameManager.Instance.Towers[_tokens[1]]);
                                            _instantiated_object.transform.name = _tokens[2];
                                            _instantiated_object.transform.SetParent(CircuitBoard.transform);
                                            _instantiated_object.transform.localPosition = NewPos(_tokens[3], _tokens[4], 20);
                                            _instantiated_object.GetComponent<PlaceableObject>().Move(NewPos(_tokens[3], _tokens[4], Y));

                                            return "good#Created '"+_tokens[2]+"' ("+_tokens[1]+") at x: "+_tokens[3]+" y: "+_tokens[4];
                                        }
                                        else
                                        {
                                            return "bad#bad spot";
                                        }
                                    }
                                    else
                                    {
                                        if(PositionIndicator.GetComponent<PositionIndicator>().Valid)
                                        {
                                             GameObject _instantiated_object = Instantiate(GameManager.Instance.Towers[_tokens[1]]);
                                            _instantiated_object.transform.name = _tokens[2];
                                            _instantiated_object.transform.SetParent(CircuitBoard.transform);
                                            _instantiated_object.transform.localPosition = NewPos(_tokens[3], _tokens[4], 20);
                                            _instantiated_object.GetComponent<PlaceableObject>().Move(NewPos(_tokens[3], _tokens[4], Y));

                                            return "good#Created '"+_tokens[2]+"' ("+_tokens[1]+") at x: "+_tokens[3]+" y: "+_tokens[4];
                                        }
                                        else
                                        {
                                            return "bad#bad spot";
                                        }
                                    }
                                }
                                else
                                {
                                    return "bad#not enough energy";
                                }
                            }
                            else
                            {
                                return "bad#"+_tokens[2]+" already exists";
                            }
                        }
                        else
                        {
                            return "bad# incorrect command";
                        }
                    }
                    else
                    {
                        return "bad#name cannot be NULL";
                    } 
                }
                else
                {
                    return "bad#"+_tokens[1]+" cannot be created as it does not exist";
                }    

            case "move":
                if(FindObjectByName(_tokens[1]) != null)
                {
                    if(_tokens.Length == 4)
                    {
                        if(PositionIndicator.GetComponent<PositionIndicator>().Valid)
                        {
                            GameObject _object = FindObjectByName(_tokens[1]);
                            _object.GetComponent<PlaceableObject>().Move(NewPos(_tokens[2], _tokens[3], Y));

                            return "good#moved "+_tokens[1]+" to x: "+_tokens[2]+" y: "+_tokens[3];
                        }
                        else
                        {
                            return "bad#bad spot";
                        }
                    }
                    else
                    {
                            return "bad# incorrect command";
                    }
                }
                else
                {
                    return "bad#"+_tokens[1]+" could not be found";
                }

            case "rename":
                if(FindObjectByName(_tokens[1]) != null)
                {
                    GameObject _object = FindObjectByName(_tokens[1]);
                    _object.transform.name = _tokens[2];

                    return "good#renamed "+_tokens[1]+" to "+_tokens[2];
                }
                else
                {
                    return "bad#"+_tokens[1]+" could not be found";
                }
            case "upgrade":
                if(FindObjectByName(_tokens[1]) != null)
                {
                    GameObject _object = FindObjectByName(_tokens[1]);

                    if(GameManager.Instance.Purchase(_object.GetComponent<PlaceableObject>().UpgradeCost))
                    {
                        if(_object.GetComponent<PlaceableObject>().ModelNumber < 5)
                        {
                            _object.GetComponent<PlaceableObject>().Upgrade();
                            
                            return "good#upgraded "+_tokens[1];
                        }
                        else
                        {
                            return "bad#maxium upgrade reached";
                        }       
                    }
                    else
                    {
                        return "bad#not enough energy";
                    }
                }
                else
                {
                    return "bad#"+_tokens[1]+" could not be found";
                }
            case "start":
            {
                return "good#Round started!";
            }

            default:
                return "bad#Command '"+_tokens[0]+"' not regonized";
        }
    }

    private GameObject FindObjectByName(string _name)
    {
        int _count = CircuitBoard.transform.childCount;

        for(int i = 0; i < _count; i++)
        {
            if(CircuitBoard.transform.GetChild(i).name == _name)
            {
                return CircuitBoard.transform.GetChild(i).gameObject;
            }
            else
            {
                continue;
            }
        }

        return null;
    }

    private Vector3 NewPos(string x, string z, float y)
    {
        return new Vector3(Clamp(int.Parse(x), -5, 5), y, Clamp(int.Parse(z), -5, 5));
    }
}