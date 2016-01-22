using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Myproto;

public class Support
{
    public static object InvokeGenericSerializerMethod(Type t, string MethodName, params object[] args)
    {
        Type type = typeof(Serialize);
        object o = Activator.CreateInstance(type);
        MethodInfo method = type.GetMethod(MethodName);
        method = method.MakeGenericMethod(t);
        return method.Invoke(o, args);
    }

    public static byte[] bytesReverse(byte[] origin)
    {
        //Debug.Log("byte Count: " + origin.Length);
        var reversed = new byte[origin.Length];
        for (int i = 0; i < origin.Length; i++)
        {
            reversed[i] = origin[origin.Length - i - 1];

        }
        return reversed;
    }

    public static UserEvent.EventType EventTypeConverter(Myproto.MsgType origin)
    {
        UserEvent.EventType ret = UserEvent.EventType.UPA;
        switch (origin)
        {
            case MsgType.AssetLoad:
                {
                    ret = UserEvent.EventType.LA;
                    break;
                }
            case MsgType.AssetUpload:
                {
                    ret = UserEvent.EventType.UPA;
                    break;
                }
            case MsgType.Command:
                {
                    ret = UserEvent.EventType.CMD;
                    break;
                }
            case MsgType.Control:
                {
                    ret = UserEvent.EventType.CTR;
                    break;
                }
            case MsgType.Environment:
                {
                    ret = UserEvent.EventType.ENV;
                    break;
                }
            case MsgType.GameObjectAdd:
                {
                    ret = UserEvent.EventType.GA;
                    break;
                }
            case MsgType.GameObjectRemove:
                {
                    ret = UserEvent.EventType.GR;
                    break;
                }
            case MsgType.StateTransfer:
                {
                    ret = UserEvent.EventType.ST;
                    break;
                }
        }
        return ret;
    }

    public static MsgType MsgTypeConverter(UserEvent.EventType origin) 
    {
        MsgType ret = MsgType.AssetUpload;
        switch (origin)
        {
            case UserEvent.EventType.CMD:
                {
                    ret = MsgType.Command;
                    break;
                }
            case UserEvent.EventType.CTR:
                {
                    ret = MsgType.Control;
                    break;
                }
            case UserEvent.EventType.ENV:
                {
                    ret = MsgType.Environment;
                    break; 
                }
            case UserEvent.EventType.GA:
                {
                    ret = MsgType.GameObjectAdd;
                    break;
                }
            case UserEvent.EventType.GR:
                {
                    ret = MsgType.GameObjectRemove;
                    break;
                }
            case UserEvent.EventType.LA:
                {
                    ret = MsgType.AssetLoad;
                    break;
                }
            case UserEvent.EventType.ST:
                {
                    ret = MsgType.StateTransfer;
                    break;
                }
            case UserEvent.EventType.UPA:
                {
                    ret = MsgType.AssetUpload;
                    break;
                }
        }
        return ret;
    }
}
