﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Web;

namespace JumboTCMS.OAuth.Renren.APIUtility
{
    public class SyncHttp
    {
        /// <summary>
        /// 同步方式发起http get请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="queryString">参数字符串</param>
        /// <returns>请求返回值</returns>
        public string HttpGet(string url, string queryString)
        {
            string responseData = null;

            if (!string.IsNullOrEmpty(queryString))
            {
                url += "?" + queryString;
            }

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "GET";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;

            StreamReader responseReader = null;
            Stream responseStream = null; 

            try
            {
                responseStream = webRequest.GetResponse().GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
            }
            finally
            {
                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }
                responseReader.Close();
                responseReader = null;
                webRequest = null;
            }

            return responseData;
        }
        /// <summary>
        /// 同步方式发起http get请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="paras">请求参数列表</param>
        /// <returns>请求返回值</returns>
        public string HttpGet(string url,List<APIParameter> paras)
        {
            string querystring = HttpUtil.GetQueryFromParas(paras);
            return HttpGet(url, querystring);
        }



        /// <summary>
        /// 同步方式发起http post请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="queryString">参数字符串</param>
        /// <returns>请求返回值</returns>
        public string HttpPost(string url, string queryString)
        {
            StreamWriter requestWriter = null;
            StreamReader responseReader = null;

            string responseData = null;

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.Method = "POST";
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;
            Stream responseStream = null; 

            try
            {
                //POST the data.
                requestWriter = new StreamWriter(webRequest.GetRequestStream());
                requestWriter.Write(queryString);
                requestWriter.Close();
                requestWriter = null;

                responseStream = webRequest.GetResponse().GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (requestWriter != null)
                {
                    requestWriter.Close();
                    requestWriter = null;
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }

                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader = null;
                }

                webRequest = null;
            }

            return responseData;
        }
        /// <summary>
        /// 同步方式发起http post请求
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="paras">请求参数列表</param>
        /// <returns>请求返回值</returns>
        public string HttpPost(string url, List<APIParameter> paras)
        {
            string querystring = HttpUtil.GetQueryFromParas(paras);
            return HttpPost(url, querystring);
        }


        /// <summary>
        /// 同步方式发起http post请求，可以同时上传文件
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="queryString">请求参数字符串</param>
        /// <param name="files">上传文件列表</param>
        /// <returns>请求返回值</returns>
        public string HttpPostWithFile(string url, string queryString, List<APIParameter> files)
        {
            Stream requestStream = null;
            StreamReader responseReader = null;
            string responseData = null;
            string boundary = DateTime.Now.Ticks.ToString("x");

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.Timeout = 20000;
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Credentials = CredentialCache.DefaultCredentials;

            Stream responseStream = null; 

            try
            {
                Stream memStream = new MemoryStream();

                byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
                string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";

                List<APIParameter> listParams = HttpUtil.GetQueryParameters(queryString);

                foreach (APIParameter param in listParams)
                {
                    string formitem = string.Format(formdataTemplate, param.Name, param.Value);
                    byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                    memStream.Write(formitembytes, 0, formitembytes.Length);
                }

                memStream.Write(boundarybytes, 0, boundarybytes.Length);

                string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: \"{2}\"\r\n\r\n";

                foreach (APIParameter param in files)
                {
                    string name = param.Name;
                    string filePath = param.Value;
                    string file = Path.GetFileName(filePath);
                    string contentType = HttpUtil.GetContentType(file);

                    string header = string.Format(headerTemplate, name, file, contentType);
                    byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                    memStream.Write(headerbytes, 0, headerbytes.Length);

                    FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    byte[] buffer = new byte[1024];
                    int bytesRead = 0;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }

                    memStream.Write(boundarybytes, 0, boundarybytes.Length);
                    fileStream.Close();
                }

                webRequest.ContentLength = memStream.Length;

                requestStream = webRequest.GetRequestStream();

                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
                requestStream.Close();
                requestStream = null;

                responseStream = webRequest.GetResponse().GetResponseStream();
                responseReader = new StreamReader(responseStream);
                responseData = responseReader.ReadToEnd();
            }
            catch
            {
                throw;
            }
            finally
            {
                if (requestStream != null)
                {
                    requestStream.Close();
                    requestStream = null;
                }

                if (responseStream != null)
                {
                    responseStream.Close();
                    responseStream = null;
                }

                if (responseReader != null)
                {
                    responseReader.Close();
                    responseReader = null;
                }

                webRequest = null;
            }

            return responseData;
        }
        /// <summary>
        /// 同步方式发起http post请求，可以同时上传文件
        /// </summary>
        /// <param name="url">请求URL</param>
        /// <param name="paras">请求参数列表</param>
        /// <param name="files">上传文件列表</param>
        /// <returns>请求返回值</returns>
        public string HttpPostWithFile(string url, List<APIParameter> paras, List<APIParameter> files)
        {
            string querystring = HttpUtil.GetQueryFromParas(paras);
            return HttpPostWithFile(url, querystring, files);
        }


    }
}
