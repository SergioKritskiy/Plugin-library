using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using Newtonsoft.Json.Linq;


namespace PluginLibrary.Extensions
{
    public static class ExtensionsMettods
    {
        #region Clone region

        public static T Clone<T>(this T objR)
        {
            var type = typeof(T);
            if (type.IsPrimitive || type.IsEnum || type == typeof(string)) //clone of Primitive...
            {
                return objR;
            }
            else if (type.IsArray) // clone of array
            {
                Type typeElement = Type.GetType(type.FullName.Replace("[]", string.Empty));
                var array = objR as Array;
                Array copiedArray = Array.CreateInstance(typeElement, array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    copiedArray.SetValue(array.GetValue(i), i);
                }
                return (T)(object)copiedArray;
            }
            else if (type.IsClass || type.IsValueType) //clone of object
            {
                object copiedObject = Activator.CreateInstance(type);
                FieldInfo[] fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object fieldValue = field.GetValue(objR);
                    if (fieldValue != null)
                    {
                        field.SetValue(copiedObject, fieldValue);
                    }
                }
                return (T)(object)copiedObject;
            }
            else {
                return objR;
            }
        }
        #endregion

        #region Random region

        public static async Task<IEnumerable<int>> GenerateRandom(int N, int max, int min, bool async=false)
        {
            // beta-key ac63b05f-484f-4208-b8ff-c133d63f6555
          var json = @"{'jsonrpc':'2.0','method':'generateIntegers','params':{'apiKey':'ac63b05f-484f-4208-b8ff-c133d63f6555','n':"+N+",'min':"+min+",'max':"+max+",'replacement':true,'base':10},'id':18057}";
          JObject jsons = JObject.Parse(json);
          byte[] bytes = Encoding.UTF8.GetBytes(jsons.ToString());
              var request = (HttpWebRequest)WebRequest.Create("https://api.random.org/json-rpc/1/invoke");

              request.Method = "POST";
              request.ContentType = "application/json";
              request.ContentLength = bytes.Length;
              request.Accept = "application/json";
              using (Stream stream = request.GetRequestStream())
              {
                  stream.Write(bytes, 0, bytes.Length);
                  stream.Close();
              }
              var response = (HttpWebResponse)request.GetResponse();
              var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
              string[] dataArray = responseString.Split('\n');
              Console.WriteLine("{0}", dataArray);
              var subtrim = responseString.Substring(responseString.IndexOf("data") + 7);
              string secondPartOfSubstring = subtrim.Substring(0, subtrim.IndexOf("completionTime")-3);
              if (secondPartOfSubstring != "")
              {
                  int[] arrayOfNumbers = Array.ConvertAll(secondPartOfSubstring.Split(','), int.Parse);
                      var query = from elem in arrayOfNumbers select elem;
                      return query;
              }else if (async==false) {
                  //async 
                  var query = await GenerateRandom(N,max,min,true);
                    return query;
              }
              else
              {
                  Random rnd = new Random();
                  int[] arrayOfNumbersOfBCLs = new int[N];
                  for (int i = 0; i < N; i++)
                  {
                      arrayOfNumbersOfBCLs[i] = rnd.Next(min, max + 1);
                  }
                  var query = from elem in arrayOfNumbersOfBCLs select elem;
                  return query;
              }
        }
        #endregion

    }
}
