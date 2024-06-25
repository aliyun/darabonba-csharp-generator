using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;
using System.Text;

namespace Darabonba.Test
{
    public class Client 
    {

        public static void FormTest(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string form = TeaForm.ToFormString(m);
            form = form + "&key7=23233&key8=" + TeaForm.GetBoundary();
            Stream r = TeaForm.ToFileForm(m, TeaForm.GetBoundary());
        }

        public static async Task FormTestAsync(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string form = TeaForm.ToFormString(m);
            form = form + "&key7=23233&key8=" + TeaForm.GetBoundary();
            Stream r = TeaForm.ToFileForm(m, TeaForm.GetBoundary());
        }

        public static void FileTest(List<string> args)
        {
            if (TeaFile.Exists("/tmp/test"))
            {
                TeaFile file = new TeaFile("/tmp/test");
                string path = file.Path();
                int? length = file.Length() + 10;
                TeaDate createTime = file.CreateTime();
                TeaDate modifyTime = file.ModifyTime();
                int? timeLong = modifyTime.Diff("minute", createTime);
                byte[] data = file.Read(300);
                file.Write(TeaBytes.From("test", "utf8"));
                Stream rs = TeaFile.CreateReadStream("/tmp/test");
                Stream ws = TeaFile.CreateWriteStream("/tmp/test");
            }
        }

        public static async Task FileTestAsync(List<string> args)
        {
            if (TeaFile.Exists("/tmp/test"))
            {
                TeaFile file = new TeaFile("/tmp/test");
                string path = file.Path();
                int? length = await file.LengthAsync() + 10;
                TeaDate createTime = await file.CreateTimeAsync();
                TeaDate modifyTime = await file.ModifyTimeAsync();
                int? timeLong = modifyTime.Diff("minute", createTime);
                byte[] data = await file.ReadAsync(300);
                await file.WriteAsync(TeaBytes.From("test", "utf8"));
                Stream rs = TeaFile.CreateReadStream("/tmp/test");
                Stream ws = TeaFile.CreateWriteStream("/tmp/test");
            }
        }

        public static void DateTest(List<string> args)
        {
            TeaDate date = new TeaDate("2023-09-12 17:47:31.916000 +0800 UTC");
            string dateStr = date.Format("YYYY-MM-DD HH:mm:ss");
            int? timestamp = date.Unix();
            TeaDate yesterday = date.Sub("day", 1);
            int? oneDay = date.Diff("day", yesterday);
            TeaDate tomorrow = date.Add("day", 1);
            int? twoDay = tomorrow.Diff("day", date) + oneDay;
            int? hour = date.Hour();
            int? minute = date.Minute();
            int? second = date.Second();
            int? dayOfMonth = date.DayOfMonth();
            int? dayOfWeek = date.DayOfWeek();
            int? weekOfMonth = date.WeekOfMonth();
            int? weekOfYear = date.WeekOfYear();
            int? month = date.Month();
            int? year = date.Year();
        }

        public static async Task DateTestAsync(List<string> args)
        {
            TeaDate date = new TeaDate("2023-09-12 17:47:31.916000 +0800 UTC");
            string dateStr = date.Format("YYYY-MM-DD HH:mm:ss");
            int? timestamp = date.Unix();
            TeaDate yesterday = date.Sub("day", 1);
            int? oneDay = date.Diff("day", yesterday);
            TeaDate tomorrow = date.Add("day", 1);
            int? twoDay = tomorrow.Diff("day", date) + oneDay;
            int? hour = date.Hour();
            int? minute = date.Minute();
            int? second = date.Second();
            int? dayOfMonth = date.DayOfMonth();
            int? dayOfWeek = date.DayOfWeek();
            int? weekOfMonth = date.WeekOfMonth();
            int? weekOfYear = date.WeekOfYear();
            int? month = date.Month();
            int? year = date.Year();
        }

        public static void UrlTest(List<string> args)
        {
            TeaURL url = new TeaURL(args[0]);
            string path = url.Path();
            string pathname = url.Pathname();
            string protocol = url.Protocol();
            string hostname = url.Hostname();
            string port = url.Port();
            string host = url.Host();
            string hash = url.Hash();
            string search = url.Search();
            string href = url.Href();
            string auth = url.Auth();
            TeaURL url2 = TeaURL.Parse(args[1]);
            path = url2.Path();
            string newUrl = TeaURL.UrlEncode(args[2]);
            string newSearch = TeaURL.PercentEncode(search);
            string newPath = TeaURL.PathEncode(pathname);
            string all = "test" + path + protocol + hostname + hash + search + href + auth + newUrl + newSearch + newPath;
        }

