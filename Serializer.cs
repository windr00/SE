using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ProtoBuf;

public class Serialize
{
    

    public enum Method
    {
        JSON,
        XML,
        PROTO
    }
    public static byte[] Serailizer<T>(T data, Method method)
    {
        switch (method)
        {
            case Method.JSON:
                return JsonSerialiazer<T>(data);
            case Method.PROTO:
                return ProtobuffSerialiazer<T>(data);
            case Method.XML:
                return XMLSerialiazer<T>(data);
            default:
                return null;
        }
    }


    public static List<T> Deserailizer<T>(byte[] recvData, Method method)
    {
        switch (method)
        {
            case Method.PROTO:
                return ProtobuffDeserialiazer<T>(recvData);
            

            default:
                return null;
        }
    }
    

    


    private static byte[] JsonSerialiazer<T>(T data)
    {
        throw new NotImplementedException();
    }

    private static byte[] XMLSerialiazer<T>(T obj)
    {
        throw new NotImplementedException();
    }

    private static byte[] ProtobuffSerialiazer<T>(T obj)
    {
        byte[] result = null;
        using (var stream = new MemoryStream())
        {
            Serializer.Serialize<T>(stream, obj);
            result = stream.ToArray();
        }
        return result;
    }

    private static List<T> ProtobuffDeserialiazer<T>(byte[] data)
    {
        List<T> obj = new List<T>();
        int i = 0;
        using (var temp = new MemoryStream(data))
        {
            while (i < data.Length)
            {
                var length = new byte[sizeof(int)];
                temp.Read(length, 0, sizeof(int));
                var lengthInt = BitConverter.ToInt32(Support.bytesReverse(length), 0);
                if (lengthInt == 0)
                {
                    break;
                }
                i += sizeof(int);
                var body = new byte[lengthInt];
                temp.Read(body, 0, lengthInt);
                i += lengthInt;
                var stream = new MemoryStream(body);
                obj.Add(Serializer.Deserialize<T>(stream));
            }
        }

        return obj;
    }
}

