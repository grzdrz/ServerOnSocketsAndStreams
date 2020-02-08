﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerOnSocketsAndStreams
{
    public class Views
    {
        public ClientSession Client;
        //public string Cookie;

        public Views(ClientSession client)
        {
            Client = client;
        }

        public byte[] MainPage(string pageName, List<string> variables)
        {
            string clientLogin = "";
            if (Client.clientStatus == ClientStatus.Visitor)
                clientLogin = "visitor";
            else
                clientLogin = Client.ClientLogin;
            variables.Add(clientLogin);

            return CreateHtmlByteCode(pageName, variables);
        }

        public static byte[] Image(string img)
        {
            byte[] byteImage;
            //var test = Directory.GetCurrentDirectory();
            using (FileStream fs = new FileStream("..\\..\\img2.jpg", FileMode.Open))
            {
                byteImage = new byte[fs.Length];
                fs.Read(byteImage, 0, byteImage.Length);
            }

            string headLine = "HTTP/1.1 200 OK" +
                "\nContent-type: image/png" +
                //"\nSet-Cookie: cookie1=" + Cookie +
                "\nContent-Length:" + byteImage.Length.ToString() +
                "\n\n";
            byte[] byteHeadLine = Encoding.UTF8.GetBytes(headLine);

            //байт код всего вышеперечисленного в определенной последовательности(заголовки+картинка)
            byte[] byteImageResponse = new byte[byteImage.Length + byteHeadLine.Length];
            byteHeadLine.CopyTo(byteImageResponse, 0);
            byteImage.CopyTo(byteImageResponse, byteHeadLine.Length);

            return byteImageResponse;
        }

        public byte[] CreateHtmlByteCode(string pageName, List<string> variables, Func<string> createCookie = null)
        {
            string html = "";
            Regex regex = null;
            byte[] byteHttpLine = null;
            using (FileStream fstream = new FileStream("..\\..\\Views\\" + pageName + ".html", FileMode.Open, FileAccess.ReadWrite))
            {
                using (StreamReader streamReader = new StreamReader(fstream))
                {
                    html = streamReader.ReadToEnd();
                }

                //вставляем переменные в соответствующих местах в разметке(если они есть)
                if (!(variables is null))
                    for (int i = 0; i < variables.Count; i++)
                    {
                        regex = new Regex("/--variable" + i + "--/");
                        html = regex.Replace(html, variables[i]);
                    }

                byteHttpLine = Encoding.UTF8.GetBytes(html);
            }

            string cookieHeader = "";
            if (createCookie != null)
            {
                Client.ClientCookie = createCookie();
                cookieHeader = "\nSet-Cookie: cookie1=" + Client.ClientCookie;
            }

            string headLine = "HTTP/1.1 200 OK" +
            "\nContent-Type: text/html" +
            cookieHeader +
            "\nContent-Length: " + byteHttpLine.Length.ToString() +
            "\n\n";

            byte[] byteHeadLine = Encoding.UTF8.GetBytes(headLine);
            byte[] byteResponse = new byte[byteHttpLine.Length + byteHeadLine.Length];
            byteHeadLine.CopyTo(byteResponse, 0);
            byteHttpLine.CopyTo(byteResponse, byteHeadLine.Length);

            return byteResponse;
        }
    }
}