        public static async Task UrlTestAsync(List<string> args)
        {
            TeaURL url = new TeaURL(args[0]);
            string path = url.Path();
            string pathname = url.Pathname();
            string protocol = url.Protocol();
            string hostname = url.Hostname();
            string port = url.Port();
            string host = url.Host();
            string hash = url.Hash();
            string search = url.Search();
            string href = url.Href();
            string auth = url.Auth();
            TeaURL url2 = TeaURL.Parse(args[1]);
            path = url2.Path();
            string newUrl = TeaURL.UrlEncode(args[2]);
            string newSearch = TeaURL.PercentEncode(search);
            string newPath = TeaURL.PathEncode(pathname);
            string all = "test" + path + protocol + hostname + hash + search + href + auth + newUrl + newSearch + newPath;
        }

        public static void StreamTest(List<string> args)
        {
            if (TeaFile.Exists("/tmp/test"))
            {
                Stream rs = TeaFile.CreateReadStream("/tmp/test");
                Stream ws = TeaFile.CreateWriteStream("/tmp/test");
                byte[] data = TeaStream.Read(rs, 30);
                ws.Write(data, 0, data.Length);
                TeaStream.Pipe(rs, ws);
                data = TeaStream.ReadAsBytes(rs);
                object obj = TeaStream.ReadAsJSON(rs);
                string jsonStr = TeaStream.ReadAsString(rs);
            }
        }

        public static async Task StreamTestAsync(List<string> args)
        {
            if (TeaFile.Exists("/tmp/test"))
            {
                Stream rs = TeaFile.CreateReadStream("/tmp/test");
                Stream ws = TeaFile.CreateWriteStream("/tmp/test");
                byte[] data = TeaStream.Read(rs, 30);
                ws.Write(data, 0, data.Length);
                TeaStream.Pipe(rs, ws);
                data = TeaStream.ReadAsBytes(rs);
                object obj = TeaStream.ReadAsJSON(rs);
                string jsonStr = TeaStream.ReadAsString(rs);
            }
        }

        public static void XmlTest(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string xml = TeaXML.ToXML(m);
            xml = xml + "<key7>132</key7>";
            Dictionary<string, object> respMap = TeaXML.ParseXml(xml, null);
        }

        public static async Task XmlTestAsync(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            string xml = TeaXML.ToXML(m);
            xml = xml + "<key7>132</key7>";
            Dictionary<string, object> respMap = TeaXML.ParseXml(xml, null);
        }

        public static void LogerTest(List<string> args)
        {
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.Error.WriteLine("test");
        }

        public static async Task LogerTestAsync(List<string> args)
        {
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.WriteLine("test");
            Console.Error.WriteLine("test");
        }

        public static void EnvTest(List<string> args)
        {
            string es = Environment.GetEnvironmentVariable("TEST");
            Environment.SetEnvironmentVariable("TEST", es + "test");
        }

        public static async Task EnvTestAsync(List<string> args)
        {
            string es = Environment.GetEnvironmentVariable("TEST");
            Environment.SetEnvironmentVariable("TEST", es + "test");
        }

        public static void NumberTest(List<string> args)
        {
            float? num = 3.2f;
            int? inum = TeaNumber.ParseInt(num);
            long? lnum = TeaNumber.ParseLong(num);
            float? fnum = TeaNumber.ParseFloat(num);
            double? dnum = Double.Parse(num.Value.ToString());
            inum = TeaNumber.ParseInt(inum);
            lnum = TeaNumber.ParseLong(inum);
            fnum = TeaNumber.ParseFloat(inum);
            dnum = Double.Parse(inum.Value.ToString());
            inum = TeaNumber.ParseInt(lnum);
            lnum = TeaNumber.ParseLong(lnum);
            fnum = TeaNumber.ParseFloat(lnum);
            dnum = Double.Parse(lnum.Value.ToString());
            inum = TeaNumber.ParseInt(fnum);
            lnum = TeaNumber.ParseLong(fnum);
            fnum = TeaNumber.ParseFloat(fnum);
            dnum = Double.Parse(fnum.Value.ToString());
            inum = TeaNumber.ParseInt(dnum);
            lnum = TeaNumber.ParseLong(dnum);
            fnum = TeaNumber.ParseFloat(dnum);
            dnum = Double.Parse(dnum.Value.ToString());
            lnum = inum;
            inum = (int)lnum.Value;
            double randomNum = new Random().NextDouble();
            inum = (int)Math.Floor((double)inum);
            inum = (int)Math.Round((double)inum);
            double min = TeaNumber.Min(inum, fnum);
            double max = TeaNumber.Max(inum, fnum);
        }

