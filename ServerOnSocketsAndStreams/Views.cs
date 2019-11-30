﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ServerOnSocketsAndStreams
{
    public class Views
    {
        public string Cookie;
        public string IP = "192.168.0.11";

        public byte[] StartPage()
        {
            string ResponseHtml = "<!DOCTYPE html>" +
                               "<html>" +
                                   "<head>" +
                                       "<meta charset='utf-8'></meta>" +
                                   "</head>" +
                                   "<body>" +
                                       "<div>" +
                                       "<p>ГЛАВНАЯ СТРАНИЦА!!!!!11111</p></br></br>" +
                                       "<a href=\'http://"+ IP + ":8005/Help\'>Список страниц сайта</a>" +
                                       "</dib>" +
                                   "</body>" +
                                "</html>";

            string Response = "HTTP/1.1 200 OK" +
                "\nContent-Type: text/html" +
                "\nSet-Cookie: clientCookie=" + Cookie +
                "\nContent-Length: " + ResponseHtml.Length +
                "\n\n" + ResponseHtml;
            return Encoding.UTF8.GetBytes(Response);
        }

        public byte[] Help()
        {
            string ResponseHtml = "<!DOCTYPE html>" +
                   "<html>" +
                       "<head>" +
                           "<meta charset='utf-8'></meta>" +
                       "</head>" +
                       "<body>" +
                           "<div>" +
                           "<p>Справка:</p></br>" +
                           "<a href='/Method1'> /Method1 - страница с картинкой </a></br>" +
                           "<a href='https://www.youtube.com'> ютьюбь </a></br>" +
                           "<a href='/AuthorizationPage'> /AuthorizationPage - авторизация </a>" +
                           "</dib>" +
                       "</body>" +
                    "</html>";

            string Response = "HTTP/1.1 200 OK" +
           "\nContent-Type: text/html" +
           "\nSet-Cookie: clientCookie=" + Cookie +
           "\nContent-Length: " + ResponseHtml.Length +
           "\n\n" + ResponseHtml;
            return Encoding.UTF8.GetBytes(Response);
        }

        public byte[] PageWithImage()
        {
            string ResponseHtml = "<!DOCTYPE html>" +
                   "<html>" +
                       "<head>" +
                           "<meta charset='utf-8'></meta>" +
                       "</head>" +
                       "<body>" +
                           "<div>" +
                             "<img src='images/img1.png'></img>" +
                           "</dib>" +
                       "</body>" +
                    "</html>";

            string Response = "HTTP/1.1 200 OK" +
            "\nContent-Type: text/html" +
            "\nSet-Cookie: clientCookie=" + Cookie +
            "\nContent-Length: " + ResponseHtml.Length +
            "\n\n" + ResponseHtml;
            return Encoding.UTF8.GetBytes(Response);
        }

        public byte[] PageWithImage2()
        {
            string ResponseHtml = "<!DOCTYPE html>" +
                   "<html>" +
                       "<head>" +
                           "<meta charset='utf-8'></meta>" +
                       "</head>" +
                       "<body>" +
                           "<div>" +
                             "<img src='images/img1.png'></img>" +
                           "</dib>" +
                       "</body>" +
                    "</html>";

            string Response = "HTTP/1.1 200 OK" +
            "\nContent-Type: text/html" +
            "\nSet-Cookie: clientCookie=" + Cookie +
            "\nContent-Length: " + ResponseHtml.Length +
            "\n\n" + ResponseHtml;
            return Encoding.UTF8.GetBytes(Response);
        }

        public byte[] Image(string img)
        {
            byte[] byteImage;
            //var test = Directory.GetCurrentDirectory();
            using (FileStream fs = new FileStream("..\\..\\img2.jpg", FileMode.OpenOrCreate))
            {
                byteImage = new byte[fs.Length];
                fs.Read(byteImage, 0, byteImage.Length);
            }

            string headLine = "HTTP/1.1 200 OK" +
                "\nContent-type: image/png" +
                "\nSet-Cookie: clientCookie=" + Cookie + 
                "\nContent-Length:" + byteImage.Length.ToString() + 
                "\n\n";
            byte[] byteHeadLine = Encoding.UTF8.GetBytes(headLine);

            //байт код всего вышеперечисленного в определенной последовательности(заголовки+картинка)
            byte[] byteImageResponse = new byte[byteImage.Length + byteHeadLine.Length];
            byteHeadLine.CopyTo(byteImageResponse, 0);
            byteImage.CopyTo(byteImageResponse, byteHeadLine.Length);

            return byteImageResponse;
        }

        public byte[] AuthorizationPage(string phrase)
        {
            string ResponseHtml = "<!DOCTYPE html>" +
                   "<html>" +
                       "<head>" +
                           "<meta charset='utf-8'></meta>" +
                       "</head>" +
                       "<body>" +
                           "<div>" +
                               "<p>" + phrase + "</p></br>" +
                               "<form accept-charset='utf-8' method='post'>" +
                                   "<p>" +
                                       "<input type='text' name='Name' />" +
                                   "</p>" +
                                   "<p>" +
                                       "<input type='password' name='Password' />" +
                                   "</p>" +
                                   "<p>" +
                                       "<input type='submit' value='Отправить' />" +
                                   "</p>" +
                               "</form>" +
                           "</dib>" +
                       "</body>" +
                   "</html>";

            string Response = "HTTP/1.1 200 OK" +
            "\nContent-Type: text/html" +
            "\nSet-Cookie: clientCookie=" + Cookie +
            "\nContent-Length: " + ResponseHtml.Length +
            "\n\n" + ResponseHtml;
            return Encoding.UTF8.GetBytes(Response);
        }

        public byte[] LoginVerification(string NameAndPassword)
        {
            string NameAndPasswordPattern = "(Name=)([a-z])+(&Password=)(\\s|\\S)+";
            Regex regex = new Regex(NameAndPasswordPattern);
            MatchCollection matchs = regex.Matches(NameAndPassword);
            if (matchs.Count != 0)
            {
                string ResponseHtml = "<!DOCTYPE html>" +
                   "<html>" +
                       "<head>" +
                           "<meta charset='utf-8'></meta>" +
                       "</head>" +
                       "<body>" +
                           "<div>" +
                               "<p>WELCOME EPTA</p></br>" +
                               "<a href='/'> Main_Page </a>" +
                           "</dib>" +
                       "</body>" +
                   "</html>";

                string Response = "HTTP/1.1 200 OK" +
                "\nContent-Type: text/html" +
                "\nSet-Cookie: clientCookie=" + Cookie +
                "\nContent-Length: " + ResponseHtml.Length +
                "\n\n" + ResponseHtml;
                return Encoding.UTF8.GetBytes(Response);
            }
            else
            {
                return null;//типо неверный логин и/или пароль
            }
        }

        public byte[] WrongStatus()
        {
            string ResponseHtml = "<!DOCTYPE html>" +
                   "<html>" +
                       "<head>" +
                           "<meta charset='utf-8'></meta>" +
                       "</head>" +
                       "<body>" +
                           "<div>" +
                           "<p>Visitors can't read this page</p></br>" +
                           "<a href='/AuthorizationPage'> Authorization </a>" +
                           "</dib>" +
                       "</body>" +
                    "</html>";

            string Response = "HTTP/1.1 200 OK" +
            "\nContent-Type: text/html" +
            "\nSet-Cookie: clientCookie=" + Cookie +
            "\nContent-Length: " + ResponseHtml.Length +
            "\n\n" + ResponseHtml;
            return Encoding.UTF8.GetBytes(Response);
        }
    }
}