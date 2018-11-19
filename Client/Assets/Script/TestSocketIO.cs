﻿#region License
/*
 * TestSocketIO.cs
 *
 * The MIT License
 *
 * Copyright (c) 2014 Fabio Panettieri
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion

using System.Collections;
using System.IO;
using UnityEngine;
using SocketIO;
using ProtoBuf;
using easy_moba;
using System.Collections.Generic;
using System;

public class TestSocketIO : MonoBehaviour
{
	private SocketIOComponent socket;

	public void Start() 
	{
        GameObject go = GameObject.Find("SocketIO");
        socket = go.GetComponent<SocketIOComponent>();
        up_msg msg = new up_msg()
        {
            data_op = new req_data_op()
            {
                op = t_op.add,
                data = 30,
                data_list = new uint[3] { 1, 2, 3 }
            }
        };
        MemoryStream ms = new MemoryStream();
        Serializer.Serialize(ms, msg);

        byte[] result = new byte[ms.Length];
        ms.Position = 0;
        ms.Read(result, 0, result.Length);
        //string byteStr = Convert.ToBase64String(result);
        //byte[] byteArray = Convert.FromBase64String(byteStr);

        //Debug.Log(byteStr);
        //MemoryStream ms2 = new MemoryStream();
        //Debug.Log(byteArray.Length);
        //ms2.Write(byteArray, 0, byteArray.Length);
        ////将流的位置归0  
        //ms2.Position = 0;
        ////使用工具反序列化对象  
        //up_msg mm = Serializer.Deserialize<up_msg>(ms2);
        //Debug.Log(mm.data_op.data);

        socket.EmitProtoMessage(result);
    }

	private IEnumerator BeepBoop()
	{
		// wait 1 seconds and continue
		yield return new WaitForSeconds(1);
		
		socket.Emit("beep");
		
		// wait 3 seconds and continue
		yield return new WaitForSeconds(3);
		
		socket.Emit("beep");
		
		// wait 2 seconds and continue
		yield return new WaitForSeconds(2);
		
		socket.Emit("beep");
		
		// wait ONE FRAME and continue
		yield return null;
		
		socket.Emit("beep");
		socket.Emit("beep");
	}

	public void TestOpen(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Open received: " + e.name + " " + e.data);
	}
	
	public void TestBoop(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Boop received: " + e.name + " " + e.data);

		if (e.data == null) { return; }

		Debug.Log(
			"#####################################################" +
			"THIS: " + e.data.GetField("this").str +
			"#####################################################"
		);
	}
	
	public void TestError(SocketIOEvent e)
	{
		Debug.Log("[SocketIO] Error received: " + e.name + " " + e.data);
	}
	
	public void TestClose(SocketIOEvent e)
	{	
		Debug.Log("[SocketIO] Close received: " + e.name + " " + e.data);
	}
}