        public static async Task NumberTestAsync(List<string> args)
        {
            float? num = 3.2f;
            int? inum = TeaNumber.ParseInt(num);
            long? lnum = TeaNumber.ParseLong(num);
            float? fnum = TeaNumber.ParseFloat(num);
            double? dnum = Double.Parse(num.Value.ToString());
            inum = TeaNumber.ParseInt(inum);
            lnum = TeaNumber.ParseLong(inum);
            fnum = TeaNumber.ParseFloat(inum);
            dnum = Double.Parse(inum.Value.ToString());
            inum = TeaNumber.ParseInt(lnum);
            lnum = TeaNumber.ParseLong(lnum);
            fnum = TeaNumber.ParseFloat(lnum);
            dnum = Double.Parse(lnum.Value.ToString());
            inum = TeaNumber.ParseInt(fnum);
            lnum = TeaNumber.ParseLong(fnum);
            fnum = TeaNumber.ParseFloat(fnum);
            dnum = Double.Parse(fnum.Value.ToString());
            inum = TeaNumber.ParseInt(dnum);
            lnum = TeaNumber.ParseLong(dnum);
            fnum = TeaNumber.ParseFloat(dnum);
            dnum = Double.Parse(dnum.Value.ToString());
            lnum = inum;
            inum = (int)lnum.Value;
            double randomNum = new Random().NextDouble();
            inum = (int)Math.Floor((double)inum);
            inum = (int)Math.Round((double)inum);
            double min = TeaNumber.Min(inum, fnum);
            double max = TeaNumber.Max(inum, fnum);
        }

        public static void StringTest(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = TeaString.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            if (fullStr.StartsWith("cn"))
            {
                string newStr2 = TeaString.Replace(fullStr, "/cn/gi", "zh");
            }
            if (fullStr.StartsWith("beijing"))
            {
                string newStr3 = TeaString.Replace(fullStr, "/beijing/", "chengdu");
            }
            int? start = fullStr.IndexOf("beijing");
            int? end = start + 7;
            string region = fullStr.Substring(start.Value, end.Value - start.Value);
            string region1 = fullStr.Substring(2, 10 - 2);
            string lowerRegion = region.ToLower();
            string upperRegion = region.ToUpper();
            if (region == "beijing")
            {
                region = region + " ";
                region = region.Trim();
            }
            byte[] tb = TeaString.ToBytes(fullStr, "utf8");
            string em = "xxx";
            if (String.IsNullOrEmpty(em))
            {
                return ;
            }
            string num = "32.01";
            int? inum = TeaString.ParseInt(num) + 3;
            long? lnum = TeaString.ParseLong(num);
            float? fnum = TeaString.ParseFloat(num) + 1f;
            double? dnum = Double.Parse(num, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo) + 1;
        }

        public static async Task StringTestAsync(List<string> args)
        {
            string fullStr = string.Join(",", args);
            args = fullStr.Split(",").ToList();
            if ((fullStr.Length > 0) && fullStr.Contains("hangzhou"))
            {
                string newStr1 = TeaString.Replace(fullStr, "/hangzhou/g", "beijing");
            }
            if (fullStr.StartsWith("cn"))
            {
                string newStr2 = TeaString.Replace(fullStr, "/cn/gi", "zh");
            }
            if (fullStr.StartsWith("beijing"))
            {
                string newStr3 = TeaString.Replace(fullStr, "/beijing/", "chengdu");
            }
            int? start = fullStr.IndexOf("beijing");
            int? end = start + 7;
            string region = fullStr.Substring(start.Value, end.Value - start.Value);
            string region1 = fullStr.Substring(2, 10 - 2);
            string lowerRegion = region.ToLower();
            string upperRegion = region.ToUpper();
            if (region == "beijing")
            {
                region = region + " ";
                region = region.Trim();
            }
            byte[] tb = TeaString.ToBytes(fullStr, "utf8");
            string em = "xxx";
            if (String.IsNullOrEmpty(em))
            {
                return ;
            }
            string num = "32.01";
            int? inum = TeaString.ParseInt(num) + 3;
            long? lnum = TeaString.ParseLong(num);
            float? fnum = TeaString.ParseFloat(num) + 1f;
            double? dnum = Double.Parse(num, NumberStyles.Float | NumberStyles.AllowThousands, NumberFormatInfo.InvariantInfo) + 1;
        }

        public static void ArrayTest(List<string> args)
        {
            if ((args.Count > 0) && args.Contains("cn-hanghzou"))
            {
                int? index = args.IndexOf("cn-hanghzou");
                string regionId = args[index.Value];
                string all = string.Join(",", args);
                string first = args[0];
                args.RemoveAt(0);
                string last = args[args.Count - 1];
                args.RemoveRange(args.Count - 1, 1);
                int? length1 = TeaArray.Unshift(args, first);
                int? length2 = TeaArray.Push(args, last);
                int? length3 = length1 + length2;
                string longStr = "long" + first + last;
                string fullStr = string.Join(",", args);
                List<string> newArr = new List<string>
                {
                    "test"
                };
                List<string> cArr = TeaArray.Concat(newArr, args);
                List<string> acsArr = TeaArray.Sort(newArr, "acs");
                List<string> descArr = TeaArray.Sort(newArr, "desc");
                List<string> llArr = TeaArray.Concat(acsArr, descArr);
                llArr.Insert(10, "test");
                llArr.RemoveAt(llArr.IndexOf("test"));
            }
        }

        public static async Task ArrayTestAsync(List<string> args)
        {
            if ((args.Count > 0) && args.Contains("cn-hanghzou"))
            {
                int? index = args.IndexOf("cn-hanghzou");
                string regionId = args[index.Value];
                string all = string.Join(",", args);
                string first = args[0];
                args.RemoveAt(0);
                string last = args[args.Count - 1];
                args.RemoveRange(args.Count - 1, 1);
                int? length1 = TeaArray.Unshift(args, first);
                int? length2 = TeaArray.Push(args, last);
                int? length3 = length1 + length2;
                string longStr = "long" + first + last;
                string fullStr = string.Join(",", args);
                List<string> newArr = new List<string>
                {
                    "test"
                };
                List<string> cArr = TeaArray.Concat(newArr, args);
                List<string> acsArr = TeaArray.Sort(newArr, "acs");
                List<string> descArr = TeaArray.Sort(newArr, "desc");
                List<string> llArr = TeaArray.Concat(acsArr, descArr);
                llArr.Insert(10, "test");
                llArr.RemoveAt(llArr.IndexOf("test"));
            }
        }

        public static void JsonTest(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            TeaCore.Sleep(10);
            string ms = TeaJSON.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            string arrStr = "[1,2,3,4]";
            object arr = JsonConvert.DeserializeObject(arrStr);
        }

        public static async Task JsonTestAsync(List<string> args)
        {
            Dictionary<string, object> m = new Dictionary<string, object>
            {
                {"key1", "test1"},
                {"key2", "test2"},
                {"key3", 3},
                {"key4", new Dictionary<string, object>
                {
                    {"key5", 123},
                    {"key6", "321"},
                }},
            };
            await TeaCore.SleepAsync(10);
            string ms = TeaJSON.SerializeObject(m);
            object ma = JsonConvert.DeserializeObject(ms);
            string arrStr = "[1,2,3,4]";
            object arr = JsonConvert.DeserializeObject(arrStr);
        }

        public static object ReturnAny()
        {
            throw new NotImplementedException();
        }

        public static void Main(string[] args)
        {
            int? a = TeaConverter.ParseInt(args[0]) + 10;
            string b = (a).ToString() + args[1] + (ReturnAny()).ToString();
            int? c = TeaConverter.ParseInt(b) + TeaConverter.ParseInt(a) + TeaConverter.ParseInt(ReturnAny());
            int? d = TeaConverter.ParseInt(b) + TeaConverter.ParseInt(a) + TeaConverter.ParseInt(ReturnAny());
            int? e = TeaConverter.ParseInt(b) + TeaConverter.ParseInt(a) + TeaConverter.ParseInt(ReturnAny());
            int? f = TeaConverter.ParseInt(b) + TeaConverter.ParseInt(a) + TeaConverter.ParseInt(ReturnAny());
            long? g = TeaConverter.ParseLong(b) + TeaConverter.ParseLong(a) + TeaConverter.ParseLong(ReturnAny());
            long? h = TeaConverter.ParseLong(b) + TeaConverter.ParseLong(a) + TeaConverter.ParseLong(ReturnAny());
            ulong? i = TeaConverter.ParseLong(b) + TeaConverter.ParseLong(a) + TeaConverter.ParseLong(ReturnAny());
            uint? j = TeaConverter.ParseInt(b) + TeaConverter.ParseInt(a) + TeaConverter.ParseInt(ReturnAny());
            uint? k = TeaConverter.ParseInt(b) + TeaConverter.ParseInt(a) + TeaConverter.ParseInt(ReturnAny());
            uint? l = TeaConverter.ParseInt(b) + TeaConverter.ParseInt(a) + TeaConverter.ParseInt(ReturnAny());
            ulong? m = TeaConverter.ParseLong(b) + TeaConverter.ParseLong(a) + TeaConverter.ParseLong(ReturnAny());
            float? n = TeaConverter.ParseFloat(b) + TeaConverter.ParseFloat(a) + TeaConverter.ParseFloat(ReturnAny());
            double? o = Double.Parse(b.ToString()) + Double.Parse(a.ToString()) + Double.Parse(ReturnAny().ToString());
            if (bool.Parse(args[2]))
            {
                // bytes 强转只允许传字符串
                byte[] data = Encoding.UTF8.GetBytes(b);
                int? length = data.Length;
                object test = data;
                Dictionary<string, string> maps = new Dictionary<string, string>
                {
                    {"key", "value"},
                };
                Dictionary<string, object> obj = maps.ToDictionary(kvp => kvp.Key, kvp => (object)kvp.Value);
                Stream ws = (Stream)obj;
                Stream rs = TeaConverter.ToStream(maps);
                data = TeaStream.Read(rs, 30);
                if (!TeaFunc.IsNull(data))
                {
                    ws.Write(data, 0, data.Length);
                }
            }
            TeaCore.Sleep(a.Value);
            string defaultVal = (!String.IsNullOrEmpty(args[0]) ? args[0] : args[1]).ToString();
            if (defaultVal == b)
            {
                return ;
            }
        }

        public static void BytesTest(List<string> args)
        {
            string fullStr = string.Join(",", args);
            byte[] data = TeaString.ToBytes(fullStr, "utf8");
            string newFullStr = Encoding.UTF8.GetString(data);
            if (fullStr != newFullStr)
            {
                return ;
            }
            string hexStr = TeaBytes.ToHex(data);
            string base64Str = Convert.ToBase64String(data);
            int? length = data.Length;
            string obj = Encoding.UTF8.GetString(data);
            byte[] data2 = TeaBytes.From(base64Str, "base64");
        }

        public static async Task BytesTestAsync(List<string> args)
        {
            string fullStr = string.Join(",", args);
            byte[] data = TeaString.ToBytes(fullStr, "utf8");
            string newFullStr = Encoding.UTF8.GetString(data);
            if (fullStr != newFullStr)
            {
                return ;
            }
            string hexStr = TeaBytes.ToHex(data);
            string base64Str = Convert.ToBase64String(data);
            int? length = data.Length;
            string obj = Encoding.UTF8.GetString(data);
            byte[] data2 = TeaBytes.From(base64Str, "base64");
        }

        public static void MapTestCase(List<string> args)
        {
            Dictionary<string, string> mapTest = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key2", "value2"},
                {"key3", "value3"},
            };
            int? length = mapTest.Count;
            int? num = length + 3;
            List<string> keys = mapTest.Keys.ToList();
            string allKey = "";

            foreach (var key in keys) {
                allKey = allKey + key;
            }
            List<KeyValuePair<string, string>> entries = mapTest.ToList();
            string newKey = "";
            string newValue = "";

            foreach (var e in entries) {
                newKey = newKey + e.Key;
                newValue = newValue + e.Value;
            }
            string json = JsonConvert.SerializeObject(mapTest);
            Dictionary<string, string> mapTest2 = new Dictionary<string, string>
            {
                {"key1", "value4"},
                {"key4", "value5"},
            };
            Dictionary<string, object> mapTest3 = TeaMap.Merge(mapTest , mapTest2);
            if (mapTest3.Get("key1") == "value4")
            {
                return ;
            }
        }

        public static async Task MapTestCaseAsync(List<string> args)
        {
            Dictionary<string, string> mapTest = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key2", "value2"},
                {"key3", "value3"},
            };
            int? length = mapTest.Count;
            int? num = length + 3;
            List<string> keys = mapTest.Keys.ToList();
            string allKey = "";

            foreach (var key in keys) {
                allKey = allKey + key;
            }
            List<KeyValuePair<string, string>> entries = mapTest.ToList();
            string newKey = "";
            string newValue = "";

            foreach (var e in entries) {
                newKey = newKey + e.Key;
                newValue = newValue + e.Value;
            }
            string json = JsonConvert.SerializeObject(mapTest);
            Dictionary<string, string> mapTest2 = new Dictionary<string, string>
            {
                {"key1", "value4"},
                {"key4", "value5"},
            };
            Dictionary<string, object> mapTest3 = TeaMap.Merge(mapTest , mapTest2);
            if (mapTest3.Get("key1") == "value4")
            {
                return ;
            }
        }

    }
}

